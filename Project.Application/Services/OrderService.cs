using Project.Domain.Entities;

namespace Project.Application.Services;

public class OrderService
{
    public Order CreateOrder()
    {
        return new Order();
    }

    public void CancelOrder(Order order)
    {
        order.Cancel();
    }
}
