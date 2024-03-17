using PetStore.Context;
using PetStore.DTOS;
using PetStore.Model;

namespace PetStore.Services.StoreService
{
    public class StoreService : IStoreService
    {
        private readonly ApplicationDBContext _context;

        public StoreService(ApplicationDBContext context)
        {
            this._context = context;
        }

        public string AddOrder(OrderDTO Order)
        {
            var newOrder = new Order
            {
                PetId = Order.PetId,
                UserId = Order.UserId,
                Quantity = Order.quantity,
                shipDate = Order.DateTime
            };
            _context.orders.Add(newOrder);
            if (_context.SaveChanges() > 1)
                return string.Empty;
            else return "Something Went Wrong \n Try Agian Later";
        }

        public string DeleteOrder(int orderid)
        {
           var order =  _context.orders.FirstOrDefault(o=>o.Id ==orderid);

            _context.orders.Remove(order);
            if (_context.SaveChanges() > 1) return "Order is deleted ";
            else return "Something Went Wrong \n Try Agian Later";
        }

        public List<PetDTO> GetAllOrders(string UserId)
        {
            var Orders = from order in _context.orders
                         join pet in _context.pets
                         on order.PetId equals pet.Id
                         where order.UserId == UserId
                         select new PetDTO
                         {
                             PetId = pet.Id,
                             PetCost = pet.Cost,
                             PetAge = pet.Age,
                             PetName = pet.Name,
                         };
            return Orders.ToList();
           
        }

        public PetDTO GetOrder(int OrderId)
        {
            var Order = (from order in _context.orders
                         join pet in _context.pets
                         on order.PetId equals pet.Id
                         where order.Id == OrderId
                         select new PetDTO
                         {
                             PetId = pet.Id,
                             PetCost = pet.Cost,
                             PetAge = pet.Age,
                             PetName = pet.Name,
                         }).FirstOrDefault();
            return Order;
        }

     
    }
}
