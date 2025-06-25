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
        public void BulkUpdateCompaniNameById(List<int> id, List<string> name)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from Companies WHERE Id = @id";
                var companies = db.Query<Company>(query, id);

                db.BulkUpdate(companies);
            }
        }
        public void BulkInsertingCompanies(List<Company> companies)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                DapperPlusManager.Entity<Company>().Table("Companies").Identity(x => x.Id);
                db.BulkInsert(companies);
            }
        }
        public void BulkDeleteCompanyById(List<int> ids)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var companies = db.Query<Company>($"select * from Companies");
                db.BulkDelete(companies.Where(x => ids.Contains(x.Id) ));
            }
        }
    }
}