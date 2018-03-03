var prefix = "mkstyle=";
var cookieStartIndex = document.cookie.indexOf(prefix);
var cookieEndIndex = document.cookie.indexOf(";", cookieStartIndex + prefix.length);
if (cookieEndIndex == -1)
cookieEndIndex = document.cookie.length;
var value=unescape(document.cookie.substring(cookieStartIndex + prefix.length, cookieEndIndex));
if ((value !='1') & (value!='2'))
document.write('<LINK REL=STYLESHEET TYPE=text/css HREF=style1.css>');
if (value=='1')
document.write('<LINK REL=STYLESHEET TYPE=text/css HREF=style1.css>');
if (value=='2')
document.write('<LINK REL=STYLESHEET TYPE=text/css HREF=style2.css>');