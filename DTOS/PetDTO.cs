using System.Text.Json.Serialization;

namespace PetStore.DTOS
{
    public class PetDTO
    {
        public int PetId { get; set; }  
        public string PetName { get; set; }
        public int PetAge { get; set; }
        public double PetCost { get; set;}
        [JsonIgnore]
        public DateTime OrderdOn { get; set; }  = DateTime.Now;
    }
}
