using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using PetApi;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using PetApi.Model;
using System.Collections.Generic;

namespace PetApiTest.ControllerTest
{
    public class PetController
    {
        [Fact]
        public async void Should_add_new_pet_to_system_successfully()
        {
            var application = new WebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            // Method: POST
            // URI: /api/addNewPet
            var pet = new Pet(name: "Kitty", type: "cat", color: "white", price: 1000);
            var serializeObject = JsonConvert.SerializeObject(pet);
            var postBody = new StringContent(serializeObject, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/addNewPet", postBody);

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var savePet = JsonConvert.DeserializeObject<Pet>(responseBody);
            Assert.Equal(pet, savePet);
        }

        [Fact]
        public async void Should_get_pet_to_system_successfully()
        {
            var application = new WebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            var pet = new Pet(name: "Kitty", type: "cat", color: "white", price: 1000);
            var serializeObject = JsonConvert.SerializeObject(pet);
            var postBody = new StringContent(serializeObject, Encoding.UTF8, "application/json");
            _ = await httpClient.PostAsync("/api/addNewPet", postBody);
            var response = await httpClient.GetAsync("/api/getAllPets");

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var allPets = JsonConvert.DeserializeObject<List<Pet>>(responseBody);
            Assert.Equal(pet, allPets[0]);
        }

        [Fact]
        public async void Should_get_pet_when_get_given_name_to_system_successfully()
        {
            var application = new WebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            var pet = new Pet(name: "Kitty", type: "cat", color: "white", price: 1000);
            var serializeObject = JsonConvert.SerializeObject(pet);
            var postBody = new StringContent(serializeObject, Encoding.UTF8, "application/json");
            _ = await httpClient.PostAsync("/api/addNewPet", postBody);
            var response = await httpClient.GetAsync("/api/getPetByName?name=Kitty");

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var petGet = JsonConvert.DeserializeObject<Pet>(responseBody);
            Assert.Equal(pet, petGet);
        }
    }
}
