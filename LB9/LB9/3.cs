using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lb9

// Клас, що описує пісню з усіма необхідними полями
class Song
{
    public string Title { get; set; } // Назва пісні
    public string Author { get; set; } // Автор пісні
    public string Composer { get; set; } // Композитор
    public int Year { get; set; } // Рік написання
    public string Lyrics { get; set; } // Текст пісні
    public List<string> Performers { get; set; } = new List<string>(); // Масив виконавців

    // Переоприділяємо ToString для зручного виводу інформації про пісню
    public override string ToString()
    {
        return $"Название: {Title}\nАвтор: {Author}\nКомпозитор: {Composer}\nГод: {Year}\nИсполнители: {string.Join(", ", Performers)}\nТекст:\n{Lyrics}\n";
    }
}

// Клас для роботи з колекцією пісень
class SongCollection
{
    private List<Song> songs = new List<Song>(); // Внутрішній список пісень

    public void AddSong(Song song) => songs.Add(song); // Додаємо пісню

    public void RemoveSong(string title) => songs.RemoveAll(s => s.Title.Equals(title, StringComparison.OrdinalIgnoreCase)); // Видаляємо пісню за назвою

    public Song FindSongByTitle(string title) => songs.FirstOrDefault(s => s.Title.Equals(title, StringComparison.OrdinalIgnoreCase)); // Знаходимо пісню за назвою

    // Знаходимо всі пісні, де є вказаний виконавець
    public List<Song> FindSongsByPerformer(string performer) =>
        songs.Where(s => s.Performers.Any(p => p.Equals(performer, StringComparison.OrdinalIgnoreCase))).ToList();

    // Зберігаємо колекцію у файл
    public void SaveToFile(string path)
    {
        using (StreamWriter sw = new StreamWriter(path))
        {
            foreach (var song in songs)
            {
                sw.WriteLine(song.Title);
                sw.WriteLine(song.Author);
                sw.WriteLine(song.Composer);
                sw.WriteLine(song.Year);
                sw.WriteLine(string.Join(";", song.Performers)); // Виконавці через крапку з комою
                sw.WriteLine(song.Lyrics.Replace("\n", "\\n")); // Заміна переносів рядків для збереження у файлі
                sw.WriteLine("---"); // Роздільник між піснями
            }
        }
    }

    // Завантажуємо колекцію з файлу
    public void LoadFromFile(string path)
    {
        songs.Clear(); // Очищаємо поточну колекцію
        if (!File.Exists(path)) return; // Якщо файл не існує — виходимо

        var lines = File.ReadAllLines(path);
        for (int i = 0; i < lines.Length; i++)
        {
            if (i + 5 >= lines.Length) break; // Перевірка, щоб не вийти за межі масиву

            var song = new Song
            {
                Title = lines[i++], // Назва
                Author = lines[i++], // Автор
                Composer = lines[i++], // Композитор
                Year = int.Parse(lines[i++]), // Рік (перетворюємо з рядка в число)
                Performers = lines[i++].Split(';').ToList(), // Масив виконавців
                Lyrics = lines[i++].Replace("\\n", "\n") // Відновлюємо перенос рядків
            };
            songs.Add(song);
            i++; // Пропускаємо рядок з роздільником ---
        }
    }

    // Виводимо всі пісні, які виконує вказаний виконавець
    public void PrintSongsByPerformer(string performer)
    {
        var found = FindSongsByPerformer(performer);
        if (found.Count == 0)
        {
            Console.WriteLine("Песни с таким исполнителем не найдены.");
            return;
        }
        foreach (var song in found)
        {
            Console.WriteLine(song);
        }
    }
}

class Program
{
    static void Main()
    {
        var collection = new SongCollection();

        // Приклад додавання пісні у колекцію
        collection.AddSong(new Song
        {
            Title = "Песня 1",
            Author = "Автор 1",
            Composer = "Композитор 1",
            Year = 2020,
            Lyrics = "Текст песни 1",
            Performers = new List<string> { "Исполнитель 1", "Исполнитель 2" }
        });

        // Збереження колекції у файл
        string filePath = "songs.txt";
        collection.SaveToFile(filePath);

        // Завантаження колекції з файлу
        collection.LoadFromFile(filePath);

        // Запитуємо виконавця і виводимо пісні, які він виконує
        Console.Write("Введите имя исполнителя: ");
        string performer = Console.ReadLine();
        collection.PrintSongsByPerformer(performer);
    }
}
