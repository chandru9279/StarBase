<%@ Control Language="C#" ClassName="MatchUp" %>

<script runat="server">

</script>

<!--Match Up by TheFreeDictionary.com-->
<div style="width:350px;position:relative;background-color:#FFF5F6;padding:4px">
<div style="font:bold 12pt '';color:#AE5259">Match Up</div>
<style>
#MatchUp {width:100%;border:1px #DDB7BA solid;background-color:#FFF5F6}
#MatchUp TD {font:normal 10pt '';color:#000000}
#MatchUp A {color:#000000}
#tfd_MatchUp INPUT.tfd_txt {border:1px black solid;height:16pt;font-size:10pt;width:100px;cursor:pointer;margin-top:2px;margin-right:4px;text-align:center}
</style>
<form name="SynMatch" method="get" action="http://www.thefreedictionary.com/_/MatchUp.aspx" style="display:inline;margin:0" target="_top">
<table id="MatchUp">
<tr><td>
<a title="Add to Google" target="_blank"
href="http://www.google.com/ig/add?moduleurl=http%3A//www.thefreedictionary.com/_/WoD/matchup-module.xml"><img
style="width:11px;height:11px;float:right;border:none;width:11px" 
src="http://img.tfd.com/m/add2g.gif"/></a><script language="javascript"
src="http://img.tfd.com/daily/matchup.js?0"></script>
Match each word in the left column with its synonym on the right. When finished, click Answer to see the results. Good luck!<br/><br/><center>
<input type="button" value="Clear" onclick="tfd_mw_clear()"/>&nbsp;<input type="submit" value="Answer" onclick="this.form.res.value=tfd_mw_answers"/></center>
</td></tr></table></form>
<div style="font:normal 8pt '';color:#000000">
<a style="color:#000000" href="http://www.thefreedictionary.com/lookup.htm#Match-Up">Match Up</a>
provided by <a style="color:#000000" href="http://www.thefreedictionary.com/">The Free Dictionary</a>
</div></div>
<!--end of Match Up-->
<br/>