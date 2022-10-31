using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PetApiTest.Controller;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace PetApiTest.ControllerTest;

public class HelloControllerTest
{
    [Fact]
    public async void Should_return_hello_world()
    {
    }

    [Fact]
    public async void Should_add_new_pet_to_system_successfully()
    {
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();

        var pet = new Pet(name: "kitty", type: "cat", color: "black", price: 1000);
        var serializeObject = JsonConvert.SerializeObject(pet);
        var postBody = new StringContent(serializeObject, Encoding.UTF8, mediaType: "application/json");
        var response = await httpClient.PostAsync("/api/addNewPet", postBody);

        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var savedPet = JsonConvert.DeserializeObject<Pet>(responseBody);
        Assert.Equal(pet, savedPet);
    }

    [Fact]
    public async void Should_return_pets_to_client_successfully()
    {
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();

        var pet = new Pet(name: "kitty", type: "cat", color: "black", price: 1000);
        var serializeObject = JsonConvert.SerializeObject(pet);
        var postBody = new StringContent(serializeObject, Encoding.UTF8, mediaType: "application/json");
        await httpClient.PostAsync("/api/addNewPet", postBody);

        var response = await httpClient.GetAsync("/api/getAllPets");
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var allPets = JsonConvert.DeserializeObject<List<Pet>>(responseBody);
        Assert.Equal(pet, allPets[0]);
    }

    [Fact]
    public async void Should_return_pet_to_client_given_name_successfully()
    {
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();

        var pet = new Pet(name: "kitty", type: "cat", color: "black", price: 1000);
        var serializeObject = JsonConvert.SerializeObject(pet);
        var postBody = new StringContent(serializeObject, Encoding.UTF8, mediaType: "application/json");
        await httpClient.PostAsync("/api/addNewPet", postBody);

        var response = await httpClient.GetAsync("/api/findPetsByName?name=kitty");
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var allPets = JsonConvert.DeserializeObject<List<Pet>>(responseBody);
        Assert.Equal(pet, allPets[0]);
    }

    [Fact]
    public async void Should_return_pet_to_client_given_type_successfully()
    {
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();

        var pet = new Pet(name: "kitty", type: "cat", color: "black", price: 1000);
        var serializeObject = JsonConvert.SerializeObject(pet);
        var postBody = new StringContent(serializeObject, Encoding.UTF8, mediaType: "application/json");
        await httpClient.PostAsync("/api/addNewPet", postBody);

        var response = await httpClient.GetAsync("/api/findPetsByType?type=cat");
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var allPets = JsonConvert.DeserializeObject<List<Pet>>(responseBody);
        Assert.Equal(pet, allPets[0]);
    }

    [Fact]
    public async void Should_return_pets_to_client_given_price_range_successfully()
    {
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();

        var pet = new Pet(name: "kitty", type: "cat", color: "black", price: 1000);
        var serializeObject = JsonConvert.SerializeObject(pet);
        var postBody = new StringContent(serializeObject, Encoding.UTF8, mediaType: "application/json");
        await httpClient.PostAsync("/api/addNewPet", postBody);

        var response = await httpClient.GetAsync("/api/findPetsByPriceRange?lowerPrice=0&upperPrice=1100");
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var allPets = JsonConvert.DeserializeObject<List<Pet>>(responseBody);
        Assert.Equal(pet, allPets[0]);
    }

    [Fact]
    public async void Should_return_pets_to_client_given_color_successfully()
    {
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();

        var pet = new Pet(name: "kitty", type: "cat", color: "black", price: 1000);
        var serializeObject = JsonConvert.SerializeObject(pet);
        var postBody = new StringContent(serializeObject, Encoding.UTF8, mediaType: "application/json");
        await httpClient.PostAsync("/api/addNewPet", postBody);

        var response = await httpClient.GetAsync("/api/findPetsByColor?color=black");
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var allPets = JsonConvert.DeserializeObject<List<Pet>>(responseBody);
        Assert.Equal(pet, allPets[0]);
    }

    [Fact]
    public async void Should_delete_pet_if_brought_successfully()
    {
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();

        var pet = new Pet(name: "kitty", type: "cat", color: "black", price: 1000);
        var serializeObject = JsonConvert.SerializeObject(pet);
        var postBody = new StringContent(serializeObject, Encoding.UTF8, mediaType: "application/json");
        await httpClient.PostAsync("/api/addNewPet", postBody);

        var response = await httpClient.DeleteAsync("/api/deleteBoughtPet?name=kitty");
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var actionResult = JsonConvert.DeserializeObject<string>(responseBody);
        Assert.Equal("delete!", actionResult);
    }
}