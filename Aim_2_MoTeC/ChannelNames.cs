using Aim_2_MoTeC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Aim_2_MoTeC
{
    public struct nameConvert
    {
        public string from;
        public string to;
        public string to_short;

        public nameConvert(string from, string to, string to_short = "")
        {
            this.from = from;
            this.to = to;
            this.to_short = to_short;
        }
    }
    public class ChannelNamesConvert
    {
        private nameConvert[] NAMES;
        public ChannelNamesConvert()
        {
            NAMES = ParseTextFile("NameConversion.txt");
        }

        static nameConvert[] ParseTextFile(string filePath)
        {
            List<nameConvert> mappings = new List<nameConvert>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (line[0] != '#')
                    {
                        // Use the modified regular expression pattern to handle optional last value
                        Match match = Regex.Match(line, "\"([^\"]+)\", \"([^\"]+)\", \"([^\"]*)\"");

                        if (match.Success)
                        {
                            nameConvert c = new nameConvert();
                            c.from = match.Groups[1].Value;
                            c.to = match.Groups[2].Value;
                            c.to_short = match.Groups[3].Value;

                            // If the last value is empty, set it to an empty string
                            if (c.to_short == string.Empty)
                            {
                                c.to_short = "";
                            }

                            mappings.Add(c);
                        }
                    }
                }

                return mappings.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public bool containsName(string from, out nameConvert nameStruct)
        {
            foreach (nameConvert str in NAMES)
            {
                if (str.from == from)
                {
                    nameStruct = str;
                    return true;
                }
            }
            nameStruct = new nameConvert();
            return false;
        }
    }
}

