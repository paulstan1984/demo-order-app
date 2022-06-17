using CPSDevExerciseWeb.Models;

namespace CPSDevExerciseWeb.Database
{
    public interface IOrdersRepository
    {

        Order CreateOrder(Order order);

        Order[] GetCustomerOrders(string CustomerName);
    }
}
