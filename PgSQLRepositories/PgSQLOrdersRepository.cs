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
            var query = from o in _context.Orders
                        select o;

            if (!string.IsNullOrWhiteSpace(CustomerName))
            {
                query = query.Where(o => o.CustomerName.ToLower() == CustomerName.ToLower());
            }

            return query.OrderByDescending(o => o.Id).ToArray();
        }
    }
}
