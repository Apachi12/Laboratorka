using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Запитуємо у користувача шлях до файлу
        Console.WriteLine("Введіть шлях до файлу:");
        string filePath = Console.ReadLine();

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл не знайдено.");
            return;
        }

        // Читаємо весь текст з файлу
        string text = File.ReadAllText(filePath);

        // Викликаємо метод для аналізу тексту
        AnalyzeText(text);
    }

    static void AnalyzeText(string text)
    {
        int sentences = CountSentences(text);
        int uppercase = CountUppercaseLetters(text);
        int lowercase = CountLowercaseLetters(text);
        int vowels = CountVowels(text);
        int consonants = CountConsonants(text);
        int digits = CountDigits(text);

        // Виводимо результати
        Console.WriteLine("\nСтатистична інформація про файл:");
        Console.WriteLine($"Кількість речень: {sentences}");
        Console.WriteLine($"Кількість великих літер: {uppercase}");
        Console.WriteLine($"Кількість малих літер: {lowercase}");
        Console.WriteLine($"Кількість голосних: {vowels}");
        Console.WriteLine($"Кількість приголосних: {consonants}");
        Console.WriteLine($"Кількість цифр: {digits}");
    }

    static int CountSentences(string text)
    {
        // Розділяємо текст за ознаками кінця речення
        return text.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    static int CountUppercaseLetters(string text)
    {
        return text.Count(char.IsUpper);
    }

    static int CountLowercaseLetters(string text)
    {
        return text.Count(char.IsLower);
    }

    static int CountVowels(string text)
    {
        string vowels = "aeiouAEIOUаеєиіїоуюяАЕЄИІЇОУЮЯ";
        return text.Count(c => vowels.Contains(c));
    }

    static int CountConsonants(string text)
    {
        string consonants = "bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZбвгджзйклмнпрстфхцчшщБВГДЖЗЙКЛМНПРСТФХЦЧШЩ";
        return text.Count(c => consonants.Contains(c));
    }

    static int CountDigits(string text)
    {
        return text.Count(char.IsDigit);
    }
}