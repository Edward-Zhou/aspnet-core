using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp.Orders
{
    public class OrderManager :IDomainService
    {
        public IRepository<Order, long> OrderRepository { get; set; }

    }
}
