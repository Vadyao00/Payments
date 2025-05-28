﻿using Microsoft.EntityFrameworkCore;
using Payments.Orders.Domain;
using Payments.Orders.Domain.Entities;
using Paymnets.Orders.Application.Abstractions;
using Paymnets.Orders.Application.Models.Carts;

namespace Paymnets.Orders.Application.Services;

public class CartsService(OrdersDbContext context) : ICartsService
{
    public async Task<CartDto> Create(CartDto cart)
    {
        var cartEntity = new CartEntity();
        var cartSaveResult = await context.Carts.AddAsync(cartEntity);
        await context.SaveChangesAsync();

        var cartItems = cart.CartItems
            .Select(item => new CartItemEntity
            {
                Name = item.Name,
                Price = item.Price,
                Quantity = item.Quantity,
                CartId = cartSaveResult.Entity.Id
            });

        await context.CartItems.AddRangeAsync(cartItems);
        await context.SaveChangesAsync();

        var result = await context.Carts
            .Include(x => x.CartItems)
            .FirstAsync(x => x.Id == cartSaveResult.Entity.Id);

        return new CartDto
        {
            Id = result.Id,
            CartItems = result.CartItems!.Select(item => new CartItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList()
        };
    }
}