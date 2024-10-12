namespace ReSharperPlugin.UsingTools.Helpers;

public static class TextHandler
{
    public static void SortAndRemoveDuplicates2(IDocument document)
    {
        var lines = new List<string>();
        var linesSystem = new List<string>();
        var linesMicrosoft = new List<string>();
        var linesStatic = new List<string>();
        var linesAliases = new List<string>();

        var linesCount = document.GetLineCount();
        for (Int32<DocLine> i = (Int32<DocLine>)0; i < linesCount; i++)
        {
            var line = document.GetLineText(i);

            if (line.StartsWith("global using ") && line.Contains("="))
            {
                linesAliases.Add(line);
            }
            else if (line.StartsWith("global using System;") || line.StartsWith("global using System."))
            {
                linesSystem.Add(line);
            }
            else if (line.StartsWith("global using Microsoft;") || line.StartsWith("global using Microsoft."))
            {
                linesMicrosoft.Add(line);
            }
            else if (line.StartsWith("global using static"))
            {
                linesStatic.Add(line);
            }
            else if (line.StartsWith("global using "))
            {
                lines.Add(line);
            }
        }

        var uniqueSortedLines = new List<string> { Constants.UsingComment + "\n" };

        uniqueSortedLines.AddRange(linesSystem
            .Distinct()
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .OrderBy(x => x));

        uniqueSortedLines.AddRange(linesMicrosoft
            .Distinct()
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .OrderBy(x => x));

        uniqueSortedLines.AddRange(lines
            .Distinct()
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .OrderBy(x => x));

        uniqueSortedLines.AddRange(linesStatic
            .Distinct()
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .OrderBy(x => x));

        uniqueSortedLines.AddRange(linesAliases
            .Distinct()
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .OrderBy(x => x));

        document.SetText(string.Join("\n", uniqueSortedLines));
    }
}
