using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XPatchLib.Example
{
    public class MemberType
    {
        class Author
        {
            public string Name { get; set; }

            public int Age;
        }

        public static void Main()
        {
            Author c = new Author();
            c.Name = "N1";
            c.Age = 3;

            //输出内容如下：
            /*
             * <?xml version="1.0" encoding="utf-16"?>
             * <Author>
             *   <Name>N1</Name>
             * </Author>
             */
            Console.WriteLine(DoSerializer(c, SerializeMemberType.Property));

            //输出内容如下：
            /*
             * <?xml version="1.0" encoding="utf-16"?>
             * <Author>
             *   <Age>3</Age>
             * </Author>
             */
            Console.WriteLine(DoSerializer(c, SerializeMemberType.Field));

            //输出内容如下：
            /*
             * <?xml version="1.0" encoding="utf-16"?>
             * <Author>
             *   <Age>3</Age>
             *   <Name>N1</Name>
             * </Author>
             */
            Console.WriteLine(DoSerializer(c, SerializeMemberType.All));
        }

        static string DoSerializer(Author author, SerializeMemberType type)
        {
            using (Serializer serializer = new Serializer(typeof(Author)))
            {
                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                {
                    using (XmlTextWriter writer = new XmlTextWriter(sw))
                    {
                        writer.Setting.MemberType = type;
                        serializer.Divide(writer, null, author);
                    }
                }
                return sb.ToString();
            }
        }
    }
}
