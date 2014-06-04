#define SUPPORT_NULL_NODES
#define SUPPORT_INTEGER_NODES
#define SUPPORT_FLOAT_NODES
#define SUPPORT_BOOLEAN_NODES
#define SUPPORT_TIMESTAMP_NODES

using System;

namespace Yaml.Net
{
    /// <summary>
    ///   All Yaml scalars are derived from this class
    /// </summary>
    public abstract class Scalar : Node
    {
        /// <summary> Constructor </summary>
        public Scalar(string uri, NodeType nodetype) : base(uri, nodetype) { }


        /// <summary>
        ///   Parses a scalar
        ///   <list type="bullet">
        ///     <item>Integer</item>
        ///     <item>String</item>
        ///     <item>Boolean</item>
        ///     <item>Null</item>
        ///     <item>Timestamp</item>
        ///     <item>Float</item>
        ///     <item>Binary</item>
        ///   </list>
        /// </summary>
        /// <remarks>
        ///   Binary is only parsed behind an explicit !!binary tag (in Node.cs)
        /// </remarks>
        public static new Scalar Parse(ParseStream stream)
        {
            // -----------------
            // Parse scalars
            // -----------------

            stream.BuildLookaheadBuffer();

            // Try Null
#if SUPPORT_NULL_NODES
			try
			{
				Scalar s = new Null (stream);
				stream.DestroyLookaheadBuffer ();
				return s;
			} catch { }
#endif
            // Try boolean
#if SUPPORT_BOOLEAN_NODES
			stream.RewindLookaheadBuffer ();
			try
			{
				Scalar scalar = new Boolean (stream);
				stream.DestroyLookaheadBuffer ();
				return scalar;
			} 
			catch { }
#endif
            // Try integer
#if SUPPORT_INTEGER_NODES
			stream.RewindLookaheadBuffer ();
			try
			{
				Scalar scalar = new Integer (stream);
				stream.DestroyLookaheadBuffer ();
				return scalar;
			} catch { }
#endif
            // Try Float
#if SUPPORT_FLOAT_NODES
			stream.RewindLookaheadBuffer ();
			try
			{
				Scalar scalar = new Float (stream);
				stream.DestroyLookaheadBuffer ();
				return scalar;
			} 
			catch { }
#endif
            // Try timestamp
#if SUPPORT_TIMESTAMP_NODES
			stream.RewindLookaheadBuffer ();
			try {
				Scalar scalar = new Timestamp (stream);
				stream.DestroyLookaheadBuffer ();
				return scalar;
			} catch { }
#endif
            // Other scalars are strings
            stream.RewindLookaheadBuffer();
            stream.DestroyLookaheadBuffer();

            return new String(stream);
        }

        /// <summary> Node info </summary>
        // TODO, move to each induvidual child
        public override Node Info()
        {
            Mapping mapping = new Mapping();
            mapping.AddMappingNode(new String("kind"), new String("scalar"));
            mapping.AddMappingNode(new String("type_id"), new String(URI));
            mapping.AddMappingNode(new String("value"), this);
            return mapping;
        }
    }
}
