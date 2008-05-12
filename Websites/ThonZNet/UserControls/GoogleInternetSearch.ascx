<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GoogleInternetSearch.ascx.cs" Inherits="Thon.GoogleInternetSearchAscx" %>
<h2>Google The Internet!</h2>
<br/>
<form action="http://www.google.com/cse" id="cse-search-box" target="_blank">
  <div>
    <input type="hidden" name="cx" value="partner-pub-5538103424429375:b8metc-z8hf" />
    <input type="text" name="q" size="20" />
    <input type="submit" name="sa" value="Search" />
  </div>
</form>
<script type="text/javascript" src="http://www.google.com/coop/cse/brand?form=cse-search-box&amp;lang=en"></script>