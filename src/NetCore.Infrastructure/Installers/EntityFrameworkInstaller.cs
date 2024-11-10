// using NetCore.Infrastructure.Persistance.MsSql;
using NetCore.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.Infrastructure.Persistance.PgSql;

namespace NetCore.Infrastructure.Installers
{
    public static class EntityFrameworkInstaller
    {
        public static void InstallEntityFramework(this WebApplicationBuilder builder)
        {
            var appSettings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            if (appSettings != null)
            {
                // var msSqlSettings = appSettings.MsSql;
                // builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(msSqlSettings.ConnectionString));

                var pgSqlSettings = appSettings.PgSql;
                builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(pgSqlSettings.ConnectionString));
                builder.Services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());
            }
        }

        public static void SeedDatabase(AppDbContext appDbContext)
        {
            appDbContext.Database.Migrate();
        }
    }
}
