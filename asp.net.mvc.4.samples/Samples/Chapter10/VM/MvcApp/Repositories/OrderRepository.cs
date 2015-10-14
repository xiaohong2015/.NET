using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity.Utility;

namespace Artech.VM.Repositories
{
    public class OrderRepository : VmRepository<Order>, IOrderRepository
    {
        public OrderRepository(VmDbContext dbContext)
            : base(dbContext)
        { }

        public void AddOrder(Order order)
        {
            Guard.ArgumentNotNull(order, "order");
            this.Add(order);
        }
    }
}