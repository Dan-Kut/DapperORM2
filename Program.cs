using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using Z.Dapper.Plus;

// Bulc CRUD https://dotnetfiddle.net/dbMVfr
namespace DapperORM2
{

    // У каждой компании есть связанная сущность - страна, где находится компания



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


            DapperPlusManager.Entity<Company>().Table("Companies").Identity(x => x.Id);

            //BulkInsertingCompanies();
            //BulkUpdateCompanies();
            //BulkDeleteCompanies();
            ShowAllCompanies();
            ShowAllUsersWCompanies();

        }

        static void BulkInsertingCompanies()
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                DapperPlusManager.Entity<Company>().Table("Companies").Identity(x => x.Id);

                var companies = new List<Company> { new Company { Name = "Xerox", CountryId = 1 } ,
                new Company { Name = "Toyota", CountryId = 2} };
                db.BulkInsert(companies);
            }
        }

        static void BulkDeleteCompanies()
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                var companies = db.Query<Company>("select * from Companies");
                db.BulkDelete(companies.Where(x => x.Name == "XXerox" || x.Name == "TToyota"));
            }
        }

    }
}
