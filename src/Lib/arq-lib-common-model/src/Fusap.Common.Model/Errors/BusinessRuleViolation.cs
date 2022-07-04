using System.Collections.Generic;
using System.Linq;

namespace Fusap.Common.Model
{
    public readonly struct BusinessRuleViolation
    {
        public string Code { get; }
        public string Message { get; }
        public IEnumerable<KeyValuePair<string, string>>? Properties { get; }

        public BusinessRuleViolation(ErrorCatalogEntry catalogEntry, IEnumerable<KeyValuePair<string, string>>? properties = null)
        {
            Code = catalogEntry.Code;
            Message = catalogEntry.Message;
            Properties = properties;
        }

        public BusinessRuleViolation(string code, string message, IEnumerable<KeyValuePair<string, string>>? properties = null)
        {
            Code = code;
            Message = message;
            Properties = properties;
        }

        public static implicit operator BusinessRuleViolation(ErrorCatalogEntry catalogEntry)
        {
            return new BusinessRuleViolation(catalogEntry);
        }

        public override string ToString()
        {
            var propertiesValue = Properties == null
                ? string.Empty
                : " [" + string.Join(";", Properties.Select(x => x.Key + "=" + x.Value)) + "]";

            return $"{Code} - {Message}{propertiesValue}";
        }
    }
}
