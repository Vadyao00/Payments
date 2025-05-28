using Paymnets.Orders.Application.Models.Carts;

namespace Paymnets.Orders.Application.Abstractions;

public interface ICartsService
{
    Task<CartDto> Create(CartDto cart);
}