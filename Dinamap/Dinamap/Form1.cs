using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Collections;
using System.Xml.Serialization;
using System.Xml.XPath;


namespace Dinamap
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void btnReadXml_Click(object sender, EventArgs e)
        {

            var doc = XDocument.Load("C:\\dinamap.xml");

            var results = from result in doc.Descendants("Result")
                          select new
                          {
                              Name = (string)result.Attribute("name"),
                              Value = (string)result.Element("Value"),
                              Units = (string)result.Element("Units").Attribute("name")
                          };

            foreach (var result in results)
            {
                MessageBox.Show("{result.Name}: {result.Value} {result.Units}");

            }
         
        }

        private Hashtable responseToHash(XmlDocument doc)
        {
            Hashtable h = new Hashtable();

            XmlNodeList doc_results = doc.GetElementsByTagName("Results");

            foreach (XmlNode pnode in doc_results)
            {
                foreach (XmlNode cnode in pnode.ChildNodes)
                {
                    if (cnode.Name == "Units")
                    {
                        h.Add(pnode.Attributes["name"].InnerText + "_" + cnode.Name, cnode.Attributes["name"].InnerText);
                    }
                    else if (cnode.Name == "Value")
                    {
                        h.Add(pnode.Attributes["name"].InnerText + cnode.Name, cnode.Attributes["name"].InnerText);
                    }
                    else if (cnode.Name == "Time_stamp")
                    {
                        DateTime d = new DateTime(
                            Convert.ToInt32(cnode.Attributes["year"].InnerText),
                            Convert.ToInt32(cnode.Attributes["month"].InnerText),
                            Convert.ToInt32(cnode.Attributes["day"].InnerText),
                            Convert.ToInt32(cnode.Attributes["hour"].InnerText),
                            Convert.ToInt32(cnode.Attributes["minute"].InnerText),
                            Convert.ToInt32(cnode.Attributes["second"].InnerText));

                        h.Add(pnode.Attributes["name"].InnerText + "_" + cnode.Name, d);
                    }
                    else
                        h.Add(pnode.Attributes["name"].InnerText + "_" + cnode.Name, cnode.InnerText);
                }
            }

            return h;
        }

    }

    }

