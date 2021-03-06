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
    public MainWindow()
    {
      InitializeComponent();
      FromPosition = "";
      ToPosition = "";
      CaseList = new List<Case>();
      PawnList = new List<Pawn>();
      PawnListWhite = new List<Pawn>();
      PawnListBlack = new List<Pawn>();


      for (int x = 0; x <=7 ; x++)
      {
        for (int y = 1; y <=8; y++)
        {
          var X = Convert.ToChar(97 + x).ToString();
          var Y = y.ToString();
          var bt = (Button)this.FindName(X+Y);
          var location = Convert.ToChar(97 + x).ToString() + y.ToString();

          //initialisation des cases
          CaseList.Add(new Case(Convert.ToChar(97+x).ToString(), y.ToString(), bt,this));


          //initialisation des pions 
          //SimplePawn
         if (y==2)
            PawnListWhite.Add(new Pawn("SimplePawn", location,X,Y, bt, "White",this));
          if (y == 7)
            PawnListBlack.Add(new Pawn("SimplePawn", location, X, Y, bt, "Black", this));
    
          //Rook (Tour)
         if(y==1 && (X =="a" || X == "h"))
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
      









      //var bt = (Button) this.FindName("a8");
      //bt.Click += button1_Click;
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
    public Case GetCase(string location)
    {
      return CaseList.FirstOrDefault(x => x.CaseName == location);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var buttonSender = (Button)sender;
    }
  }
}
