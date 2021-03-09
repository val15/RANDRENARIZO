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
          if (y == 2)
            PawnListWhite.Add(new Pawn("SimplePawn", location, X, Y, bt, "White", this));
          if (y == 7)
            PawnListBlack.Add(new Pawn("SimplePawn", location, X, Y, bt, "Black", this));
        
          //Rook (Tour)
          if (y == 1 && (X == "a" || X == "h"))
            PawnListWhite.Add(new Pawn("Rook", location, X, Y, bt, "White", this));
          if (y == 8 && (X == "a" || X == "h"))
            PawnListBlack.Add(new Pawn("Rook", location, X, Y, bt, "Black", this));
         
          //Knight (chevalier)
          if (y == 1 && (X == "b" || X == "g"))
            PawnListWhite.Add(new Pawn("Knight", location, X, Y, bt, "White", this));
         if (y == 8 && (X == "b" || X == "g"))
            PawnListBlack.Add(new Pawn("Knight", location, X, Y, bt, "Black", this));
      
          //Bishop (fou)
          if (y == 1 && (X == "c" || X == "f"))
            PawnListWhite.Add(new Pawn("Bishop", location, X, Y, bt, "White", this));
          if (y == 8 && (X == "c" || X == "f"))
            PawnListBlack.Add(new Pawn("Bishop", location, X, Y, bt, "Black", this));
       
          //Queen
          if (y == 1 && (X == "d"))
            PawnListWhite.Add(new Pawn("Queen", location, X, Y, bt, "White", this));
          if (y == 8 && (X == "d"))
            PawnListBlack.Add(new Pawn("Queen", location, X, Y, bt, "Black", this));
       
          //King
          if (y == 1 && (X == "e"))
            PawnListWhite.Add(new Pawn("King", location, X, Y, bt, "White", this));
          if (y == 8 && (X == "e"))
            PawnListBlack.Add(new Pawn("King", location, X, Y, bt, "Black", this));
     

        }
      }
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
      
      
      if (result==null)
      {
        Debug.WriteLine("NULL");
      }
      else
      {
        var l = result.Location;
        Debug.WriteLine(l);
      }
        



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

    private void searchAndExecuteBestMove(List<Pawn> pawnList)
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


      MoveTo(((Pawn)bestPawn).Location, allBestPosition);
    }


    private async void RunEngineForWhite_Click(object sender, RoutedEventArgs e)
    {


      // tbkLabel.Text = "two seconds delay";

      //searchAndExecuteBestMove(PawnListWhite);


      while (true)
      {
        await Task.Delay(200);
        searchAndExecuteBestMove(PawnListWhite);
        await Task.Delay(200);
        searchAndExecuteBestMove(PawnListBlack);
      }
      
    }

    private void RunEngineForBlack_Click(object sender, RoutedEventArgs e)
    {
      searchAndExecuteBestMove(PawnListBlack);
    }
  }
}
