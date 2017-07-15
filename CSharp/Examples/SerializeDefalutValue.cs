using System;
using System.IO;
using System.Text;

namespace XPatchLib.Example
{
    public class SerializeDefalutValue
    {
        static readonly CreditCard card = new CreditCard
        {
            CardNumber = "0123456789",
            CardExpiration = "05/17",
            CardCode = 0
        };

        public static void Main()
        {
            /*
             * 输出内容：
             * <?xml version=""1.0"" encoding=""utf-8""?>
             * <CreditCard>
             *   <CardExpiration>05/17</CardExpiration>
             *   <CardNumber>0123456789</CardNumber>
             * </CreditCard>
             */
            Console.WriteLine(DoSerialize());
            /*
             * 输出内容：
             * <?xml version=""1.0"" encoding=""utf-8""?>
             * <CreditCard>
             *   <CardCode>0</CardCode>
             *   <CardExpiration>05/17</CardExpiration>
             *   <CardNumber>0123456789</CardNumber>
             * </CreditCard>
             */
            Console.WriteLine(DoSerialize(true));
        }

        static string DoSerialize(bool serializeDefaultValue=false)
        {
            StringBuilder result = new StringBuilder();
            Serializer serializer = new Serializer(typeof(string[]));
            using (StringWriter writer = new StringWriter(result))
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(writer))
                {
                    xmlWriter.Setting.SerializeDefalutValue = serializeDefaultValue;
                    serializer.Divide(xmlWriter, null, card);
                }
            }
            return result.ToString();
        }
    }


    public class CreditCard
    {
        public int CardCode { get; set; }

        public string CardExpiration { get; set; }

        public string CardNumber { get; set; }
    }
}
