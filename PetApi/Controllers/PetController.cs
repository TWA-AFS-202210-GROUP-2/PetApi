using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetApi.Model;
using System;
using System.Collections.Generic;

namespace PetApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class PetController : ControllerBase
    {
        static public List<Pet> pets = new List<Pet>();
        [HttpPost("addNewPet")]
        public Pet AddNewPet(Pet pet)
        {
            pets.Add(pet);
            Console.WriteLine(pets);
            return pet;
        }

        [HttpGet("getAllPets")]
        public List<Pet> GetAllPets()
        {
            return pets;
        }

        [HttpGet("getPetByName")]
        public Pet GetPetByName([FromQuery] string name)
        {
            return pets.FindLast(item => item.name == name);
        }

        [HttpDelete("deleteAllName/{name}")]
        public IActionResult DeletePetByName([FromRoute] string name)
        {
            pets.RemoveAll(item => item.name == name);
            return Ok();
        }
    }
}
