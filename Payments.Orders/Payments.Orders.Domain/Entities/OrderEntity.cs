﻿namespace Payments.Orders.Domain.Entities;

public class OrderEntity : BaseEntity
{
    public string? Name { get; set; }
    public long OrderNumber { get; set; }
    
    public CustomerEntity? Customer { get; set; }
    public long? CustomerId { get; set; }
    
    public CartEntity? Cart { get; set; }
    public long? CartId { get; set; }
}