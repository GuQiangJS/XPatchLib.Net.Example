using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XPatchLib.Example
{
    public class IgnoreAttributeType
    {
        public static void Main()
        {
            var c1 = new XmlIgnoreClass { A = "A", B = "B" };
            var c2 = new XmlIgnoreClass { A = "C", B = "D" };

            //输出内容如下：
            /*
             * <?xml version=""1.0"" encoding=""utf-16""?>
             * <XmlIgnoreClass>
             *   <B>D</B>
             *   </XmlIgnoreClass>
             */
            Console.WriteLine(UseDefaultIgnoreAttribute(c1, c2));

            //输出内容如下：
            /*
             * <?xml version=""1.0"" encoding=""utf-16""?>
             * <XmlIgnoreClass>
             *   <A>C</A>
             *   <B>D</B>
             *   </XmlIgnoreClass>
             */
            Console.WriteLine(SetIgnoreAttributeToNull(c1, c2));

            //输出内容如下：
            /*
             * <?xml version=""1.0"" encoding=""utf-16""?>
             * <XmlIgnoreClass>
             *   <A>C</A>
             *   </XmlIgnoreClass>
             */
            Console.WriteLine(SetIgnoreAttributeToNull(c1, c2));
        }

        public static string UseDefaultIgnoreAttribute(XmlIgnoreClass c1, XmlIgnoreClass c2)
        {
            StringBuilder result = new StringBuilder();
            Serializer serializer = new Serializer(typeof(XmlIgnoreClass));
            using (StringWriter writer = new StringWriter(result))
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(writer))
                {
                    //序列化前不变更默认的IgnoreAttributeType设置。
                    serializer.Divide(xmlWriter, c1, c2);
                }
            }
            return result.ToString();
        }

        public static string SetIgnoreAttributeToNull(XmlIgnoreClass c1,XmlIgnoreClass c2)
        {
            StringBuilder result = new StringBuilder();
            Serializer serializer = new Serializer(typeof(XmlIgnoreClass));
            using (StringWriter writer = new StringWriter(result))
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(writer))
                {
                    //在本次序列化过程中，类型定义中的XmlIgnoreAttribute无效。
                    xmlWriter.Setting.IgnoreAttributeType = null;
                    serializer.Divide(xmlWriter, c1, c2);
                }
            }
            return result.ToString();
        }

        public static string SetIgnoreAttributeToOther(XmlIgnoreClass c1, XmlIgnoreClass c2)
        {
            StringBuilder result = new StringBuilder();
            Serializer serializer = new Serializer(typeof(XmlIgnoreClass));
            using (StringWriter writer = new StringWriter(result))
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(writer))
                {
                    //在本次序列化过程中，使用 XPatchLibXmlIgnoreAttribute 作为忽略特性，而不是用默认的 XmlIgnoreAttribute
                    xmlWriter.Setting.IgnoreAttributeType = typeof(XPatchLibXmlIgnoreAttribute);
                    serializer.Divide(xmlWriter, c1, c2);
                }
            }
            return result.ToString();
        }
    }
    public class XmlIgnoreClass
    {
        [System.Xml.Serialization.XmlIgnore]
        public string A { get; set; }

        [XPatchLibXmlIgnore]
        public string B { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class XPatchLibXmlIgnoreAttribute : Attribute
    {

    }
}
