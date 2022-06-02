using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Utilities;

namespace Utilities
{
    public class LogColorizer
    {
        private static readonly Dictionary<string, Color> Colors = new Dictionary<string, Color>()
        {
            {"red",Color.red},
            {"yellow",Color.yellow},
            {"green",Color.green},
            {"blue",Color.blue},
            {"cyan",Color.cyan},
            {"magenta",Color.magenta},
            {"orange",StringUtilities.ToColor("#FFA500")},
            {"olive",StringUtilities.ToColor("#808000")},
            {"purple",StringUtilities.ToColor("#800080")},
            {"darkred",StringUtilities.ToColor("#8B0000")},
            {"darkgreen",StringUtilities.ToColor("#006400")},
            {"darkorange",StringUtilities.ToColor("#FF8C00")},
            {"gold",StringUtilities.ToColor("#FFD700")},
        };

        private static readonly Dictionary<string, Color> RainBowColors = new Dictionary<string, Color>()
        {
            {"red",Color.red},
            {"orange",StringUtilities.ToColor("#FFA500")},
            {"yellow",Color.yellow},
            {"green",Color.green},
            {"lightblue",StringUtilities.ToColor("#0000FF")},
            {"violet",StringUtilities.ToColor("#8B00FF")},
        };

        private static readonly Dictionary<string, string> Emojis = new Dictionary<string, string>()
        {
            {"love",'\u2764'.ToString()},
            {"sniper","(　-_･) ︻デ═一 ▸"},
            {"bug",@"¯\_(ツ)_/¯"}
        };

        private static  string _prefix;

        private const string Suffix = "</color>";

        private static string[] styles =  {"b", "i"};

        private static string __prefix;

        private static string _suffix;

        private static string ConvertToHtml(Color color){
           return  $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>";
        }
        
        public static string GetPossibleValue(string value)
        {
            
            return Colors.ContainsKey(value) ||  value == "rainbow" ? value : string.Empty;
        }

        private static void ConvertToHtml_(string format)
        {
            __prefix = $"<{format}>";
            _suffix = $"</{format}>";
        }

        public static string GetPossibleValue_(string value)
        {
            return styles.Contains(value) ? value : string.Empty;
        }
        
        public static string GetEmoji(string text,string emoji)
        {
            emoji = emoji.Trim();
            if (!Emojis.ContainsKey(emoji)) return text;

            string spaces = Regex.Replace(text, "[^ ]", "");
            return spaces+Emojis[emoji];
        }

        public static string GetColorfulText(string text,GroupCollection groups)
        {
            string colorName = string.Empty;

            for (int i = 0; i < groups.Count; i++)
            {
                colorName =  GetPossibleValue(groups[i].Value);

                if(colorName != string.Empty) break;
            }
            
            
            
            if (colorName == "rainbow")
            {
                string message = "";
                int counter = 0;
            foreach (char c in text)
            {
                if (counter >= RainBowColors.Count) counter = 0;
                var randomColor = RainBowColors.ElementAt(counter).Key;
                _prefix = ConvertToHtml(RainBowColors[randomColor]);
                message += _prefix + c + Suffix;
                counter++;
            }

            return message;
            }
            
            
            if (Colors.ContainsKey(colorName))
                _prefix = ConvertToHtml(Colors[colorName]);
            else
                return text;

            return _prefix + text + Suffix;
        }

        public static string GetStyledText(string text,GroupCollection groups)
        {
            string style = string.Empty;

            for (int i = 0; i < groups.Count; i++)
            {
                style =  GetPossibleValue_(groups[i].Value);
                if(style != string.Empty) break;
            }
            
            
            
            if (styles.Contains(style))
                ConvertToHtml_(style);
            else
                return text;
            
            return __prefix + text + _suffix;
        }

        public static string Colorize(string str) {
            string pattern = @"([^;:]*)\:?([^;:]*)\:?([^;:]*)\:?([^;:]*)\;";
            RegexOptions options = RegexOptions.Multiline;
            Regex regex = new Regex(pattern, options);
            MatchCollection matches = regex.Matches(str);
string text = string.Empty;


            for (int i = 0; i < matches.Count; i++)
            {
                var emoji = GetEmoji(matches[i].Groups[1].Value, matches[i].Groups[1].Value);
                var colorfulText = GetColorfulText(emoji, matches[i].Groups);
                var styledText = GetStyledText(colorfulText, matches[i].Groups);

                text += styledText;
            }

            return text;
        }
    }
}