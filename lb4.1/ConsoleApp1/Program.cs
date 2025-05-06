using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string[] testEmails = {
            "user12@example.com",  // допустимый
            "user@gmail.com",      // допустимый
            "bug@@@com.ru",        // недопустимый
            "@gmail.com",          // недопустимый
            "Just Text2"           // недопустимый
        };

        string pattern = @"^[a-zA-Z][a-zA-Z0-9._%+-]*@[a-zA-Z0-9.-]+\.(com|localhost)$";

        foreach (string email in testEmails)
        {
            bool isMatch = Regex.IsMatch(email, pattern);
            Console.WriteLine($"{email} → {(isMatch ? "допустимий" : "недопустимий")}");
        }
    }
}
