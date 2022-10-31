using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PetApi.Controllers;
using PetApi.Models;
using Xunit;
namespace PetApiTest.ControllerTest;
public class PetController
{
    [Fact]
    public async Task Should_return_add_pet_success_when_add_pet()
    {
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();
        await httpClient.DeleteAsync("/api/deleteAllPets");
        var pet = new Pet(name: "Kitty", type: "cat", age: 1, price: 1000);
        var serializeObject = JsonConvert.SerializeObject(pet);
        var stringContent = new StringContent(serializeObject, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("api/addNewPet", stringContent);
        response.EnsureSuccessStatusCode();
        var readAsStringAsync = await response.Content.ReadAsStringAsync();
        var savedPet = JsonConvert.DeserializeObject<Pet>(readAsStringAsync);
        Assert.Equal(pet, savedPet);
    }

    [Fact]
    public async Task Should_return_get_pet_success_from_get_pet()
    {
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();
        await httpClient.DeleteAsync("/api/deleteAllPets");
        var pet = new Pet(name: "Kitty", type: "cat", age: 1, price: 1000);
        var serializeObject = JsonConvert.SerializeObject(pet);
        var stringContent = new StringContent(serializeObject, Encoding.UTF8, "application/json");
        // when
        await httpClient.PostAsync("api/addNewPet", stringContent);
        var response = await httpClient.GetAsync("api/getAllPets");
        // then
        response.EnsureSuccessStatusCode();
        var readAsStringAsync = await response.Content.ReadAsStringAsync();
        var savedPet = JsonConvert.DeserializeObject<List<Pet>>(readAsStringAsync);
        Assert.Equal(pet, savedPet[0]);
    }

    [Fact]
    public async Task Should_return_get_pet_success_from_get_pet_name()
    {
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();
        await httpClient.DeleteAsync("/api/deleteAllPets");
        var pet = new Pet(name: "Kitty", type: "cat", age: 1, price: 1000);
        var serializeObject = JsonConvert.SerializeObject(pet);
        var stringContent = new StringContent(serializeObject, Encoding.UTF8, "application/json");
        // when
        await httpClient.PostAsync("api/addNewPet", stringContent);
        var response = await httpClient.GetAsync("api/getAllPets");
        // then
        response.EnsureSuccessStatusCode();
        var readAsStringAsync = await response.Content.ReadAsStringAsync();
        var savedPet = JsonConvert.DeserializeObject<List<Pet>>(readAsStringAsync);
        Assert.Equal(pet, savedPet[0]);
    }

    [Fact]
    public async Task Should_return_delete_pet_success_from_delete_pet()
    {
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();
        await httpClient.DeleteAsync("/api/deleteAllPets");
        var pet = new Pet(name: "Kitty", type: "cat", age: 1, price: 1000);
        var serializeObject = JsonConvert.SerializeObject(pet);
        var stringContent = new StringContent(serializeObject, Encoding.UTF8, "application/json");
        // when
        await httpClient.PostAsync("api/addNewPet", stringContent);
        var response = await httpClient.GetAsync("api/getAllPets");
        // then
        response.EnsureSuccessStatusCode();
        var readAsStringAsync = await response.Content.ReadAsStringAsync();
        var savedPet = JsonConvert.DeserializeObject<List<Pet>>(readAsStringAsync);
        Assert.Equal(pet, savedPet[0]);
    }
}