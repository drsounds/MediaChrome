using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace xmlfick
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load the xml file
            XmlDocument DT = new XmlDocument();
            DT.Load("C:\\testdocument.xml");

            // Get the inner text of the first element 
            var innerText = DT.GetElementsByTagName("element1")[0].InnerText;

            // Write the text
            Console.Write(innerText);

            /**
             * Now we want to get the position of the <attribute> tag so we can
             * parse it later
             * */

            XmlElement element1 = (XmlElement)DT.GetElementsByTagName("element1")[0];
            XmlText text = (XmlText)element1.GetElementsByTagName("element1")[0];

            Console.Write("Text data: " + text.Data);
            Console.ReadLine();
        }
    }
}
