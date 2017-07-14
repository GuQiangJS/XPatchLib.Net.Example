using System;
using System.Text;
using XPatchLib;
using System.IO;

namespace XPatchLib.Example
{
    public class SimpleExample
    {
        public static void Main(string[] args)
        {
            Book OriBook = new Book() { ISBN = "0-385-50420-9", Name = "The Da Vinci Code", Author = new Author() { FirstName = "Dan", LastName = "Brown" } };

            Book RevBook = new Book() { ISBN = "0-385-50420-9", Name = "达芬奇密码", Author = new Author() { FirstName = "丹", LastName = "布朗" } };

            StringBuilder context = new StringBuilder();

            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.OmitXmlDeclaration = true;
            settings.ConformanceLevel = System.Xml.ConformanceLevel.Document;
            Serializer serializer = new Serializer(typeof(Book));

            using (StringWriter strWriter=new StringWriter(context))
            {
                using (XmlTextWriter writer = new XmlTextWriter(strWriter))
                {
                    serializer.Divide(writer, OriBook, RevBook);
                }
            }

            Book NewBook = null;

            using (System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(new StringReader(context.ToString())))
            {
                using (XmlTextReader reader = new XmlTextReader(xmlReader))
                {
                    NewBook = serializer.Combine(reader, OriBook, false) as Book;
                }
            }

            Console.WriteLine(NewBook);
        }
    }

    [PrimaryKey("ISBN")]
    public class Book
    {
        static string OUTPUT;

        static Book()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("ISBN:{0}");
            output.AppendLine("Name:{1}");
            output.AppendLine("Author:{2} {3}");
            OUTPUT = output.ToString();
        }

        public string ISBN { get; set; }

        public string Name { get; set; }

        public Author Author { get; set; }

        public override string ToString()
        {
            return string.Format(OUTPUT, this.ISBN, this.Name, this.Author.FirstName, this.Author.LastName);
        }
    }

    public class Author
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
