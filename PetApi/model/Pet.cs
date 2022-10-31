namespace PetApi.Models;
public class Pet
{
    private string name;
    private string type;
    private int age;
    private int price;

    public Pet(string name, string type, int age, int price)
    {
        this.name = name;
        this.type = type;
        this.Age = age;
        this.Price = price;
    }

    public string Name { get => name; set => name = value; }
    public string Type { get => type; set => type = value; }
    public int Age { get => age; set => age = value; }
    public int Price { get => price; set => price = value; }

    public override bool Equals(object? obj)
    {
        return obj is Pet pet &&
               name == pet.name &&
               type == pet.type &&
               Age == pet.Age &&
               Price == pet.Price &&
               Name == pet.Name;
    }
}