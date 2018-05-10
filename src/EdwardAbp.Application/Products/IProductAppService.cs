using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp.Products
{
    public interface IProductAppService: IApplicationService
    {
        List<Product> GetAll();
    }
}
