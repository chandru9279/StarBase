<%@ Page Title="" Language="C#" MasterPageFile="~/Software/SoftMaster.master" AutoEventWireup="true" CodeFile="Shelf.aspx.cs" Inherits="Thon.Warez.ShelfAspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphsoftware" Runat="Server">
    <table cellpadding="5" cellspacing="5" style="font-family:Verdana,Tahoma;font-size:small;">
        <tr>
            <td>
            <center><h1>Shelf.Net</h1></center>
            </td>
        </tr>
    <tr><td>
    <p>Shelf.Net is Google Desktop like application that indexes various kinds of files.
    It is web-based however, and is desingned to be part of an e-Library. The library
    will hold a compendium of documents, which ought to help the upcomin developer.
    
    The special feature of this library, is it is practically the <b>first and only</b>
    C# based digital library. It can also serve as a file repository.
    
    </p>
    <ol>
    <li>Stemming, Stopping, GoWords </li>
    <li>Wordnet based Synonymic searching</li>
    <li>XML Rule based reasoning</li>
    <li>Tentative Rule extraction engine</li>
    <li>"isa" "partof" OWL (Web Ontology Lang) ruleset comprehension</li>
    </ol>
    </td></tr>
    
    <tr><td>
    <center>The Search Page</center>
    <img src="Images/search.jpg" alt="search" />
    </td></tr>
    
    <tr>
    <td>
    <p>A simple check box allows one to use semantics, other wise it'll
    be a standard keyword based searching.</p>
    </td>
    </tr>
        
    <tr><td>
    <center>The Results Page</center>
    <img src="Images/results1.jpg" alt="results" />
    <p>Note the extremely long browser and 2 scrollbars - some image manip :-)</p>
    </td></tr>
    
    <tr><td>
    <center>
    <a href="javascript:alert('Please wait, download will be available within a few days');"><img alt="download" style="border:none;border-width:0px;" src="../Images/Download.gif" /></a>
    <br />Download source
    </center>
    </td></tr>
    
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphsidebar" Runat="Server">
    
</asp:Content>

