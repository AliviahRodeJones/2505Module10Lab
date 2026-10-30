using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Reflection;

// The main class for this page
public class UgliestDogsModel : PageModel
{
    // Class Attributes
    public List<SelectListItem> DogList { get; set; }
    public Dog SelectedDog { get; set; }

    // Runs when the web app starts.
    public void OnGet()
    { 
        LoadDogList(); // Loads the dogs 
    }

    // Runs when the form is submitted
    public void OnPost(string selectedDog)
    {
        LoadDogList();
        if (!string.IsNullOrEmpty(selectedDog))
        // If the selectedDog string isn't null. 
        {
            SelectedDog = GetDogByInt(int.Parse(selectedDog));
        }
    }

    // Method used in the OnGet and OnPost methods
    private void LoadDogList()
    {
        DogList = new List<SelectListItem>();
        using (var connection = new SqliteConnection("Data Source=UgliestDogs.db"))
        // Specifies the connection that will be used to load the list. 
        {
            connection.Open();
            // Opens the connection with the database.
            var command = connection.CreateCommand();
            // Stores the SQL command that will be used to query the database. 
            command.CommandText = "SELECT Id, Name FROM Dogs";
            // An SQL statement that selects the ID and Name from the Dogs table. 
            using (var reader = command.ExecuteReader())
            // Opens a new reader. 
            {
                while (reader.Read())
                // Runs while there are new lines availible. 
                {
                    DogList.Add(new SelectListItem
                    // Adds the values to the doglist.
                    {
                        Value = reader.GetInt32(0).ToString(),
                        // Id
                        Text = reader.GetString(1)
                        // Name
                    });
                }
            }
        }
    }

    // Method used to get a dog from the Dogs table by Id. 
    private Dog GetDogByInt(int id)
    // Takes the id int as a parameter. 
    {
        using (var connection = new SqliteConnection("Data Source=UgliestDogs.db"))
        // Specifies what connection is being used
        {
            connection.Open();
            // Opens the connection
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Dogs WHERE Id = @Id";
            // SQL statement. Queries the Dogs table for the dog with a matching ID. 
            command.Parameters.AddWithValue("@Id", id);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Dog
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Breed = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        ImageFileName = reader.GetString(4)
                    };
                }
            }
        }

        return null;
    }
}

// The Dog class. Used to create objects from the data stored in the database. 
public class Dog
{
    public int Id { get; set;}
    public string Name { get; set; }
    public string Breed { get; set; }
    public int Year { get; set; }
    public string ImageFileName { get; set; }
}