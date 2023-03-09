namespace Infrastructure.Models;

public enum PaymentStatus : uint
{
    Unpayed = 0,
    Payed = 1,
    Cancelled = 2
}