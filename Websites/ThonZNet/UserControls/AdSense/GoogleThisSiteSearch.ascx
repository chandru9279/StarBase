<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GoogleThisSiteSearch.ascx.cs" Inherits="Thon.GoogleThisSiteSearchAscx" %>
<style type="text/css">@import url(http://www.google.com/cse/api/branding.css);</style>
<br/><br/>
<h2>Google This Site!</h2>
<br/>
<div class="cse-branding-bottom" style="background-color:#FFFFFF;color:#000000">
  <div class="cse-branding-form">
    <form action="http://www.google.co.in/cse" id="cse-search-box" target="_blank">
      <div>
        <input type="hidden" name="cx" value="partner-pub-5538103424429375:inv28-ped3o" />
        <input type="text" name="q" size="22" />
        <input type="submit" name="sa" value="Search" />
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