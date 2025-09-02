using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq;

namespace TodoApp.EntityFrameworkCore
{
    public static class UtcDateTimeConverter
    {
        public static void ApplyUtcDateTimeConverter(this ModelBuilder builder)
        {
            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?));
                foreach (var property in properties)
                {
                    builder.Entity(entityType.ClrType)
                        .Property(property.Name)
                        .HasConversion(dateTimeConverter);
                }
            }
        }
    }
}
