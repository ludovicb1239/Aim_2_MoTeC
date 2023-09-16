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
        /// <summary>
        /// Parses a text file containing name conversion mappings and returns an array of nameConvert structures.
        /// </summary>
        /// <param name="filePath">The path to the text file containing the mappings.</param>
        /// <returns>
        /// An array of nameConvert structures representing the parsed name conversion mappings.
        /// Returns null in case of an error during file reading or parsing.
        /// </returns>
        private static nameConvert[] ParseTextFile(string filePath)
        {
            List<nameConvert> mappings = new List<nameConvert>();

            try
            {
                // Read all lines from the specified file
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (line[0] != '#')
                    {
                        // Use regular expression pattern to parse the line into fields
                        Match match = Regex.Match(line, "\"([^\"]+)\", \"([^\"]+)\", \"([^\"]*)\"");

                        if (match.Success)
                        {
                            nameConvert c = new nameConvert(match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value);

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
        /// <summary>
        /// Checks if the provided input string matches any name conversion in the NAMES array.
        /// </summary>
        /// <param name="from">The input string to check for a matching conversion.</param>
        /// <param name="nameStruct">When a match is found, this parameter will contain the corresponding name conversion struct; otherwise, it will be initialized as an empty struct.</param>
        /// <returns>True if a matching conversion is found, otherwise false.</returns>
        public bool containsName(string from, out nameConvert nameStruct)
        {
            foreach (nameConvert str in NAMES)
            {
                if (str.from == from)
                {
                    // Set the nameStruct parameter to the matching conversion struct
                    nameStruct = str;
                    return true;
                }
            }

            // No matching conversion found, return false
            nameStruct = new nameConvert();
            return false;
        }
    }
}

