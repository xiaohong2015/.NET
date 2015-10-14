using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artech.VM.Models;

namespace Artech.VM.Services
{
public interface IOrderService
{
    void SubmitOrder(Order order);
}
}
