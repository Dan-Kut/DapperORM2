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
    class CountryRepositori
    {
        private readonly string? _connectionString;
        public CountryRepositori(string? connectionString) => _connectionString = connectionString;
        public void ShowAllCountries()
        {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var countries = db.Query<Country>("SELECT * FROM Countries");
                foreach (var country in countries)
                {
                    Console.WriteLine($"Name: {country.Name}");
                }
            }
        }
        public void BulkInsertingCountries(List<Country> countries)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                DapperPlusManager.Entity<Country>().Table("Countries").Identity(x => x.Id);
                db.BulkInsert(countries);
            }
        }
        public void BulkDeleteCountryByName(List<int> ids)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var countries = db.Query<Company>("select * from Countries");
                db.BulkDelete(countries.Where(x => ids.Contains(x.Id) ));
            }
        }

    }
}
