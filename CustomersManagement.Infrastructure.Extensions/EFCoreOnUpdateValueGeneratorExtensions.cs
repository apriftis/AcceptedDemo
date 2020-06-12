using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomersManagement.Infrastructure.Extensions
{
    public static class EFCoreOnUpdateValueGeneratorExtensions
    {
        private const string OnUpdatedGenerateValueAnnotationName = "OnUpdateGenerateValue";
        private const string OnCreatedGenerateValueAnnotationName = "OnCreateGenerateValue";

        private const string DateTimeUtcNow = "ValueGenerator:DateTimeUtcNow";
        private const string DateTimeOffsetUtcNow = "ValueGenerator:DateTimeOffsetUtcNow";
        private const string DateTimeNow = "ValueGenerator:DateTimeNow";
        private const string DateTimeOffsetNow = "ValueGenerator:DateTimeOffsetNow";

        private static readonly Dictionary<string, ValueGenerator> DelegatingValueGenerators = new Dictionary<string, ValueGenerator>  {
            { DateTimeUtcNow, new DelegateValueGenerator(() => DateTime.UtcNow )},
            { DateTimeOffsetUtcNow , new DelegateValueGenerator(() => DateTimeOffset.UtcNow )},
            { DateTimeNow , new DelegateValueGenerator(() => DateTimeOffset.Now )},
            { DateTimeOffsetNow , new DelegateValueGenerator(() => DateTimeOffset.Now )},
        };

        public static PropertyBuilder<TProperty> NewDateAutoInsertedOnUpdate<TProperty>(this PropertyBuilder<TProperty> propertyBuilder, DateTimeKind dateTimeKind = DateTimeKind.Utc)
        {
            if (typeof(TProperty) == typeof(DateTime) || typeof(TProperty) == typeof(DateTime?))
            {
                propertyBuilder
                    .HasAnnotation(OnUpdatedGenerateValueAnnotationName, dateTimeKind == DateTimeKind.Utc ? DateTimeUtcNow : DateTimeNow);
            }
            else if (typeof(TProperty) == typeof(DateTimeOffset) || typeof(TProperty) == typeof(DateTimeOffset?))
            {
                propertyBuilder
                    .HasAnnotation(OnUpdatedGenerateValueAnnotationName, dateTimeKind == DateTimeKind.Utc ? DateTimeOffsetUtcNow : DateTimeOffsetNow);
            }
            return propertyBuilder;
        }

        public static PropertyBuilder<TProperty> NewDateAutoInsertedOnCreate<TProperty>(this PropertyBuilder<TProperty> propertyBuilder, DateTimeKind dateTimeKind = DateTimeKind.Utc)
        {
            if (typeof(TProperty) == typeof(DateTime) || typeof(TProperty) == typeof(DateTime?))
            {
                propertyBuilder
                    .HasAnnotation(OnCreatedGenerateValueAnnotationName, dateTimeKind == DateTimeKind.Utc ? DateTimeUtcNow : DateTimeNow);
            }
            else if (typeof(TProperty) == typeof(DateTimeOffset) || typeof(TProperty) == typeof(DateTimeOffset?))
            {
                propertyBuilder
                    .HasAnnotation(OnCreatedGenerateValueAnnotationName, dateTimeKind == DateTimeKind.Utc ? DateTimeOffsetUtcNow : DateTimeOffsetNow);
            }
            return propertyBuilder;
        }

        public static void ApplyModificationTimestampChanges(this DbContext context)
        {
            var onUpdateGeneratedProperties = context.ChangeTracker.Entries()
                    .Where(entry => entry.State == EntityState.Modified || entry.State == EntityState.Added)
                    .SelectMany(entry => entry.GetPropertiesWithAnotation(OnUpdatedGenerateValueAnnotationName))
                    .ToList();

            var onCreateUpdatedProperties = context.ChangeTracker.Entries()
                .Where(entry => entry.State == EntityState.Added)
                .SelectMany(entry => entry.GetPropertiesWithAnotation(OnCreatedGenerateValueAnnotationName))
                .ToList();

            foreach (var property in onUpdateGeneratedProperties)
            {
                var generator = DelegatingValueGenerators[property.Metadata.FindAnnotation(OnUpdatedGenerateValueAnnotationName).Value.ToString()];
                property.CurrentValue = generator.Next(property.EntityEntry);
            };

            foreach (var property in onCreateUpdatedProperties)
            {
                var generator = DelegatingValueGenerators[property.Metadata.FindAnnotation(OnCreatedGenerateValueAnnotationName).Value.ToString()];
                property.CurrentValue = generator.Next(property.EntityEntry);
            };
        }

        public static List<PropertyEntry> GetPropertiesWithAnotation(this EntityEntry entry, string annotationName)
             => entry.Properties
                    .Where(x => x.Metadata.GetAnnotations()
                    .Any(a => a.Name == annotationName))
                    .ToList();
    }


    public class DelegateValueGenerator : ValueGenerator
    {
        private readonly Func<object> func;

        public DelegateValueGenerator(Func<object> func) => this.func = func;

        public override bool GeneratesTemporaryValues => false;

        protected override object NextValue(EntityEntry entry) => func();

    }
}
