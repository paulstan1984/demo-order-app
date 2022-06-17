using CPSDevExerciseWeb.Models;

namespace CPSDevExerciseWeb.Services
{
    public class ImportOrderPackage
    {
        public Order[] Orders { get; set; }

        public List<string> ValidationErrors { get; set; }
    }
    public interface IOrdersReader
    {
        ImportOrderPackage ReadOrders(string inputSource);
    }
}
