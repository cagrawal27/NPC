/* 
 * File: site.js
 * Purpose: Javascript functions common to the entire site.
 * Author: Monish Nagisetty
 * Date: 03/22/2006
*/
<!--

function PopUp(filename){

	NewWindow(filename,'Help','600','500','yes');
	
}

function NewWindow(mypage, myname, w, h, scroll){
	
	LeftPosition = (screen.width) ? (screen.width-w)/2 : 0;
	
	TopPosition = (screen.height) ? (screen.height-h)/2 : 0;
	
	settings = 'height='+h+',width='+w+',top='+TopPosition+
	            ',left='+LeftPosition+',scrollbars='+scroll+',resizable';
	
	win = window.open(mypage, myname, settings)
}

// -->