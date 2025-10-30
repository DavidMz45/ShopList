using System;

namespace ShopList.Helpers;

public static class ValidationRules
{
    public static bool ValidateProductName(string? name, out string message)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            message = "El nombre es obligatorio.";
            return false;
        }

        var trimmed = name.Trim();
        if (trimmed.Length < 2 || trimmed.Length > 80)
        {
            message = "El nombre debe tener entre 2 y 80 caracteres.";
            return false;
        }

        message = string.Empty;
        return true;
    }

    public static bool ValidateQuantity(int quantity, out string message)
    {
        if (quantity <= 0)
        {
            message = "La cantidad debe ser mayor que cero.";
            return false;
        }

        message = string.Empty;
        return true;
    }
}
