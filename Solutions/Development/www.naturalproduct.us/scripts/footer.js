/*
 *   File:  footer.js
 *
 *   Insert a footer into the current document
 *
 */

// Insert document properties:
document.writeln( '<div class="foot">' );
document.writeln( document.title + '<br>' );
document.writeln( document.URL + '<br>' );
document.writeln( '©&nbsp;Monish Nagisetty<br>' );
document.write  ( '<em>Last update:</em>&nbsp;&nbsp;' );
document.writeln( document.lastModified );
document.writeln( '</div>' );
