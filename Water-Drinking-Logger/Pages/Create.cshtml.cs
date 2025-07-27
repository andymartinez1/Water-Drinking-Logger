using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Water_Drinking_Logger.Models;

namespace Water_Drinking_Logger.Pages;

public class Create : PageModel
{
    private readonly IConfiguration _configuration;

    public Create(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [BindProperty]
    public DrinkingWater DrinkingWater { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        using (
            var connection = new SqliteConnection(
                _configuration.GetConnectionString("DefaultConnection")
            )
        )
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                @$"INSERT INTO drinking_water(date, quantity) 
                    VALUES('{DrinkingWater.Date}', 
                            {DrinkingWater.Quantity})";

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }

        return RedirectToPage("/Index");
    }
}
