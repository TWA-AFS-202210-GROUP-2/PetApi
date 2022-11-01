namespace PetApi.Model
{
    public class Pet
    {
        public string name { get; set; }
        public string type { get; set; }
        public string color { get; set; }
        public int price { get; set; }

        public Pet()
        { 
        }

        public Pet(string name, string type, string color, int price)
        {
            this.name = name;
            this.type = type;
            this.color = color;
            this.price = price;
        }

        public override bool Equals(object? obj)
        {
            var pet = obj as Pet;
            return pet != null && 
                pet.name.Equals(name) &&
                pet.type.Equals(type) &&
                pet.color.Equals(color) &&
                pet.price == price;
        }
    }
}
