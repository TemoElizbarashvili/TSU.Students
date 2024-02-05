namespace TSUS.Domain.ValidationAttributes;

public class EmailTsuEnding : System.ComponentModel.DataAnnotations.ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null) return false;
        if (value is string email)
        {
            return email.EndsWith("tsu.edu.ge", StringComparison.OrdinalIgnoreCase);
        }

        return false;
    }
}