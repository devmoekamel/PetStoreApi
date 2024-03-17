using System.Text.Json.Serialization;

namespace PetStore.Model
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        
        public double Cost { get; set; }
        [JsonIgnore]
        public List<Order> Orders { get; set; }
    }
}
