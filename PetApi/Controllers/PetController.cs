using Microsoft.AspNetCore.Mvc;
using PetApi.Model;
using System.Collections.Generic;

namespace PetApi.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api")]
    public class PetController : ControllerBase
    {
        private static List<Pet> pets = new List<Pet>();
            
        [HttpPost("addNewPet")]
        public Pet AddNewPet(Pet pet)
        {
            pets.Add(pet);
            return pet;
        }

        [HttpGet("getAllPets")]
        public List<Pet> GetAllPets()
        {
            return pets;
        }

        [HttpDelete("deleteAllPets")]
        public List<Pet> DeleteAllPets(List<Pet> pets)
        {
            pets.Clear();
            return pets;
        }
    }
}
