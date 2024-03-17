using PetStore.DTOS;

namespace PetStore.Services.StoreService
{
    public interface IStoreService
    {
        List<PetDTO> GetAllOrders(string UserId);
        PetDTO GetOrder(int OrderId);

        string AddOrder(OrderDTO Order);
       string DeleteOrder(int orderid);

    }
}
