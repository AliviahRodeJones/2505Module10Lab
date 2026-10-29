using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VirtualPetAdoption.Data;
using VirtualPetAdoption.Models;

namespace VirtualPetAdoption.Pages
{
    public class IndexModel : PageModel
    {
        // Create a new instance of the database context - PetAdoptionContext.
        private readonly PetAdoptionContext _context;
        
        // Initialize the new database context object created 
        // using the ped adoption context created by the service. 
        public IndexModel(PetAdoptionContext context)
        {
            _context = context;
        }

        // Attributes of the class. 
        [BindProperty]
        public string Name { get; set; } = "";
        // name: name of the person completing the form

        [BindProperty]
        public int EnergyPreference { get; set; }
        // energypreference: the 

        public Pet? Match { get; set; }
        // The pet object the user is matched with. 

        public void OnGet()
        {
        }

        // Method that runs when the user submits the form. 
        public void OnPost()
        {
            // The best match is the pet with the closest energy level.
            Match = _context.Pets
                .OrderBy(p => Math.Abs(p.EnergyLevel - EnergyPreference))
                .First();
            // Sqlite. 
            // The math calculates the closest value to the users energy level 
            // using the absolute value of the pets EnergyLevel minus the EnergyPreference. 
        }
    }
}