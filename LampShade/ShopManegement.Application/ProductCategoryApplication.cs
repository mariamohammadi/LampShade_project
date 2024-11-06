using _0_Framework.Applcation;
using _0_Framework.Application;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManegement.Application.Contract.ProductCategory;
using System.Collections.Generic;

namespace ShopManegement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository; 
        }
        public OperationResult Create(CreateProductCategory command)
        {
           var operation=new OperationResult();
            if (_productCategoryRepository.Exist(x=>x.Name==command.Name))
                return operation.Faild("امکان ثبت رکورد تکراری وجود ندارد ");

            var slug=command.Slug.Slugify();
            var productCategory = new ProductCategory(command.Name, command.Picture, command.MetaDescription,
                command.Description, command.PictureTitle, command.Keywords, command.Slug, command.PictureAlt);

            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
            return operation.Succedded();

            
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory=_productCategoryRepository.Get(command.Id);
            if (productCategory == null)
                return operation.Faild("رکورد با اطلاعات خواسته شده یافت نشد");
            if (_productCategoryRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Faild("امکان ثبت رکورد تکراری وجود ندارد");

            var slug=command.Slug.Slugify();
            productCategory.Edit(command.Name, command.Description, command.Picture, command.PictureAlt,
                command.MetaDescription, command.PictureTitle, command.Keywords, slug);

            _productCategoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }
    }
}
