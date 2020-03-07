using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace BL
{
    public static class SettingsXmlProvider
    {
        private const string FILE_NAME = "config.xml";

        public static void WritePriceGame(Dictionary<int, double> dicPricePerMin)
        {
            XDocument xRoot = new XDocument();

            XElement mainElement = new XElement("PricesForGame");

            xRoot.Add(mainElement);

            foreach (KeyValuePair<int,double> item in dicPricePerMin)
            {
                XAttribute min = new XAttribute("Min", item.Key);
                XAttribute price = new XAttribute("Price", item.Value);
                XElement priceElement = new XElement("PricePerMin");

                priceElement.Add(min);
                priceElement.Add(price);

                mainElement.Add(priceElement);
            }

            xRoot.Save(FILE_NAME);
        }

        public static Dictionary<int, double> ReadPriceGame()
        {
            Dictionary<int, double> dicPricePerMin =
                new Dictionary<int, double>();

            XDocument xRoot = XDocument.Load(FILE_NAME);

            XElement mainElement = xRoot.Root;

            foreach (XElement element in mainElement.Elements())
            {
                string strMin = element.Attribute("Min").Value;
                string strPrice = element.Attribute("Price").Value;

                int nMin = 0;
                int nPrice = 0;

                if (int.TryParse(strMin, out nMin))
                {
                    if (int.TryParse(strPrice, out nPrice))
                    {
                        dicPricePerMin.Add(nMin, nPrice);
                    }
                }
            }

            return dicPricePerMin;
        }
    }
}
