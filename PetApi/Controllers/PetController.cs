using Microsoft.AspNetCore.Mvc;
using PetApi.Models;
using System.Collections.Generic;

namespace PetApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class PetController : Controller
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
        public void DeleteAllPets()
        {
            pets.Clear();
        }

        [HttpDelete("deleteByName")]
        public List<Pet> DeleteByName([FromQuery] string name)
        {
            pets.RemoveAll(x => x.Name.Equals(name));
            return pets;
        }

        [HttpGet("getByType")]
        public List<Pet> GetByType([FromQuery] string type)
        {
            return pets.FindAll(x => x.Type.Equals(type));
        }

        [HttpGet("getByPrice")]
        public List<Pet> GetByPrice([FromQuery] int upper, [FromQuery] int lower)
        {
            return pets.FindAll(x => (x.Price < upper && x.Price > lower)); 
        }

        [HttpPut("changePrice")]
        public Pet ChangePrice([FromQuery] string name, [FromQuery] int price)
        {
            Pet pet = pets.Find(x => x.Name.Equals(name));
            pet.Price = price;
            return pet;
        }

        [HttpGet("getByColor")]
        public List<Pet> GetByColor([FromQuery] string color)
        {
            return pets.FindAll(x => x.Color.Equals(color)); 
        }
    }
}