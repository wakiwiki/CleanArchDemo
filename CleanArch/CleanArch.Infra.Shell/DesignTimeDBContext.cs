using CleanArch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Infra.Shell;

public class UniversityIdentityDesignTimeDBContext : IDesignTimeDbContextFactory<UniversityIdentityDBContext>
{
    public UniversityIdentityDBContext CreateDbContext(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<UniversityIdentityDBContext>(options => options.UseSqlServer(ConnectionHelper.GetDbConnection("UniversityIdentityDBConnection")));
        var serviceProvider = serviceCollection.BuildServiceProvider();
        return serviceProvider.GetService<UniversityIdentityDBContext>();
    }
}

public class UniversityDesignTimeDBContext : IDesignTimeDbContextFactory<UniversityDBContext>
{
    public UniversityDBContext CreateDbContext(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(ConnectionHelper.GetDbConnection("UniversityDBConnection")));
        var serviceProvider = serviceCollection.BuildServiceProvider();
        return serviceProvider.GetService<UniversityDBContext>();
    }
}