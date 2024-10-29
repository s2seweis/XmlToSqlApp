using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace XmlToSqlApp
{
    public class XmlProcessor
    {

        string result;

        //Read items from xml and returns them in a list
        public List<Item> ReadXml(string filePath)
        {
            List<Item> items = new List<Item>();
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            string selectNodes = "/VFPData/" + result;
            XmlNodeList nodeList = doc.SelectNodes(selectNodes);

            foreach (XmlNode node in nodeList)
            {
                var hlp_id = node["hlp_id"]?.InnerText;
                var hlp_title = node["hlp_title"]?.InnerText;
                var hlp_text = node["hlp_text"]?.InnerText;
                var hlp_verweisid = node["hlp_verweisid"]?.InnerText;
                var hlp_forceupd = node["hlp_forceupd"]?.InnerText;

                items.Add(new Item
                {
                    Hlp_id = hlp_id,
                    Hlp_title = hlp_title,
                    Hlp_text = hlp_text,
                    Hlp_verweisid = hlp_verweisid,
                    Hlp_forceupd = hlp_forceupd


                });
            }

            return items;
        }



        //Reads, Modifies and Returns the Node Name
        public string CheckXMLNodeName(string item)
        {
            int lineToRead = 35;
            using (StreamReader sr = new StreamReader(item))
            {
                string line;
                int currentLine = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    currentLine++;
                    if (currentLine == lineToRead)
                    {
                        string pattern = @"[ <>]"; // Zeichen, die entfernt werden sollen
                        result = Regex.Replace(line, pattern, "");

                        return (result);
                    }
                }

            }


            return null;
        }



    }


    public class Item
    {
        public IEnumerable<char> Id;

        public string Hlp_id { get; set; }
        public string Hlp_title { get; set; }
        public string Hlp_text { get; set; }
        public string Hlp_verweisid { get; set; }
        public string Hlp_forceupd { get; set; }
    }
}
