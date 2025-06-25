using Dapper;
using DapperORM2.Models;
using DapperORM2.Repositories;
using Microsoft.Extensions.Configuration;
using System.Data;
using Z.Dapper.Plus;

// Bulc CRUD https://dotnetfiddle.net/dbMVfr
namespace DapperORM2
{



    internal class Program
    {
        static string? connectionString;


        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder(); // Створення конфігуратора.
            string path = Directory.GetCurrentDirectory(); // Отримує поточну директорію програми.
            builder.SetBasePath(path); // Встановлює базовий шлях для конфігураційних файлів.
            builder.AddJsonFile("appsettings.json"); // Додає файл конфігурації.
            var config = builder.Build(); // Завантажує конфігурацію.
            if (config == null)
            {
                throw new Exception("Configuration not found"); // Викидає помилку, якщо конфігурація не знайдена.
            }
            string connectionString = config.GetConnectionString("DefaultConnection"); // Отримує рядок підключення.



        }

    }
}
