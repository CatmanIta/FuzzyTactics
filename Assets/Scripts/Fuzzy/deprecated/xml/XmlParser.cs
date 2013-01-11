using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;

public class XmlParser {
	
	public void load(){
		StringBuilder output = new StringBuilder();

		string xmlString =
		        @"<?xml version='1.0'?>
		        <!-- This is a sample XML document -->
		        <Items>
		          <Item>test with a child element <more/> stuff</Item>
		        </Items>";
		
		// Create an XmlReader
		using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
		{
			
			// Parse the file and display each of the nodes.
	        while (reader.Read())
	        {
	            switch (reader.NodeType)
	            {
	                case XmlNodeType.Element:
	                    //writer.WriteStartElement(reader.Name);
						Debug.Log(reader.Name);
	                    break;
	                case XmlNodeType.Text:
	                    //writer.WriteString(reader.Value);
						Debug.Log(reader.Value);
	                    break;
	                case XmlNodeType.XmlDeclaration:
	                case XmlNodeType.ProcessingInstruction:
	                    //writer.WriteProcessingInstruction(reader.Name, reader.Value);
						Debug.Log(reader.Name + " " + reader.Value);
	                    break;
	                case XmlNodeType.Comment:
	                    //writer.WriteComment(reader.Value);
						Debug.Log(reader.Value);
	                    break;
	                case XmlNodeType.EndElement:
	                    //writer.WriteFullEndElement();
	                    break;
	            }
	        }
			
			
		    /* XmlWriterSettings ws = new XmlWriterSettings();
		    ws.Indent = true;
		    using (XmlWriter writer = XmlWriter.Create(output, ws))
		    {
		
		        
		
		    }*/
		}
		//OutputTextBlock.Text = output.ToString();
	}
}
