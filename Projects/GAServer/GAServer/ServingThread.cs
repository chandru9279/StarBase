using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GAServer
{
    
public class ServingThread : Thread 
{
	private Socket soc;
	private ServerSocket server;
	private int port;
	private OutputStream toClient;
	private ObjectOutputStream arrayToClient;
	private InetAddress thisThreadConnectsTo, IA[];
	private FileSend syncSendFile;
	
	public ServingThread(int port, InetAddress[] IA, String name, FileSend syncSendFile)
	{
		this.port = port;		
		setName(name);
		this.IA = IA;
		this.syncSendFile = syncSendFile;
	}
	
	public void run() 
	{
		try
		{
			System.out.println(getName() + "Listening to Port " + port + ".....\n");
			server = new ServerSocket(port);
			soc = server.accept();
			IA[SpawnServer.IAPos++] = thisThreadConnectsTo = soc.getInetAddress();
			System.out.println(getName() + "Connected to : " + soc);
			soc.close();
		}
		catch (Exception e)
		{
			System.out.println(getName() + "Error in creating server socket or accepting connection - \n" + e);
			try { soc.close(); }catch (IOException e1) { }
			try { server.close(); }catch (IOException e1) { }		
			return;
		}		
		
		try { synchronized (this) { this.wait(); } }
		catch (InterruptedException e) { System.out.println(getName() + "Error in waiting - " + e); return;}		

		try
		{
			soc = new Socket(thisThreadConnectsTo, SpawnServer.clientListensTo);
			toClient = soc.getOutputStream();
			arrayToClient = new ObjectOutputStream(toClient);
			for (int var = 0; var < 7; var++)
				arrayToClient.writeObject(IA[var]);
			arrayToClient.flush();
			soc.close();
			System.out.println(getName() + "Sent the IP Array.");
		}
		catch (IOException e)
		{
			System.out.println(getName() + "Error in sending the IP Array - \n" + e);
			try { soc.close(); }
			catch (IOException e1) { } 
			return;
		}		

		try
		{
			soc = new Socket(thisThreadConnectsTo, SpawnServer.clientListensTo);
			toClient = soc.getOutputStream();
		}
		catch (Exception e)
		{ 
			System.out.println(getName() + "Error in creating socket to send file. - \n" + e);
			try { soc.close(); }
			catch (IOException e1) { } 
			return;
		}
		

		System.out.println(getName() + "Sending File.............\n");
		syncSendFile.sendFilePiece(toClient);

		try { toClient.close(); }
		catch (Exception e) { }

		System.out.println(getName() + "File Piece sent.\nThread terminating.\n");
		return;
	}

	public void finalize()
	{
		try
		{
			toClient.close();
			arrayToClient.close();
		}
		catch (IOException e)
		{ System.out.println("IOException in finalize - " + e); }
		finally { try { soc.close(); } catch (IOException e) { } }		
	}
	
}


}
