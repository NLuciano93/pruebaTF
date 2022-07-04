using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fusap.Common.Model.ErrorCatalogs
{
    public static class ErrorCatalogDescriptionExtensions
    {
        public static string SerializeAsMarkdown(this ErrorCatalogDescription catalogDescription)
        {
            var sb = new StringBuilder();
            using var sw = new StringWriter(sb);

            sw.WriteLine("# Error catalog");

            SerializeAsMarkdown(catalogDescription, null, sw);

            sw.Flush();

            return sb.ToString();
        }

        private static void SerializeAsMarkdown(ErrorCatalogDescription catalogDescription, string? name, StringWriter writer)
        {
            writer.WriteLine("");

            if (!string.IsNullOrWhiteSpace(name))
            {
                writer.WriteLine($"## {name}");
            }
            writer.WriteLine("");

            if (catalogDescription.Entries.Any())
            {
                const int MinColumnLength = 30;

                var lines = new List<(string, string, string)> { ("Code".PadRight(MinColumnLength),
                    "Key".PadRight(MinColumnLength), "Message".PadRight(MinColumnLength * 2)) };
                lines.AddRange(catalogDescription
                    .Entries
                    .OrderBy(x => x.Value.Code)
                    .Select(x => (x.Value.Code, x.Key, x.Value.Message)));

                var maxColumns = new int[]
                {
                    lines.Max(x => x.Item1.Length),
                    lines.Max(x => x.Item2.Length),
                    lines.Max(x => x.Item3.Length)
                };


                const int FirstColumn = 0;
                const int SecondColumn = 1;
                const int ThirdColumn = 2;

                var headerSplit = false;
                foreach (var (item1, item2, item3) in lines)
                {
                    writer.WriteLine($"| {item1.PadRight(maxColumns[FirstColumn])} | {item2.PadRight(maxColumns[SecondColumn])} " +
                                     $"| {item3.PadRight(maxColumns[ThirdColumn])} |");
                    if (!headerSplit)
                    {
                        const int Spacing = 2;
                        writer.WriteLine("|{0}|{1}|{2}|", new string('-', maxColumns[FirstColumn] + Spacing),
                            new string('-', maxColumns[SecondColumn] + Spacing), new string('-', maxColumns[ThirdColumn] + Spacing));
                        headerSplit = true;
                    }
                }

                writer.WriteLine("");
            }

            if (catalogDescription.Catalogs.Any())
            {
                foreach (var (key, value) in catalogDescription.Catalogs)
                {
                    SerializeAsMarkdown(value, (string.IsNullOrEmpty(name) ? string.Empty : name + '\\') + key, writer);
                }

                writer.WriteLine("");
            }
        }
    }
}
