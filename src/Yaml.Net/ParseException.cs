using System;

namespace Yaml.Net
{
    /// <summary>
    ///   ParseException, could be thrown while parsing a YAML stream
    /// </summary>
    public class ParseException : Exception
    {
        // Line of the Yaml stream/file where the fault occures
        private readonly int linenr;

        /// <summary> Constructor </summary>
        /// <param name="stream"> The parse stream (contains the line number where it went wrong) </param>
        /// <param name="message"> Info about the exception </param>
        public ParseException(ParseStream stream, string message) :
            base("Parse error near line " + stream.CurrentLine + ": " + message)
        {
            this.linenr = stream.CurrentLine;
        }

        /// <summary> Constructor </summary>
        /// <param name="stream"> The parse stream (contains the line number where it went wrong) </param>
        /// <param name="child"> The exception that is for example throwed again </param>
        public ParseException(ParseStream stream, Exception child) :
            base("Parse error near line " + stream.CurrentLine, child)
        {
            this.linenr = stream.CurrentLine;
        }

        /// <summary> The line where the error occured </summary>
        public int LineNumber
        {
            get { return linenr; }
        }
    }
}
