using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Strago.Extensions
{
    public class ApplicationExtensions
    {
        public static string[] GetExpArray(string expString)
        {
            expString = expString.Replace("Showing all skills that you have skill in.", String.Empty);
            expString = expString.Replace("SKILL: Rank/Percent towards next rank/Amount learning/Mindstate Fraction", String.Empty);
            expString = Regex.Replace(expString, @"Circle: \d{1,3}", String.Empty);
            expString = Regex.Replace(expString, @"\t|\n|\r", String.Empty);
            // expString = Regex.Replace(expString, @"\d\d%", ">");
            expString = Regex.Replace(expString, "\\(.*?\\)", String.Empty);
            expString = Regex.Replace(expString, @"clear|dabbling|perusing|learning|thoughtful|thinking|considering|pondering|ruminating|concentrating|attentive|deliberative|interested|examining|understanding|absorbing|intrigued|scrutinizing|analyzing|studious|focused|very focused|engaged|very engaged|cogitating|fascinated|captivated|engrossed|riveted|very riveted|rapt|very rapt|enthralled|nearly locked|mind lock", String.Empty);
            var expArray = Regex.Split(expString, @" \d\d%");
            expArray = expArray.Select(x => Regex.Replace(x, @"\s+", String.Empty)).ToArray();
            return expArray;
        }
    }
}