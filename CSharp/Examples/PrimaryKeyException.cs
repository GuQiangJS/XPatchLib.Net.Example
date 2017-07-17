﻿using System;

namespace XPatchLib.Example
{
    public class ThrowPrimaryKeyException
    {
        public static void Main(string[] args)
        {
            try
            {
                new Serializer(typeof(ErrorPrimaryKeyDefineClass));
            }
            catch (PrimaryKeyException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.PrimaryKeyName);
                Console.WriteLine(ex.ErrorType);
            }
        }

        [XPatchLib.PrimaryKey("Author")]
        public class ErrorPrimaryKeyDefineClass
        {
            public AuthorClass Author { get; set; }
        }

        public class AuthorClass
        {
            public string FirstName { get; set; }
            public string SecondName { get; set; }
        }
    }
}