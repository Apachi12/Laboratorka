using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace Laboratorna10
{
    // Клас "Запис" представляє запис про особу з особистими даними
    [Serializable] // Дозволяє об'єкт серіалізувати у бінарний формат
    public class Запис
    {
        // ПІБ особи
        public string FullName { get; set; }  // Прізвище, Ім’я, По-батькові
        public string HomePhone { get; set; }  // Домашній телефон
        // Номер робочого телефону
        public string WorkPhone { get; set; }  // Робочий телефон
        // Номер мобільного телефону
        public string MobilePhone { get; set; }  // Мобільний телефон
        // Дата народження особи
        public DateTime BirthDate { get; set; }  // День народження
        // Конструктор для створення нового запису
        public Запис(string fullName, string homePhone, string workPhone,
                     string mobilePhone, DateTime birthDate)
        {
            FullName = fullName;
            HomePhone = homePhone;
            WorkPhone = workPhone;
            MobilePhone = mobilePhone;
            BirthDate = birthDate;
        }

        // Перевизначений метод ToString для зручного виведення даних
        public override string ToString()
        {
            return $"ПІБ: {FullName}, День народження: {BirthDate:dd.MM.yyyy}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створення списку записів для тестування
            List<Запис> записи = new List<Запис>
            {
                new Запис("Іванов Іван Іванович", "1234567890", "0987654321", "5555555555", new DateTime(1990, 5, 15)),
                new Запис("Петров Петро Петрович", "9876543210", "1111111111", "3333333333", new DateTime(1985, 10, 20)),
                new Запис("Сидоров Сидор Сидорович", "7777777777", "2222222222", "4444444444", new DateTime(1995, 3, 5)),
                new Запис("Ковальов Костянтин Миколайович", "8888888888", "3333333333", "6666666666", new DateTime(1980, 8, 10))
            };

            // Введення користувачем початкової та кінцевої дат діапазону
            Console.Write("Введіть початок діапазону (дд.мм.рррр): ");
            DateTime початок = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", null);

            Console.Write("Введіть кінець діапазону (дд.мм.рррр): ");
            DateTime кінець = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", null);

            // Фільтруємо записи за датою народження
            Console.WriteLine("\nЛюди, чиї дні народження потрапляють у діапазон:");
            foreach (var запис in записи)
            {
                if (запис.BirthDate >= початок && запис.BirthDate <= кінець)
                {
                    Console.WriteLine(запис);
                }
            }

            // Серіалізація та десеріалізація даних

            // 1. BinaryFormatter (працює у .NET Framework або при підключенні сумісності)
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream("записи.bin", FileMode.Create))
                {
                    formatter.Serialize(fs, записи); // Серіалізація у файл
                }

                using (FileStream fs = new FileStream("записи.bin", FileMode.Open))
                {
                    List<Запис> завантаженіЗаписи = (List<Запис>)formatter.Deserialize(fs); // Десеріалізація
                    Console.WriteLine("\nДесеріалізація із Binary успішна.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка серіалізації Binary: " + ex.Message);
            }

            // 2. JSON (використовується System.Text.Json)
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(записи, options); // Серіалізація у JSON
            File.WriteAllText("записи.json", jsonString);

            string jsonFromFile = File.ReadAllText("записи.json");
            List<Запис> завантаженіJson = JsonSerializer.Deserialize<List<Запис>>(jsonFromFile); // Десеріалізація
            Console.WriteLine("Серіалізація/десеріалізація JSON успішна.");
        }
    }
}