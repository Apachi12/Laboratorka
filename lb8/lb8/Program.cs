// Завдання 1 - Використання делегатів Action, Predicate, Func
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // Метод відображення поточного часу
    // Виводить поточний час у форматі короткого часу (години:хвилини)
    static void ShowCurrentTime() => Console.WriteLine($"Час: {DateTime.Now.ToShortTimeString()}");

    // Метод відображення поточної дати
    static void ShowCurrentDate() => Console.WriteLine($"Дата: {DateTime.Now.ToShortDateString()}");

    // Метод відображення поточного дня тижня
    static void ShowCurrentDay() => Console.WriteLine($"День тижня: {DateTime.Now.DayOfWeek}");

    // Перевірка на просте число
    // Перевіряє, чи є число простим, використовуючи LINQ і перевірку дільників від 2 до number - 1
    static bool IsPrime(int number) => number > 1 && Enumerable.Range(2, number - 2).All(x => number % x != 0);

    // Перевірка на число Фібоначчі
    static bool IsFibonacci(int n)
    {
        bool IsPerfectSquare(int x) => Math.Sqrt(x) % 1 == 0;
        return IsPerfectSquare(5 * n * n + 4) || IsPerfectSquare(5 * n * n - 4);
    }

    // Площа трикутника
    static double TriangleArea(double b, double h) => 0.5 * b * h;

    // Площа прямокутника
    static double RectangleArea(double a, double b) => a * b;

    // Завдання 2 - Клас "Валіза"
    // Клас, що представляє об'єкт, який можна додати до валізи (має назву та обсяг)
    class Object
    {
        public string Name { get; set; }
        public double Volume { get; set; }
    }

    // Клас, що моделює валізу з властивостями (колір, виробник, вага, об'єм) і списком предметів
    class Suitcase
    {
        public string Color { get; set; }
        public string Manufacturer { get; set; }
        public double Weight { get; set; }
        public double Volume { get; set; }

        public List<Object> Items { get; set; } = new List<Object>();
        public event Action<string> OnItemAdded;

        public void AddItem(Object item)
        {
            double usedVolume = Items.Sum(i => i.Volume);
            if (usedVolume + item.Volume > Volume)
                throw new InvalidOperationException("Перевищено обсяг валізи");

            Items.Add(item);
            OnItemAdded?.Invoke($"Додано: {item.Name} ({item.Volume})");
        }
    }

    // Завдання 3 - Лямбда вирази
    // Метод демонструє використання лямбда-виразів для підрахунку та перевірок в масиві
    static void LambdaTests()
    {
        int[] array = { 7, 14, 1, 21, 5, 256 };

        // Кількість кратних 7
        var countDiv7 = array.Count(x => x % 7 == 0);
        Console.WriteLine($"Кратних 7: {countDiv7}");

        // Кількість позитивних
        var countPositive = array.Count(x => x > 0);
        Console.WriteLine($"Позитивних: {countPositive}");

        // Чи є 256 день року — днем програміста
        Func<int, bool> isProgrammersDay = day => day == 256;
        Console.WriteLine($"Чи день програміста (256): {isProgrammersDay(256)}");

        // Перевірка наявності слова у тексті
        string[] words = { "кіт", "пес", "програміст" };
        Func<string[], string, bool> containsWord = (arr, word) => arr.Contains(word);
        Console.WriteLine($"Є слово 'кіт': {containsWord(words, "кіт")}");
    }

    static void Main()
    {
        // --- Завдання 1 ---
        Action showTime = ShowCurrentTime;
        Action showDate = ShowCurrentDate;
        Action showDay = ShowCurrentDay;

        Predicate<int> isPrime = IsPrime;
        Predicate<int> isFibo = IsFibonacci;

        Func<double, double, double> triangle = TriangleArea;
        Func<double, double, double> rectangle = RectangleArea;

        showTime();
        showDate();
        showDay();

        Console.WriteLine($"7 - просте? {isPrime(7)}");
        Console.WriteLine($"8 - Фібоначчі? {isFibo(8)}");

        Console.WriteLine($"Площа трикутника: {triangle(5, 4)}");
        Console.WriteLine($"Площа прямокутника: {rectangle(5, 4)}");

        // --- Завдання 2 ---
        var suitcase = new Suitcase { Color = "Синій", Manufacturer = "Nike", Weight = 3, Volume = 30 };
        suitcase.OnItemAdded += msg => Console.WriteLine(msg);
        suitcase.AddItem(new ObjectInSuitcase { Name = "Футболка", Volume = 2 });
        suitcase.AddItem(new ObjectInSuitcase { Name = "Ноутбук", Volume = 5 });

        // --- Завдання 3 ---
        LambdaTests();
    }
}
