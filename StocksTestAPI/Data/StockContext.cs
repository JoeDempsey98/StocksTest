using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StocksTestAPI.Models;
using StocksTestLibrary.Models;

namespace StocksTestAPI.Data
{
    public class StockContext : DbContext
    {
        private string _connectionString;
        public StockContext(IOptions<ConnectionString> connectionString)
        {
            _connectionString = connectionString.Value.DefaultConnection;
        }
        public DbSet<StockModel> Stocks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}
