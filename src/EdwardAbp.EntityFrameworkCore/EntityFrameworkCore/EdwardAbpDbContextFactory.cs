using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using EdwardAbp.Configuration;
using EdwardAbp.Web;

namespace EdwardAbp.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class EdwardAbpDbContextFactory : IDesignTimeDbContextFactory<EdwardAbpDbContext>
    {
        public EdwardAbpDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EdwardAbpDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            EdwardAbpDbContextConfigurer.Configure(builder, configuration.GetConnectionString(EdwardAbpConsts.ConnectionStringName));

            return new EdwardAbpDbContext(builder.Options);
        }
    }
}
