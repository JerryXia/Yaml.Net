#define ENABLE_COMPRESSION

using System;
using System.Collections;

using System.IO;

namespace Yaml.Net
{
    /// <summary>
    ///   Help class for writing a Yaml tree to a string
    /// </summary>
    public class WriteStream
    {
        private TextWriter stream;
        private int indentation = 0;
        private bool lastcharisnewline = false;

        private static string indentationChars = "    ";

        /// <summary> Constructor </summary>
        public WriteStream(TextWriter stream)
        {
            this.stream = stream;
        }

        /// <summary> Append a string </summary>
        public void Append(string s)
        {
            // Just add the text to the output stream when
            // there is no indentation
            if (indentation == 0)
                stream.Write(s);

            // Otherwise process each individual char
            else
                for (int i = 0; i < s.Length; i++)
                {
                    // Indent after a newline
                    if (lastcharisnewline)
                    {
                        WriteIndentation();
                        lastcharisnewline = false;
                    }

                    // Add char
                    stream.Write(s[i]);

                    // Remember newlines
                    if (s[i] == '\n')
                        lastcharisnewline = true;
                }
        }

        /// <summary> Indentation </summary>
        public void Indent()
        {
            // Increase indentation level
            indentation++;

            // Add a newline
#if ENABLE_COMPRESSION
			lastcharisnewline = false;
#else
            stream.Write("\n");
            lastcharisnewline = true;
#endif
        }

        /// <summary> Write the indentation to the output stream </summary>
        private void WriteIndentation()
        {
            for (int i = 0; i < indentation; i++)
                stream.Write(indentationChars);
        }

        /// <summary> Unindent </summary>
        public void UnIndent()
        {
            if (indentation > 0)
            {
                // Decrease indentation level
                indentation--;

                // Add a newline
                if (!lastcharisnewline)
                    stream.Write("\n");
                lastcharisnewline = true;
            }
            else
                throw new Exception("Cannot unindent a not indented writestream.");

        }
    }
}
