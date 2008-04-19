using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace GAServer
{

public class SpawnServer
{
	static InetAddress[] IPArray = new InetAddress[7];
	static int IAPos = 0, clientListensTo;
	private ServingThread[] ServerThreads = new ServingThread[7];
	private int[] PortArray;
	private FileSend fileSendObject;
	private DataInputStream console;
	private Integer intObject;
	private String threadName;

	public SpawnServer( int Client_Listens_To,
                        int Server_Port_Array
                       )
	{
		PortArray = new int[7];
		fileSendObject = new FileSend();
		clientListensTo = Client_Listens_To;
		for (int var = 0; var < 7; var++)
			PortArray[var] = Server_Port_Array+var;
	}

	public void startSpawning()
	{
		for (int var = 0; var < 7; var++)
		{
			threadName = new String("Thread Number " + var.toString() + "  ");
			ServerThreads[var] = new ServingThread(PortArray[var], IPArray, threadName ,fileSendObject);
			ServerThreads[var].start();
		}

		while (true)
		{
			if (IPArray[6] != null)
			{
				for (int var = 0; var < 7; var++)
					//synchronized (ServerThreads[var]) { ServerThreads[var].notify(); }
				break;
			}
		}
		try
		{
			for (int var = 0; var < 7; var++)
				ServerThreads[var].join();
		}
		catch (InterruptedException e)
		{ System.out.println("Main thread : Main Thread Interrupted!"); }
	}

	public static void main(String[] arguments)
	{ new SpawnServer().startSpawning(); }
} 
}
