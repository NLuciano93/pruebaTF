using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Fusap.Common.Model.ErrorCatalogs
{
    public class ErrorCatalogDescription
    {
        public IReadOnlyDictionary<string, ErrorCatalogDescription> Catalogs { get; }
        public IReadOnlyDictionary<string, ErrorCatalogEntry> Entries { get; }

        private ErrorCatalogDescription(IDictionary<string, ErrorCatalogDescription> catalogs, IDictionary<string, ErrorCatalogEntry> entries)
        {
            Catalogs = new ReadOnlyDictionary<string, ErrorCatalogDescription>(catalogs);
            Entries = new ReadOnlyDictionary<string, ErrorCatalogEntry>(entries);
        }

        public static ErrorCatalogDescription For(Type catalogType)
        {
            var nestedCatalogs = catalogType
                .GetNestedTypes()
                .Where(x => x.IsAbstract)
                .OrderBy(x => x.Name)
                .ToDictionary(x => x.Name, For);

            var entries = catalogType
                .GetProperties()
                .Where(p => p.CanRead && !p.CanWrite && p.PropertyType == typeof(ErrorCatalogEntry) &&
                            p.GetMethod.IsStatic)
                .OrderBy(p => p.Name)
                .ToDictionary(p => p, p => (ErrorCatalogEntry)p.GetMethod.Invoke(null, null));

            // Ensuring unique codes
            var codesWithMultipleUses = entries
                .GroupBy(e => e.Value.Code)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToArray();
            if (codesWithMultipleUses.Length > 0)
            {
                throw new ArgumentException("Error codes duplicated in catalog: " +
                                            string.Join(", ", codesWithMultipleUses));
            }

            // Get properties that do not point to entries
            var invalidProperties = catalogType
                .GetProperties()
                .Where(x => !entries.ContainsKey(x))
                .ToArray();
            if (invalidProperties.Length > 0)
            {
                throw new ArgumentException("All properties in the error catalog must be static, have only a getter and have the return " +
                                            "type of `ErrorCatalogEntry`. These properties violate this rule: " +
                                            string.Join(", ", invalidProperties.Select(x => x.DeclaringType!.FullName + "." + x.Name)));

            }

            // Ensure that there are no fields with entry declarations
            var invalidFields = catalogType
                .GetFields()
                .Where(x => x.FieldType == typeof(ErrorCatalogEntry))
                .ToArray();
            if (invalidFields.Length > 0)
            {
                throw new ArgumentException("You should not store error catalog entries as fields. Please convert these to properties: " +
                                            string.Join(", ", invalidFields.Select(x => x.DeclaringType!.FullName + "." + x.Name)));
            }

            // Ensure all entries have error codes and messages
            var emptyCodes = entries
                .Where(x => string.IsNullOrWhiteSpace(x.Value.Code) || string.IsNullOrWhiteSpace(x.Value.Message))
                .Select(x => x.Key)
                .ToArray();
            if (emptyCodes.Length > 0)
            {
                throw new ArgumentException("Catalog entries have missing error codes or messages: " +
                                            string.Join(", ", emptyCodes.Select(x => x.DeclaringType!.FullName + "." + x.Name)));
            }

            return new ErrorCatalogDescription(nestedCatalogs, entries.ToDictionary(p => p.Key.Name, p => p.Value));
        }
    }
}
