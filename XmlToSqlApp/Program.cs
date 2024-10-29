using System;
using System.IO;
using System.Text.RegularExpressions;

namespace XmlToSqlApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Constructs the path to the XML directory by combining the current base directory with a relative path
            string xmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\XmlToDb");

            // Outputs the constructed path to the console for debugging purposes
            Console.WriteLine($"Looking for XML file at: {xmlFilePath}");

            // Checks if the specified directory exists
            if (!Directory.Exists(xmlFilePath))
            {
                // If directory doesn't exist, display an error message and exit
                Console.WriteLine("XML file not found!");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return; // Exits the Main method if the directory is not found
            }

            // Attempts to process and save data from each XML file in the directory to the database
            try
            {
                // Iterates over each file in the specified XML directory
                foreach (string item in Directory.GetFiles(xmlFilePath))
                {
                    // Creates an instance of XmlProcessor to handle XML processing
                    XmlProcessor processor = new XmlProcessor();

                    // Checks the XML node name for the current item
                    processor.CheckXMLNodeName(item);

                    // Reads the XML content and converts it into a list of items
                    var items = processor.ReadXml(item);

                    // Creates an instance of SqlRepository to manage database operations
                    SqlRepository repository = new SqlRepository();

                    // Saves the list of items to the database
                    repository.SaveData(items);

                    // Indicates that data has been successfully saved
                    Console.WriteLine("Data has been saved to the database.");
                }

                // Informs the user that the process is complete and waits for a key press to exit
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                // If an error occurs during processing, output the exception details
                Console.WriteLine(ex.ToString());
                Console.Read(); // Waits for user input before closing the console
            }
        }
    }
}
