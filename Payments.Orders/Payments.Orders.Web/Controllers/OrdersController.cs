using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Paymnets.Orders.Application.Abstractions;
using Paymnets.Orders.Application.Models.Orders;

namespace Payments.Orders.Web.Controllers;

[Route("api/orders")]
public class OrdersController(IOrdersService orders, ILogger<OrdersController> logger) : ApiBaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto request)
    {
        logger.LogInformation($"Method api/order Create started. Request : {JsonSerializer.Serialize(request)}");

        var result = await orders.Create(request);

        logger.LogInformation($"Method api/order Create finished. Request : {JsonSerializer.Serialize(request)}" +
                              $"Response: {JsonSerializer.Serialize(result)}");

        return Ok(result);
    }
}