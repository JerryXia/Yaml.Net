using System;

namespace Yaml.Net
{
    /// <summary>
    ///   Class for storing a Yaml Null node
    ///   tag:yaml.org,2002:null
    /// </summary>
    public class Null : Scalar
    {
        /// <summary> Null Constructor </summary>
        public Null() : base("tag:yaml.org,2002:null", NodeType.Null) { }

        /// <summary> Parse a null node </summary>
        public Null(ParseStream stream) :
            base("tag:yaml.org,2002:null", NodeType.Null)
        {
            // An empty string is a valid null node
            if (stream.EOF)
                return;

            else
            {
                // Read the first 4 chars
                char[] chars = new char[8];
                int length = 0;
                for (int i = 0; i < chars.Length && !stream.EOF; i++)
                {
                    chars[i] = stream.Char;
                    length++;
                    stream.Next();
                }

                // Compare
                if (length == 1)
                {
                    string s = "" + chars[0];

                    // Canonical notation
                    if (s == "~")
                        return;
                }
                if (length == 4)
                {
                    string s = "" + chars[0] + chars[1] + chars[2] + chars[3];

                    // null, Null, NULL
                    if (s == "NULL" || s == "Null" || s == "null")
                        return;
                }

                throw new ParseException(stream, "Not NULL");
            }
        }

        /// <summary> Content property </summary>
        public object Content
        {
            get { return null; }
        }

        /// <summary> To String </summary>
        public override string ToString()
        {
            return "[NULL]~[/NULL]";
        }

        /// <summary> Write to YAML </summary>
        protected internal override void Write(WriteStream stream)
        {
            stream.Append("~");
        }
    }
}
