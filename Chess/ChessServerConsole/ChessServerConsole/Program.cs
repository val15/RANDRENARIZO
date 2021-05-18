using ChessServerConsole.Utils;
using System;
using System.Collections.Generic;
using System.IO;
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
      var server = new Engine(colore);
      server.GeneratePawnList(enterStringList);
      var pawnList = server.PawnList;

      foreach (var pawn in pawnList)
      {
        Console.WriteLine($"{pawn.Name} {pawn.Location} {pawn.Colore} {pawn.PossibleTrips.Count}");
      }

      //Generaion de l'arbre
      Console.WriteLine("Tree is generated");
      var startTime = DateTime.Now;



      var bestNode = server.ThreadGetBestMove();








      //MoveTo(bestNode.Location, bestNode.BestChildPosition);
      Console.WriteLine($"Best position : {bestNode.AssociatePawn.Name} {bestNode.Location} to {bestNode.BestChildPosition}");


      var executionTime = DateTime.Now - startTime;
      Console.WriteLine($"Execution time = {executionTime}");
      Console.WriteLine("Finish");
      return bestNode.Location + ";" + bestNode.BestChildPosition+";"+bestNode.AssociatePawn.Name;

    }
    static void Main(string[] args)
    {
      Console.WriteLine("Chess server is run...");

      
      while(true)
        StartSocketServer();
      Console.ReadLine ();
      
      
     
      //TEST
     /* var enterStringList = new List<string>();
      //LECTURE DU FICHIER QUI CONTIEN LA LISTE DES POINS (FICHE DE TEST)
      enterStringList = Load();
      var engine = new Engine("Black");
      engine.GeneratePawnList(enterStringList);
      var pawnList = engine.PawnList;

      foreach (var pawn in pawnList)
      {
        Console.WriteLine($"{pawn.Name} {pawn.Location} {pawn.Colore} {pawn.PossibleTrips.Count}");
      }

      //Generaion de l'arbre
      Console.WriteLine("Tree is generated");
      var startTime = DateTime.Now;


      

      

      



      var bestNode = engine.ThreadGetBestMove();
      //MoveTo(bestNode.Location, bestNode.BestChildPosition);
      Console.WriteLine($"Best position : {bestNode.AssociatePawn.Name} {bestNode.Location} to {bestNode.BestChildPosition}");


      var executionTime = DateTime.Now - startTime;
      Console.WriteLine($"Execution time = {executionTime}");
      Console.WriteLine("Finish");
      Console.ReadLine();   */
    }


    public static List<string> Load()
    {
      var whiteListFile = "./WHITEListOld.txt";
      var blackListFile = "./BLACKListOld.txt";

      if (!File.Exists(whiteListFile) || !File.Exists(blackListFile))
        return null;




    var readText = File.ReadAllText(whiteListFile);


      var resultList = new List<string>();
      using (StringReader sr = new StringReader(readText))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          //Debug.WriteLine(line);

          resultList.Add(line);

        }
      }

       readText = File.ReadAllText(blackListFile);

      using (StringReader sr = new StringReader(readText))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          resultList.Add(line);

        }
      }
      return resultList;



    }



  }
}
