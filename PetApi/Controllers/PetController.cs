using Microsoft.AspNetCore.Mvc;
using PetApiTest.Controller;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpPut("addNewPet")]
        public Pet UpdatePetPrice(Pet pet)
        {
            pets.FirstOrDefault(_ => _.Name.Equals(pet.Name)).Price = pet.Price;
            return pets.FirstOrDefault(_ => _.Name.Equals(pet.Name));
        }

        [HttpGet("getAllPets")]
        public List<Pet> GetAllPets()
        {
            return pets;
        }

        [HttpGet("findPetsByName")]
        public List<Pet> FindPetByName([FromQuery] string name)
        {
            return pets.Where(_ => _.Name.Equals(name)).ToList();
        }

        [HttpGet("findPetsByType")]
        public List<Pet> FindPetByType([FromQuery] string type)
        {
            return pets.Where(_ => _.Type.Equals(type)).ToList();
        }

        [HttpGet("findPetsByColor")]
        public List<Pet> FindPetByColor([FromQuery] string color)
        {
            return pets.Where(_ => _.Color.Equals(color)).ToList();
        }

        [HttpGet("findPetsByPriceRange")]
        public List<Pet> FindPetByPriceRange([FromQuery] int lowerPriceRange, [FromQuery] int upperPriceRange)
        {
            return pets.Where(_ => _.Price <= upperPriceRange && _.Price >= lowerPriceRange).ToList();
        }

        [HttpDelete("deleteBoughtPet")]
        public string DeletePetByName([FromQuery] string name)
        {
            pets.Remove(pets.First(_ => _.Name.Equals(name)));
            return "delete!";
        }

        [HttpDelete]
        public void DeleteAllPets()
        {
            pets.Clear();
        }
    }
}
