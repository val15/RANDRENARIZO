﻿using Chess.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;

namespace Chess
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {

    public List<Case> CaseList { get; set; }
    public List<Pawn> PawnList { get; set; }
    public List<Pawn> PawnListWhite { get; set; }
    public List<Pawn> PawnListBlack { get; set; }

    public Pawn SelectedPawn { get; set; }

    public string FromPosition { get; set; }
    public string ToPosition { get; set; }

    public string CurrentTurn { get; set; }

    private int deepStep = 0;
    private int levele = 0;

    private List<Node> Tree { get; set; }

  public MainWindow()
    {
      InitializeComponent();
      CurrentTurn = "White";
      //CurrentTurn = "Black";
      WhiteTurnButton.Visibility = Visibility.Visible;
      BlackTurnButton.Visibility = Visibility.Hidden;
      FromPosition = "";
      ToPosition = "";
      CaseList = null;
      PawnList = null;
      PawnListWhite = null;
      PawnListBlack = null;
      CaseList = new List<Case>();
      PawnList = new List<Pawn>();
      PawnListWhite = new List<Pawn>();
      PawnListBlack = new List<Pawn>();

      Tree = new List<Node>();



      for (int x = 0; x <= 7; x++)
      {
        for (int y = 1; y <= 8; y++)
        {
          var X = Convert.ToChar(97 + x).ToString();
          var Y = y.ToString();
          var bt = (Button)this.FindName(X + Y);
          var location = Convert.ToChar(97 + x).ToString() + y.ToString();

          //initialisation des cases
          CaseList.Add(new Case(Convert.ToChar(97 + x).ToString(), y.ToString(), bt, this));


          //initialisation des pions 
          //SimplePawn
           /*if (y == 2)
             PawnListWhite.Add(new Pawn("SimplePawn", location, X, Y, bt, "White", this));*/
           if (y == 7)
             PawnListBlack.Add(new Pawn("SimplePawn", location, X, Y, bt, "Black", this));

           //Rook (Tour)
         /*  if (y == 1 && (X == "a" || X == "h"))
             PawnListWhite.Add(new Pawn("Rook", location, X, Y, bt, "White", this));*/
           if (y == 8 && (X == "a" || X == "h"))
             PawnListBlack.Add(new Pawn("Rook", location, X, Y, bt, "Black", this));
       
           //Knight (chevalier)
          /* if (y == 1 && (X == "b" || X == "g"))
             PawnListWhite.Add(new Pawn("Knight", location, X, Y, bt, "White", this));*/
          if (y == 8 && (X == "b" || X == "g"))
             PawnListBlack.Add(new Pawn("Knight", location, X, Y, bt, "Black", this));

           //Bishop (fou)
        /*   if (y == 1 && (X == "c" || X == "f"))
             PawnListWhite.Add(new Pawn("Bishop", location, X, Y, bt, "White", this));*/
           if (y == 8 && (X == "c" || X == "f"))
             PawnListBlack.Add(new Pawn("Bishop", location, X, Y, bt, "Black", this));
        
          //Queen
          

        /* if (y == 1 && (X == "d"))
            PawnListWhite.Add(new Pawn("Queen", location, X, Y, bt, "White", this));*/
          if (y == 8 && (X == "d"))
            PawnListBlack.Add(new Pawn("Queen", location, X, Y, bt, "Black", this));
       
          //King
        /*  if (y == 1 && (X == "e"))
            PawnListWhite.Add(new Pawn("King", location, X, Y, bt, "White", this));*/
          if (y == 8 && (X == "e"))
            PawnListBlack.Add(new Pawn("King", location, X, Y, bt, "Black", this));
     

        }
      }
      //TEST
      // PawnListWhite.Add(new Pawn("SimplePawn", "d8", "d", "8", (Button)this.FindName("d8"), "White", this));
      PawnListWhite.Add(new Pawn("Knight", "b1", "b", "1", (Button)this.FindName("b1"), "White", this));
      //PawnListWhite.Add(new Pawn("Queen", "b1", "b", "1", (Button)this.FindName("b1"), "White", this));
     // PawnListWhite.Add(new Pawn("Knight", "d6", "d", "6", (Button)this.FindName("d6"), "White", this));


      PawnList.AddRange(PawnListWhite);
      PawnList.AddRange(PawnListBlack);

      FillAllPossibleTrips();
    }


    public void SwithTurn()
    {
      if(CurrentTurn == "White")
      {
        CurrentTurn = "Black";
        BlackTurnButton.Visibility = Visibility.Visible;
        WhiteTurnButton.Visibility = Visibility.Hidden;
        //searchAndExecuteBestMove(PawnListBlack);
      }
      else
      {
        CurrentTurn = "White";
        WhiteTurnButton.Visibility = Visibility.Visible;
        BlackTurnButton.Visibility = Visibility.Hidden;
      }
        
    }
    public void FillAllPossibleTrips()
    {
      foreach (var pawn in PawnList)
      {
        pawn.FillPossibleTrips();
      }
    }

    public void SetDefaultColoreAllCases()
    {
      foreach (var item in CaseList)
      {
        item.SetDefaultColore();
      }
    }
    public Pawn GetPawn(string location)
    {
      var result = PawnList.FirstOrDefault(x => x.Location == location);
      
      
      /*if (result==null)
      {
        Debug.WriteLine("NULL");
      }
      else
      {
        var l = result.Location;
        Debug.WriteLine(l);
      }
        
      */


      return result;
    }
    public List<Pawn> GetOpignonPawnList(string colore)
    {
      if (colore == "Black")
        return PawnListWhite;
      else
        return PawnListBlack;
    }


    public Pawn GetKing(string colore)
    {
      return PawnList.FirstOrDefault(x => x.Colore == colore && x.Name=="King");
    }
    public Pawn GetRightRook(string colore)
    {
      return PawnList.FirstOrDefault(x => x.Colore == colore && x.Name == "Rook" && x.X == "h");
    }
    public Pawn GetLeftRook(string colore)
    {
      return PawnList.FirstOrDefault(x => x.Colore == colore && x.Name == "Rook" && x.X == "a");
    }
    public Case GetCase(string location)
    {
      return CaseList.FirstOrDefault(x => x.CaseName == location);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var buttonSender = (Button)sender;
    }

    private void WhiteGiveUp_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("BLACK WIN");
      //System.Windows.Forms.Application.Restart();

      System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
      Application.Current.Shutdown();
    }

    

    private void BlackGiveUp_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("WHITE WIN");
      //System.Windows.Forms.Application.Restart();

      System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
      Application.Current.Shutdown();
    }
    
    public void Win(string colore)
    {
      MessageBox.Show($"{colore} WIN");
      //System.Windows.Forms.Application.Restart();

      System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
      Application.Current.Shutdown();
    }

    public void MoveTo(string fromPosition, string toPosition)
    {


      var fromPawn = this.GetPawn(fromPosition);
      var toCase = this.GetCase(toPosition);



      //gestion du roc
      if (fromPawn.PossibleTrips.Contains(toPosition))
      {

        if (fromPawn.Name == "Rook")
        {
          //pour le roc, il faut prend le roi et emplecher le roc déplacement
          var associateKing = this.GetKing(fromPawn.Colore);
         
            if (fromPawn.X == "a")
              associateKing.IsLeftRookFirstMove = false;
            if (fromPawn.X == "h")
              associateKing.IsRightRookFirstMove = false;
          
         
        }

        //if (this.SelectedPawn == null)
        //this.SelectedPawn = fromPawn;
        fromPawn.Move(toCase);
      }



      this.FromPosition = "";
      this.ToPosition = "";
    }

    public (string  minPosition,int minScore, string maxPosition,int maxScore, string bestPosition) Emulate(string initialPosition,string newPosition, List<Pawn> initialPawnsList)
    {
     // if (maxDeepStep == 5)
      //  return("", 0, "", 0);
      //maxDeepStep++;
      List<Pawn> depPawnsList = new List<Pawn>();
      depPawnsList.AddRange(initialPawnsList);
      Pawn selectedPawn = new Pawn();
      selectedPawn = pawnGetPawnsInList(depPawnsList, initialPosition);
      var destinationPawn = pawnGetPawnsInList(depPawnsList, newPosition);
      if (destinationPawn != null)
      {
        depPawnsList.Remove(destinationPawn);
      }
      selectedPawn.Location = newPosition;
      selectedPawn.X = newPosition[0].ToString();
      selectedPawn.Y = newPosition[1].ToString();

      selectedPawn.FillPossibleTrips();
      selectedPawn.EvaluateScorePossibleTrips();
      
      if (selectedPawn.Location == "d7")
      {
        var lst = selectedPawn.PossibleTrips;
      }

      //on regarde les scores de la nouvelle position
      var maxScore = 0;
      var maxPosition = "";
      var position = "";
      var score = 0;

      for (int i = 0; i < selectedPawn.PossibleTrips.Count; i++)
      {
        position = selectedPawn.PossibleTrips[i];
        score = selectedPawn.PossibleTripsScore[i];
        if (score >= maxScore)
        {
          maxScore = score;
          maxPosition = position;
        }
      }
      

      var minScore = 0;
      var minPosition = "";



      for (int i = 0; i < selectedPawn.PossibleTrips.Count; i++)
      {
        position = selectedPawn.PossibleTrips[i];
        score = selectedPawn.PossibleTripsScore[i];
        if (score <= minScore)
        {
          minScore = score;
          minPosition = position;
        }
      }

      //Max 
      var n_ = selectedPawn.Name;
      var t_ = maxPosition;
      var s_ = maxScore;

      //Min
      var t_min = maxPosition;
      var s_min = minPosition;


     /* if(newPosition=="d7")
      {
        var n_ = selectedPawn.Name;
        var t_ = maxPosition;
        var s_ = maxScore;
      }*/

      //Emulate(selectedPawn.Location, maxPosition, depPawnsList);


      //fin, on reinitalise
      selectedPawn.Location = initialPosition;
      selectedPawn.X = initialPosition[0].ToString();
      selectedPawn.Y = initialPosition[1].ToString();

      selectedPawn.FillPossibleTrips();
      selectedPawn.EvaluateScorePossibleTrips();
      //selectedPawn.PossibleTrips = initialPossibleTrips;




      return (minPosition, minScore,maxPosition,maxScore, newPosition);

    }



    private Pawn pawnGetPawnsInList(List<Pawn> pawnsList,string position)
    {
      return pawnsList.FirstOrDefault(x => x.Location == position);
    }

    private (string initialPosition, string destionitionPosition, int score) elulateAll(List<Pawn> pawnList)
    {


      foreach (var pawn in pawnList)
      {
        pawn.EmulateAllPossibleTips();
      }

      foreach (var pawn in pawnList)
      {
        var t = pawn.MinPosition;
        var tm = pawn.MaxPosition ;
      }

      var minScorePawn = pawnList.OrderBy(x => x.MinScore).First();
      var maxScorePawn = pawnList.OrderByDescending(x => x.MaxScore).First();
      minScorePawn.FillPossibleTrips();

      //MoveTo(minScorePawn.Location, minScorePawn.MinPosition);
      //MoveTo(maxScorePawn.Location, maxScorePawn.BestPositionAfterEmul);
      return (maxScorePawn.Location,maxScorePawn.BestPositionAfterEmul, maxScorePawn.MaxScore);
    }

    private (string initialPosition,string destionitionPosition, int score) searchAndExecuteBestMove(List<Pawn> pawnList)
    {
      //pour tous le pion de la list
      //on avalue les scrore pour chque déplacement
      //Pawn selectedPawn = new Pawn();

      dynamic bestPawn = null;
      var allMaxScore = 0;
      var allBestPosition = "";
      var pawnListInOrder = pawnList.OrderBy(x => x.PossibleTrips.Count);

      foreach (var pawn in pawnListInOrder)
      {
        var maxScore = 0;
        var maxPosition = "";
        pawn.EvaluateScorePossibleTrips();
        var position = "";
        var score = 0;

        for (int i = 0; i < pawn.PossibleTrips.Count; i++)
        {
          position = pawn.PossibleTrips[i];
          score = pawn.PossibleTripsScore[i];
          if (score >= maxScore)
          {
            maxScore = score;
            maxPosition = position;
          }
        }
        var n_ = pawn.Name;
        var t_ = maxPosition;
        var s_ = maxScore;

        if (maxScore >= allMaxScore)
        {
          allMaxScore = maxScore;
          bestPawn = pawn;
          allBestPosition = maxPosition;
        }

      }
      //le pion à deplacer est celui qui a le milleur score
      //et il sera déplacer vers la position du milleur score
      var t_allMaxScore = allMaxScore;
      var T_allBestPosition = allBestPosition;

      //ajout d'un hazar
      if(allMaxScore == 0)//si tout les score sont à 0
      {
        var ls = pawnList.Where(x => x.PossibleTrips.Count > 0).ToList();
        Random rnd = new Random();
        int index = rnd.Next(0,ls.Count);
        bestPawn = ls.ElementAt(index);

       // bestPawn = ls[Random.Range(0, ls.Count)];
      }


      //MoveTo(((Pawn)bestPawn).Location, allBestPosition);
      return (((Pawn)bestPawn).Location, allBestPosition, allMaxScore);
    }

    private void GenereTree()
    {
      Tree.Clear();
      DebugTextBlock.Text = "";
      foreach (var pawn in PawnListWhite)
      {
        //var deep = 0;
        levele = 0;

        Node newNode = new Node();
        newNode.Location = pawn.Location;
        newNode.ParentName = "";
        newNode.Weight = 0;
        newNode.Level = 0;

        Tree.Add(newNode);
        pawn.EvaluateScorePossibleTrips();



        for (int i = 0; i < pawn.PossibleTrips.Count; i++)
        {
          //deep++;
          
          GenerateThread(pawn.Location, pawn.PossibleTrips[i], pawn.PossibleTripsScore[i], PawnList, newNode);
          deepStep = 0;
        }


      }
      MinMax();
      
      foreach (var node in Tree)
      {
        /*if(node.Location=="f1")
        {*/
          Debug.WriteLine(node.Location);
          DebugTextBlock.Text += "postition : " + node.Location + "  score :" + node.Weight.ToString() + "    level :" + node.Level +
             "  number of child : " + node.ChildList.Count() + "   parent : " + node.ParentName + "   BestChildPosition : " + node.BestChildPosition + "\n";

        //}
      }
    }

    private void MinMax()
    {
      for (int i = Tree.Count-1; i >=0 ; i--)
      {
        var node = Tree[i];
        var parent = Tree[i].Parent;
        if (parent == null)
          continue;
        if (parent.Weight <= node.Weight)
        {
          parent.Weight = node.Weight;
          parent.BestChildPosition = node.Location;
         // parent.BestChildPosition = parent.ChildList.Where(x=>x.Weight > parent.Weight) .OrderBy(x => x.Level).ThenBy(x=>x.ChildList.Count).First().Location; //node.Location;//parent.ChildList.FirstOrDefault(x=>x.Weight == parent.Weight).Location;
        }
          

        /*if (Tree[i].ChildList.Count > 0)
        {
          var maxNode = Tree[i].ChildList.OrderByDescending(x => x.Weight).First();
          Tree[i].Weight = maxNode.Weight;
        }*/
      }

      /*foreach (var item in Tree.OrderByDescending())
      {

      }*/
      
    }

    private void GenerateThread(string initialPosition,string evaluatePosition,int score,List<Pawn> actualPawnList,Node parentNode)
    {
      
      List<Pawn> depPawnsList = new List<Pawn>();
      depPawnsList.AddRange(actualPawnList);
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
      selectedPawn.EvaluateScorePossibleTrips();

      Node newNode = new Node();
      newNode.Location = evaluatePosition;
      newNode.ParentName = initialPosition;
       newNode.Weight = score;
      newNode.Parent = parentNode;
      newNode.Level = parentNode.Level +1;
      //newNode.BestChildPosition = evaluatePosition;

      //newNode.Level = deepStep;
      //si a un parent, levele == levele parent +1
      //si non levele =0
      /* var parent = Tree.FirstOrDefault(x => x.Location == newNode.ParentName);
       if (parent == null)
       {
         newNode.Level = 0;
       }
       else
       {
         newNode.Level = parent.Level + 1;
         parent.ChildList.Add(newNode);
        // parent.Weight = -8888;

         if (parent.Weight < newNode.Weight)
           parent.Weight = newNode.Weight;

       }*/


      Tree.Add(newNode);
      parentNode.ChildList.Add(newNode);
      /*if (parentNode.Weight < newNode.Weight)
        parentNode.Weight = newNode.Weight;
      if(parentNode.Location == "b1")
      {
        var w = newNode.Weight;
        var t_ = parentNode.Weight;
      }*/

      /*if(score==1000)
      {
        //fin, on reinitalise
        selectedPawn.Location = initialPosition;
        selectedPawn.X = initialPosition[0].ToString();
        selectedPawn.Y = initialPosition[1].ToString();

        selectedPawn.FillPossibleTrips();
        selectedPawn.EvaluateScorePossibleTrips();
        return;
      }*/

      if (deepStep < 5 )
      {
        //levele = 0;
        for (int i = 0; i < selectedPawn.PossibleTrips.Count; i++)
        {
          deepStep++;
          GenerateThread(selectedPawn.Location, selectedPawn.PossibleTrips[i], selectedPawn.PossibleTripsScore[i], actualPawnList, newNode);
          

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
        selectedPawn.EvaluateScorePossibleTrips();
        return;
      }

      //fin, on reinitalise
      selectedPawn.Location = initialPosition;
      selectedPawn.X = initialPosition[0].ToString();
      selectedPawn.Y = initialPosition[1].ToString();

      selectedPawn.FillPossibleTrips();
      selectedPawn.EvaluateScorePossibleTrips();







      //newLocation =;
      // newList
    }




    private async void RunEngineForWhite_Click(object sender, RoutedEventArgs e)
    {


     GenereTree();

      

       /* while (true)
        {
          await Task.Delay(200);
        GenereTree();
        MoveTo(Tree.First().Location, Tree.First().BestChildPosition);
      
       
        //GetBestForWhite();
        // await Task.Delay(200);
        // GetBestForBlack();
      }*/
      
    }

    private void GetBestForWhite()
    {
      // tbkLabel.Text = "two seconds delay";

      //searchAndExecuteBestMove(PawnListWhite);
      //elulateAll(PawnListWhite);
    //  var emuleResults = elulateAll(PawnListWhite);
      var searchResults = searchAndExecuteBestMove(PawnListWhite);


      /*var emuleResultsIsvalide = true;
      var opignonList = this.GetOpignonPawnList(PawnListWhite.First().Colore);
      foreach (var opignonPawn in opignonList)
      {
        if (opignonPawn.PossibleTrips.Contains(emuleResults.destionitionPosition))
        {
          emuleResultsIsvalide = false;
          break;
        }
      }
      if (emuleResultsIsvalide)
      {
        if (searchResults.score >= emuleResults.score)
          MoveTo(searchResults.initialPosition, searchResults.destionitionPosition);
        else
        {
          MoveTo(emuleResults.initialPosition, emuleResults.destionitionPosition);
        }
      }
      else*/
        MoveTo(searchResults.initialPosition, searchResults.destionitionPosition);
    }

    private void GetBestForBlack()
    {
      //var emuleResults = elulateAll(PawnListBlack);
      var searchResults = searchAndExecuteBestMove(PawnListBlack);


   /*   var emuleResultsIsvalide = true;
      var opignonList = this.GetOpignonPawnList(PawnListBlack.First().Colore);
      foreach (var opignonPawn in opignonList)
      {
        if (opignonPawn.PossibleTrips.Contains(emuleResults.destionitionPosition))
        {
          emuleResultsIsvalide = false;
          break;
        }
      }
      if (emuleResultsIsvalide)
      {
        if (searchResults.score >= emuleResults.score)
          MoveTo(searchResults.initialPosition, searchResults.destionitionPosition);
        else
        {
          MoveTo(emuleResults.initialPosition, emuleResults.destionitionPosition);
        }
      }
      else
   */
        MoveTo(searchResults.initialPosition, searchResults.destionitionPosition);
    }

    private void RunEngineForBlack_Click(object sender, RoutedEventArgs e)
    {
      
      //searchAndExecuteBestMove(PawnListBlack);
    }
  }
}
