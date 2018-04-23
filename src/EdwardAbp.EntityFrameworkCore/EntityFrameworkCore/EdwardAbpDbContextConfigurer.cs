using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace EdwardAbp.EntityFrameworkCore
{
    public static class EdwardAbpDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<EdwardAbpDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<EdwardAbpDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
