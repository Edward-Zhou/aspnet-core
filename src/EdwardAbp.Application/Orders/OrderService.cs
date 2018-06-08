using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using EdwardAbp.EntityFrameworkCore;
using EdwardAbp.EntityFrameworkCore.Repositories;
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
        private readonly ICustomRepository<Product, long> CustomRepository;
        private readonly IRepository<Order, long> _orderRepository;
        private readonly IRepository<Product, long> _productRepository;
        public OrderService(IRepository<Order, long> orderRepository, IRepository<Product, long> productRepository, ICustomRepository<Product, long> CustomRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            this.CustomRepository = CustomRepository;
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
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            foreach (var item in orders)
            {
                await _productRepository.InsertAsync(item);
            }
            await CurrentUnitOfWork.SaveChangesAsync();
            stopwatch.Stop();
            var r2 = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();
            //await CurrentUnitOfWork.GetDbContext<EdwardAbpDbContext>().BulkInsertAsync(orders);
            //await CurrentUnitOfWork.SaveChangesAsync();
            //stopwatch.Stop();
            //var r1 = stopwatch.ElapsedMilliseconds;
            //stopwatch.Restart();

            await CustomRepository.BulkInsertAsync(orders);
            await CurrentUnitOfWork.SaveChangesAsync();

            stopwatch.Stop();
            var r3 = stopwatch.ElapsedMilliseconds;

            //_orderRepository.InsertBluck(orders);

        }
        public async Task DeleteOrder(EntityDto<long> input)
        {
            await _orderRepository.DeleteAsync(o => o.Id == input.Id);
            //await _orderRepository.DeleteAsync();
        }
    }
}
