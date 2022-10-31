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
        var pet = new Pet(name: "Kitty", type: "cat", color: "white", price: 1000);
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
        var pet = new Pet(name: "Kitty", type: "cat", color: "white", price: 1000);
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
    public async Task Should_return_get_pet_success_from_get_pet_type()
    {
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();
        await httpClient.DeleteAsync("/api/deleteAllPets");
        var pet = new Pet(name: "Kitty", type: "cat", color: "white", price: 1000);
        var serializeObject = JsonConvert.SerializeObject(pet);
        var stringContent = new StringContent(serializeObject, Encoding.UTF8, "application/json");
        // when
        await httpClient.PostAsync("api/addNewPet", stringContent);
        var response = await httpClient.GetAsync($"api/addNewPet?type={pet.Type}");
        // then
        response.EnsureSuccessStatusCode();
        var readAsStringAsync = await response.Content.ReadAsStringAsync();
        var savedPet = JsonConvert.DeserializeObject<List<Pet>>(readAsStringAsync);
        Assert.Equal(pet, savedPet[0]);
    }

    [Fact]
    public async Task Should_return_get_pet_success_from_get_pet_color()
    {
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();
        await httpClient.DeleteAsync("/api/deleteAllPets");
        var pet = new Pet(name: "Kitty", type: "cat", color: "white", price: 1000);
        var serializeObject = JsonConvert.SerializeObject(pet);
        var stringContent = new StringContent(serializeObject, Encoding.UTF8, "application/json");
        // when
        await httpClient.PostAsync("api/addNewPet", stringContent);
        var response = await httpClient.GetAsync($"api/addNewPet?color={pet.Color}");
        // then
        response.EnsureSuccessStatusCode();
        var readAsStringAsync = await response.Content.ReadAsStringAsync();
        var savedPet = JsonConvert.DeserializeObject<List<Pet>>(readAsStringAsync);
        Assert.Equal(pet, savedPet[0]);
    }

    [Fact]
    public async Task Should_return_get_pet_success_from_get_pet_price()
    {
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();
        await httpClient.DeleteAsync("/api/deleteAllPets");
        var pet = new Pet(name: "Kitty", type: "cat", color: "white", price: 1000);
        var serializeObject = JsonConvert.SerializeObject(pet);
        var stringContent = new StringContent(serializeObject, Encoding.UTF8, "application/json");
        // when
        await httpClient.PostAsync("api/addNewPet", stringContent);
        var response = await httpClient.GetAsync($"api/addNewPet?upper={pet.Price + 50}&lower={pet.Price - 50}");
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
        var pet = new Pet(name: "Kitty", type: "cat", color: "white", price: 1000);
        var serializeObject = JsonConvert.SerializeObject(pet);
        var stringContent = new StringContent(serializeObject, Encoding.UTF8, "application/json");
        // when
        await httpClient.PostAsync("api/addNewPet", stringContent);
        var response = await httpClient.DeleteAsync($"api/addNewPet?name={pet.Name}");
        // then
        response.EnsureSuccessStatusCode();
        var readAsStringAsync = await response.Content.ReadAsStringAsync();
        var savedPet = JsonConvert.DeserializeObject<List<Pet>>(readAsStringAsync);
        Assert.Equal(null, savedPet[0]);
    }

    [Fact]
    public async Task Should_return_change_pet_success_from_change_pet()
    {
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();
        await httpClient.DeleteAsync("/api/deleteAllPets");
        var pet = new Pet(name: "Kitty", type: "cat", color: "white", price: 1000);
        var serializeObject = JsonConvert.SerializeObject(pet);
        var stringContent = new StringContent(serializeObject, Encoding.UTF8, "application/json");
        var newPet = new Pet(name: "Kitty", type: "cat", color: "white", price: 2000);
        var price = JsonConvert.SerializeObject(newPet);
        var stringContent2 = new StringContent(price, Encoding.UTF8, "application/json");
        // when
        await httpClient.PostAsync("api/addNewPet", stringContent);
        var response = await httpClient.PutAsync($"api/addNewPet?name={pet.Name}", stringContent2);
        // then
        response.EnsureSuccessStatusCode();
        var readAsStringAsync = await response.Content.ReadAsStringAsync();
        var savedPet = JsonConvert.DeserializeObject<List<Pet>>(readAsStringAsync);
        Assert.Equal(newPet, savedPet[0]);
    }
}