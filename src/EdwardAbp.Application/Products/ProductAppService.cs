using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using EdwardAbp.EntityFrameworkCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdwardAbp.Products
{
    public class ProductAppService: EdwardAbpAppServiceBase, IProductAppService
    {
        private readonly IRepository<Product, long> _productRepositry;
        public IUnitOfWorkManager _unitOfWorkManager;
        public ProductAppService(IRepository<Product, long> productRepositry)
        {
            _productRepositry = productRepositry;
        }
        public ProductManager ProductManager { get; set; }
        public List<Product> GetAll()
        {


            var r3 = _productRepositry
                        .GetAll()
                        .WhereIfIgnore(true, p => p.Name.Contains("p")).ToList();
            //.Where(p => p.Name.Contains("a"))
            //.WhereIf(true,p => p.Name.Contains("p")).ToList();
            var ouId = CustomAbpSession.OrganizationUnitId;
            using (CurrentUnitOfWork.SetTenantId(1))
            {
                var result = ProductManager.ProductRepository.GetAll().ToList();
                var ourResult1 = ProductManager.Products.ToList();

                var result1 = _productRepositry.GetAll().ToList();
                using (((CustomActiveUnitOfWork)CurrentUnitOfWork).SetOrganizationUnitId(3))
                {
                    var result11 = _productRepositry.GetAll().ToList();
                }
            }
            using (CurrentUnitOfWork.SetTenantId(2))
            {
                var result2 = _productRepositry.GetAll().ToList();
            }
            return _productRepositry.GetAll().ToList();
        }
        public ICustomRepository<Product,long> ProductRepository { get; set; }
        public Product Create(Product product)
        {
            var result = ProductRepository.PagedResult("Select Count(Id) from AbpProducts;Select * from AbpProducts");
            //ProductRepository.Connection.Query
            _productRepositry.Insert(product);
            return product;
        }
    }
}
