using _0_Framework.Infrastructore;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManegement.Application.Contract.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ShopManagementInfrastructure.EFCore.Repository
{
    public class ProductCategoryRepository : RepositoryBase<long,ProductCategory>,IProductCategoryRepository
    {
        private readonly ShopContext _Context;
        public ProductCategoryRepository(ShopContext Context):base(Context)
        {
            _Context = Context;
        }
        
        public EditProductCategory GetDetails(long id)
        {
            return _Context.ProductCategories.Select(x=>new EditProductCategory()
            {
                Id =x.Id,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
            }).FirstOrDefault(x=>x.Id==id);
        }

       
        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var query = _Context.ProductCategories.Select(x => new ProductCategoryViewModel()
            {
                Id = x.Id,
                CreationDate = x.CreationDate.ToString(),
                Name = x.Name,
                Picture = x.Picture,

            });
            if(!string.IsNullOrEmpty(searchModel.Name))
            query=query.Where(x=>x.Name.Contains(searchModel.Name));
            return query.OrderByDescending(x=>x.Id).ToList();
        }
    }
}
