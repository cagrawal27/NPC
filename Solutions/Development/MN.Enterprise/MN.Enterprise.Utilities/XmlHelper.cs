using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Xsl;

namespace MN.Enterprise.Utilities
{
	/// <summary>
	/// XmlNoPrefixTextWriter is a internal private class used to prevent output of 'xml' root tag.
	/// </summary>
	/// <author>Monish Nagisetty</author>
	/// <modified_date>12/27/2006</modified_date>
	internal class XmlNoPrefixTextWriter : XmlTextWriter
	{
		/// <summary>
		/// Default instance constructor.
		/// </summary>
		/// <param name="textwriter">TextWriter handling Xml data.</param>
		public XmlNoPrefixTextWriter(TextWriter textwriter) : base(textwriter)
		{
		}

		/// <summary>
		/// Overridden method used to prevent 'xml' root tag
		/// </summary>
		public override void WriteStartDocument()
		{
		}
	}

	/// <summary>
	/// XmlHelper is a public utility class providing the implementation of common Xml operations.
	/// </summary>
	public class XmlHelper
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public XmlHelper()
		{
		}

		/// <summary>
		/// Serializes the given object to Xml.  Note this method does not output
		/// the root 'xml' tag, and does not add the default namespaces to the
		/// root document tag.  The output is formatted.
		/// </summary>
		/// <param name="o">Object to be serialized.</param>
		/// <returns>Xml string representing the object.</returns>
		public static string Serialize(object o)
		{
			//create empty namespace to prevent namespace output
			XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
			ns.Add("", "");

			//create serializer
			XmlSerializer ser = new XmlSerializer(o.GetType());
			
			using(StringWriter writer = new StringWriter())
			{
				//use custom writer to prevent root 'xml' tag output
				XmlNoPrefixTextWriter xwriter = new XmlNoPrefixTextWriter(writer);
				xwriter.Formatting = Formatting.None;

				ser.Serialize(xwriter, o, ns);
				return writer.ToString();
			}
		}

		/// <summary>
		/// Creates an object from the given Xml.
		/// </summary>
		/// <param name="xml">Xml representing of the object.</param>
		/// <param name="type">System.Type value for class created.</param>
		/// <returns>The object that was deserialized given the Xml string.</returns>
		public static object DeserializeXml(string xml, System.Type type)
		{
			//create serializer
			XmlSerializer serializer = new XmlSerializer(type);
			
			using(StringReader sr = new StringReader(xml))
			{
				//create return object
				object desO = serializer.Deserialize(sr);
				return desO;
			}
		}

		/// <summary>
		/// Transforms the given Xml using the given Xslt file.
		/// </summary>
		/// <param name="xml">Xml to be parsed.</param>
		/// <param name="xsltPath">File path to Xslt.</param>
		/// <returns>String representing transformed Xml.</returns>
		public static string TransformXml(string xml, string xsltPath)
		{
			//load xml
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			// Transform the file.
			XslCompiledTransform xslt = new XslCompiledTransform();
			xslt.Load(xsltPath);

            StringWriter sw = new StringWriter();
			XmlTextWriter writer = new XmlTextWriter(sw);
			xslt.Transform(doc, null, writer);

			return sw.ToString();
		}

		/// <summary>
		/// Encodes the given string so that it is value text for an Xml document.
		/// </summary>
		/// <param name="text">String to be encoded.</param>
		/// <returns>Encoded string.</returns>
		public static string Encode(string text)
		{
			return text.Replace("&", "&amp;").Replace("'", "&apos;").Replace("\"", "&quot;").Replace("<","&lt;").Replace(">","&gt;");
		}

		/// <summary>
		/// Decodes the given string from an encoded Xml string.
		/// </summary>
		/// <param name="text">String to be decoded.</param>
		/// <returns>Decoded string.</returns>
		public static string Decode(string text)
		{
			return text.Replace("&amp;", "&").Replace("&apos;", "'").Replace("&quot;", "\"").Replace("&lt;","<").Replace("&gt;",">");
		}
	}
}
