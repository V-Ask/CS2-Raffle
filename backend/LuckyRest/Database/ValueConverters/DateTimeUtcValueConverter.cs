using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LuckyRest.Database.ValueConverters;

public class DateTimeUtcValueConverter() : ValueConverter<DateTime, DateTime>(
    d => d.ToUniversalTime(),
    d => DateTime.SpecifyKind(d, DateTimeKind.Utc).ToLocalTime()
);