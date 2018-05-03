using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdwardAbp.Products
{
    public class ProductTypeAppService : EdwardAbpAppServiceBase
    {
        private readonly IRepository<PType> _pTypeRepository;
        public ProductTypeAppService(IRepository<PType> pTypeRepository)
        {
            _pTypeRepository = pTypeRepository;
        }
        public async Task<List<PType>> Get()
        {
            using (UnitOfWorkManager.Current.SetTenantId(null))
            {
                var result = _pTypeRepository.GetAll().ToList();
                return _pTypeRepository.GetAll().ToList();

            }
        }
        public async Task Create(PType pType)
        {
            await _pTypeRepository.InsertAsync(pType);
        }
        public async Task<List<NameValueDto>> GetAll()
        {
            using (UnitOfWorkManager.Current.SetTenantId(null))
            {
                return _pTypeRepository.GetAll().Select(p => new NameValueDto(p.Name, p.Id.ToString())).ToList();
            }
        }
    }
}
