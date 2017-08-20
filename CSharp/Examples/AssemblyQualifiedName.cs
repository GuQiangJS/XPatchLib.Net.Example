using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;

namespace XPatchLib.Example
{
    public class AssemblyQualifiedName
    {
        public static void Main()
        {
            DefaultAssemblyQualifiedName();
            SetNewAssemblyQualifiedName();
        }

        static void DoDivide(ISerializeSetting setting)
        {
            dynamic d = new SampleDynamicObject();
            d.Text = "Text";

            StringBuilder result = new StringBuilder();
            Serializer serializer = new Serializer(d.GetType());
            using (StringWriter writer = new StringWriter(result))
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(writer))
                {
                    if (setting != null)
                        xmlWriter.Setting = setting;
                    serializer.Divide(xmlWriter, null, d);
                }
            }
            Console.WriteLine(result.ToString());
        }

        private static void SetNewAssemblyQualifiedName()
        {
            DoDivide(new XmlSerializeSetting() {AssemblyQualifiedName = "NewAssemblyAttr"});

            /*
            <?xml version="1.0" encoding="utf-16"?>
            <SampleDynamicObject>
              <Text NewAssemblyAttr="System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">Text</Text>
            </SampleDynamicObject> 
            */
        }

        private static void DefaultAssemblyQualifiedName()
        {
            DoDivide(new XmlSerializeSetting());

            /*
            <?xml version="1.0" encoding="utf-16"?>
            <SampleDynamicObject>
              <Text AssemblyQualified="System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">Text</Text>
            </SampleDynamicObject> 
            */
        }
    }

    public class SampleDynamicObject : DynamicObject
    {
        Dictionary<string, object> dictionary
            = new Dictionary<string, object>();
        public int Count
        {
            get
            {
                return dictionary.Count;
            }
        }
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return dictionary.Keys;
        }

        public override bool TryGetMember(
            GetMemberBinder binder, out object result)
        {
            string name = binder.Name;
            return dictionary.TryGetValue(name, out result);
        }
        public override bool TrySetMember(
            SetMemberBinder binder, object value)
        {
            dictionary[binder.Name] = value;
            return true;
        }
    }
}
