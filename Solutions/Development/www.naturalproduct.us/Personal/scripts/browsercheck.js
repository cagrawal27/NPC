function browsercheck() {
//
// Browser Detection
//
isMac = (navigator.appVersion.indexOf("Mac")!=-1) ? true : false;
NS4 = (document.layers) ? true : false;
IEmac = ((document.all)&&(isMac)) ? true : false;
IE4plus = (document.all) ? true : false;
IE4 = ((document.all)&&(navigator.appVersion.indexOf("MSIE 4.")!=-1)) ? true : false;
IE5 = ((document.all)&&(navigator.appVersion.indexOf("MSIE 5.")!=-1)) ? true : false;
IE6 = ((document.all)&&(navigator.appVersion.indexOf("MSIE 6.")!=-1)) ? true : false;
ver4 = (NS4 || IE4plus) ? true : false;
NS6 = (!document.layers) && (navigator.userAgent.indexOf('Netscape')!=-1)?true:false;

IE5plus = IE5 || IE6;
IEMajor = 0;

if (IE4plus)
{
	var start = navigator.appVersion.indexOf("MSIE");
	var end = navigator.appVersion.indexOf(".",start);
	IEMajor = parseInt(navigator.appVersion.substring(start+5,end));
	IE5plus = (IEMajor>=5) ? true : false;
}

//if it's a netscape, redirect them to this page.
if ( (NS4) ) {
	window.location.replace('/BrowserDetection.html');
}

/*
if (isMac)
	document.write("<p>You are using a Macintosh</p>");
if (NS4)
	document.write("<p>You are using Netscape 4 or later</p>");
if (IEmac)
	document.write("<p>You are using Internet Explorer on Macintosh</p>");
if (IE4plus )
	document.write("<p>You are using a Internet Explorer version 4.0 or later</p>");
if (IE5plus )
	document.write("<p>You are using a Internet Explorer version 5.0 or later</p>");
if (IE4)
	document.write("<p>You are using Internet Explorer 4</p>");
if (IE5)
	document.write("<p>You are using Internet Explorer 5</p>");
if (IE6)
	document.write("<p>You are using Internet Explorer 6</p>");
if (ver4)
	document.write("<p>You are using a version 4.0 or later browser</p>");
if (NS6)
	document.write("<p>You are using Netscape 6 or later</p>");
if (IEMajor>0)
	document.write("<p>The IE major version component is "  + IEMajor + "</p>");
*/
}

browsercheck()  
  
  