using System;

class Program
{
    static void Main()
    {
        SongCollection collection = new SongCollection();

        // Приклад додавання пісні
        collection.AddSong(new Song(
            "Ой там на горі",
            "Народна творчість",
            "Невідомий",
            1800,
            "Ой там на горі вітер грає...",
            new List<string> { "Кайрат Нуртас", "Максим" }
        ));

        // Зберігаємо у файл
        collection.SaveToFile("songs.txt");

        // Очищуємо колекцію і завантажуємо з файлу
        collection.LoadFromFile("songs.txt");

        // Виводимо всі пісні
        collection.PrintAllSongs();

        // Шукаємо пісні за виконавцем
        var result = collection.SearchByPerformer("Кайрат Нуртас");
        Console.WriteLine("\nПісні, виконані 'Кайрат Нуртас':");
        foreach (var song in result)
        {
            Console.WriteLine(song.Title);
        }
    }
}