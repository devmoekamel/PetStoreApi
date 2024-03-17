using PetStore.DTOS;

namespace PetStore.Services.PetService
{
    public interface IPetService
    {
        List<PetDTO> GetPets(); // done
        PetDTO GetPet(int id); // done 
        bool update(int Petid, PetDTO pet); //working on 
        bool delete(int Petid);  //done
        PetDTO AddPet(PetDTO newPet); //done


    }
}
