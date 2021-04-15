using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServerConsole.Utils
{
  public class Server
  {


    public List<Case> CaseList { get; set; }

    public List<Pawn> CurrentPawnList { get; set; }

    public List<Node>  Tree { get; set; }
    //public List<List<Node>> PanwTreeList { get; set; }// chaque pion à deplacer a sa propre arbre


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
     // Tree = null;
      //Tree = new List<Node>();
      //Tree.Clear();
      var computerPawnList = CurrentPawnList.Where(x => x.Colore == colore).ToList();






      /* Parallel.ForEach(computerPawnList,new ParallelOptions() { MaxDegreeOfParallelism = 1 }, pawn =>
       {
          //var deep = 0;


         
       });*/
      
      foreach (var pawn in computerPawnList)
      {
        //Ajout du précedent au tableau des arbres
       


         Tree = null;
        Tree = new List<Node>();
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


          //PanwTreeList.Add(Tree);



      }


       MinMax();
     // MinMaxAlphaBeta();





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
        /* //if(node.Lz)
         if(node.Level == 3)//max
         {
           if (parent.Weight < node.Weight)
           {
             parent.Weight = node.Weight - 1;

           }
         }
         if (node.Level == 2)//max
         {
           if (node.Weight > parent.Weight)
           {
             parent.Weight = node.Weight - 1;


           }
         }
         if (node.Level == 1)//max
         {
           if (node.Weight > parent.Weight)
           {
             parent.Weight = node.Weight - 1;


           }
         }
         if (parent.Level == 0)
           parent.BestChildPosition = node.Location;


         */

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

        /*if(node.Level == 2)
        {
          if (node.Weight < parent.Weight)
          {
            parent.Weight = node.Weight - 1;
          }
          //parent.Weight = node.Weight - 1;
        }*/







      }

    }

   
    private void GenerateThread(string initialPosition, string evaluatePosition, string actualColore, Node parentNode, Pawn associatePawn, int deepLevel, string computerColore)
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
        if (destinationPawn.Colore == "White" && destinationPawn.Name == "King")
        {
          var tdk = destinationPawn;
        }

        if (selectedPawn.Colore == "Black" && destinationPawn.Colore == "White")
        {
          var dezer = depPawnsList.Count;
          var tdzere = destinationPawn;
        }

        depPawnsList.Remove(destinationPawn);
        if (selectedPawn.Colore == "Black" && destinationPawn.Colore == "White")
        {
          var dezer = depPawnsList.Count;
          var tdzere = destinationPawn;
        }

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

      /*if (newNode.Level == 3 && newNode.Colore =="White")
      {
        var tdk = parentNode.GetCurrentLocalPawnListAllier().Count;
      }


      if (newNode.AssociatePawn.Name == "Rook" && newNode.Location == "e1")
      {
        var tdeze = parentNode;
      }*/

      /* if (parentNode.Location == "e1" )
       {
         var tdeze = parentNode;
         var name = parentNode.AssociatePawn.Name;
       }*/

      if (newNode.Parent.AssociatePawn.Name == "Rook" && newNode.Parent.Location == "e1")
      {
        var tdeze = parentNode;
      }

      if (computerColore == "Black")
        newNode.Weight = evaluateScoreForBlack(actualColore, depPawnsList, selectedPawn);
      if (computerColore == "White")
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
          if (item.Name == "King" && newNode.Level == 2 && item.Location == "d2" && newNode.AssociatePawn.Name == "Rook")
          {
            var t = item.PossibleTrips;
          }
        }

      }

      /*PawnList = null;
      PawnList = new List<Pawn>();
      PawnList.AddRange(originalPawnList);*/







      //var deepLevel = 0;
      /*if(actualColore == "White")
        deepLevel = deepWhiteLevel ;
      if(actualColore == "Black")
        deepLevel = deepBlackLevel;*/


      /* if (deepStep > deepLevel)
         deepStep = 0;*/
      // deepStep++;
      if (parentNode.Level < deepLevel - 1)
      {

        if (newNode.AssociatePawn.Name == "Rook" && newNode.Location == "e1" && newNode.Level == 2 && newNode.Parent.AssociatePawn.Name == "King" && newNode.Parent.AssociatePawn.Location == "d2")
        {
          var tdeze = opignionPawnList;
        }
        foreach (var pawn in opignionPawnList)
        {

          //var deep = 0;
          //pawn.EvaluateScorePossibleTrips();


          /* var opignionColore = "";
            if (pawn.Colore == "Black")
              opignionColore = "White";
            else
              opignionColore = "Black";

            */


          for (int i = 0; i < pawn.PossibleTrips.Count; i++)
          {
            //deep++;
            /* if(pawn.Name=="King")
             {
               var c = opignionPawnList.First().Colore;
               //if(c!="White")
               if (c != "Black")
               {
                 var nb = c;
               }
             }*/
            // var c = opignionPawnList.First().Colore;

            if (newNode.AssociatePawn.Name == "King" && newNode.Location == "e1")
            {
              var tdeze = parentNode;
            }
            GenerateThread(pawn.Location, pawn.PossibleTrips[i], pawn.Colore, newNode, pawn, deepLevel,computerColore);
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
