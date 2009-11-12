<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GoogleInternetSearch.ascx.cs" Inherits="Thon.GoogleInternetSearchAscx" %>

<form action="http://www.google.co.in/cse" id="cse-search-box" target="_blank">
  <div style="margin-top:10px">
    <input type="hidden" name="cx" value="partner-pub-5538103424429375:1jjgtmnkufs" />
    <input type="hidden" name="ie" value="ISO-8859-1" />
    <label for="q">Search the Internet&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;</label>
    <input type="text" name="q" id="q" size="55" />&nbsp;&nbsp;&nbsp;
    <input type="submit" name="sa" id="sa" value="Search"/>
  </div>
</form>
<script type="text/javascript" src="http://www.google.com/coop/cse/brand?form=cse-search-box&amp;lang=en"></script>