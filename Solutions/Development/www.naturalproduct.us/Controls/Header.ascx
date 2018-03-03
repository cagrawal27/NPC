<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="Header" %>
<link type="text/css" rel="stylesheet" href="../styles/layout.css" />

<table id="Header" width="760" height="80" background="../images/1px_line.gif" >
	<tr>
		<td width="150"><div id=NPC align="left" Valign="top"><img src="../images/npc.gif" useMap=#atlas border=0 width="81" height="80"></div>
			<MAP name=atlas>
				<AREA title="NPC Inc." shape=RECT target=_top coords=0,0,80,80 href="http://www.naturalproduct.us" target=_parent>
			</MAP>
		</td>
		<td width="610"><div align="right" Valign="center"><font face="Arial, Times, Verdana " color="#000080" size="+3">Natural Product Communications</font></div></td>
	</tr>
</table>
<div id="PageHeader">
    <span id="PageSubHeaderLeft"><img src="../images/npc.gif" alt="NPC Banner" /></span>
    <span id="PageSubHeaderRight">Natural Product Communications</span>
</div>