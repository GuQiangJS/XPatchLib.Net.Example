using System;
using System.IO;
using System.Text;

namespace XPatchLib.Example
{
    public class ActionName
    {
        static readonly string[] StrArray = { "ABC", "DEF" };

        public static void Main()
        {
            DefaultAction();
            SetNewAction();
        }

        private static void DefaultAction()
        {
            StringBuilder result = new StringBuilder();
            Serializer serializer = new Serializer(typeof(string[]));
            using (StringWriter writer = new StringWriter(result))
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(writer))
                {
                    serializer.Divide(xmlWriter, null, StrArray);
                }
            }
            //输出内容: <Array1OfString Action="SetNull" />
            Console.WriteLine(result.ToString());
        }

        private static void SetNewAction()
        {
            StringBuilder result = new StringBuilder();
            Serializer serializer = new Serializer(typeof(string[]));
            using (StringWriter writer = new StringWriter(result))
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(writer))
                {
                    xmlWriter.Setting.ActionName = "NewActionName";
                    serializer.Divide(xmlWriter, null, StrArray);
                }
            }
            //输出内容: <Array1OfString NewActionName="SetNull" />
            Console.WriteLine(result.ToString());
        }
    }
}
