namespace PetApi.Models;
public class Pet
{
    private string name;
    private string type;
    private string color;
    private int price;

    public Pet(string name, string type, string color, int price)
    {
        this.name = name;
        this.type = type;
        this.Color = color;
        this.Price = price;
    }

    public string Name { get => name; set => name = value; }
    public string Type { get => type; set => type = value; }
    public string Color { get => color; set => color = value; }
    public int Price { get => price; set => price = value; }

    public override bool Equals(object? obj)
    {
        return obj is Pet pet &&
               name == pet.name &&
               type == pet.type &&
               Color == pet.Color &&
               Price == pet.Price &&
               Name == pet.Name;
    }
}