using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServerConsole.Utils
{
  public class Engine
  {


    public List<Case> CaseList { get; set; }

    public List<Pawn> PawnList { get; set; }

    public List<Node>  Tree { get; set; }
    public List<List<Node>> AllCumputerPawnTreeList { get; set; }
    private int cumputerLevel = 3;
    private string _computerColore;
    public Pawn GetPawnFromPawnList(string tripsPosition)
    {
      return PawnList.FirstOrDefault(x => x.Location == tripsPosition);
    }

    public List<Pawn> GetOpignonPawnList(string colore)
    {
      return PawnList.Where(x => x.Colore != colore).ToList();
    }

    public Case GetCase(string location)
    {
      return CaseList.FirstOrDefault(x => x.CaseName == location);
    }
    public Engine(string computerColore)
    {
      _computerColore = computerColore;
      //PanwTreeList = new List<List<Node>>();
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
      PawnList = null;
      PawnList = new List<Pawn>();
      foreach (var line in enterStringList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], datas[2],this);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        PawnList.Add(newPawn);
      }
      PawnList = PawnList.OrderByDescending(x => x.Value).ToList();

      FillAllPossibleTrips();

    }

    public void FillAllPossibleTrips()
    {
      foreach (var pawn in PawnList)
      {
        pawn.FillPossibleTrips();
      }
    }

    public Node ThreadGetBestMove()
    {



      var color = _computerColore;

        Tree = null;
        Tree = new List<Node>();
        AllCumputerPawnTreeList = null;
        AllCumputerPawnTreeList = new List<List<Node>>();
        //if (CurrentTurn == "White")
          GenereTree(color, 3);
        /*if (CurrentTurn == "Black")
          GenereTree(color, 3);*/

        var t_tree = Tree;
        var bestNode = GetBestNodePostion();
      //TODO
      //  MoveTo(bestNode.Location, bestNode.BestChildPosition);
      Console.WriteLine("ThreadGetBestMove() is finished");
      return bestNode;
    }

    private void GenereTree(string colore, int deepLevel)
    {
      //Tree = null;
      // Tree = new List<Node>();
      //Tree.Clear();
      var computerPawnList = PawnList.Where(x => x.Colore == colore).ToList();
      







      foreach (var pawn in computerPawnList)
      {
        //var deep = 0;
        Tree = null;
        Tree = new List<Node>();


        Node newNode = new Node(PawnList);
        newNode.Location = pawn.Location;
        newNode.OldPositionName = "";
        newNode.Weight = -10000000;
        newNode.Level = 0;
        newNode.Colore = colore;
        newNode.AssociatePawn = pawn;

        Tree.Add(newNode);







        for (int i = 0; i < pawn.PossibleTrips.Count; i++)
        {
          //deep++;
          GenerateThread(pawn.Location, pawn.PossibleTrips[i], colore, newNode, pawn, deepLevel);
          //deepStep = 0;
          foreach (var item in PawnList)
          {
            item.FillPossibleTrips();
          }
        }
        //Chaque pion a son arbre(Tree)
        AllCumputerPawnTreeList.Add(Tree);
        




      }


      MinMax();





      foreach (var pawn in PawnList)
      {
        //fin, on reinitalise

        pawn.Location = pawn.Location;
        pawn.X = pawn.Location[0].ToString();
        pawn.Y = pawn.Location[1].ToString();
        pawn.FillPossibleTrips();
        // pawn.EvaluateScorePossibleTrips();

      }
    }

    private void GenerateThread(string initialPosition, string evaluatePosition, string actualColore, Node parentNode, Pawn associatePawn, int deepLevel)
    {

      List<Pawn> depPawnsList = new List<Pawn>();
      depPawnsList.AddRange(parentNode.GetCurrentLocalPawnList());
      Pawn selectedPawn = new Pawn();
      selectedPawn = pawnGetPawnsInList(depPawnsList, initialPosition);
      if (selectedPawn == null)
      {
        //return
        var tN = selectedPawn;
      }
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


      //parentNode.Location = initialPosition;


      //  newNode.Parent = parentNode;

      newNode.Level = parentNode.Level + 1;
      //associatePawn.Location = evaluatePosition;
      Pawn tempsPawn = new Pawn();
      tempsPawn.Name = associatePawn.Name;
      tempsPawn.Location = associatePawn.Location;
      tempsPawn.Colore = associatePawn.Colore;
      newNode.AssociatePawn = tempsPawn;
 
      newNode.Parent = parentNode;

    


      if (_computerColore == "Black")
        newNode.Weight = evaluateScoreForBlack(actualColore, depPawnsList, selectedPawn);
      if (_computerColore == "White")
        newNode.Weight = evaluateScoreForWhite(actualColore, depPawnsList, selectedPawn);
      /*   DebugTextBlock2.Text = "";
        if (newNode.Colore == "Black" && newNode.AssociatePawn.Name == "Rook" && newNode.Level ==2)
         {
           DebugTextBlock2.Text += newNode.AssociatePawn.Name + "   "+"    " + newNode.Level+"   "  
             + newNode.Weight + "    " + selectedPawn.Location+"\n";

         }*/



      /*if(newNode.Location=="e8")
      {
        var s = newNode.Weight;
      }*/



      Tree.Add(newNode);
      //parentNode.ChildList.Add(newNode);

      //opignion pawn list
      //var opignionPawnList = depPawnsList.Where(x => x.Colore != newNode.Colore).ToList();
      // var opignionKing = opignionPawnList.FirstOrDefault(x => x.Name == "King");
      // if (opignionKing == null)
      //   return;

      var originalPawnList = new List<Pawn>();
      originalPawnList.AddRange(PawnList);

      var opignionPawnList = new List<Pawn>();
      foreach (var item in depPawnsList)
      {
        if (item.Colore != actualColore)
        {

          //A VERIFIER
          // PawnList = depPawnsList;
          item.FillPossibleTrips();
          opignionPawnList.Add(item);
          if (item.Name == "Rook" && newNode.Level == 2 && item.Location == "d2" && newNode.AssociatePawn.Name == "Rook")
          {
            var t = item.PossibleTrips;
          }
        }

      }

     
      if (parentNode.Level < cumputerLevel - 1)
      {

        if (newNode.AssociatePawn.Name == "Rook" && newNode.Location == "e1" && newNode.Level == 2 && newNode.Parent.AssociatePawn.Name == "King" && newNode.Parent.AssociatePawn.Location == "d2")
        {
          var tdeze = opignionPawnList;
        }
        foreach (var pawn in opignionPawnList)
        {

        


          for (int i = 0; i < pawn.PossibleTrips.Count; i++)
          {


            if (newNode.AssociatePawn.Name == "King" && newNode.Location == "e1")
            {
              var tdeze = parentNode;
            }
            GenerateThread(pawn.Location, pawn.PossibleTrips[i], pawn.Colore, newNode, pawn, deepLevel);
            //deepStep = 0;
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
    }

    private int evaluateScoreForBlack(string colore, List<Pawn> actualPawnList, Pawn movingPawn)
    {

      if (actualPawnList.FirstOrDefault(x => x.Name == "King") == null)
        return -9999999;
      if (actualPawnList.FirstOrDefault(x => x.Name == "Queen") == null)
        return -900;
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
      if (movingPawn.Colore == colore)
        return blackScore - whiteScore;
      else
        return whiteScore - blackScore;
    }



    private int evaluateScoreForWhite(string colore, List<Pawn> actualPawnList, Pawn movingPawn)
    {
      if (actualPawnList.FirstOrDefault(x => x.Name == "King") == null)
        return -9999999;
      if (actualPawnList.FirstOrDefault(x => x.Name == "Queen") == null)
        return -900;
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
      if (movingPawn.Colore == colore)
        return whiteScore - blackScore;
      else
        return blackScore - whiteScore;


    }

    private void MinMax()
    {

      //pour chaque arbre, on amplique le MinMax
      foreach (var tree in AllCumputerPawnTreeList)
      {
        for (int i = tree.Count - 1; i >= 0; i--)
        {
          var node = tree[i];
          var parent = tree[i].Parent;
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
        }
      }



    }

    private Node GetBestNodePostion()
    {

     

      //  bestNode = new Node();


      var bestNodeList = new List<Node>();

      //on prend les milleurs arbre
      foreach (var tree in AllCumputerPawnTreeList)
      {
        bestNodeList.Add(tree.First());
      }
      var maxWeiht = bestNodeList.OrderByDescending(x => x.Weight).First().Weight;

      var bestMaxWeithNodeList = bestNodeList.Where(x => x.Weight == maxWeiht).ToList();
      foreach (var node in bestMaxWeithNodeList)
      {
        Console.WriteLine($"{node.Level} {node.AssociatePawn.Name} {node.Weight} {node.BestChildPosition}");
      }

      if (bestMaxWeithNodeList.Count() == 1)
      {
        var node = bestMaxWeithNodeList.First(); ;
        Console.WriteLine($"BEST : {node.Level} {node.AssociatePawn.Name} {node.Weight} {node.BestChildPosition}  {node.Location} to {node.BestChildPosition}");
        return node;
      }

      //si il y a plusieur meilleurs arbre
      //On Simule les noeuds qui on les milleurs score
      AllCumputerPawnTreeList = null;
      AllCumputerPawnTreeList = new List<List<Node>>();


      foreach (var node in bestMaxWeithNodeList)
      {
        Tree = null;
        Tree = new List<Node>();
        var pawn = node.AssociatePawn;


        for (int i = 0; i < pawn.PossibleTrips.Count; i++)
        {
          //deep++;
          GenerateThread(pawn.Location, pawn.PossibleTrips[i], pawn.Colore, node, pawn, cumputerLevel);
          //deepStep = 0;
          foreach (var item in PawnList)
          {
            item.FillPossibleTrips();
          }
        }
        //tC = Tree.Count;
        AllCumputerPawnTreeList.Add(Tree);
      }


      //var tl = bestMaxWeithNodeList.Count();
      MinMax();
      foreach (var tree in AllCumputerPawnTreeList)
      {
        var node = tree.First();
        Console.WriteLine($"{node.Level} {node.AssociatePawn.Name} {node.Weight} {node.BestChildPosition}");

      }

      var bestNodeListSecond = new List<Node>();

      foreach (var tree in AllCumputerPawnTreeList)
      {
        bestNodeListSecond.Add(tree.First());
      }
      var maxWeihtSecond = bestNodeListSecond.OrderByDescending(x => x.Weight).First().Weight;

      var bestMaxWeithNodeListSecond = bestNodeListSecond.Where(x => x.Weight == maxWeihtSecond);


      var dzeere = bestMaxWeithNodeListSecond.Count();
      if (bestMaxWeithNodeListSecond.Count() == 1)
      {
        var nodeSeconde = bestMaxWeithNodeListSecond.First();
        //var t = nodeSeconde.AssociatePawn;
        //var tl0 = bestMaxWeithNodeList.Count();
        var node = bestMaxWeithNodeList.FirstOrDefault(x => x.AssociatePawn.Name == nodeSeconde.AssociatePawn.Name);

        Console.WriteLine($"BEST : {node.Level} {node.AssociatePawn.Name} {node.Weight} {node.BestChildPosition}" +
          $" {node.Location} to {node.BestChildPosition}");
        return node;
      }
      Console.WriteLine($"NO BEST FOND");

      //quand il n'y a pas de milleurs apres une seconde simulation
      //on prend au hazard

      Random rnd = new Random();
      int index = rnd.Next(0, bestMaxWeithNodeListSecond.Count());
      var nodeSecondeRandom = bestMaxWeithNodeListSecond.ElementAt(index);
      //bestMaxWeithNodeList.RemoveAll(x => x.AssociatePawn.Name == "King" || x.AssociatePawn.Name == "Queen");
      var nodeRandomList = bestMaxWeithNodeList.Where(x => (x.AssociatePawn.Name == nodeSecondeRandom.AssociatePawn.Name)).ToList();
      Random rand = new Random();
      var randomIndex = rand.Next(0, nodeRandomList.Count);

      var nodeRandom = nodeRandomList.ElementAt(randomIndex);


      Console.WriteLine($"RANDOM : {nodeRandom.Level} {nodeRandom.AssociatePawn.Name} {nodeRandom.Weight} {nodeRandom.BestChildPosition}" +
          $" {nodeRandom.Location} to {nodeRandom.BestChildPosition}");





      return nodeRandom;

    }



    private Pawn pawnGetPawnsInList(List<Pawn> pawnsList, string position)
    {
      return pawnsList.FirstOrDefault(x => x.Location == position);
    }




  }
}
