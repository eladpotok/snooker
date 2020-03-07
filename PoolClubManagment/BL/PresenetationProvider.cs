using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.IO;
using System.Windows;

namespace BL
{
    /// <summary>
    /// Enum that represent the presentation objects in the system
    /// </summary>
    public enum PresentationObject
    {
        POOL_TABLE,
        BAR_CHAIR,
        BAR_TABLE
    }

    /// <summary>
    /// struct that represent the relevant details of each presentation object
    /// </summary>
    public struct ObjectDetails
    {
        public ObjectDetails(string strName, PresentationObject poType)
        {
            this.poType = poType;
            this.strName = strName;
        }

        public PresentationObject poType;
        public string strName;
    }

    /// <summary>
    /// struct that represent the relevant details of each presentation object
    /// </summary>
    public struct PresentationDetails
    {
        public PresentationDetails(int nLastID,
                                   Dictionary<Point, ObjectDetails> dicObject)
        {
            this.dicObject = dicObject;
            this.nLastElementID = nLastID;
        }

        public Dictionary<Point, ObjectDetails> dicObject;
        public int nLastElementID;
    }

    /// <summary>
    /// Enables saving and restoring different presentation settings
    /// </summary>
    public static class PresenetationProvider
    {
        private const string ROOT_ELEMENT = "PresentationObjects";
        private const string OBJECT_ELEMENT = "Object";
        private const string POINT_ATTRIBUTE = "Point";
        private const string NAME_ATTRIBUTE = "Name";
        private const string TYPE_ATTRIBUTE = "Type";
        private const string LAST_ID_ARRTRIBUTE = "LastID";


        /// <summary>
        /// Save the presentation object to file in specified path, create 
        /// new file in every call.
        /// </summary>
        /// <param name="strFilePath">The file full path</param>
        /// <param name="dicPresentationObjects">The presentation objects</param>
        public static void SavePresentation(string strFilePath,
                                            Dictionary<Point, ObjectDetails> dicPresentationObjects,
                                            int nLastElementID)
        {
            // Creating xml writer settings
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            try
            {
                // Creating the xml writer
                XmlWriter writer = XmlWriter.Create(strFilePath, settings);
                PointConverter converter = new PointConverter();

                writer.WriteStartDocument();

                // Write root element
                writer.WriteStartElement(ROOT_ELEMENT);
                writer.WriteAttributeString(LAST_ID_ARRTRIBUTE, nLastElementID.ToString());

                foreach (KeyValuePair<Point, ObjectDetails> currentPair in dicPresentationObjects)
                {
                    // append each object element
                    writer.WriteStartElement(OBJECT_ELEMENT);

                    writer.WriteAttributeString(POINT_ATTRIBUTE, converter.ConvertToString(currentPair.Key));
                    writer.WriteAttributeString(TYPE_ATTRIBUTE, currentPair.Value.poType.ToString());
                    writer.WriteAttributeString(NAME_ATTRIBUTE, currentPair.Value.strName);

                    // Close each object element
                    writer.WriteEndElement();
                }

                // Close root element
                writer.WriteEndElement();

                writer.Close();
            }
            catch (Exception ex)
            {
                // TODO : write to log
                throw new Exception("שגיאה בשמירת תצוגה אנא נסה שנית");
            }
        }

        /// <summary>
        /// Get the presentation objects from file
        /// </summary>
        /// <param name="strFilePath">The file path</param>
        /// <returns>Dictionary of location to object tyoe</returns>
        public static PresentationDetails ReadPresentation(string strFilePath)
        {
            Dictionary<Point, ObjectDetails> dicResult =
                new Dictionary<Point, ObjectDetails>();

            int nLastElementId = 0;

            PointConverter converter = new PointConverter();

            try
            {
                // Creating the xml reader
                XmlReader reader = XmlReader.Create(strFilePath);

                // As long as not end of file
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == ROOT_ELEMENT)
                    {
                        // Checking that the root element isn't empty element
                        if (!reader.IsEmptyElement)
                        {
                            // Get the element last ID 
                            nLastElementId =
                                int.Parse(reader.GetAttribute(LAST_ID_ARRTRIBUTE));

                            // As long we havn't got yet to the end element
                            while (reader.NodeType != XmlNodeType.EndElement)
                            {
                                reader.Read();

                                if (reader.Name == OBJECT_ELEMENT)
                                {
                                    // Get attributes values
                                    Point pCurrent =
                                        (Point)converter.ConvertFromString(reader.GetAttribute(POINT_ATTRIBUTE));

                                    PresentationObject currentObject =
                                        (PresentationObject)Enum.Parse(typeof(PresentationObject), reader.GetAttribute(TYPE_ATTRIBUTE));

                                    string strName =
                                        reader.GetAttribute(NAME_ATTRIBUTE);

                                    ObjectDetails details = new ObjectDetails();
                                    details.poType = currentObject;
                                    details.strName = strName;

                                    // Adding to dictionary
                                    dicResult.Add(pCurrent, details);
                                }
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                // TODO : write to log
                throw new Exception("קובץ התצוגה שקיים אינו קיים");
            }
            catch (NotSupportedException ex)
            {
                // TODO : write to log
                throw new Exception("קובץ תצוגה מושחת אנא טען קובץ אחר");
            }
            catch (ArgumentException ex)
            {
                // TODO : write to log
                throw new Exception("קובץ תצוגה מושחת אנא טען קובץ אחר");
            }
            catch (FormatException ex)
            {
                // TODO : write to log
                throw new Exception("קובץ תצוגה מושחת אנא טען קובץ אחר");
            }
            catch (Exception ex)
            {
                // TODO : write to log
                throw new Exception("שגיאה בקריאת נתונים מקובץ אנא נסה שוב");
            }

            return new PresentationDetails(nLastElementId, dicResult);
        }
    }
}
