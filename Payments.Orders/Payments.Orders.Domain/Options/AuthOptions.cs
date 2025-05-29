﻿namespace Payments.Orders.Domain.Options;

public class AuthOptions
{
    public required string TokenPrivateKey { get; set; }
    public long ExpireIntervalMinutes { get; set; }
}