using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataStorage.source
{
    public class DesignDbContext:IDesignTimeDbContextFactory<RemembrallContext>
    {
        public RemembrallContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<RemembrallContext>();
            //todo: вынести  в файл настройки  строку подключения
            builder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = storageDB; Trusted_Connection = True;",
                opts => opts.CommandTimeout((int) TimeSpan.FromMinutes(10).TotalSeconds));
            return new RemembrallContext(builder.Options);
        }
    }
}
