using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace PetApi.Controllers
{
    [ApiController]
    [Route("api")]
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

        [HttpGet("findPetByName")]
        public Pet FindPetByName([FromQuery] string name)
        {
            return pets.First(pet => pet.Name.Equals(name));
        }

        [HttpDelete("deletePetByName")]
        public List<Pet> DeletePetByName([FromQuery] string name)
        {
            Pet pet = pets.First(pet => pet.Name.Equals(name));
            pets.Remove(pet);
            return pets;
        }

        [HttpPut("modifyPrice")]
        public Pet ModifyPriceByName(Pet pet)
        {
            Pet modifyPet = pets.First(oldPet => oldPet.Name.Equals(pet.Name));
            modifyPet.Price = pet.Price;
            return modifyPet;
        }

        [HttpGet("findPetsByType")]
        public List<Pet> FindPetsByType([FromQuery] string type)
        {
            return pets.Where(pet => pet.Type.Equals(type)).ToList();
        }

        [HttpGet("findPetsByColor")]
        public List<Pet> FindPetsByColor([FromQuery] string color)
        {
            return pets.Where(pet => pet.Color.Equals(color)).ToList();
        }

        [HttpGet("findPetsByPriceRange")]
        public List<Pet> FindPetsByPriceRange([FromQuery] int startPrice, int endPrice)
        {
            return pets.Where(pet => pet.Price >= startPrice && pet.Price <= endPrice).ToList();
        }

        [HttpDelete("deleteAllPets")]
        public void DeleteAllPets()
        {
            pets.Clear();
        }
    }
}
