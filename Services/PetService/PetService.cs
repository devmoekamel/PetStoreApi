using PetStore.Context;
using PetStore.DTOS;
using PetStore.Model;

namespace PetStore.Services.PetService
{
    public class PetService : IPetService
    {
        private readonly ApplicationDBContext _context;

        public PetService(ApplicationDBContext context)
        {
            this._context = context;
        }
        public PetDTO AddPet(PetDTO newPet)
        {
            var pet = new Pet
            {
                Name = newPet.PetName,
                Age = newPet.PetAge,
                Cost = newPet.PetCost,
            };
            _context.pets.Add(pet);
            _context.SaveChanges();
            
            return newPet;
         
        
        }

        public bool delete(int Petid)
        {
          var pet  = _context.pets.FirstOrDefault(p=>p.Id == Petid);
            if (pet is null) return false;
            _context.pets.Remove(pet);
            _context.SaveChanges();
            return true;
        }

        public PetDTO GetPet(int id)
        {
            var pet = _context.pets.FirstOrDefault(p => p.Id == id);

            return new PetDTO
            {
                PetId = pet.Id,
                PetName = pet.Name,
                PetAge = pet.Age,
                PetCost = pet.Cost,
                OrderdOn = DateTime.Now,
            };
        }

        public List<PetDTO> GetPets()
        {
            var pets = from pet in _context.pets
                       select new PetDTO 
                       {
                           PetId = pet.Id,
                           PetName = pet.Name,
                           PetAge = pet.Age,
                           PetCost = pet.Cost,
                           OrderdOn = DateTime.Now,
                       };

            return pets.ToList();
        }

        public bool update(int Petid, PetDTO pet)
        {
            var oldpet = _context.pets.FirstOrDefault(p => p.Id == Petid);
            if (oldpet is null) return false;
            oldpet.Name = pet.PetName;
            oldpet.Cost = pet.PetCost;
            oldpet.Age = pet.PetAge;
            _context.SaveChanges();
            return true;

        }
    }
}
