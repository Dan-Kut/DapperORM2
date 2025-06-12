using DapperORM2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace DapperORM2.Repositories
{
    public class UserRepositori
    {
        private readonly string? _connectionString;
        public UserRepositori(string? connectionString) => _connectionString = connectionString;

        static UserRepositori()
        {
            DapperPlusManager.Entity<Company>().Table("Companies").Identity(x => x.Id);
        }
        public void ShowAllUsersWCompanies()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var users = db.Query<User>("SELECT * FROM Users");
                foreach (var user in users)
                {
                    var company = db.QueryFirstOrDefault<String>($"select Name\r\nfrom Companies\r\nwhere Id = {user.CompanyId}");
                    Console.WriteLine($"{user.Id} {user.Name} {company}");
                }
            }
        }
    }
}
