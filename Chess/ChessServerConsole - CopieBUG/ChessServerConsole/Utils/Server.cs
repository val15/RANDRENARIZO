using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServerConsole.Utils
{
  public class Server
  {

    static Node MAX = new Node();
    static Node MIN = new Node();
    public List<Case> CaseList { get; set; }

    public List<Pawn> CurrentPawnList { get; set; }

    public List<Node>  Tree { get; set; }

   public List<BestNodeBestMode> BestNodeBestModeList { get; set; }


    public Pawn GetPawnFromPawnList(string tripsPosition)
    {
      return CurrentPawnList.FirstOrDefault(x => x.Location == tripsPosition);
    }

    public List<Pawn> GetOpignonPawnList(string colore)
    {
      return CurrentPawnList.Where(x => x.Colore != colore).ToList();
    }

    public Case GetCase(string location)
    {
      return CaseList.FirstOrDefault(x => x.CaseName == location);
    }
    public Server()
    {

      MAX.Location = "MAX";
      MAX.Weight = 99999;
      MIN.Location = "MIN";
      MIN.Weight = -99999;
      CaseList = new List<Case>();
      for (int x = 0; x <= 7; x++)
      {
        for (int y = 1; y <= 8; y++)
        {


          //initialisation des cases
          CaseList.Add(new Case(Convert.ToChar(97 + x).ToString(), y.ToString()));

        }
      }
    }

    public void GeneratePawnList(List<string> enterStringList)
    {
      CurrentPawnList = null;
      CurrentPawnList = new List<Pawn>();
      foreach (var line in enterStringList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], datas[2],this);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        CurrentPawnList.Add(newPawn);
      }

      FillAllPossibleTrips();

    }

    public void FillAllPossibleTrips()
    {
      foreach (var pawn in CurrentPawnList)
      {
        pawn.FillPossibleTrips();
      }
    }

    public Node GetBestNodePostion()//TODO
    {

      /*
      var maxWeight = Tree.Where(x => x.Level == 0).OrderByDescending(x => x.Weight).First().Weight;
      var bestNodeList = Tree.Where(x => x.Level == 0 && x.Weight == maxWeight);
      Node bestNode = new Node(new List<Pawn>());
      if (bestNodeList.Count() == 1)
      {
        bestNode = bestNodeList.First();
      

      }
      else
      {
        Random rnd = new Random();
        int index = rnd.Next(0, bestNodeList.Count());
        bestNode = bestNodeList.ElementAt(index);

      }

      return bestNode;*/
      Console.WriteLine("BEST MOVE LIST:");
      foreach (var item in BestNodeBestModeList)
      {
        Console.WriteLine("BestNode = " + item.BestNode.AssociatePawn.Name + "   " + item.BestNode.Weight + "    " + item.FromPosition + " to " + item.ToPosition); ;
      }
      return null;

    }

    private void generateAllNodFromPawn(Pawn pawn )
    {

    }


    public void GenereTree(string colore, int deepLevel)
    {
       Tree = null;
       Tree = new List<Node>();
      //Tree.Clear();

      //TreeList = null;
      //TreeList = new List<List<Node>>();
      BestNodeBestModeList = null;
      BestNodeBestModeList = new List<BestNodeBestMode>();
      var computerPawnList = CurrentPawnList.Where(x => x.Colore == colore).ToList();






      /* Parallel.ForEach(computerPawnList,new ParallelOptions() { MaxDegreeOfParallelism = 1 }, pawn =>
       {
          //var deep = 0;


         
       });*/
      
      foreach (var pawn in computerPawnList)
      {

        //var tree = new List<Node>();
       Node newNode = new Node(CurrentPawnList);
        newNode.Location = pawn.Location;
        newNode.OldPositionName = "";
        newNode.Weight = -10000000;
        newNode.Level = 0;
        newNode.Colore = colore;
        newNode.AssociatePawn = pawn;

        Tree.Add(newNode);









        for (int i = 0; i < pawn.PossibleTrips.Count; i++)
        {
          GenerateThread(pawn.Location, pawn.PossibleTrips[i], colore, newNode, pawn, deepLevel, colore);
        }
        //MinMaxAlphaBeta(tree);



      }


      // MinMax();
      MinMaxAlphaBeta();





      foreach (var pawn in CurrentPawnList)
      {
        //fin, on reinitalise

        pawn.Location = pawn.Location;
        pawn.X = pawn.Location[0].ToString();
        pawn.Y = pawn.Location[1].ToString();
        pawn.FillPossibleTrips();
        // pawn.EvaluateScorePossibleTrips();

      }
    }

    private void MinMax()
    {
      for (int i = Tree.Count - 1; i >= 0; i--)
      {
        var node = Tree[i];
        var parent = Tree[i].Parent;
        if (parent == null)
          continue;
        parent.ChildList.Add(node);
       

        if ((node.Level % 2) != 0)//Max
        {
          //on remonte le max
          if (parent.Weight < node.Weight)
          {
            parent.Weight = node.Weight - 1;
            if (parent.Level == 0)
              parent.BestChildPosition = node.Location;

          }
         


        }
        else //Min
        {

          if (parent.Weight > node.Weight)
          {
            parent.Weight = node.Weight - 1;
          }

        }

        Console.WriteLine($"{node.Level} {node.AssociatePawn.Name} {node.Location} {node.Weight}");






      }

    }


    static Node minimax(int depth, int nodeIndex,
                Boolean maximizingPlayer,
                List<Node> values, Node alpha,
                Node beta)
    {
      // Terminating condition. i.e 
      // leaf node is reached
      var currentNode = values[nodeIndex];
      if (currentNode.Level == 3)
        return currentNode;

      if (maximizingPlayer)
      {
        var best = MIN;

        // Recur for left and
        // right children
        //for (int i = 0; i < currentNode.ChildList.Count; i++)
        foreach (var node in currentNode.ChildList)
        {

          var index = values.IndexOf(node);
          var val = minimax(currentNode.Level + 1, index,
                          false, values, alpha, beta);
          best = Max(best, val);
          alpha = Max(alpha, best);

          // Alpha Beta Pruning
          if (beta.Weight <= alpha.Weight)
            break;
        }
        return best;
      }
      else
      {
        var best = MAX;

        // Recur for left and
        // right children
        foreach (var node in currentNode.ChildList)
        {
          var index = values.IndexOf(node);
          var val = minimax(currentNode.Level + 1, index,
                          true, values, alpha, beta);
          best = Min(best, val);
          beta = Min(beta, best);

          // Alpha Beta Pruning
          if (beta.Weight <= alpha.Weight)
            break;
        }
        if (currentNode.Weight > best.Weight)
        {
          best = currentNode;
        }
        return best;
      }
    }

    public static Node Max(Node node0, Node node1)
    {
      if (node0.Weight > node1.Weight)
        return node0;
      return node1;
    }
    public static Node Min(Node node0, Node node1)
    {
      if (node0.Weight < node1.Weight)
        return node0;
      return node1;
    }

    // Driver Code

    private void MinMaxAlphaBeta()
    {
      //Console.WriteLine("MinMaxAlphaBeta");

      var bestNode = minimax(0, 0, true, Tree, MIN, MAX);
      Console.WriteLine(bestNode.Weight);
      Console.WriteLine("The optimal value is : Name=" + bestNode.AssociatePawn.Name + "    Location="+
                         bestNode.Location + "   Weight=" + bestNode.Weight);
      Console.WriteLine("The optimal NodeName is : " +
                         bestNode.Location + "Parent = "+ bestNode.Parent.Location);

      //firstLevelParentNode

      var currentNodeParent = bestNode.Parent;
      if (currentNodeParent == null)
        return;
      while (currentNodeParent.Level > 1)
      {
        currentNodeParent = currentNodeParent.Parent;

      }


      var bestNodeBestMode = new BestNodeBestMode();
      bestNodeBestMode.BestNode = bestNode;
      bestNodeBestMode.FromPosition = currentNodeParent.Parent.Location;
      bestNodeBestMode.ToPosition = currentNodeParent.Location;

      Console.WriteLine("Best move : " + bestNodeBestMode.FromPosition + "To" +
                   bestNodeBestMode.ToPosition);

      BestNodeBestModeList.Add(bestNodeBestMode);



    }
    private void GenerateThread(string initialPosition, string evaluatePosition, string actualColore, Node parentNode, Pawn associatePawn, int deepLevel,string computerColore)
    {

      List<Pawn> depPawnsList = new List<Pawn>();
      depPawnsList.AddRange(parentNode.GetCurrentLocalPawnList());
      Pawn selectedPawn = new Pawn();
      selectedPawn = pawnGetPawnsInList(depPawnsList, initialPosition);

      var destinationPawn = pawnGetPawnsInList(depPawnsList, evaluatePosition);
      if (destinationPawn != null)
      {
        

       

        depPawnsList.Remove(destinationPawn);
       

      }




      selectedPawn.Location = evaluatePosition;
      selectedPawn.X = evaluatePosition[0].ToString();
      selectedPawn.Y = evaluatePosition[1].ToString();


      selectedPawn.FillPossibleTrips();
      //selectedPawn.EvaluateScorePossibleTrips();

      Node newNode = new Node(depPawnsList);
      newNode.Location = evaluatePosition;
      newNode.OldPositionName = initialPosition;
      newNode.Colore = actualColore;
      /*Node tempParent = new Node();
      tempParent.AssociatePawn = parentNode.AssociatePawn;*/

      //parentNode.Location = initialPosition;


      //  newNode.Parent = parentNode;

      newNode.Level = parentNode.Level + 1;
      //associatePawn.Location = evaluatePosition;
      Pawn tempsPawn = new Pawn();
      tempsPawn.Name = associatePawn.Name;
      tempsPawn.Location = associatePawn.Location;
      tempsPawn.Colore = associatePawn.Colore;
      newNode.AssociatePawn = tempsPawn;
      /*Node tempsParentNode = new Node();
      tempsParentNode.AssociatePawn = parentNode.AssociatePawn;
      tempsParentNode.Location = parentNode.Location;*/
      //tempsParentNode.AssociatePawn = tempsParentNode.AssociatePawn;
      newNode.Parent = parentNode;

      

     

      if (computerColore == "Black")
        newNode.Weight = evaluateScoreForBlack(actualColore, depPawnsList, selectedPawn);
      if (computerColore == "White")
        newNode.Weight = evaluateScoreForWhite(actualColore, depPawnsList, selectedPawn);

      /*   var parent = Tree.FirstOrDefault(x => x.AssociatePawn == parentNode.AssociatePawn);
         //On Modifie le parentNode.Weight
         if ((newNode.Level % 2) != 0)//Max
         {
           //on remonte le max
           if (parent.Weight < newNode.Weight)
           {
             parent.Weight = newNode.Weight - 1;
             if (parent.Level == 0)
               parent.BestChildPosition = newNode.Location;

           }
           else
           {
             selectedPawn.Location = initialPosition;
             selectedPawn.X = initialPosition[0].ToString();
             selectedPawn.Y = initialPosition[1].ToString();
             selectedPawn.FillPossibleTrips();
             //selectedPawn.EvaluateScorePossibleTrips();
             return;
           }




         }
         else //Min
         {
           if (parent.Weight > newNode.Weight)
           {
             parent.Weight = newNode.Weight - 1;
           }

         }

         */
      parentNode.ChildList.Add(newNode);
      Tree.Add(newNode);

      ////Console.WriteLine($"{newNode.Level} {newNode.AssociatePawn.Name} {newNode.Location} {newNode.Weight}");



      var originalPawnList = new List<Pawn>();
      originalPawnList.AddRange(CurrentPawnList);

      var opignionPawnList = new List<Pawn>();
      foreach (var item in depPawnsList)
      {
        if (item.Colore != actualColore)
        {

          //A VERIFIER
          // PawnList = depPawnsList;
          item.FillPossibleTrips();
          opignionPawnList.Add(item);
         
        }

      }


      if (parentNode.Level < deepLevel - 1)
      {
        foreach (var pawn in opignionPawnList)
        {
          for (int i = 0; i < pawn.PossibleTrips.Count; i++)
          {
            GenerateThread(pawn.Location, pawn.PossibleTrips[i], pawn.Colore, newNode, pawn, deepLevel, computerColore);

          }
        }
      }
      else
      {
        // deepStep = 0;
        //fin, on reinitalise
        selectedPawn.Location = initialPosition;
        selectedPawn.X = initialPosition[0].ToString();
        selectedPawn.Y = initialPosition[1].ToString();
        selectedPawn.FillPossibleTrips();
        //selectedPawn.EvaluateScorePossibleTrips();
        return;
      }

      //fin, on reinitalise
      selectedPawn.Location = initialPosition;
      selectedPawn.X = initialPosition[0].ToString();
      selectedPawn.Y = initialPosition[1].ToString();
      selectedPawn.FillPossibleTrips();
      //selectedPawn.EvaluateScorePossibleTrips();







      //newLocation =;
      // newList
    }

    private int evaluateScoreForBlack(string colore, List<Pawn> actualPawnList, Pawn movingPawn)
    {
      var whiteScore = 0;
      var blackScore = 0;

      foreach (var pawn in actualPawnList)
      {
        if (pawn.Colore == "Black")
          blackScore += pawn.Value;
        else
        {
          //# NB : here is for black piece or empty square
          whiteScore += pawn.Value;
        }

      }


      /* var kingAlier = actualPawnList.FirstOrDefault(x => x.Colore == "Black" && x.Name == "King");
       if (kingAlier != null)
       {
         if (kingAlier.FindIsMaced(actualPawnList))
           return -9999999;
       }
    */

      if (movingPawn.Colore == colore)
        return blackScore - whiteScore;
      else
        return whiteScore - blackScore;
    }



    private int evaluateScoreForWhite(string colore, List<Pawn> actualPawnList, Pawn movingPawn)
    {
      var whiteScore = 0;
      var blackScore = 0;

      foreach (var pawn in actualPawnList)
      {
        if (pawn.Colore == "White")
          whiteScore += pawn.Value;
        else
        {
          //# NB : here is for black piece or empty square
          blackScore += pawn.Value;
        }

      }



      /*   var kingAlier = actualPawnList.FirstOrDefault(x => x.Colore == "White" && x.Name=="King");
          if(kingAlier!=null)
          {
            if (kingAlier.FindIsMaced(actualPawnList))
              return -9999999;
          }

        */






      if (movingPawn.Colore == colore)
        return whiteScore - blackScore;
      else
        return blackScore - whiteScore;


    }




    private Pawn pawnGetPawnsInList(List<Pawn> pawnsList, string position)
    {
      return pawnsList.FirstOrDefault(x => x.Location == position);
    }

  }
}
