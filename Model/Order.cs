using System.ComponentModel.DataAnnotations.Schema;

namespace PetStore.Model
{
    public class Order
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime shipDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(User))]
        public String UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(Pet))]
        public int PetId { get; set; }
        public Pet Pet { get; set; }
   
    }
}



//{
//    "id": 0,
//  "petId": 0,
//  "quantity": 0,
//  "shipDate": "2024-03-14T10:29:48.205Z",
//  "status": "placed",
//  "complete": true
//}