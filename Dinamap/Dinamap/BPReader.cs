using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace Dinamap
{
    class BPReader
    {
        //Declaration of Dinawin DLL functions.
        [DllImport("DinaWin.dll")]
        internal static extern bool checkReadiness();

        [DllImport("DinaWin.dll")]
        internal static extern string getState();

        [DllImport("DinaWin.dll")]
        internal static extern int getBufferLength();

        [DllImport("DinaWin.dll")]
        internal static extern bool resetMonitor();


        public static bool Dina_CheckReadiness()
        {
            bool bRet = false;

            try
            {
                bRet = checkReadiness();
            }
            catch (System.DllNotFoundException ex)
            {
                MessageBox.Show("DinaWin.dll could not load.");
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);

            }

            //return true;
            return bRet;
        }

        public static XmlDocument Dina_GetStateOn()
        {
            try
            {
                XmlDocument xmlData = new XmlDocument();
                xmlData.LoadXml(getState());
                return xmlData;
            }
            catch (System.DllNotFoundException ex)
            {
                return new XmlDocument();
            }

        }
    }
}
