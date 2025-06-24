using System;
using System.IO;
using System.Linq;

namespace lb9 

class CensorApp
{
    static void Main()
    {
        Console.Write("Введите путь к текстовому файлу: "); // Запитуємо шлях до текстового файлу
        string textPath = Console.ReadLine();

        Console.Write("Введите путь к файлу со словами для цензуры: "); // Запитуємо шлях до файлу зі словами для цензури
        string censorPath = Console.ReadLine();

        if (!File.Exists(textPath) || !File.Exists(censorPath)) // Перевіряємо наявність обох файлів
        {
            Console.WriteLine("Один из файлов не найден."); // Якщо якогось немає — повідомляємо
            return;
        }

        string text = File.ReadAllText(textPath); // Зчитуємо текст з файлу
        string[] badWords = File.ReadAllLines(censorPath); // Зчитуємо слова для цензури по рядках

        foreach (var word in badWords) // Для кожного небажаного слова
        {
            string replacement = new string('*', word.Length); // Створюємо рядок з * такої ж довжини, як слово
            text = ReplaceWord(text, word, replacement); // Замінюємо слово у тексті на зірочки
        }

        Console.WriteLine("Отцензурированный текст:"); // Виводимо результат
        Console.WriteLine(text);
    }

    // Метод для заміни слова у тексті з урахуванням регістру та цілого слова
    static string ReplaceWord(string text, string word, string replacement)
    {
        return System.Text.RegularExpressions.Regex.Replace(text,
            $@"\b{System.Text.RegularExpressions.Regex.Escape(word)}\b", // \b — межа слова
            replacement,
            System.Text.RegularExpressions.RegexOptions.IgnoreCase); // Ігноруємо регістр
    }
}
