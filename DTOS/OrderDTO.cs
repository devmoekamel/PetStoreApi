namespace PetStore.DTOS
{
    public class OrderDTO
    {
        public string UserId { get; set; }
        public  int PetId { get; set; }
        public int quantity { get; set; } = 1; 
        public DateTime DateTime { get; set; } = DateTime.Now;
   

    }
}



//{
//    "id": 0,
//  "petId": 0,
//  "quantity": 0,
//  "shipDate": "2024-03-14T18:15:37.736Z",
//  "status": "placed",
//  "complete": true
//}