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

    public Node GetBestNodePostion()
    {
     

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

      return bestNode;

    }

    private void generateAllNodFromPawn(Pawn pawn )
    {

    }


    public void GenereTree(string colore, int deepLevel)
    {
      Tree = null;
      Tree = new List<Node>();
      //Tree.Clear();
      var computerPawnList = CurrentPawnList.Where(x => x.Colore == colore).ToList();






      /* Parallel.ForEach(computerPawnList,new ParallelOptions() { MaxDegreeOfParallelism = 1 }, pawn =>
       {
          //var deep = 0;


         
       });*/
      
      foreach (var pawn in computerPawnList)
      {
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
          Tree.Add(GenerateThread(pawn.Location, pawn.PossibleTrips[i], colore, newNode, pawn, deepLevel, colore));
        }



      }

      
    // MinMax();





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

    private void MinMaxAlphaBeta()
    {

    }
    private Node GenerateThread(string initialPosition, string evaluatePosition, string actualColore, Node parentNode, Pawn associatePawn, int deepLevel, string computerColore)
    {
      //TODO
      //SI DANS LA BASE


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
        newNode.Weight =  evaluateScoreForWhite(actualColore, depPawnsList, selectedPawn);

     parentNode.ChildList.Add(newNode);
      

      //MinMax
        if (parentNode != null)
      {
        if ((newNode.Level % 2) != 0)//Max
        {
          //on remonte le max
          if (parentNode.Weight < newNode.Weight)
          {
            parentNode.Weight = newNode.Weight - 1;
            if (parentNode.Level == 0)
              parentNode.BestChildPosition = newNode.Location;

          }



        }
        else //Min
        {

          if (parentNode.Weight > newNode.Weight)
          {
            parentNode.Weight = newNode.Weight - 1;
          }

        }
      }


        

        //Console.WriteLine($"{newNode.Level} {newNode.AssociatePawn.Name} {newNode.Location} {newNode.Weight}");

      


    
      //Tree.Add(newNode);
     // Console.WriteLine($"{newNode.Level} {newNode.AssociatePawn.Name} {newNode.Location} {newNode.Weight}");



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
            Tree.Add(GenerateThread(pawn.Location, pawn.PossibleTrips[i], pawn.Colore, newNode, pawn, deepLevel, computerColore));
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
        return newNode;
      }

      //fin, on reinitalise
      selectedPawn.Location = initialPosition;
      selectedPawn.X = initialPosition[0].ToString();
      selectedPawn.Y = initialPosition[1].ToString();
      selectedPawn.FillPossibleTrips();
      //selectedPawn.EvaluateScorePossibleTrips();







      //newLocation =;
      // newList
      return newNode;
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
