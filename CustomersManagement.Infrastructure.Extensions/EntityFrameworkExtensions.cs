using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CustomersManagement.Infrastructure.Extensions
{
    public static class EntityFrameworkExtensions
    {
        private const string OnUpdatedGenerateValueAnnotationName = "OnUpdateGenerateValue";
        private const string OnCreatedGenerateValueAnnotationName = "OnCreateGenerateValue";
        private const string DateTimeUtcNow = "ValueGenerator:DateTimeUtcNow";
        private const string DateTimeNow = "ValueGenerator:DateTimeUtcNow";

        public static PropertyBuilder<TProperty> NewDateAutoInsertedOnCreate<TProperty>(this PropertyBuilder<TProperty> propertyBuilder, DateTimeKind dateTimeKind = DateTimeKind.Utc)
        {
            if (typeof(TProperty) == typeof(DateTime) || typeof(TProperty) == typeof(DateTime?))
            {
                propertyBuilder
                    .HasAnnotation(OnCreatedGenerateValueAnnotationName, dateTimeKind == DateTimeKind.Utc ? DateTimeUtcNow : DateTimeNow);
            }

            return propertyBuilder;
        }

        public static PropertyBuilder<TProperty> NewDateAutoInsertedOnUpdate<TProperty>(this PropertyBuilder<TProperty> propertyBuilder, DateTimeKind dateTimeKind = DateTimeKind.Utc)
        {
            if (typeof(TProperty) == typeof(DateTime) || typeof(TProperty) == typeof(DateTime?))
            {
                
                propertyBuilder
                    .HasAnnotation(OnUpdatedGenerateValueAnnotationName, dateTimeKind == DateTimeKind.Utc ? DateTimeUtcNow : DateTimeNow);
            }
            return propertyBuilder;
        }
    }
}
