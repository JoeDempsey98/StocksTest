using Microsoft.EntityFrameworkCore;
using StocksTestLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksTestAPI.Data
{
    public class StockContext : DbContext
    {
        public StockContext()
        {

        }

        public DbSet<StockModel> Stocks { get; set; }
    }
}
