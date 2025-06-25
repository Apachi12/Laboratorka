// SongCollection.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class SongCollection
{
    private List<Song> songs = new List<Song>();

    // Додавання пісні
    public void AddSong(Song song)
    {
        songs.Add(song);
    }

    // Видалення пісні за назвою
    public void RemoveSong(string title)
    {
        songs.RemoveAll(s => s.Title == title);
    }

    // Оновлення пісні
    public void UpdateSong(string title, Song updatedSong)
    {
        var index = songs.FindIndex(s => s.Title == title);
        if (index != -1)
        {
            songs[index] = updatedSong;
        }
    }

    // Пошук пісень за автором
    public List<Song> SearchByAuthor(string author)
    {
        return songs.Where(s => s.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    // Пошук пісень за виконавцем
    public List<Song> SearchByPerformer(string performer)
    {
        return songs.Where(s => s.Performers.Any(p => p.Equals(performer, StringComparison.OrdinalIgnoreCase))).ToList();
    }

    // Збереження у файл
    public void SaveToFile(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var song in songs)
            {
                writer.WriteLine($"Title: {song.Title}");
                writer.WriteLine($"Author: {song.Author}");
                writer.WriteLine($"Composer: {song.Composer}");
                writer.WriteLine($"Year: {song.Year}");
                writer.WriteLine($"Lyrics: {song.Lyrics}");
                writer.WriteLine($"Performers: {string.Join(", ", song.Performers)}");
                writer.WriteLine("-----");
            }
        }
    }

    // Завантаження з файлу
    public void LoadFromFile(string filePath)
    {
        songs.Clear();

        using (StreamReader reader = new StreamReader(filePath))
        {
            while (!reader.EndOfStream)
            {
                string title = reader.ReadLine().Split(':')[1].Trim();
                string author = reader.ReadLine().Split(':')[1].Trim();
                string composer = reader.ReadLine().Split(':')[1].Trim();
                int year = int.Parse(reader.ReadLine().Split(':')[1].Trim());
                string lyrics = reader.ReadLine().Split(':')[1].Trim();
                string performersStr = reader.ReadLine().Split(':')[1].Trim();
                List<string> performers = performersStr.Split(',').Select(p => p.Trim()).ToList();
                reader.ReadLine(); // Пропустити "-----"

                songs.Add(new Song(title, author, composer, year, lyrics, performers));
            }
        }
    }

    // Виведення всіх пісень
    public void PrintAllSongs()
    {
        foreach (var song in songs)
        {
            Console.WriteLine($"\nНазва: {song.Title}");
            Console.WriteLine($"Автор: {song.Author}");
            Console.WriteLine($"Композитор: {song.Composer}");
            Console.WriteLine($"Рік: {song.Year}");
            Console.WriteLine($"Текст: {song.Lyrics}");
            Console.WriteLine($"Виконавці: {string.Join(", ", song.Performers)}");
            Console.WriteLine("-----");
        }
    }
}