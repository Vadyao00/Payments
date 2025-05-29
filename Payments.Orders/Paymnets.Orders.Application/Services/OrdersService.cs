﻿using Microsoft.EntityFrameworkCore;
using Payments.Orders.Domain;
using Payments.Orders.Domain.Entities;
using Payments.Orders.Domain.Exceptions;
using Paymnets.Orders.Application.Abstractions;
using Paymnets.Orders.Application.Mappers;
using Paymnets.Orders.Application.Models.Orders;

namespace Paymnets.Orders.Application.Services;

public class OrdersService(OrdersDbContext context, ICartsService cartsService) : IOrdersService
{
    public async Task<OrderDto> Create(CreateOrderDto order)
    {
        if (order.Cart == null)
        {
            throw new ArgumentNullException();
        }
        
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

        return orderEntityResult.ToDto();
    }

    public async Task<OrderDto> GetById(long orderId)
    {
        var entity = await context.Orders
            .Include(o => o.Cart)
            .ThenInclude(c => c.CartItems)
            .FirstOrDefaultAsync(x => x.Id == orderId);

        if (entity == null)
        {
            throw new EntityNotFoundException($"Order entity with id {orderId} not found");
        }

        return entity.ToDto();
    }

    public async Task<List<OrderDto>> GetByUser(long customerId)
    {
        var entity = await context.Orders
            .Include(o => o.Cart)
            .ThenInclude(c => c.CartItems)
            .Where(x => x.CustomerId == customerId)
            .ToListAsync();

        return entity.Select(x => x.ToDto()).ToList();
    }

    public async Task<List<OrderDto>> GetAll()
    {
        var entity = await context.Orders
            .Include(o => o.Cart)
            .ThenInclude(c => c.CartItems)
            .ToListAsync();

        return entity.Select(x => x.ToDto()).ToList();
    }

    public Task Reject(long orderId)
    {
        throw new NotImplementedException();
    }
}