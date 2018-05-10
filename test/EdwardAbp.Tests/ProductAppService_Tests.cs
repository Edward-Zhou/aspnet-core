using EdwardAbp.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EdwardAbp.Tests
{
    public class ProductAppService_Tests:EdwardAbpTestBase
    {
        private readonly IProductAppService _productAppService;
        public ProductAppService_Tests()
        {
            
            _productAppService = LocalIocManager.Resolve<IProductAppService>();
        }
        [MultiTenantFact]
        public async Task Test()
        {
            LoginAsDefaultTenantAdmin();
            var result = _productAppService.GetAll();
        }
    }
}
