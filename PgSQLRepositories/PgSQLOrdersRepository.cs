using CPSDevExerciseWeb.Database;
using CPSDevExerciseWeb.Models;

namespace CPSDevExerciseWeb.PgSQLRepositories
{
    public class PgSQLOrdersRepository : IOrdersRepository
    {
        public readonly AppDatabaseContext _context;
        public PgSQLOrdersRepository()
        {
            _context = new AppDatabaseContext();
        }

        public Order CreateOrder(Order order)
        {
            var dbOrder = _context.Orders?.Add(order);
            _context.SaveChanges();

            return dbOrder?.Entity;
        }

        public Order[] GetCustomerOrders(string CustomerName)
        {
            if (string.IsNullOrWhiteSpace(CustomerName))
            {
                return new Order[] { };
            }

            var query = from o in _context.Orders
                        where o.CustomerName.ToLower() == CustomerName.ToLower()
                        orderby o.Id descending
                        select o;

            return query.ToArray();
        }
    }
}
