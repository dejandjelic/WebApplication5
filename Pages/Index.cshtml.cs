using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

public class IndexModel : PageModel
{
    private readonly IConfiguration _config;

    public IndexModel(IConfiguration config)
    {
        _config = config;
    }

    [BindProperty]
    public string FirstName { get; set; }

    [BindProperty]
    public string LastName { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        // Pravljenje konekcije
        string ConnectionStrings = _config.GetConnectionString("DefaultConnection");
        using (MySqlConnection connection = new MySqlConnection (ConnectionStrings))
        {
            
            connection.Open();

            // Kreiranje komande za ubacivanje podataka u tabelu Table sa FirstName I LastName 
            string query = "INSERT INTO Table (FirstName, LastName) VALUES (@FirstName, @LastName)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);

            // Izvrsna komanda
            command.ExecuteNonQuery();
        }

        return RedirectToPage("Index");
    }
}
