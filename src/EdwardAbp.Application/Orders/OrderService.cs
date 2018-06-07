using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using EdwardAbp.EntityFrameworkCore;
using EdwardAbp.Extensions;
using EdwardAbp.Orders.Dtos;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EdwardAbp.Orders
{
    public class OrderService : EdwardAbpAppServiceBase
    {
        public EdwardAbpDbContext EdwardAbpDbContext { get; set; }
        private readonly IRepository<Order, long> _orderRepository;
        public OrderService(IRepository<Order, long> orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<OrderDto>> GetOrders()
        {
            int orderStatus = 2;
            int r = (int)EdwardExtension.ToValue<OrderStatus>("发货1");
            string status = ((OrderStatus)orderStatus).ToDisplayName();
            var orders = await _orderRepository.GetAll().Include(o => o.OrderItems).ToListAsync();
            return ObjectMapper.Map<List<OrderDto>>(orders);
        }
        public async Task<OrderDto> CreateOrder(OrderDto input)
        {
            using (CurrentUnitOfWork.SetTenantId(1))
            {
                var order = ObjectMapper.Map<Order>(input);
                await _orderRepository.InsertAsync(order);
                return ObjectMapper.Map<OrderDto>(order);
            }
        }
        public async Task BluckInsert()
        {
            var orders = new List<Product>();
            for (int i = 0; i < 1000; i++)
            {
                orders.Add(new Product {  Name = "P" + i.ToString() });
            }
            using (var transaction = EdwardAbpDbContext.Database.BeginTransaction())
            {
                EdwardAbpDbContext.BulkInsert(orders);
                transaction.Commit();
            }

        }
        public async Task DeleteOrder(EntityDto<long> input)
        {
            await _orderRepository.DeleteAsync(o => o.Id == input.Id);
            //await _orderRepository.DeleteAsync();
        }
    }
}
