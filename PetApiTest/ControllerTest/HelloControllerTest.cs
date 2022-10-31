using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PetApi.Model;
using System.Collections.Generic;
using System.Drawing;
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
    public async void Should_add_new_pet_to_system_successsfully()
    {
        //given
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();
        var pet = new Pet(name: "Kitty", type: "cat", color: "white", price: 1000);
        var serializeObject = JsonConvert.SerializeObject(pet);
        var postBody = new StringContent(serializeObject, Encoding.UTF8, mediaType: "application/json");
        var response = await httpClient.PostAsync("api/addNewPet", postBody);

        //then
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var savedPet = JsonConvert.DeserializeObject<Pet>(responseBody);
        Assert.Equal(pet, savedPet);
    }

    [Fact]
    public async void Should_return_app_pet_success_when_get_pet()
    {
        //given
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();
        await httpClient.DeleteAsync("api/deleteAllPets");
        var pet = new Pet(name: "Kitty", type: "cat", color: "white", price: 1000);
        var serializeObject = JsonConvert.SerializeObject(pet);
        var stringContent = new StringContent(serializeObject, Encoding.UTF8, "application/json");
        await httpClient.PostAsync("api/addNewPet", stringContent);
        var response = await httpClient.GetAsync("api/getAllPets");

        //then
        response.EnsureSuccessStatusCode();
        var readAsStringAsync = await response.Content.ReadAsStringAsync();
        var savedPet = JsonConvert.DeserializeObject<List<Pet>>(readAsStringAsync);
        Assert.Equal(pet, savedPet[0]);
    }

    [Fact]
    public async void Should_get_pet_success_when_query()
    {
        //given
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();
        await httpClient.DeleteAsync("api/deleteAllPets");
        var pet = new Pet(name: "Kitty", type: "cat", color: "white", price: 1000);
        var serializeObject = JsonConvert.SerializeObject(pet);
        var stringContent = new StringContent(serializeObject, Encoding.UTF8, "application/json");
        await httpClient.PostAsync("api/addNewPet", stringContent);
        var response = await httpClient.GetAsync("api/findPetsByName/type=cat?");

        //then
        response.EnsureSuccessStatusCode();
        var readAsStringAsync = await response.Content.ReadAsStringAsync();
        var savedPet = JsonConvert.DeserializeObject<List<Pet>>(readAsStringAsync);
        Assert.Equal(pet, savedPet[0]);
    }
}