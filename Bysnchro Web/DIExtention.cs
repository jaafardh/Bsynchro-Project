using Bsynchro.Domain.Repositories;
using Bsynchro.InfraStructure.Data;
using Bsynchro.InfraStructure.Repositories;
using Bsynchro.Services.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Bysnchro_Web
{
    public static class DIExtention
    {
        public static IServiceCollection AddBysnchroServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BsynchroDatabaseContext>(options => options.UseSqlServer(connectionString).
            ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning)));

            services.AddSingleton<ILogger>(provider => provider.GetRequiredService<ILogger<AccountsManager>>());
            services.AddSingleton<ILogger>(provider => provider.GetRequiredService<ILogger<CustomersManager>>());
            services.AddSingleton<ILogger>(provider => provider.GetRequiredService<ILogger<TransactionsManager>>());
            services.AddSingleton<ILogger>(provider => provider.GetRequiredService<ILogger<BysnchroSeed>>());

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddScoped<AccountsManager>();
            services.AddScoped<CustomersManager>();
            services.AddScoped<TransactionsManager>();
            services.AddScoped<BysnchroSeed>();

            return services;
        }
    }
}
