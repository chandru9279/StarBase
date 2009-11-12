<%@ Control Language="C#" ClassName="Spelling" %>

<script runat="server">

</script>

<!--Spelling Bee by TheFreeDictionary.com-->
<div style="width:350px;position:relative;background-color:#FFF5F6;padding:4px">
<div style="font:bold 12pt '';color:#AE5259">Spelling Bee</div>
<style>
#TfdBee {border:1px #DDB7BA solid;background-color:#FFF5F6;padding:2px}
#TfdBee {font:normal 10pt '';color:#000000}
#TfdBee A {color:#000000}
#TfdBee .spell_word {font-size:110%;clear:all;margin-bottom:5px}
#TfdBee INPUT { font-size: 11pt; margin:0; padding:0;}
#tfd_bee_sound {float:left;width:40px;height:40px;margin-right:5px}
#tfd_bee_sound IMG {width:100%;height:100%;border:none}
#tfd_bee_def {font-size:80%}
#tfd_bee_score {float:right;margin:0 0 5px 2px}
.tfd_bee_correct {background-color: lightgreen}
.tfd_bee_wrong {background-color: pink}
.tfd_bee_na {background-color: white}
</style>
<div id="TfdBee"><a style="float:right" 
href="http://www.google.com/ig/add?moduleurl=http%3A//www.thefreedictionary.com/_/WoD/spellbee-module.xml"
target="_blank" title="Add to Google"><img alt="" border="0" src="http://img.tfd.com/m/add2g.gif"/></a>
<form name="tfd_bee_f" style="display:inline;margin:0">
<div id="tfd_bee_difficulty">
difficulty&nbsp;level: <span style="white-space:nowrap">
<input type="radio" name="level" value="1" id="tfd_bee_level1" 
onclick="tfd_level_click(this)"/><label for="tfd_bee_level1">easy</label>
<input type="radio" name="level" value="2" id="tfd_bee_level2" 
onclick="tfd_level_click(this)" checked="yes"/><label for="tfd_bee_level2">hard</label>
<input type="radio" name="level" value="3" id="tfd_bee_level3" 
onclick="tfd_level_click(this)"/><label for="tfd_bee_level3">expert</label></span>
</div><div id="tfd_bee_score">score: -</div>
<div id="tfd_bee_sound"></div>
<div id="tfd_bee_def"><b>please wait...</b></div>
<div id="tfd_bee_answ" style="font-size:110%">&nbsp;</div>
<div class="spell_word">spell the word:
<input type="text" class="tfd_bee_na" id="tfd_bee_uword" size="12"/></div>
<input type="button" onclick="tfd_bee_answer()" value="answer"/>
<input type="button" onclick="tfd_bee_new()" value="new word"/>
<script type="text/javascript" src="http://img.tfd.com/daily/spellbee-top.js"></script>
</form></div><div style="font:normal 8pt '';color:#000000">
<a style="color:#000000" href="http://www.thefreedictionary.com/lookup.htm#Spelling-Bee">Spelling Bee</a>
provided by <a style="color:#000000" href="http://www.thefreedictionary.com/">The Free Dictionary</a>
</div></div>
<!--end of Spelling Bee-->
<br/>