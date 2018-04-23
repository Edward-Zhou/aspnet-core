using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdwardAbp.Products
{
    public class ProductAppService: EdwardAbpAppServiceBase
    {
        private readonly IRepository<Product, long> _productRepositry;
        public IUnitOfWorkManager _unitOfWorkManager;
        public ProductAppService(IRepository<Product, long> productRepositry)
        {
            _productRepositry = productRepositry;
        }
        public List<Product> GetAll()
        {
            using (CurrentUnitOfWork.SetTenantId(1))
            {
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
    }
}
