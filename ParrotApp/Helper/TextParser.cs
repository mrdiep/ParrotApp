using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParrotApp.Helper
{
    public static class TextParser
    {
        public static string GetHtmlMarkup(string text)
        {
            var lines = text.Split('\n');
            var formatPre = "<pre>{0}</pre>";
            StringBuilder builder = new StringBuilder();
            foreach (var line in lines)
            {
                builder.AppendLine(GetHtmlLineMarkup(line));
            }

            return string.Format(formatPre, builder.ToString());
        }

        private static string GetHtmlLineMarkup(string lineText)
        {
            //lineText = "[C]Ông bà anh yêu nhau thời chưa [G]có tivi ";
            string textChord = "<span class='hopamchuan_chord '>{0}</span>";
            var regex = new Regex(@"\[(.*?)\]");

            StringBuilder builder = new StringBuilder();
            var lastIndex = 0;
            var lastValue = "";
            do
            {
                var matches = regex.Matches(lineText);
                if (matches.Count == 0)
                    break;

                var match = matches[0];
                var value = match.Value.Substring(1, match.Value.Length - 2);

                builder.Append(GetSpace(match.Index - lastIndex - lastValue.Length) + string.Format(textChord, value));
                lineText = lineText.Remove(match.Index, match.Length);
                lastIndex = match.Index;
                lastValue = value;

            } while (true);


            builder.Append("\r\n" + lineText);
            return builder.ToString();
        }

        private static string GetSpace(int count)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                builder.Append(" ");
            }
            return builder.ToString();
        }
    }
}
