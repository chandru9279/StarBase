<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GoogleInternetSearch.ascx.cs" Inherits="Thon.GoogleInternetSearchAscx" %>
<style type="text/css">
@import url(http://www.google.com/cse/api/branding.css);
</style>
<div class="cse-branding-bottom" style="background-color:#FFFFFF;color:#000000">
  <div class="cse-branding-form">
    <form action="http://www.google.com/cse" id="Form1" target="_blank">
      <div>
        <input type="hidden" name="cx" value="partner-pub-5538103424429375:b8metc-z8hf" />
        <input type="hidden" name="ie" value="ISO-8859-1" />
        <input style="margin-top:2px;margin-bottom:2px;"
                type="text" name="q" size="20" />
        <input style="FONT-WEIGHT: bold; FONT-SIZE: larger; BORDER-LEFT-COLOR: maroon; BORDER-BOTTOM-COLOR: maroon; COLOR: maroon; BORDER-TOP-STYLE: double; BORDER-TOP-COLOR: maroon; BORDER-RIGHT-STYLE: double; BORDER-LEFT-STYLE: double; BACKGROUND-COLOR: cornsilk; BORDER-RIGHT-COLOR: maroon; BORDER-BOTTOM-STYLE: double"
                type="submit" name="sa" value="Search" />
      </div>
    </form>
  </div>
  <div class="cse-branding-logo">
    <img src="http://www.google.com/images/poweredby_transparent/poweredby_FFFFFF.gif" alt="Google" />
  </div>
  <div class="cse-branding-text">
    Custom Search
  </div>
</div>
