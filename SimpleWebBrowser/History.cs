using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SimpleWebBrowser
{
    
    public class HistoryEntity
    {
        public string name;
        public string url;

        public HistoryEntity(string websiteName, string websiteUrl)
        {
            this.name = websiteName;
            this.url = websiteUrl;
        }

        public HistoryEntity()
        {

        }

        [Serializable]
        public class History
        {
            public List<HistoryEntity> history = new List<HistoryEntity>();

            public History(string websiteName, string websiteUrl)
            {
                this.history.Add(new HistoryEntity(websiteName, websiteUrl));
            }

            public History()
            {
                this.history = null;
            }

            public void Add(string websiteName, string websiteUrl)
            {
                this.history.Add(new HistoryEntity(websiteName, websiteUrl));
            }

            public List<HistoryEntity> toList()
            {
                List<HistoryEntity> temp = new List<HistoryEntity>();
                foreach(var e in this.history)
                {
                    temp.Add(e);
                }
                return temp;
            }

            public void WriteXML()
            {
                System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(History));

                var path = Application.StartupPath + "//SerializationHistory.xml";
                System.IO.FileStream file = System.IO.File.Create(path);

                writer.Serialize(file, this);
                file.Close();
            }

            public void ReadXML()
            {
                string path = Application.StartupPath + "//SerializationHistory.xml";
                History temp = new History();
                if (!File.Exists(path))
                {
                    temp.WriteXML();
                }

                // Now we can read the serialized book ...
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
