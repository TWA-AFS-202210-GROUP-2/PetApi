namespace PetApi
{
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
            this.color = color;
            this.price = price;
        }

        public Pet()
        {
        }

        public string Name { get => name; set => name = value; }
        public string Type { get => type; set => type = value; }
        public string Color { get => color; set => color = value; }
        public int Price { get => price; set => price = value; }

        public override bool Equals(object? obj)
        {
            var pet = obj as Pet;
            return pet != null &&
                name.Equals(pet.name) &&
                type.Equals(pet.type) &&
                color.Equals(pet.color) &&
                price.Equals(pet.price);
        }
    }
}