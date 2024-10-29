using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace XmlToSqlApp
{
    public class XmlProcessor
    {
        // Variable to hold the result of node name processing
        string result;

        // Method to read items from an XML file and return them as a list
        public List<Item> ReadXml(string filePath)
        {
            List<Item> items = new List<Item>(); // List to hold the items
            XmlDocument doc = new XmlDocument(); // Create an XML document
            doc.Load(filePath); // Load the XML file

            // Construct the XPath query using the result variable
            string selectNodes = "/VFPData/" + result;
            XmlNodeList nodeList = doc.SelectNodes(selectNodes); // Select nodes from the XML document

            // Iterate through each selected node
            foreach (XmlNode node in nodeList)
            {
                // Retrieve values from the XML node, using null-conditional operator to avoid null reference exceptions
                var hlp_id = node["hlp_id"]?.InnerText;
                var hlp_title = node["hlp_title"]?.InnerText;
                var hlp_text = node["hlp_text"]?.InnerText;
                var hlp_verweisid = node["hlp_verweisid"]?.InnerText;
                var hlp_forceupd = node["hlp_forceupd"]?.InnerText;

                // Add the item to the list
                items.Add(new Item
                {
                    Hlp_id = hlp_id,
                    Hlp_title = hlp_title,
                    Hlp_text = hlp_text,
                    Hlp_verweisid = hlp_verweisid,
                    Hlp_forceupd = hlp_forceupd
                });
            }

            return items; // Return the list of items
        }

        // Method to read, modify, and return the node name from a specific line in an XML file
        public string CheckXMLNodeName(string item)
        {
            int lineToRead = 35; // Line number to read from the file
            using (StreamReader sr = new StreamReader(item)) // Open the file for reading
            {
                string line;
                int currentLine = 0;

                // Read the file line by line
                while ((line = sr.ReadLine()) != null)
                {
                    currentLine++;
                    if (currentLine == lineToRead) // Check if the current line is the one to read
                    {
                        string pattern = @"[ <>]"; // Pattern to remove unwanted characters
                        result = Regex.Replace(line, pattern, ""); // Remove unwanted characters from the line

                        return (result); // Return the modified result
                    }
                }
            }

            return null; // Return null if the line was not found
        }
    }

    // Class representing an item with properties corresponding to XML fields
    public class Item
    {
        // This property appears unused; it may be a placeholder
        public IEnumerable<char> Id;

        // Properties to hold data from XML
        public string Hlp_id { get; set; }
        public string Hlp_title { get; set; }
        public string Hlp_text { get; set; }
        public string Hlp_verweisid { get; set; }
        public string Hlp_forceupd { get; set; }
    }
}
