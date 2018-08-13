using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using EdwardAbp.EntityFrameworkCore;
using EdwardAbp.EntityFrameworkCore.Repositories;
using EdwardAbp.Extensions;
using EdwardAbp.Orders.Dtos;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EdwardAbp.Orders
{
    public class OrderService : EdwardAbpAppServiceBase
    {
        private readonly ICustomRepository<Order, long> CustomRepository;
        private readonly IRepository<Order, long> _orderRepository;
        private readonly IRepository<Product, long> _productRepository;
        public ICustomRepository<Order, long> OrderRepository { get; set; }
        private readonly OrderManager _orderManager;
        public OrderService(IRepository<Order, long> orderRepository, 
            IRepository<Product, long> productRepository, 
            ICustomRepository<Order, long> CustomRepository,
            OrderManager orderManager)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            this.CustomRepository = CustomRepository;
            _orderManager = orderManager;
        }
        public async Task<OrderDto> GetOrderById(long id)
        {
            using (CurrentUnitOfWork.SetTenantId(1))
            {
                var result = await _orderManager.OrderRepository.GetAsync(id);
                return ObjectMapper.Map<OrderDto>(result);
            }
        }

        public async Task<List<OrderDto>> GetOrders()
        {
            var result = OrderRepository.PagedResult("exec orderpro 0");
            int orderStatus = 2;
            int r = (int)EdwardExtension.ToValue<OrderStatus>("发货1");
            string status = ((OrderStatus)orderStatus).ToDisplayName();
            var orders = await _orderRepository.GetAll().Include(o => o.OrderItems).ToListAsync();
            return ObjectMapper.Map<List<OrderDto>>(orders);
        }
        public async Task<OrderDto> CreateOrder([FromForm]OrderDto input)
        {
            using (CurrentUnitOfWork.SetTenantId(1))
            {
                var order = ObjectMapper.Map<Order>(input);
                order.Id = 10011;
                order.OrderItems = new List<OrderItem>();
                order.OrderItems.Add(new OrderItem { Id = 1006 });
                await _orderRepository.InsertOrUpdateAsync(order);
                await CurrentUnitOfWork.SaveChangesAsync();
                return ObjectMapper.Map<OrderDto>(order);
            }
        }
        public async Task BluckInsert()
        {
            var orders = new List<Order>() {
                new Order{ OrderId = 1, OrderItems = new List<OrderItem>{
                    new OrderItem{ SkuId = "S1", Status = "S1" }
                } },
                new Order{ OrderId = 2, OrderItems = new List<OrderItem>{
                    new OrderItem{ SkuId = "S2", Status = "S2" }
                }  }

            };
            await CustomRepository.BulkInsertAsync(orders);
            //for (int i = 0; i < 1000; i++)
            //{
            //    orders.Add(new Product {  Name = "P" + i.ToString() });
            //}
            //System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            //stopwatch.Start();
            ////foreach (var item in orders)
            ////{
            ////    await _productRepository.InsertAsync(item);
            ////}
            //await CurrentUnitOfWork.SaveChangesAsync();
            //stopwatch.Stop();
            //var r2 = stopwatch.ElapsedMilliseconds;
            //stopwatch.Restart();
            ////await CurrentUnitOfWork.GetDbContext<EdwardAbpDbContext>().BulkInsertAsync(orders);
            ////await CurrentUnitOfWork.SaveChangesAsync();
            ////stopwatch.Stop();
            ////var r1 = stopwatch.ElapsedMilliseconds;
            ////stopwatch.Restart();

            //await CustomRepository.BulkInsertAsync(orders);
            //await CurrentUnitOfWork.SaveChangesAsync();

            //stopwatch.Stop();
            //var r3 = stopwatch.ElapsedMilliseconds;

            //_orderRepository.InsertBluck(orders);

        }
        public async Task BluckUpdate()
        {
            var orders = new List<Order>() {
                new Order{ Number = "n1",  OrderItems = new List<OrderItem>{
                    new OrderItem{ SkuId = "S1", Status = "S3" }  
                } },
                new Order{ Number = "n1",  OrderItems = new List<OrderItem>{
                    new OrderItem{ SkuId = "S2", Status = "S4" }
                }  }

            };
            await CustomRepository.BulkUpdateAsync(orders);
            //for (int i = 0; i < 1000; i++)
            //{
            //    orders.Add(new Product {  Name = "P" + i.ToString() });
            //}
            //System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            //stopwatch.Start();
            ////foreach (var item in orders)
            ////{
            ////    await _productRepository.InsertAsync(item);
            ////}
            //await CurrentUnitOfWork.SaveChangesAsync();
            //stopwatch.Stop();
            //var r2 = stopwatch.ElapsedMilliseconds;
            //stopwatch.Restart();
            ////await CurrentUnitOfWork.GetDbContext<EdwardAbpDbContext>().BulkInsertAsync(orders);
            ////await CurrentUnitOfWork.SaveChangesAsync();
            ////stopwatch.Stop();
            ////var r1 = stopwatch.ElapsedMilliseconds;
            ////stopwatch.Restart();

            //await CustomRepository.BulkInsertAsync(orders);
            //await CurrentUnitOfWork.SaveChangesAsync();

            //stopwatch.Stop();
            //var r3 = stopwatch.ElapsedMilliseconds;

            //_orderRepository.InsertBluck(orders);

        }

        public async Task DeleteOrder(EntityDto<long> input)
        {
            await _orderRepository.DeleteAsync(o => o.Id == input.Id);
            //await _orderRepository.DeleteAsync();
        }
    }
}
