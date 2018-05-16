using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdwardAbp.Products
{
    public class ProductManager: DomainService
    {

        public IRepository<Product, long> ProductRepository { get; set; }
        public IQueryable<Product> Products
        {
            get
            {
                using (CurrentUnitOfWork.DisableFilter("MayHaveOrganizationUnit"))
                {
                    return ProductRepository.GetAll();
                }
            }
        }
    }
}
