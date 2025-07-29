using System.ComponentModel.DataAnnotations;

namespace Water_Drinking_Logger.Models;

public class DrinkingWater
{
    public int Id { get; set; }

    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }

    [Range(0, Int32.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
    public int Quantity { get; set; }
}
