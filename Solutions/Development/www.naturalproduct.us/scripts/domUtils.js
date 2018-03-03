var isNav4, isNav6, isIE4;
var status = "closed";

/*
 *
 * Given an id and a property (as strings), return
 * the given property of that id.  Navigator 6 will
 * first look for the property in a tag; if not found,
 * it will look through the stylesheet.
 *
 * Note: do not precede the id with a # -- it will be
 * appended when searching the stylesheets
 *
 */
function getIdProperty( id, property )
{

	return document.all[id].style[property];

}

/*
 *
 * Given an id and a property (as strings), set
 * the given property of that id to the value provided.
 *
 * The property is set directly on the tag, not in the
 * stylesheet.
 *
 */
function setIdProperty( id, property, value )
{

	document.all[id].style[property] = value;

}




function fieldValidate()
{
	var objCurElem;
	
	for (var i = 0; i < document.forms[0].elements.length; i++) 
	{
	    objCurElem = document.forms[0].elements[i]
		    
	    if (objCurElem.type == "text" || objCurElem.type == "textarea")
	    {
			objCurElem.value = TrimField(objCurElem);
				
			if (objCurElem.value.length == 0)
			{
				window.alert("Missing field(s).  All fields are required!");
				objCurElem.focus();
				return false;	
			}
		}
		else if (objCurElem.type=="select-one" && objCurElem.selectedIndex == 0)
		{	
			window.alert("Please select a value from the dropdown field.")
			objCurElem.focus();
			return false;
		}
	}
	formatFields();
	return true;
	
}

function formatFields()
{
	for (var i = 0; i < document.forms[0].elements.length; i++) 
	{
	    objCurElem = document.forms[0].elements[i]
		    
	    if (objCurElem.type == "textarea" || objCurElem.type == "text")
	    {
			objCurElem.value = TrimField(objCurElem);
				
			if (objCurElem.value.length != 0)
			{
				escapeVal(objCurElem,"<br>"); 	
			}
		    
		}
	}

}



//Added by MN on 4/15/03
//Referenced from http://www.scriptbreaker.com/examples/remove_spaces.asp
function TrimField(curFld)
{
	strFldVal = curFld.value;

	while (strFldVal.charAt(strFldVal.length-1) == " " || strFldVal.charAt(strFldVal.length-1) == "\r"
			|| strFldVal.charAt(strFldVal.length-1) == "\n")
	{
		strFldVal = strFldVal.substring(0,strFldVal.length-1);
	} 
  		
	while(strFldVal.substring(0,1) == " " || strFldVal.substring(0,1) == "\r" || strFldVal.substring(0,1) == "\n")
	{	
		strFldVal = strFldVal.substring(1,strFldVal.length);

	}


	return strFldVal;

}

/*
Function used for the hidden menu.
*/
function showMenu( divNum )
{
    if (getIdProperty( "s" + divNum, "display") != "block" )
    {
        setIdProperty("s" + divNum, "display", "block");
        document.images["i" + divNum].src = "./images/minus.jpg";
    }
    else
    {
        setIdProperty("s" + divNum, "display", "none");
        document.images["i" + divNum].src = "./images/plus.jpg";
    }
}

/*
Function used for the hidden Navigation tree.
*/
function showTree( divNum )
{
    if (getIdProperty( "s" + divNum, "display") != "block" )
    {
        setIdProperty("s" + divNum, "display", "block");
        document.images["i" + divNum].src = "./minus.jpg";        
    }
    else
    {
        setIdProperty("s" + divNum, "display", "none");
        document.images["i" + divNum].src = "./plus.jpg";        
    }
}

function activateAllMenus()
{
	var iCt = 0;
	var strTmp = new String();
	var tmpStatus = new String();
	
	strTemp = "s" + iCt;

	while (document.getElementById(strTemp))
	{
		if (status == "closed")
		{
			if (getIdProperty("s" + iCt, "display") != "block" )
			{
				setIdProperty("s" + iCt, "display", "block");
				document.images["i" + iCt].src = "./images/minus.jpg";
			}
			tmpStatus = "open";
		}
		else
		{
			if (getIdProperty("s" + iCt, "display") == "block" )
			{
				setIdProperty("s" + iCt, "display", "none");
				document.images["i" + iCt].src = "./images/plus.jpg";
			}
			tmpStatus = "closed";		
		}
		
		iCt += 1;
		strTemp = "s" + iCt;
	}
	
	status = tmpStatus;
	if (status == "closed")
	{
		document.images["imgActivate1"].src = "./images/fold.gif";
		document.images["imgActivate2"].src = "./images/fold.gif";

	}
	else
	{
		document.images["imgActivate1"].src = "./images/open.gif";
		document.images["imgActivate2"].src = "./images/open.gif";
	}	
}

function checkAll(objStatus)
{
	for (var i = 0; i < document.forms[0].elements.length; i++) 
	{
        if (document.forms[0].elements[i].type == "checkbox")
        {
			document.forms[0].elements[i].checked = objStatus;
		}
    }

}

function checkDelBoxes()
{
	var retVal;

	for (var i = 0; i < document.forms[0].elements.length; i++) 
	{
        	if (document.forms[0].elements[i].type == "checkbox")
			if (document.forms[0].elements[i].checked == true)
			{
			        retVal = confirm("Are you sure you want to delete the selected item(s)?");
				
				if (retVal)
					return gatherCheckedInfo();
				else
					return false;
			}
	}
	
	window.alert("Please choose an item to delete.");
	return false;

}

function gatherCheckedInfo()
{
	var strChkRslts = "";
	
	for (var i = 0; i < document.forms[0].elements.length; i++) 
	{
        	if (document.forms[0].elements[i].type == "checkbox")
			if (document.forms[0].elements[i].checked == true)
				strChkRslts += document.forms[0].elements[i].value + ","; 
    	}

	strChkRslts = strChkRslts.substring(0,strChkRslts.length-1);
	document.forms[0].chkRslts.value = strChkRslts;
	return true;	
}


function newCalendar(formNum, frmElem, bolTime)
{
	var cal = new calendar2(document.forms[formNum].elements[frmElem]);
	cal.year_scroll = true;
	cal.time_comp = bolTime;
	cal.popup();
}

function RedirectTo( strUrl )
{
	window.location.href = strUrl;
}

function escapeVal(textarea,replaceWith) //textarea is reference to that object, replaceWith is string that will replace the encoded return
{
	textarea.value = escape(textarea.value) //encode textarea string's carriage returns

	for(i=0; i<textarea.value.length; i++) //loop through string, replacing carriage return encoding with HTML break tag
	{
	 	if(textarea.value.indexOf("%0D%0A") > -1) //Windows encodes returns as \r\n hex
		{
			textarea.value=textarea.value.replace("%0D%0A",replaceWith)
		}
	}
	
	textarea.value=unescape(textarea.value) //unescape all other encoded characters
}
