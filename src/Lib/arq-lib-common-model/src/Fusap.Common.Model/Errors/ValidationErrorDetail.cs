namespace Fusap.Common.Model
{
    public readonly struct ValidationErrorDetail
    {
        public string? PropertyName { get; }
        public string Code { get; }
        public string Message { get; }

        public ValidationErrorDetail(ErrorCatalogEntry catalogEntry)
        {
            PropertyName = default;
            Code = catalogEntry.Code;
            Message = catalogEntry.Message;
        }

        public ValidationErrorDetail(string propertyName, ErrorCatalogEntry catalogEntry)
        {
            PropertyName = propertyName;
            Code = catalogEntry.Code;
            Message = catalogEntry.Message;
        }

        public ValidationErrorDetail(string propertyName, string code, string message)
        {
            PropertyName = propertyName;
            Code = code;
            Message = message;
        }

        public static implicit operator ValidationErrorDetail(ErrorCatalogEntry catalogEntry)
        {
            return new ValidationErrorDetail(catalogEntry);
        }

        public override string ToString()
        {
            var propertyValue = PropertyName == null
                ? string.Empty
                : $" on property {PropertyName}";

            return $"{Code}{propertyValue} - {Message}";
        }
    }
}
