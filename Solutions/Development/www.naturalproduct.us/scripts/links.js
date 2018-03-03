// File links.js version of 9/17/03
// Entirely written by Monish Nagisetty unless noted otherwise
// References used:
// XML DOM Reference - http://msdn.microsoft.com/library/default.asp?url=/library/en-us/xmlsdk/htm/sdk_intro_6g53.asp

function enumerateLinks() {
	var strXmlFile = new String();
	strXmlFile = "../links.xml";

	var objXmlDoc, objXmlRoot, strAnchor, strAnchorText;
	var iLen = 0;
	var q = new String();
	q = "\"";
	
	objXmlDoc = new ActiveXObject("Microsoft.XMLDOM");
	objXmlDoc.async = false;
	objXmlDoc.load(strXmlFile);

	objXmlRoot = objXmlDoc.documentElement;
	if (objXmlRoot != null) {    
		while (iLen != objXmlRoot.childNodes.length) {

            //Get Anchor HREF		
		    strAnchor = objXmlRoot.childNodes.item(iLen).getAttribute("href");

            //Get Anchor Text
		    strAnchorText = objXmlRoot.childNodes.item(iLen).childNodes.item(0).text;
		    
		    //If processing the current document
		    if (strAnchor == document.title)
        	    document.writeln ("<li id=active><a href=\"" + strAnchor + "\">" + strAnchorText + "</a></li>");
		    else //Any other document
        	    document.writeln ("<li><a href=\"" + strAnchor + "\">" + strAnchorText + "</a></li>");
		    
			++iLen;
		}

	}
}

//finally the call to the function
enumerateLinks();
