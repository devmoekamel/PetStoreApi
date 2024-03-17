using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetStore.DTOS;
using PetStore.Services.PetService;

namespace PetStore.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "User,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            this._petService = petService;
        }
        [HttpGet("Pets")]
        public IActionResult getPets()
        {
            var pets = _petService.GetPets();
            return Ok(pets);

        }
        [HttpGet("{id:int}")]
        public IActionResult GetPet(int id)
        {
            var pet =   _petService.GetPet(id);
            return Ok(pet); 
        }
        [HttpPost]
        public IActionResult  AddPet(PetDTO pet)
        {
            var newpet  = _petService.AddPet(pet);

            return Ok(newpet);
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeletePet(int id)
        {
            var result = _petService.delete(id);
            if(!result) return BadRequest("something went wrong");
            return Ok(new {status="Deleted ok"});
        }
        [HttpPut("{id:int}")]
        public IActionResult Updatepet(int id, PetDTO pet)
        {
            var result  = _petService.update(id, pet);
            if (!result) return BadRequest();
            return Ok(new { status = "Updated ok" });
        }
    }
}
