using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artech.VM.Models;
using Artech.VM.Repositories;
using Artech.Mvc.Extensions.IoC;
using Microsoft.Practices.Unity.Utility;

namespace Artech.VM.Services
{
public class OrderService :ServiceBase, IOrderService, IDisposable
{
    //Repository和ProductService都采用构造器注入的方式进行初始化
    public IOrderRepository OrderRepository { get; private set; }
    public IProductService ProductService { get; private set; }
    public OrderService(IOrderRepository orderRepository, IProductService productService)
    {
        this.OrderRepository= orderRepository;
        this.ProductService = productService;

        this.AddDisposableObject(orderRepository);
        this.AddDisposableObject(productService);
    }

    [TransactionCallHandler]
    public void SubmitOrder(Order order)
    {
        Guard.ArgumentNotNull(order, "order");
        CheckStock(order);            
        this.OrderRepository.AddOrder(order);
    }
    private void CheckStock(Order order)
    {
        foreach (var line in order.OrderLines)
        {
            if (this.ProductService.GetStock(line.ProductId) < line.Quantity)
            {
                throw new OutOfStockException("Out of stock...");
            }
        }
    }
}
}