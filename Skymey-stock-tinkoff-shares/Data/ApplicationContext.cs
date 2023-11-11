using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using Skymey_main_lib.Models.Bonds.Tinkoff;
using Skymey_main_lib.Models.Currencies.Tinkoff;
using Skymey_main_lib.Models.ETF.Tinkoff;
using Skymey_main_lib.Models.Futures.Tinkoff;
using Skymey_main_lib.Models.Tickers.Polygon;
using Skymey_main_lib.Models.Tickers.Tinkoff;
using System.Collections.Generic;
using System.Reflection.Emit;
using Tinkoff.InvestApi.V1;

namespace Skymey_stock_tinkoff_shares.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<TickerList> TickerList { get; init; }
        public DbSet<TinkoffSharesInstrument> Shares { get; init; }
        public static ApplicationContext Create(IMongoDatabase database) =>
            new(new DbContextOptionsBuilder<ApplicationContext>()
                .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
                .Options);
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TickerList>().ToCollection("stock_tickerlist");
            modelBuilder.Entity<TinkoffSharesInstrument>().ToCollection("stock_shareslist");
        }
    }
}
