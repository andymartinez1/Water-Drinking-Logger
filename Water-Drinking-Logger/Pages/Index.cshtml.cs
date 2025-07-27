using System.Globalization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Water_Drinking_Logger.Models;

namespace Water_Drinking_Logger.Pages;

public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;

    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<DrinkingWater> Records { get; set; }

    public void OnGet()
    {
        Records = GetAllRecords();
    }

    private List<DrinkingWater> GetAllRecords()
    {
        using (
            var connection = new SqliteConnection(
                _configuration.GetConnectionString("DefaultConnection")
            )
        )
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = "SELECT * FROM drinking_water";
            var tableData = new List<DrinkingWater>();
            var reader = tableCmd.ExecuteReader();

            while (reader.Read())
                tableData.Add(
                    new DrinkingWater
                    {
                        Id = reader.GetInt32(0),
                        Date = DateTime.Parse(
                            reader.GetString(1),
                            CultureInfo.CurrentUICulture.DateTimeFormat
                        ),
                        Quantity = reader.GetInt32(2),
                    }
                );
            connection.Close();

            return tableData;
        }
    }
}
