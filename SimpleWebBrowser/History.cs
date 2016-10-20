using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FireDogeWebBrowser
{
    /// <summary>
    /// This class represents an element of the history
    /// </summary>
    public class HistoryEntity
    {
        /// <summary>
        /// This attribute is the name of the website
        /// </summary>
        public string name;

        /// <summary>
        /// This attribute is the URL of the website
        /// </summary>
        public string url;

        /// <summary>
        /// This is the constructor using all attributes
        /// </summary>
        public HistoryEntity(string websiteName, string websiteUrl)
        {
            this.name = websiteName;
            this.url = websiteUrl;
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        /// <remarks>
        /// DO NOT REMOVE. It is mandatory for the serialization. 
        /// </remarks>
        public HistoryEntity()
        {

        }

        /// <summary>
        /// This nested class represents the History.
        /// </summary>
        /// <remarks>
        /// It is a nested class, the choice of such class can by debatable. 
        /// We simply encapsulate a generic List beacause this class is <c>Serializable</c> and generics like <c>List</c> can't be serialized
        /// So we needed to have a convenient object that we can serialize
        /// </remarks>
        [Serializable]
        public class History
        {
            /// <summary>
            /// This is the reason why we need the Favourites class : We use a <c>List</c>
            /// which is not serializable 
            /// </summary>
            public List<HistoryEntity> history = new List<HistoryEntity>();

            /// <summary>
            /// Constructor used generally used to set the attributes
            /// </summary>
            /// <param name="websiteName">Is for the name of the website</param>
            /// <param name="websiteUrl">Is for the URL of the website</param>
            public History(string websiteName, string websiteUrl)
            {
                this.history.Add(new HistoryEntity(websiteName, websiteUrl));
            }

            /// <summary>
            /// Empty constructor
            /// </summary>
            /// <remarks>
            /// It is mandatory for the serialization
            /// </remarks>
            public History()
            {
                
            }

            /// <summary>
            /// This methods allows us to add an item to our custom list
            /// </summary>
            /// <remarks>
            /// As we encapsulated a List we need to define some methods to access items of that list
            /// </remarks>
            public void Add(string websiteName, string websiteUrl)
            {
                this.history.Add(new HistoryEntity(websiteName, websiteUrl));
            }

            /// <summary>
            /// This methods allows us to extract our items as a list of <c>HistoryEntity</c>
            /// </summary>
            /// <remarks>
            /// As we encapsulated a List we need to define some methods to access items of that list
            /// </remarks>
            public List<HistoryEntity> toList()
            {
                List<HistoryEntity> temp = new List<HistoryEntity>();
                foreach(var e in this.history)
                {
                    temp.Add(e);
                }
                return temp;
            }

            /// <summary>
            /// This methods serializes the object into a XML file
            /// </summary>
            /// <remarks>
            /// This is the main purpose of this class: being serialized
            /// </remarks>
            public void WriteXML()
            {
                System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(History));

                //We create the file if it does not exist
                var path = Application.StartupPath + "//SerializationHistory.xml";
                System.IO.FileStream file = System.IO.File.Create(path);

                writer.Serialize(file, this);
                file.Close();
            }

            /// <summary>
            /// This methods deserializes the object from a XML file
            /// </summary>
            /// <remarks>
            /// This is the main purpose of this class: being deserialized
            /// </remarks>
            public void ReadXML()
            {
                //First we make sure that a file exists by creating if needed
                string path = Application.StartupPath + "//SerializationHistory.xml";
                History temp = new History();
                if (File.Exists(path))
                {
                    // Now we can read the serialized object in the file.
                    System.Xml.Serialization.XmlSerializer reader =
                        new System.Xml.Serialization.XmlSerializer(typeof(History));

                    System.IO.StreamReader file = new System.IO.StreamReader(
                        Application.StartupPath + "//SerializationHistory.xml");

                    temp = (History)reader.Deserialize(file);
                    this.history = temp.history.FindAll(eval => eval.name.Equals(eval.name));
                    file.Close();
                }
            }
        }
    }
}
