using System;
using System.IO;

namespace XmlToSqlApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Adjusted path construction
            string xmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\XmlToDb", "sample.xml");

            // Print the path for debugging
            Console.WriteLine($"Looking for XML file at: {xmlFilePath}");

            if (!File.Exists(xmlFilePath))
            {
                Console.WriteLine("XML file not found!");
                // Prevent the window from closing
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return;
            }

            XmlProcessor processor = new XmlProcessor();
            var items = processor.ReadXml(xmlFilePath);

            SqlRepository repository = new SqlRepository();
            repository.SaveData(items);

            Console.WriteLine("Data has been saved to the database.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
