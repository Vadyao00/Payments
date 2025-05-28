using Payments.Orders.Domain;
using Payments.Orders.Domain.Entities;
using Paymnets.Orders.Application.Abstractions;
using Paymnets.Orders.Application.Models.Orders;

namespace Paymnets.Orders.Application.Services;

public class OrdersService(OrdersDbContext context, ICartsService cartsService) : IOrdersService
{
    public async Task<OrderDto> Create(CreateOrderDto order)
    {
        var cart = await cartsService.Create(order.Cart);
        
        var entity = new OrderEntity
        {
            OrderNumber = order.OrderNumber,
            Name = order.Name,
            CustomerId = order.CustomerId,
            CartId = cart.Id
        };

        var orderSaveResult = await context.Orders.AddAsync(entity);
        await context.SaveChangesAsync();

        var orderEntityResult = orderSaveResult.Entity;

        return new OrderDto
        {
            Id = orderEntityResult.Id,
            CustomerId = orderEntityResult.CustomerId!.Value,
            Cart = cart,
            Name = orderEntityResult.Name,
            OrderNumber = orderEntityResult.OrderNumber
        };
    }

    public Task<OrderDto> GetById(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderDto>> GetByUser(Guid customerId)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderDto>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Reject(Guid orderId)
    {
        throw new NotImplementedException();
    }
}