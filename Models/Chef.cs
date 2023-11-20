#pragma warning disable CS8618
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChefsNDishes.Models;

public class Chef
{

    [Key]
    public int ChefId { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "First Name number be at least 2 characters")]
    [DisplayName("First Name")]
    public string FirstName { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "Last Name number be at least 2 characters")]
    [DisplayName("Last Name")]
    public string LastName { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [MinAge(18)]
    public DateTime DOB { get; set; }

    public List<Dish> AllDishes { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}

public class MinAge : ValidationAttribute
{
    private int _Limit;
    public MinAge(int Limit)
    {
        this._Limit = Limit;
    }
    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {


        if (value == null)
        {
            return new ValidationResult("Please enter a date of birth");
        }

        DateTime DOB = (DateTime)value;
        DateTime Today = DateTime.Today;
        int age = Today.Year - DOB.Year;
        if (DOB > Today.AddYears(-age))
        {
            age--;
        }
        if (age < _Limit)
        {
            var result = new ValidationResult($"You must be {_Limit} years or older.");
            return result;
        }

        return ValidationResult.Success;

    }
}