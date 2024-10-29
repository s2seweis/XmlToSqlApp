using System;
using System.IO;
using System.Text.RegularExpressions;



namespace XmlToSqlApp

{
    class Program
    {
        static void Main(string[] args)
        {
            // Adjusted path construction
            string xmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\XmlToDb");

            // Print the path for debugging
            Console.WriteLine($"Looking for XML file at: {xmlFilePath}");



            if (!Directory.Exists(xmlFilePath))
            {
                Console.WriteLine("XML file not found!");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return;
            }

            //Saves Data from List into DB
            try
            {
                foreach (string item in Directory.GetFiles(xmlFilePath))
                {
                    XmlProcessor processor = new XmlProcessor();
                    processor.CheckXMLNodeName(item);
                    var items = processor.ReadXml(item);

                    SqlRepository repository = new SqlRepository();
                    repository.SaveData(items);

                    Console.WriteLine("Data has been saved to the database.");
                }


                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.Read();
            }



        }

    }
}


