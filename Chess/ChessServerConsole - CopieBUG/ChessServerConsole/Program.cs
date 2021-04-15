using ChessServerConsole.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChessServerConsole
{
  class Program
  {



    public static void StartSocketServer()
    {
      // Get Host IP Address that is used to establish a connection  
      // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
      // If a host has multiple addresses, you will get a list of addresses  
      IPHostEntry host = Dns.GetHostEntry("localhost");
      IPAddress ipAddress = host.AddressList[0];
      IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);


      try
      {

        // Create a Socket that will use Tcp protocol      
        Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        // A Socket must be associated with an endpoint using the Bind method  
        listener.Bind(localEndPoint);
        // Specify how many requests a Socket can listen before it gives Server busy response.  
        // We will listen 10 requests at a time  
        listener.Listen(10);

        Console.WriteLine("Waiting for a connection...");
        Socket handler = listener.Accept();

        // Incoming data from the client.    
        string data = null;
        byte[] bytes = null;

        while (true)
        {
          bytes = new byte[1024];
          int bytesRec = handler.Receive(bytes);
          data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
          if (data.IndexOf("<EOF>") > -1)
          {
            break;
          }
        }

        Console.WriteLine("Text received : {0}", data);
        data = data.Replace("<EOF>", "");
        
       var enterStringList = data.Split('.').ToList();
        var colore = enterStringList[0];
        var level = enterStringList[1];
       enterStringList.RemoveAt(0);
       enterStringList.RemoveAt(0);
        var bestMove = TreatMessage(enterStringList, colore, level);






        byte[] msg = Encoding.ASCII.GetBytes(bestMove);
        handler.Send(msg);
        handler.Shutdown(SocketShutdown.Both);
        handler.Close();
        listener.Close();
      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
      }

     /* Console.WriteLine("\n Press any key to continue...");
      Console.ReadKey();*/
    }


    static string TreatMessage(List<string> enterStringList,string colore,string stringLevel)
    {
      var level = int.Parse(stringLevel);
      var server = new Server();
      server.GeneratePawnList(enterStringList);
      var pawnList = server.CurrentPawnList;

      foreach (var pawn in pawnList)
      {
        Console.WriteLine($"{pawn.Name} {pawn.Location} {pawn.Colore} {pawn.PossibleTrips.Count}");
      }

      //Generaion de l'arbre
      Console.WriteLine("Tree is generated");
      var startTime = DateTime.Now;



      server.GenereTree(colore, level);







      var bestNode = server.GetBestNodePostion();
      //MoveTo(bestNode.Location, bestNode.BestChildPosition);
      Console.WriteLine($"Best position : {bestNode.AssociatePawn.Name} {bestNode.Location} to {bestNode.BestChildPosition}");


      var executionTime = DateTime.Now - startTime;
      Console.WriteLine($"Execution time = {executionTime}");
      Console.WriteLine("Finish");
      return bestNode.Location + ";" + bestNode.BestChildPosition;

    }
    static void Main(string[] args)
    {
      Console.WriteLine("Chess server is run...");


     /* while(true)
        StartSocketServer();
      Console.ReadLine ();

      */

      
      var enterStringList = new List<string>();

      /*enterStringList.Add("Rook;a1;White;False;False;False;False");
      enterStringList.Add("SimplePawn;a2;White;True;False;False;False");
      enterStringList.Add("Knight;b1;White;False;False;False;False");
      enterStringList.Add("SimplePawn;b2;White;True;False;False;False");
      enterStringList.Add("Bishop;c1;White;False;False;False;False");
      enterStringList.Add("SimplePawn;c2;White;True;False;False;False");*/
      enterStringList.Add("Queen;e1;White;False;False;False;False");
      /*enterStringList.Add("SimplePawn;d2;White;True;False;False;False");
      enterStringList.Add("King;e1;White;False;True;True;True");
      enterStringList.Add("SimplePawn;e2;White;False;False;False;False");
      enterStringList.Add("Bishop;f1;White;False;False;False;False");
      enterStringList.Add("SimplePawn;f2;White;True;False;False;False");
      enterStringList.Add("Knight;g1;White;False;False;False;False");
      enterStringList.Add("SimplePawn;g2;White;True;False;False;False");
      enterStringList.Add("Rook;h1;White;False;False;False;False");
      enterStringList.Add("SimplePawn;h2;White;True;False;False;False");*/




      //enterStringList.Add("SimplePawn;a7;Black;True;False;False;False");
      //enterStringList.Add("Rook;a8;Black;False;False;False;False");
      //enterStringList.Add("SimplePawn;b7;Black;True;False;False;False");
      //enterStringList.Add("Knight;b8;Black;False;False;False;False");
      //enterStringList.Add("SimplePawn;c7;Black;True;False;False;False");
     // enterStringList.Add("Bishop;c8;Black;False;False;False;False");
      //enterStringList.Add("SimplePawn;d7;Black;False;False;False;False");
     // enterStringList.Add("Queen;d8;Black;False;False;False;False");
      //enterStringList.Add("SimplePawn;e7;Black;True;False;False;False");
      enterStringList.Add("King;e8;Black;False;True;True;True");
      //enterStringList.Add("SimplePawn;f7;Black;True;False;False;False");
      //enterStringList.Add("Bishop;f8;Black;False;False;False;False");
      //enterStringList.Add("SimplePawn;g7;Black;True;False;False;False");
      //enterStringList.Add("Knight;g8;Black;False;False;False;False");
      //enterStringList.Add("SimplePawn;h7;Black;True;False;False;False");
     // enterStringList.Add("Rook;h8;Black;False;False;False;False");





      var server = new Server();
      server.GeneratePawnList(enterStringList);
      var pawnList = server.CurrentPawnList;

      foreach (var pawn in pawnList)
      {
        Console.WriteLine($"{pawn.Name} {pawn.Location} {pawn.Colore} {pawn.PossibleTrips.Count}");
      }

      //Generaion de l'arbre
      Console.WriteLine("Tree is generated");
      var startTime = DateTime.Now;


      server.GenereTree("White", 3);

      

      



      var bestNode = server.GetBestNodePostion();
      //MoveTo(bestNode.Location, bestNode.BestChildPosition);
     // Console.WriteLine($"Best position : {bestNode.AssociatePawn.Name} {bestNode.Location} to {bestNode.BestChildPosition}");


      var executionTime = DateTime.Now - startTime;
      Console.WriteLine($"Execution time = {executionTime}");
      Console.WriteLine("Finish");
      Console.ReadLine();
      
    }

   
      
  }
}
