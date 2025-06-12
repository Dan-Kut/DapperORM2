using Dapper;
using DapperORM2.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace DapperORM2.Repositories
{
    public class CompanyRepositori
    {
        private readonly string? _connectionString;
        public CompanyRepositori(string? connectionString) => _connectionString = connectionString;

        static CompanyRepositori()
        {
            DapperPlusManager.Entity<Company>().Table("Companies").Identity(x => x.Id);
        }
        public void ShowAllCompanies()
        {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var companies = db.Query<Company>("SELECT * FROM Companies");
                foreach (var company in companies)
                {
                    var contrey = db.QueryFirstOrDefault<String>($"select Name\r\nfrom Countries\r\nwhere Id = {company.CountryId}");
                    Console.WriteLine($"{company.Id} {company.Name} {contrey}");
                }
            }
        }
        // Transform method to List<int> id, List<string> name
        public void BulkUpdateCompaniNameById(List<int> id, List<string> name)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from Companies WHERE Id = @id";
                var compani = db.QueryFirstOrDefault<Company>(query, new {id});

                db.BulkUpdate(compani);
            }
        }
    }
}
