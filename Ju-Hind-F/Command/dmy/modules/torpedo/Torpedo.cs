using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json; // For JSON handling
using CsvHelper; // For CSV handling
using CsvHelper.Configuration;
using MongoDB.Bson; // For BSON handling
using System.Globalization;
using System.Text;

namespace cali_vm.Command.dmy.modules.torpedo
{
    public class Torpedo
    {
        public void Execute(List<string> commands)
        {
            string websiteUrl = string.Empty;
            List<string> extractTags = new List<string>();
            bool extractAll = false;

            // Command-line argument parsing
            for (int i = 0; i < commands.Count; i++)
            {
                switch (commands[i])
                {
                    case "-u":
                        websiteUrl = commands[i + 1];
                        i++;
                        break;

                    case "--extract":
                        while (i + 1 < commands.Count && !commands[i + 1].StartsWith("-"))
                        {
                            extractTags.Add(commands[i + 1]);
                            i++;
                        }
                        break;

                    case "--extract-all":
                        extractAll = true;
                        break;

                    default:
                        Console.WriteLine($"Unknown command: {commands[i]}");
                        break;
                }
            }

            if (string.IsNullOrEmpty(websiteUrl))
            {
                Console.WriteLine("Website URL not specified. Use the -u flag to specify.");
                return;
            }

            // Placeholder: This is where you would implement actual web scraping logic.
            var extractedData = new Dictionary<string, List<string>>();

            if (extractAll)
            {
                extractedData["all_tags"] = new List<string> { "<html>", "<body>", "</body>", "</html>" }; // Dummy data
            }
            else
            {
                // Extract based on the requested tags
                foreach (var tag in extractTags)
                {
                    extractedData[tag] = new List<string> { $"<{tag}>Some content</{tag}>" }; // Dummy data
                }
            }

            // Save data to various formats
            SaveToAllFormats(extractedData, websiteUrl);
        }

        private void SaveToAllFormats(Dictionary<string, List<string>> data, string websiteUrl)
        {
            string outputDirectory = $"./output_{websiteUrl.Replace(":", "").Replace("/", "")}";
            Directory.CreateDirectory(outputDirectory);

            // JSON
            SaveAsJson(data, Path.Combine(outputDirectory, "output.json"));

            // CSV
            SaveAsCsv(data, Path.Combine(outputDirectory, "output.csv"));

            // TOML
            SaveAsToml(data, Path.Combine(outputDirectory, "output.toml"));

            // BSON
            SaveAsBson(data, Path.Combine(outputDirectory, "output.bson"));

            Console.WriteLine($"Data saved in formats: JSON, CSV, TOML, BSON at {outputDirectory}");
        }

        private void SaveAsJson(Dictionary<string, List<string>> data, string filePath)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        private void SaveAsCsv(Dictionary<string, List<string>> data, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                foreach (var entry in data)
                {
                    csv.WriteField(entry.Key);
                    csv.WriteField(string.Join(",", entry.Value));
                    csv.NextRecord();
                }
            }
        }

        private void SaveAsToml(Dictionary<string, List<string>> data, string filePath)
        {
            var sb = new StringBuilder();

            foreach (var entry in data)
            {
                sb.AppendLine($"[{entry.Key}]");
                foreach (var value in entry.Value)
                {
                    sb.AppendLine($"value = \"{value}\"");
                }
                sb.AppendLine();
            }

            File.WriteAllText(filePath, sb.ToString());
        }

        private void SaveAsBson(Dictionary<string, List<string>> data, string filePath)
        {
            var bsonDocument = new BsonDocument();

            foreach (var entry in data)
            {
                bsonDocument.Add(entry.Key, new BsonArray(entry.Value));
            }

            File.WriteAllBytes(filePath, bsonDocument.ToBson());
        }
    }
}
