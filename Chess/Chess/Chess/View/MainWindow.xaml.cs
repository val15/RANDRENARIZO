using Chess.Utils;
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
using System.IO;

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
    private int deepBlackLevel = 11;//5;//5;//4;
    private int deepWhiteLevel = 15;//1;//2;
   // private int levele = 0;

    public List<Node> Tree { get; set; }

    private string _computerColore;
    private int blackTurnNumber = 0;
    private int whiteTurnNumber = 0;
    public MainWindow()
    {
      InitializeComponent();
      WhiteRunEngineButton.IsEnabled = false;
      BlackRunEngineButton.IsEnabled = false;
      /* CurrentTurn = "White";
        WhiteTurnButton.Visibility = Visibility.Visible;
        BlackTurnButton.Visibility = Visibility.Hidden;*/

      /*CurrentTurn = "Black";
      WhiteTurnButton.Visibility = Visibility.Hidden;
      BlackTurnButton.Visibility = Visibility.Visible;*/

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
           if (y == 2 )
             PawnListWhite.Add(new Pawn("SimplePawn", location, bt, "White", this));
           if (y == 7 )
             PawnListBlack.Add(new Pawn("SimplePawn", location, bt, "Black", this));

           //Rook (Tour)
           if (y == 1 && (X == "a" || X == "h"))
             PawnListWhite.Add(new Pawn("Rook", location, bt, "White", this));
           if (y == 8 && (X == "a" || X == "h"))
             PawnListBlack.Add(new Pawn("Rook", location,bt, "Black", this));
       
           //Knight (chevalier)
          if (y == 1 && (X == "b" || X == "g") )
             PawnListWhite.Add(new Pawn("Knight", location, bt, "White", this));
          if (y == 8 && (X == "b" || X == "g"))
             PawnListBlack.Add(new Pawn("Knight", location, bt, "Black", this));

           //Bishop (fou)
           if (y == 1 && (X == "c" || X == "f"))
             PawnListWhite.Add(new Pawn("Bishop", location, bt, "White", this));
           if (y == 8 && (X == "c" || X == "f"))
             PawnListBlack.Add(new Pawn("Bishop", location, bt, "Black", this));
        
          //Queen
          

         if (y == 1 && (X == "d"))
            PawnListWhite.Add(new Pawn("Queen", location,  bt, "White", this));
          if (y == 8 && (X == "d"))
            PawnListBlack.Add(new Pawn("Queen", location, bt, "Black", this));
       
          //King
          if (y == 1 && (X == "e"))
            PawnListWhite.Add(new Pawn("King", location, bt, "White", this));
          if (y == 8 && (X == "e"))
            PawnListBlack.Add(new Pawn("King", location, bt, "Black", this));
     
          
        }
      }
      //TEST
      // PawnListBlack.Add(new Pawn("SimplePawn", "e5", "e", "5", (Button)this.FindName("e5"), "Black", this));

      // PawnListWhite.Add(new Pawn("SimplePawn", "c4", "c", "4", (Button)this.FindName("c4"), "White", this));
      //PawnListWhite.Add(new Pawn("King", "e1", "e", "1", (Button)this.FindName("e1"), "White", this));
     // PawnListBlack.Add(new Pawn("Queen", "d8", "d", "8", (Button)this.FindName("d8"), "Black", this));
      //PawnListWhite.Add(new Pawn("Queen", "e2", "e", "2", (Button)this.FindName("e2"), "White", this));
//      PawnListBlack.Add(new Pawn("King", "e8", "e", "8", (Button)this.FindName("e8"), "Black", this));

      //PawnListBlack.Add(new Pawn("Knight", "b8", "b", "8", (Button)this.FindName("b8"), "Black", this));

      //  PawnListWhite.Add(new Pawn("Knight", "f3", "f", "3", (Button)this.FindName("f3"), "White", this));
      //PawnListWhite.Add(new Pawn("Queen", "b1", "b", "1", (Button)this.FindName("b1"), "White", this));
      // PawnListWhite.Add(new Pawn("Knight", "d6", "d", "6", (Button)this.FindName("d6"), "White", this));


      /*PawnListBlack.Add(new Pawn("Rook", "a8", "a", "8", (Button)this.FindName("a8"), "Black", this));
      PawnListBlack.Add(new Pawn("Knight", "b8", "b", "8", (Button)this.FindName("b8"), "Black", this));
      PawnListBlack.Add(new Pawn("King", "e8", "e", "8", (Button)this.FindName("e8"), "Black", this));
      PawnListBlack.Add(new Pawn("Bishop", "f8", "f", "8", (Button)this.FindName("e8"), "Black", this));*/


      PawnList.AddRange(PawnListBlack);
      PawnList.AddRange(PawnListWhite);

      FillAllPossibleTrips();
    }


    public async Task SwithTurnAsync()
    {
      if(CurrentTurn == "White")
      {
        whiteTurnNumber++;
        CurrentTurn = "Black";
        BlackTurnButton.Visibility = Visibility.Visible;
        WhiteTurnButton.Visibility = Visibility.Hidden;
        if(_computerColore == CurrentTurn)
        {
          await Task.Delay(100);
          GetBestPositionAndMoveFor(CurrentTurn);
        }
        


      }
      else
      {
        blackTurnNumber++;
        CurrentTurn = "White";
        WhiteTurnButton.Visibility = Visibility.Visible;
        BlackTurnButton.Visibility = Visibility.Hidden;
        //searchAndExecuteBestMove(PawnListBlack);
        if (_computerColore == CurrentTurn)
        {
          await Task.Delay(100);
          GetBestPositionAndMoveFor(CurrentTurn);
        }
      
      }

      Save();



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

    public void SetOldPositionColore(string location)
    {
      var oldCase = GetCase(location);
      oldCase.SetOldPositionColore();
     
    }
    public Pawn GetPawn(string location)
    {
      var result = PawnList.FirstOrDefault(x => x.Location == location);
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


    private void GenereTree(string colore,int deepLevel)
    {
      Tree = null;
      Tree = new List<Node>();
      //Tree.Clear();
      DebugTextBlock.Text = "";
      var computerPawnList = PawnList.Where(x => x.Colore == colore);


      foreach (var pawn in computerPawnList)
      {
        //var deep = 0;


        Node newNode = new Node();
        newNode.Location = pawn.Location;
        newNode.OldPositionName = "";
        newNode.Weight = -10000000;
        newNode.Level = 0;
        newNode.Colore = colore;
        newNode.AssociatePawn = pawn;

        Tree.Add(newNode);

      
          
        //pawn.EvaluateScorePossibleTrips();



        for (int i = 0; i < pawn.PossibleTrips.Count; i++)
        {
          //deep++;

          GenerateThread(pawn.Location, pawn.PossibleTrips[i], PawnList, colore, newNode, pawn, deepLevel);
          deepStep = 0;
        }
       


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

      foreach (var node in Tree/*.Where(x=>x.Colore=="White" && x.AssociatePawn.Name == "Queen")*/.OrderBy(x=>x.Level))
      {
        /*if(node.Location=="f1")
        {*/
        Debug.WriteLine(node.Location);
        DebugTextBlock.Text += "postition : " + node.Location + "   score : " + node.Weight.ToString() + "  level : "  + node.Level +
           "    number of child : " + node.ChildList.Count() + "    OldPosition : " + node.OldPositionName + "   parent : "+node.Parent?.Location+"   Colore : " + node.Colore  + "    Pawn : " + node.AssociatePawn?.Name + "   BSP : " + node?.BestChildPosition +"\n";

        //}
      }
    }

    private int evaluateScoreForBlack(string colore, List<Pawn> actualPawnList,Pawn movingPawn)
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
      /*var tblackScore = blackScore;
      var twhiteScore = whiteScore;*/
      if (movingPawn.Colore == colore)
        return  blackScore - whiteScore;
      else
        return whiteScore - blackScore ;
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

      if (movingPawn.Colore == colore)
        return whiteScore - blackScore;
      else
        return blackScore - whiteScore;


    }


    private void MinMax()
    {
      for (int i = Tree.Count - 1; i >= 0; i--)
      {
        var node = Tree[i];
        var parent = Tree[i].Parent;
        if (parent == null)
          continue;
        /*if (parent.Weight <= node.Weight)
        {
          parent.Weight = node.Weight;
          parent.BestChildPosition = node.Location;
        }*/
        parent.ChildList.Add(node);
        if((node.Level%2) != 0)//Max
        {
          //on remonte le max
          if (parent.Weight < node.Weight)
          {
            parent.Weight = node.Weight-1;
            if (parent.Level == 0)
              parent.BestChildPosition = node.Location;

          }
            

        }
        else //Min
        {
          //on remonte le min
          if (parent.Weight > node.Weight)
          {
            parent.Weight = node.Weight-1;
            
          }
            
        }



      }

    }
    private void GenerateThread(string initialPosition, string evaluatePosition, List<Pawn> actualPawnList,string actualColore, Node parentNode,Pawn associatePawn,int deepLevel)
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
      //selectedPawn.EvaluateScorePossibleTrips();

      Node newNode = new Node();
      newNode.Location = evaluatePosition;
      newNode.OldPositionName = initialPosition;
      newNode.Colore = actualColore;
      newNode.Parent = parentNode;
    //  newNode.Parent = parentNode;
      newNode.Level = parentNode.Level + 1;
      newNode.AssociatePawn = associatePawn;
      if (CurrentTurn == "White")
        newNode.Weight = evaluateScoreForWhite(actualColore, depPawnsList, selectedPawn);
      else
        newNode.Weight = evaluateScoreForBlack(actualColore, depPawnsList, selectedPawn);

      ;

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

      var opignionPawnList = new List<Pawn>();
      foreach (var item in depPawnsList)
      {
        if (item.Colore != actualColore)
          opignionPawnList.Add(item);
      }

    



      //var deepLevel = 0;
      /*if(actualColore == "White")
        deepLevel = deepWhiteLevel ;
      if(actualColore == "Black")
        deepLevel = deepBlackLevel;*/
      deepStep++;
      if (deepStep < deepLevel)
      {
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
            GenerateThread(pawn.Location, pawn.PossibleTrips[i], depPawnsList, pawn.Colore, newNode, pawn,deepLevel);
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





    private Node GetBestNodePostion()
    {
      var maxWeight = Tree.Where(x => x.Level == 0).OrderByDescending(x => x.Weight).First().Weight;
      var bestNodeList = Tree.Where(x => x.Level == 0 && x.Weight == maxWeight);
      Node bestNode = new Node();
      if(bestNodeList.Count()==1)
      {
        bestNode = bestNodeList.First();
           //var bestNode = zeroLeveleNode.OrderBy(x => x.ChildList .Count).First();
        

      }
      else
      {
        Random rnd = new Random();
        int index = rnd.Next(0, bestNodeList.Count());
        bestNode = bestNodeList.ElementAt(index);
        
      }
      var blackScore = GetScore("Black");
      var whiteScore = GetScore("White");
      lbInfo.Content = "Best node for "+ bestNode.Colore +" : " + bestNode.AssociatePawn.Name + "  " + bestNode.Weight +"Position : "+ bestNode.Location+ " to " + bestNode.BestChildPosition
        +"    Black score : "+ blackScore+" ("+blackTurnNumber+" turn) "+"    White score : "+ whiteScore+" ("+ whiteTurnNumber + " turn)";
      return bestNode;

    }

    private int GetScore(string colore)
    {
      var result = 0;
      if(colore=="Black")
      {
        foreach (var item in PawnListBlack)
        {
          result += item.Value;
        }
      }
      else
      {
        foreach (var item in PawnListWhite)
        {
          result += item.Value;
        }
      }
      return result;
      
    }
    private Node GetBestPositionAndMoveFor(string colore)
    {
      Tree = null;
      Tree = new List<Node>();
      if (colore=="White")
        GenereTree(colore, deepWhiteLevel);
      if (colore == "Black")
        GenereTree(colore, deepBlackLevel);
      var bestNode = GetBestNodePostion();
      MoveTo(bestNode.Location, bestNode.BestChildPosition);
      return bestNode;
    }
    private async void RunEngineForWhite_Click(object sender, RoutedEventArgs e)
    {

      /* CurrentTurn = "White";
      var bestNode = GetBestPositionAndMoveFor("White");
       */

      /*if (bestNode.Colore == CurrentTurn)//bug, on load
      {
        Load();
        GetBestPositionAndMoveFor(CurrentTurn);
      }*/
      // _computerColore = "White";

      // Save();

      _computerColore = "";

      while (true)
      {

        await Task.Delay(1);
        GetBestPositionAndMoveFor("White");
        await Task.Delay(1);
        var bestNode = GetBestPositionAndMoveFor("Black");


        if (bestNode.Colore == CurrentTurn)//bug, on load
        {
          Load();
          GetBestPositionAndMoveFor(CurrentTurn);
        }

        Save();




      }

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

    private async void RunEngineForBlack_Click(object sender, RoutedEventArgs e)
    {
      /*  CurrentTurn = "Black";
      var bestNode =GetBestPositionAndMoveFor("Black");
        */

      /* if (bestNode.Colore == CurrentTurn)//bug, on load
       {
         Load();
         GetBestPositionAndMoveFor(CurrentTurn);
       }*/
      //  _computerColore = "Black";

      // Save();
      _computerColore = "";

       while (true)
       {
        
        await Task.Delay(1);
         GetBestPositionAndMoveFor("Black");
       await Task.Delay(1);
      var bestNode= GetBestPositionAndMoveFor("White");


        if(bestNode.Colore == CurrentTurn)//bug, on load
        {
          Load();
          GetBestPositionAndMoveFor(CurrentTurn);
        }
       
        Save();

      }



    }

    private void ChoiseButon_Click(object sender, RoutedEventArgs e)
    {

      
    }

    private void WhiteFirstButon_Click(object sender, RoutedEventArgs e)
    {
      CurrentTurn = "White";
      //GetBestPositionAndMoveFor("Black");
      WhiteTurnButton.Visibility = Visibility.Visible;
      BlackTurnButton.Visibility = Visibility.Hidden; 
     //  _computerColore = "Black";
      WhiteRunEngineButton.IsEnabled = true;
      BlackRunEngineButton.IsEnabled = true;

      if (_computerColore == "Black")
        return;
      if(!String.IsNullOrEmpty(_computerColore))
      {
        var bestNode = GetBestPositionAndMoveFor(_computerColore);
        Save();
      }
      
    }

    private void BlackFirstButon_Click(object sender, RoutedEventArgs e)
    {
      CurrentTurn = "Black";
      WhiteTurnButton.Visibility = Visibility.Hidden;
      BlackTurnButton.Visibility = Visibility.Visible; 
       //GetBestPositionAndMoveFor("White");
     //  _computerColore = "White";
      WhiteRunEngineButton.IsEnabled = true;
      BlackRunEngineButton.IsEnabled = true;

      if (_computerColore == "White")
        return;
      if (!String.IsNullOrEmpty(_computerColore))
      {
        var bestNode = GetBestPositionAndMoveFor(_computerColore);
        Save();
      }


    }



    private void saveButton_Click(object sender, RoutedEventArgs e)
    {

      Save();
    }

    public void Save()
    {
      using (var writer = new StreamWriter("./WHITEList.txt"))
      {

        foreach (var pawn in PawnListWhite)
        {
          writer.WriteLine($"{pawn.Name};{pawn.Location};{pawn.Colore}");
        }
      }
      using (var writer = new StreamWriter("./BLACKList.txt"))
      {

        foreach (var pawn in PawnListBlack)
        {
          writer.WriteLine($"{pawn.Name};{pawn.Location};{pawn.Colore}");
        }
      }
    }


    private void CleanPawnList()
    {
      foreach (var  pawn in PawnList)
      {
        pawn.Clean();
      }
    

      PawnListWhite.Clear();
      PawnListBlack.Clear();
      PawnList.Clear();
    }

    private void Load()
    {
      CleanPawnList();
      var readText = File.ReadAllText("./WHITEList.txt");

      using (StringReader sr = new StringReader(readText))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          //Debug.WriteLine(line);

          var datas = line.Split(';');
          var bt = (Button)this.FindName(datas[1]);
          PawnListWhite.Add(new Pawn(datas[0], datas[1], bt, datas[2], this));

        }
      }

      readText = File.ReadAllText("./BLACKList.txt");

      using (StringReader sr = new StringReader(readText))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          //Debug.WriteLine(line);

          var datas = line.Split(';');
          var bt = (Button)this.FindName(datas[1]);
          PawnListBlack.Add(new Pawn(datas[0], datas[1], bt, datas[2], this));

        }
      }

      PawnList.AddRange(PawnListBlack);
      PawnList.AddRange(PawnListWhite);

      FillAllPossibleTrips();
    }

    private void loadButton_Click(object sender, RoutedEventArgs e)
    {
      Load();
    }

    private void ChoseWhiteForCoputerButon_Click(object sender, RoutedEventArgs e)
    {
      _computerColore = "White";
      ChoseBlackForCoputerButon.IsEnabled = false;

    }

    private void ChoseBlackForCoputerButon_Click(object sender, RoutedEventArgs e)
    {
      _computerColore = "Black";
      ChoseWhiteForCoputerButon.IsEnabled = false;
    }
  }
}
