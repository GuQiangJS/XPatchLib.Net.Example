using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XPatchLib.Example
{
    public class Mode
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
             *   <PaidDate>2008-08-08T00:00:00+08:00</PaidDate>
             * </Invoice>
             */
            Console.WriteLine(DoSerializer(DateTimeSerializationMode.Local));

            //输出内容如下：
            /*
             * <?xml version="1.0" encoding="utf-16"?>
             * <Author>
             *   <PaidDate>2008-08-08T00:00:00</PaidDate>
             * </Author>
             */
            Console.WriteLine(DoSerializer(DateTimeSerializationMode.RoundtripKind));

            //输出内容如下：
            /*
             * <?xml version="1.0" encoding="utf-16"?>
             * <Author>
             *   <PaidDate>2008-08-08T00:00:00</PaidDate>
             * </Author>
             */
            Console.WriteLine(DoSerializer(DateTimeSerializationMode.Unspecified));

            //输出内容如下：
            /*
             * <?xml version="1.0" encoding="utf-16"?>
             * <Author>
             *   <PaidDate>2008-08-08T00:00:00Z</PaidDate>
             * </Author>
             */
            Console.WriteLine(DoSerializer(DateTimeSerializationMode.Utc));
        }

        static string DoSerializer(DateTimeSerializationMode mode)
        {
            using (Serializer serializer = new Serializer(typeof(Author)))
            {
                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                {
                    using (XmlTextWriter writer = new XmlTextWriter(sw))
                    {
                        writer.Setting.Mode = mode;
                        writer.Setting.Modifier = SerializeMemberModifier.Internal;
                        serializer.Divide(writer, null, new Invoice());
                    }
                }
                return sb.ToString();
            }
        }
    }
}
