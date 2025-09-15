using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.Application.Extensions
{
    public static class ObjectExtensions
    {
        public static void ValidateValue(this object? value, string fieldName)
        {
            if (value == null)
                throw new ValidationException($"The field '{fieldName}' is required.");

            switch (value)
            {
                case string str when string.IsNullOrWhiteSpace(str):
                    throw new ValidationException($"The field '{fieldName}' is required.");
                case int i when i < 0:
                    throw new ValidationException($"The field '{fieldName}' cannot be negative.");
                case decimal d when d < 0:
                    throw new ValidationException($"The field '{fieldName}' cannot be negative.");
            }
        }
    }
}
