using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XPatchLib.Example
{
    public class Modifier
    {
        class Invoice
        {
            public Invoice()
            {
                Company = "C";
                Amount = 1.2m;
                Paid = true;
                PaidDate = new DateTime(2008, 8, 8);
            }

            public string Company { get; set; }
            protected decimal Amount { get; set; }

            private bool Paid { get; set; }

            internal DateTime PaidDate { get; set; }
        }

        public static void Main()
        {
            //输出内容如下：
            /*
             * <?xml version="1.0" encoding="utf-16"?>
             * <Invoice>
             *   <Company>C</Company>
             * </Invoice>
             */
            Console.WriteLine(DoSerializer(SerializeMemberModifier.Public));

            //输出内容如下：
            /*
             * <?xml version="1.0" encoding="utf-16"?>
             * <Author>
             *   <Paid>true</Paid>
             * </Author>
             */
            Console.WriteLine(DoSerializer(SerializeMemberModifier.Private));

            //输出内容如下：
            /*
             * <?xml version="1.0" encoding="utf-16"?>
             * <Author>
             *   <Amount>1.2</Amount>
             * </Author>
             */
            Console.WriteLine(DoSerializer(SerializeMemberModifier.Protected));

            //输出内容如下：
            /*
             * <?xml version="1.0" encoding="utf-16"?>
             * <Author>
             *   <PaidDate>2008-08-08T00:00:00</PaidDate>
             * </Author>
             */
            Console.WriteLine(DoSerializer(SerializeMemberModifier.Internal));

            //输出内容如下：
            /*
             * <?xml version="1.0" encoding="utf-16"?>
             * <Author>
             *   <Amount>1.2</Amount>
             *   <Paid>true</Paid>
             *   <PaidDate>2008-08-08T00:00:00</PaidDate>
             * </Author>
             */
            Console.WriteLine(DoSerializer(SerializeMemberModifier.NonPublic));

            //输出内容如下：
            /*
             * <?xml version="1.0" encoding="utf-16"?>
             * <Author>
             *   <Amount>1.2</Amount>
             *   <Company>C</Company>
             *   <Paid>true</Paid>
             *   <PaidDate>2008-08-08T00:00:00</PaidDate>
             * </Author>
             */
            Console.WriteLine(DoSerializer(SerializeMemberModifier.All));
        }

        static string DoSerializer(SerializeMemberModifier modifier)
        {
            using (Serializer serializer = new Serializer(typeof(Author)))
            {
                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                {
                    using (XmlTextWriter writer = new XmlTextWriter(sw))
                    {
                        writer.Setting.Modifier = modifier;
                        serializer.Divide(writer, null, new Invoice());
                    }
                }
                return sb.ToString();
            }
        }
    }
}
