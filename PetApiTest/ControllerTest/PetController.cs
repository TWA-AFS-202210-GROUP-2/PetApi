using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using PetApi;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using PetApi.Model;
using System.Collections.Generic;
using System.Linq;

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

        [Fact]
        public async void Should_delete_pet_when_delete_given_name_to_system_successfully()
        {
            var application = new WebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            var pet = new Pet(name: "Kitty", type: "cat", color: "white", price: 1000);
            var serializeObject = JsonConvert.SerializeObject(pet);
            var postBody = new StringContent(serializeObject, Encoding.UTF8, "application/json");
            _ = await httpClient.PostAsync("/api/addNewPet", postBody);

            var responseDelete = await httpClient.DeleteAsync("/api/deleteByName?name=Kitty");

            responseDelete.EnsureSuccessStatusCode();

            var responseGet = await httpClient.GetAsync("/api/getPetByName?name=Kitty");
            var responseGetBody = await responseGet.Content.ReadAsStringAsync();
            var petGet = JsonConvert.DeserializeObject<Pet>(responseGetBody);
            Assert.Null(petGet);
        }

        [Fact]
        public async void Should_return_updated_pet_when_put_given_new_price_to_system_successfully()
        {
            var application = new WebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            var pet = new Pet(name: "Kitty", type: "cat", color: "white", price: 1000);
            var petNew = new Pet(name: "Kitty", type: "cat", color: "white", price: 2000);
            var serializeObject = JsonConvert.SerializeObject(pet);
            var serializeObjectNew = JsonConvert.SerializeObject(petNew);
            var postBody = new StringContent(serializeObject, Encoding.UTF8, "application/json");
            var putBody = new StringContent(serializeObjectNew, Encoding.UTF8, "application/json");
            _ = await httpClient.PostAsync("/api/addNewPet", postBody);
            var responsePut = await httpClient.PutAsync("/api/updatePet", putBody);

            responsePut.EnsureSuccessStatusCode();

            var responseGet = await httpClient.GetAsync("/api/getPetByName?name=Kitty");
            var responseGetBody = await responseGet.Content.ReadAsStringAsync();
            var petGet = JsonConvert.DeserializeObject<Pet>(responseGetBody);
            Assert.Equal(pet, petGet);
        }

        [Fact]
        public async void Should_return_same_type_pets_when_get_by_type_given_type_to_system_successfully()
        {
            var application = new WebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            var pets = new List<Pet>
            {
                new Pet(name: "Kitty", type: "cat", color: "white", price: 1000),
                new Pet(name: "Kitty", type: "dog", color: "white", price: 2000),
                new Pet(name: "Aitty", type: "cat", color: "white", price: 1500),
            };
            foreach (var pet in pets)
            {
                var serializeObject = JsonConvert.SerializeObject(pet);
                var postBody = new StringContent(serializeObject, Encoding.UTF8, "application/json");
                _ = await httpClient.PostAsync("/api/addNewPet", postBody);
            }

            var responseGet = await httpClient.GetAsync("/api/getPetsByType?type=dog");
            responseGet.EnsureSuccessStatusCode();

            var responseGetBody = await responseGet.Content.ReadAsStringAsync();
            var petsGet = JsonConvert.DeserializeObject<List<Pet>>(responseGetBody);
            var petsGroupByType = pets.Where(item => item.type == "dog").ToList();

            _ = await httpClient.DeleteAsync("/api/deleteAllPets");

            var responseDelete = await httpClient.DeleteAsync("/api/deleteByName?name=Kitty");
            Assert.Equal(petsGroupByType.Union(petsGet).Count(), 2);
        }

        [Fact]
        public async void Should_return_same_color_pets_when_get_by_type_given_color_to_system_successfully()
        {
            var application = new WebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            var pets = new List<Pet>
            {
                new Pet(name: "Kitty", type: "cat", color: "white", price: 1000),
                new Pet(name: "Kitty", type: "dog", color: "red", price: 2000),
                new Pet(name: "Aitty", type: "cat", color: "red", price: 1500),
            };
            foreach (var pet in pets)
            {
                var serializeObject = JsonConvert.SerializeObject(pet);
                var postBody = new StringContent(serializeObject, Encoding.UTF8, "application/json");
                _ = await httpClient.PostAsync("/api/addNewPet", postBody);
            }

            var responseGet = await httpClient.GetAsync("/api/getPetsByRange?upperBound=1001&lowerBound=200");
            responseGet.EnsureSuccessStatusCode();

            var responseGetBody = await responseGet.Content.ReadAsStringAsync();
            var petsGet = JsonConvert.DeserializeObject<List<Pet>>(responseGetBody);
            var petsGroupByType = pets.Where(item => item.price <= 1001 && item.price >= 200).ToList();

            _ = await httpClient.DeleteAsync("/api/deleteAllPets");

            Assert.Equal(petsGroupByType, petsGet);
        }
    }
}
