using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XPatchLib.Example
{
    public class PrimaryKeyAttribute
    {
        public static void Main(string[] args)
        {
            var list1 = new List<MulitPrimaryKeyClass>();
            var list2 = new List<MulitPrimaryKeyClass>();

            list1.Add(new MulitPrimaryKeyClass { Name = "Name1", Id = 1 });
            list1.Add(new MulitPrimaryKeyClass { Name = "Name2", Id = 2 });
            list1.Add(new MulitPrimaryKeyClass { Name = "Name3", Id = 3 });
            list1.Add(new MulitPrimaryKeyClass { Name = "Name4", Id = 4 });

            list2.Add(new MulitPrimaryKeyClass { Name = "Name1", Id = 1 });
            list2.Add(new MulitPrimaryKeyClass { Name = "Name2", Id = 2 });
            list2.Add(new MulitPrimaryKeyClass { Name = "Name5", Id = 5 });

            StringBuilder context = new StringBuilder();
            using (StringWriter strWriter = new StringWriter(context))
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(strWriter))
                {
                    Serializer serializer=new Serializer(typeof(List<MulitPrimaryKeyClass>));
                    serializer.Divide(xmlWriter, list1, list2);
                }
            }

            Console.WriteLine(context.ToString());
        }
    }


    [XPatchLib.PrimaryKey("Name", "Id")]
    public class MulitPrimaryKeyClass
    {
        public override bool Equals(object obj)
        {
            var c = obj as MulitPrimaryKeyClass;
            if (c == null)
                return false;
            return Id.Equals(c.Id)
                   && Name.Equals(c.Name);
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
