using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artech.VM.Repositories
{
public interface IOrderRepository
{
    void AddOrder(Order order);
}
}