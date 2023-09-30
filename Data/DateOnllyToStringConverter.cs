using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Zerno.Data
{
    public class DateOnlyToStringConverter : ValueConverter<DateOnly, string>
    {
        public DateOnlyToStringConverter() : base(v => v.ToString(), v => DateOnly.Parse(v)) {}
    }
}
