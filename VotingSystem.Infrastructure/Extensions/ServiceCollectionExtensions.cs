using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VotingSystem.Application.Services;
using VotingSystem.Infrastructure.Data;
using VotingSystem.Infrastructure.Services;

namespace VotingSystem.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Infrastructure層のサービス登録
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<IBlockchainService, BlockchainService>();
            services.AddScoped<IElectionService, ElectionService>();
            services.AddScoped<IVotingService, VotingService>();
            
            return services;
        }
        
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<VotingDbContext>(options =>
                options.UseSqlite(connectionString));
                
            return services;
        }
    }
}