using System.ComponentModel.DataAnnotations;

namespace GameStore.Shared.Validations;

public class MinAge : ValidationAttribute
{
    public int AllowedMinAge { get; set; }

    public override bool IsValid(object? value)
    {
        if (value is not DateTime dob) return false;
        var now = DateTime.Now;
        var age = now.Year - dob.Year;
        return age >= AllowedMinAge;
    }

    public override string FormatErrorMessage(string name)
    {
        return "Age must be greater than or equal to minimum age.";
    }
}