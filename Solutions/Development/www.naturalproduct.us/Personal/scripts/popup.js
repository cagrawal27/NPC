	function openLanding( pageToLoad, winName, width, height, center) {
			 
    xposition=0; yposition=0;
    if ((parseInt(navigator.appVersion) >= 4 ) && (center)){
        xposition = (screen.width - width) / 2;
        yposition = (screen.height - height) / 2;
    }
    args = "width=" + width + "," 
    + "height=" + height + "," 
    + "location=0," 
    + "menubar=0,"
    + "resizable=0,"
    + "scrollbars=1,"
    + "status=0," 
    + "titlebar=0,"
    + "toolbar=0,"
    + "hotkeys=0,"
    + "screenx=" + xposition + ","  //NN Only
    + "screeny=" + yposition + ","  //NN Only
    + "left=" + xposition + ","     //IE Only
    + "top=" + yposition;           //IE Only

    window.open( pageToLoad,winName,args );

}

//--------------------------------

	function openAWindow( pageToLoad, winName, width, height, center) {
			 
    xposition=0; yposition=0;
    if ((parseInt(navigator.appVersion) >= 4 ) && (center)){
        xposition = (screen.width - width) / 2;
        yposition = (screen.height - height) / 2;
    }
    args = "width=" + width + "," 
    + "height=" + height + "," 
    + "location=0," 
    + "menubar=0,"
    + "resizable=0,"
    + "scrollbars=1,"
    + "status=0," 
    + "titlebar=0,"
    + "toolbar=0,"
    + "hotkeys=0,"
    + "screenx=" + xposition + ","  //NN Only
    + "screeny=" + yposition + ","  //NN Only
    + "left=" + xposition + ","     //IE Only
    + "top=" + yposition;           //IE Only

    window.open( pageToLoad,winName,args );
	
}
	
	function openAWindow2( pageToLoad, winName, width, height, center) {
			 
    xposition=0; yposition=0;
    if ((parseInt(navigator.appVersion) >= 4 ) && (center)){
        xposition = (screen.width - width) / 2;
        yposition = (screen.height - height) / 2;
    }
    args = "width=" + width + "," 
    + "height=" + height + "," 
    + "location=0," 
    + "menubar=0,"
    + "resizable=0,"
    + "scrollbars=0,"
    + "status=0," 
    + "titlebar=0,"
    + "toolbar=0,"
    + "hotkeys=0,"
    + "screenx=" + xposition + ","  //NN Only
    + "screeny=" + yposition + ","  //NN Only
    + "left=" + xposition + ","     //IE Only
    + "top=" + yposition;           //IE Only

    window.open( pageToLoad,winName,args );

}

	function openRRPWindow(pageToLoad) {
		xposition=0; yposition=0;
		width=600; height=280;
		center=1;
		if ((parseInt(navigator.appVersion) >= 4 ) && (center)){
			xposition = (screen.width - width) / 2;
			yposition = (screen.height - height) / 2;
		}
		args = "width=" + width + "," 
		+ "height=" + height + "," 
		+ "location=0," 
		+ "menubar=0,"
		+ "resizable=0,"
		+ "scrollbars=0,"
		+ "status=0," 
		+ "titlebar=0,"
		+ "toolbar=0,"
		+ "hotkeys=0,"
		+ "screenx=" + xposition + ","  //NN Only
		+ "screeny=" + yposition + ","  //NN Only
		+ "left=" + xposition + ","     //IE Only
		+ "top=" + yposition;           //IE Only

		window.open( pageToLoad,"RRP",args );

	}
