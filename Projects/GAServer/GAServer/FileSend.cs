using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GAServer
{
   
public final class FileSend
{
	private String fileName;
	private File resourceFile;
	private long LoF, pieceLength, netRead;
	private int readStatus, Count;
	private byte byt[];
	private DataInputStream fromConsole;
	private FileInputStream fromFile;	
			
	FileSend()
	{		
		try
		{
			fromConsole = new DataInputStream(System.in);
			while (true)
			{
				System.out.println("\n\nEnter Server resource file : ");
				fileName = fromConsole.readLine();
				resourceFile = new File(fileName);
				if (resourceFile.exists() && resourceFile.canRead())
				{ break; }
				else
				{ System.out.println("\n\nEnter a file that exists and is readable."); }
			}
			fromFile = new FileInputStream(resourceFile);			
		}
		catch (Exception e)
		{ System.out.println("Error in getting Server resource file - " + e); System.exit(0); }

		LoF = resourceFile.length();
		pieceLength = (long)(LoF / 7);
		Count = 1; netRead = 0;
		byt = new byte[512];		
	}
	
	public synchronized void sendFilePiece(OutputStream toClient)
	{
		try
		{
			toClient.write(Count);
			toClient.flush();
			do
			{
				readStatus = fromFile.read(byt);
				toClient.write(byt,0,readStatus);
				netRead += readStatus;	 
			}
			while(  (readStatus!=0)&&(netRead <= (Count*pieceLength)  )  );
			toClient.flush();
			Count++;
		}
		catch(Exception e)
		{ System.out.println("Error in sending file piece - " + e); return; }
	}

	public void finalize()
	{
		try
		{
			fromConsole.close();
			fromFile.close();			
		}
		catch (Exception e)
		{ System.out.println("Error while closing file and console streams in finalize method - " + e); return; }
	}

}
}
