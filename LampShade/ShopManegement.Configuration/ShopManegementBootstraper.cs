using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagementInfrastructure.EFCore;
using ShopManagementInfrastructure.EFCore.Repository;
using ShopManegement.Application;
using ShopManegement.Application.Contract.ProductCategory;
using System;

namespace ShopManegement.Configuration
{
    public class ShopManegementBootstraper
    {
        public static void  Configure(IServiceCollection services,string connectionString)
        {
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
