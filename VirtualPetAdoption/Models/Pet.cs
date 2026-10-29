namespace VirtualPetAdoption.Models
{
    public class Pet
    {
        // Attributes of the pet class. 
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Species { get; set; } = "";
        public string Description { get; set; } = "";
        public string Emoji { get; set; } = "";
        public int EnergyLevel { get; set; } // 1 to 5 scale
    }
}