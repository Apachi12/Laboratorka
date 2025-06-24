using System;
using System.IO;
using System.Linq;

namespace lb9
class Program

{
    static void Main()
    {
        Console.Write("Введите путь к файлу: "); // Просимо користувача ввести шлях до файлу
        string path = Console.ReadLine();

        if (!File.Exists(path)) // Перевіряємо, чи існує файл за вказаним шляхом
        {
            Console.WriteLine("Файл не найден."); // Якщо ні — виводимо повідомлення і завершуємо програму
            return;
        }

        string text = File.ReadAllText(path); // Зчитуємо весь текст з файлу у рядок

        // Рахуємо кількість речень, розбиваючи текст за знаками кінця речення
        int sentenceCount = text.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;

        int upperCount = text.Count(char.IsUpper); // Рахуємо кількість великих літер
        int lowerCount = text.Count(char.IsLower); // Рахуємо кількість малих літер
        int digitCount = text.Count(char.IsDigit); // Рахуємо кількість цифр

        // Визначаємо голосні літери (українські та англійські)
        string vowels = "aeiouAEIOUаеєиіїоуюяАЕЄИІЇОУЮЯ";
        int vowelCount = text.Count(c => vowels.Contains(c)); // Рахуємо голосні
        int consonantCount = text.Count(c => char.IsLetter(c) && !vowels.Contains(c)); // Рахуємо приголосні

        // Виводимо результати
        Console.WriteLine($"Количество предложений: {sentenceCount}");
        Console.WriteLine($"Количество заглавных букв: {upperCount}");
        Console.WriteLine($"Количество строчных букв: {lowerCount}");
        Console.WriteLine($"Количество гласных букв: {vowelCount}");
        Console.WriteLine($"Количество согласных букв: {consonantCount}");
        Console.WriteLine($"Количество цифр: {digitCount}");
    }
}
