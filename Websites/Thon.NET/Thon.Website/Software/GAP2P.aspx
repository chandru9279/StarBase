<%@ Page Title="" Language="C#" MasterPageFile="~/Software/SoftMaster.master" AutoEventWireup="true" CodeFile="GAP2P.aspx.cs" Inherits="Thon.Warez.GAP2PAspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphsoftware" Runat="Server">
    
    
    
    
     <table cellpadding="5" cellspacing="5" style="font-family:Verdana,Tahoma;font-size:small;">
        <tr>
            <td>
            <center><h1>GAP2P</h1></center>
            </td>
        </tr>
    <tr><td>
    <center><h2> Computer Networking &amp; Workload Distribution</h2></center><br />
    
    <h3 style="margin-top:10px;margin-bottom:10px;">problem being solved</h3>
    <p>
	In any Local or Wide Area Network users (especially administrators) always find a need to send a file or folder to all or a subset a computers in the network. These are slow because the resource in question usually is from a removable media (a setup/installation file) or has just been obtained from the internet (maybe updates) and therefore is present in only one of the systems.<br />
	You need the file (possibly huge) in all systems hard drives and want it done quick.<br />
    Sharing won’t do here because typically the number of computers involved are more than hundred while an OS in the computer that contains a resource that we have shared can’t allow more than 5 simultaneous accessors. Which means the goal of getting the file onto all the participating systems hard drives takes a long time and uses lot of network traffic and the upload bandwidth of the computer containing the necessary resource may not be high (what if you have the resource on a computer that isn’t a server) in which case the situation gets considerably worse.<br />
    </p>
    
    <h3 style="margin-top:10px;margin-bottom:10px;">technology and usage issues causing the problem</h3>
    <p>
	    Typical networking hardware have independent upload and download bandwidths. Meaning we can send and receive data simultaneously across shared media (Ethernet). For achieving the given goal if we can make use of both these bandwidths and speed up the process we must definitely do so. If we have the hardware we ought to try and keep it busy always (mantra for anything computer related). OS technology in typical systems can’t handle bulk requests without rejecting some or total slowdown - motivates the problem.<br />
	    A usage issue has been discussed above another one is : Say we got the service pack 2 for windows XP and all the two hundred and forty systems in our company/college network has XP SP1 and needs it. That’ll take physical work using temporary medias or a long time to get the downloaded file to all systems via networks (measures ~300 mb). Happens everytime an update is out for any software or you want a new software. Or we want the Visual Web Developer Express Edition on all systems in a lab – there’s only one CD belonging to the seminar – conductor who wants everyone to work on it. He definitely can’t wait till the 700 MB goes to all the 40 systems in the lab and the students install it. A large number of situations can be given all of them concerning resource replication across big networks – which happens surprisingly frequently in all institutions.<br />
    </p>
    <h3 style="margin-top:10px;margin-bottom:10px;">research  hypothesis</h3>
    <p>
	    A node in a system of computers that have a common resource requirement can upload whatever part of the resource it has to other computers in the system even if it has not got the entire resource itself. This policy if followed by all systems will distribute network load over all the connections available instead of concentrating load on client-server connection in addition to improving the performance with respect to time.<br />
	    The answer to why we need to distribute the load is obvious considering the fact that concentrating network traffic on specific connections lead to congestion and slowdown.<br />
    </p>
    
    <h3 style="margin-top:10px;margin-bottom:10px;">how do we verify the hypothesis</h3>
    <p>
	    A practical experiment will convince the average user. Let a program send a 4 GB file out to all 10 systems sequentially. Let another program divide up the file into 10 400 MB parts and send each part to each system sequentially , letting the client systems to exchange the parts among themselves. In both cases we have the resource on all the client systems, but with a time reduction of upto 80% when using the second case. How this reduction comes and why the number 80% are theory – looked at in detail in the following sections.<br />
    </p>

    <h3 style="margin-top:10px;margin-bottom:10px;">impact on industry</h3>
     <p>
	    If the logic behind the hypothesis is feasible we might have extra chips on the standard network card where the concepts of workload distribution are implemented on a CHIP-PROTOCOL pair. Or the software implementation can find itself present in the kernel DLL routines in all commercial and server operating systems. The former is cheaper and faster but the fact remains that only those computers on big networks find use for the hardware. Home or Small Office networks can do without the hardware since number of computers involved are usually in the 5 or 10 range. <br />
	    Still the functionality of the hardware can be made use of in the VPNs over internet where workload distribution can mean all the difference. Also the difference in the cost of network cards before and after using the logic is fairly negligible which allows the manufacturers to implement it as a service to end users , or to gain an edge over competition.<br />
    </p>

    <h3 style="margin-top:10px;margin-bottom:10px;">research on the proposal</h3>
    <p>
	Implications of this hypothesis on grid and cluster computing is fairly straightforward. Getting data files to individual processing elements is as important as the actual processing itself. A collective of usage issues and situations where the hypothesis improves performance has been organized. Practical File Transfer utilities have been tested against this, all of which facing all the same constraints and environmental restrictions the implementation of this concept faces. Meaning the available utilities and the implementation were tested in the same place in the same hardware and interconnects.
    </p>
    
    </td></tr>
    
    <tr><td>
    <center>1st 7 steps of a sample case</center>
    <p><a href="Images/ga1.jpg" target="_blank"><img alt="GAP2P1" src="Images/ga1.JPG" /></a></p>
    </td></tr>
    
    <tr>
    <td>
    <p>The document explaining in detail the <a href="Downloads/GAP2P/One.doc" target="_blank">concept</a>. </p>
    <p>The <a href="Downloads/GAP2P/Two.doc" target="_blank">code</a> copy pasted into a MS Word Doc</p>
    </td>
    </tr>
    
    <tr>
    <td>
    <p><a href="Images/ga2.jpg" target="_blank"><img src="Images/Mini/gap2pcode.jpg" width="700" height="363" /></a></p>
    </td>
    </tr>

    <tr>
    <td>
    <p>Team project : T Chandirasekar, <a href="http://www.intrepidkarthi.net/"> N.G. Karthikeyan</a></p>
    </td>
    </tr>

    <tr><td>
    <center>Last 7 steps of a sample case, total:14 steps</center>
    <p><a href="Images/ga2.jpg" target="_blank"><img alt="GAP2P2" src="Images/ga2.JPG" /></a></p>
    <p>This is only a sample orchestration case (for comprehension), not a formal specification.</p>
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

