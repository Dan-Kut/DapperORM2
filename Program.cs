using Dapper;
using System.Data;
using Z.Dapper.Plus;

// Bulc CRUD https://dotnetfiddle.net/dbMVfr
namespace DapperORM2
{

    // У каждой компании есть связанная сущность - страна, где находится компания
    public class Country
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CountryId { get; set; }         
    }

    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CompanyId { get; set; }
    }



    internal class Program
    {
        static string? connectionString;


        static void Main(string[] args)
        {
            connectionString = "Data Source=dapper2.db";

            DapperPlusManager.Entity<Company>().Table("Companies").Identity(x => x.Id);

            //BulkInsertingCompanies();
            //BulkUpdateCompanies();
            //BulkDeleteCompanies();
            ShowAllCompanies();
            ShowAllUsersWCompanies();

        }

        static void ShowAllUsersWCompanies()
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                var users = db.Query<User>("SELECT * FROM Users");
                foreach (var user in users)
                {
                    var company = db.QueryFirstOrDefault<String>($"select Name\r\nfrom Companies\r\nwhere Id = {user.CompanyId}");
                    Console.WriteLine($"{user.Id} {user.Name} {company}");
                }
            }
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

        static void ShowAllCompanies()
        {

            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                var companies = db.Query<Company>("SELECT * FROM Companies");
                foreach (var company in companies)
                {
                    var contrey = db.QueryFirstOrDefault<String>($"select Name\r\nfrom Countries\r\nwhere Id = {company.CountryId}");
                    Console.WriteLine($"{company.Id} {company.Name} {contrey}");
                }
            }
        }

        static void BulkUpdateCompanies()
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                var companies = db.Query<Company>("select * from Companies").ToList();
                companies[2].Name = "XXerox";
                companies[3].Name = "TToyota";


                db.BulkUpdate(companies);
            }
        }
    }
}
