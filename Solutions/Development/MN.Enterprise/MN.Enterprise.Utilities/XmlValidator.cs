using System;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace MN.Enterprise.Utilities
{
	/// <summary>
	/// XmlValidator validates a given Xml string using the given schema.
	/// </summary>
    /// <author>Monish</author>
    // Revision History
    //
    //=============================================================================
    // Change   Initial Date   Description
    //=============================================================================
	public class XmlValidator
	{
		/// <summary>
		/// Holds message indicating outcome of operation.
		/// </summary>
		private StringBuilder _ValidationMessage;

		/// <summary>
		/// Maintains collection of added schemas.
		/// </summary>
		private	XmlSchemaSet _cache;

		/// <summary>
		/// Used to indicate if validatorcallback was called (indicates validation failed)
		/// </summary>
		private bool _IsValid;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public XmlValidator()
		{
			_IsValid = true;
		}

		/// <summary>
		/// Adds an additional schema to be used for validation.
		/// </summary>
		/// <param name="nameSpace">The namespace Uri associated with the schema.</param>
		/// <param name="schemafile">The schema file to be added to the XmlSchemaCollection.</param>
		public void AddSchema(string nameSpace, string schemafile)
		{
            if (_cache == null)
                _cache = new XmlSchemaSet();

			_cache.Add(nameSpace, schemafile);
		}

		/// <summary>
		/// Gets the message indicating outcome of XmlHelper operation that was called.
		/// </summary>
		public string ValidationMessage
		{
			get{return _ValidationMessage.ToString();}
		}

		/// <summary>
		/// Validates the given Xml string using the specified schema file.
		/// </summary>
		/// <param name="xmlData">Xml string.</param>
		/// <param name="schemaFile">Path to schema file.</param>
		/// <returns>A boolean value representing the success or the failure of the validation.</returns>
		public bool ValidateDocument(string xmlData, string schemaFile)
		{
			//clear current message
			_ValidationMessage = new StringBuilder();

			//load schema into collection
			XmlSchemaSet cache = new XmlSchemaSet();
			cache.Add(null, schemaFile);

            // load the xml data into a reader
			XmlTextReader txtreader = new XmlTextReader(new StringReader(xmlData));

            // create the settings object and set the event handler
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = cache;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidateDocumentCallBack);

            // create the reader and read the xml (which does the validation as
            // it is reading)
            XmlReader reader = XmlReader.Create(txtreader, settings);
			while(reader.Read()){}
			reader.Close();

            // return whether is was valid or not
			return _IsValid;
		}

		/// <summary>
		/// Valdates the given Xml using the assigned schemas.
		/// </summary>
		/// <param name="xmlData">Xml data to load.</param>
		/// <returns>A boolean value representing the success or the failure of the validation.</returns>
		public bool ValidateDocument(string xmlData)
		{
			//clear current message
			_ValidationMessage = new StringBuilder();

			//load into the text reader
			XmlTextReader txtreader = new XmlTextReader(new StringReader(xmlData));

            // create the settings object and set the event handler
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = _cache;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidateDocumentCallBack);

            // create the reader and read the xml (which does the validation as
            // it is reading)
            XmlReader reader = XmlReader.Create(txtreader, settings);
            while (reader.Read()) { }
            reader.Close();

            // return whether is was valid or not
			return _IsValid;
		}

		/// <summary>
		/// Callback method used to handle validation.
		/// </summary>
		/// <param name="sender">The calling object.</param>
		/// <param name="args">Detailed information related to the ValidationEventHandler.</param>
		private void ValidateDocumentCallBack (object sender, ValidationEventArgs args)
		{
			// weset flag
			_IsValid = false;

			//add error to document
			_ValidationMessage.Append(args.Message);
			_ValidationMessage.Append(Environment.NewLine);
		} 
	}
}

