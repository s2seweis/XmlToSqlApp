using System;
using System.Collections.Generic;
using System.Xml;

namespace XmlToSqlApp
{
    public class XmlProcessor
    {
        public List<Item> ReadXml(string filePath)
        {
            List<Item> items = new List<Item>();
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNodeList nodeList = doc.SelectNodes("/Items/Item");

            foreach (XmlNode node in nodeList)
            {
                var name = node["Name"]?.InnerText;
                var value = node["Value"]?.InnerText;

                items.Add(new Item
                {
                    Name = name,
                    Value = value
                });
            }

            return items;
        }
    }

    public class Item
    {
        public IEnumerable<char> Id;

        public string Name { get; set; }
        public string Value { get; set; }
    }
}
