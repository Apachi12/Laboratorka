using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // Запитуємо шляхи до файлів
        Console.WriteLine("Введіть шлях до текстового файлу:");
        string textPath = Console.ReadLine();

        Console.WriteLine("Введіть шлях до файлу зі словами для цензури:");
        string censorPath = Console.ReadLine();

        if (!File.Exists(textPath) || !File.Exists(censorPath))
        {
            Console.WriteLine("Один із файлів не знайдено.");
            return;
        }

        // Читаємо текст і слова для цензури
        string text = File.ReadAllText(textPath);
        string[] wordsToCensor = File.ReadAllLines(censorPath);

        // Оброблюємо текст
        string censoredText = ApplyCensorship(text, wordsToCensor);

        // Виводимо результат
        Console.WriteLine("\nВідцензурений текст:");
        Console.WriteLine(censoredText);
    }

    static string ApplyCensorship(string text, string[] words)
    {
        foreach (string word in words)
        {
            // Формуємо патерн для регулярного виразу
            string pattern = $@"\b{Regex.Escape(word)}\b";
            string replacement = new string('*', word.Length);

            // Замінюємо слова без урахування регістру
            text = Regex.Replace(text, pattern, replacement, RegexOptions.IgnoreCase);
        }

        return text;
    }
}