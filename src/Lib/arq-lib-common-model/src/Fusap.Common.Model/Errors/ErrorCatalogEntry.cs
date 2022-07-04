namespace Fusap.Common.Model
{
    public readonly struct ErrorCatalogEntry
    {
        public ErrorCatalogEntry(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; }
        public string Message { get; }

        public static implicit operator string(ErrorCatalogEntry err)
        {
            return err.Code;
        }

        public override string ToString()
        {
            return Code;
        }

        public static implicit operator Error(ErrorCatalogEntry entry)
        {
            return new Error(entry);
        }

        public static implicit operator ErrorCatalogEntry((string Code, string Message) codeMessage)
        {
            return new ErrorCatalogEntry(codeMessage.Code, codeMessage.Message);
        }
    }
}
