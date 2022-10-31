using Microsoft.AspNetCore.Mvc;
using PetApi.Model;
using System.Collections.Generic;
using System.Linq;

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

        [HttpGet("findPetsByName")]
        public List<Pet> FindPetsByName([FromQuery] string name)
        { 
        return pets.Where(x => x.Name.Equals(name)).ToList();
        }

        [HttpGet("findPetsByType")]
        public List<Pet> FindPetsByType([FromQuery] string type)
        {
            return pets.Where(x => x.Type.Equals(type)).ToList();
        }

        [HttpGet("findPetsByColor")]
        public List<Pet> FindPetsByColor([FromQuery] string color)
        {
            return pets.Where(x => x.Color.Equals(color)).ToList();
        }

        [HttpDelete("deleteByName")]
        public Pet DeleteByName([FromQuery] string name)
        {
            Pet pet = new Pet();
            pet = pets.Find(item => item.Name == name);
            pets.Remove(pet);
            return pet;
        }

        [HttpPut("modifyByName")]
        public Pet ModifyByName(Pet pet)
        {
            pets.Find(item => item.Equals(pet)).Price = pet.Price;
            return pets.Find(item => item.Name == pet.Name);
        }

        [HttpGet("getPetByRange")]
        public List<Pet> GetByType([FromQuery] int upper, [FromQuery] int lower)
        {
            return pets.FindAll(item => item.Price > lower && item.Price < upper);
        }
    }
}
