using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Chess.Utils
{
  public class Pawn
  {

    

   public MainWindow MainWindowParent { get; set; }
    public string Name { get; set; }
    public string Location { get; set; } //position ex "a2" "a1" ...
    public string X { get; set; } //a-h

    public string Y { get; set; }//1-8
    public List<string> PossibleTrips { get; set; } //Les déplacement possible: liste des positions possible

    public string Image { get; set; }
    public string Colore { get; set; }
    public Button AssociateButton { get; set; }

    public Boolean IsSelected { get; set; }

    private DockPanel _dockPanel;
    private Image _image;
    private bool _isFirstMove;

    public Pawn(string name,string location,string x,string y , Button associateButton,string colore, MainWindow mainWindowParent)
    {
      IsSelected = false;

      _isFirstMove = true;
      MainWindowParent = mainWindowParent;
      Name = name;
      Colore = colore;
      Location = location;
      X = x;
      Y = y;
      AssociateButton = associateButton;
      _dockPanel = new DockPanel();
      _image = new Image();
      _image.Height = 60;
      _image.Width = 60;
      _dockPanel.Background = Brushes.DarkCyan;
      if (Colore== "White")
        _dockPanel.Background = Brushes.White;


      _image.Source = new BitmapImage(new Uri(@"/Images/"+Name+".png", UriKind.Relative));
        _dockPanel.Children.Add(_image);
        AssociateButton.Content = _dockPanel;

      PossibleTrips = new List<string>();
     // fillPossibleTrips();




      /* Image image = new Image();
       image.Source = new BitmapImage(new Uri(@"Images\SimplePawn.png", UriKind.Relative));
       AssociateButton.Content = image;*/

      //<DockPanel Margin="0,0,0,0">
      //  <Image  Height="59"  Width="58" Source="../Images/SimplePawn.png"/>
      //</DockPanel>


      //AssociateButton.Click += buttonCase_Click;

    }

    public void FillPossibleTrips()
    {
      PossibleTrips.Clear();
      if(this.Name== "SimplePawn")
        fillPossibleTripsSimplePawn(this.Colore);
      if (this.Name == "Knight")
        fillPossibleTripsKnight();
      if (this.Name == "Rook")
        fillPossibleTripsRook(); 
      if (this.Name == "Bishop")
        fillPossibleTripsBishop(); 
      if (this.Name == "Queen")
        fillPossibleTripsQueen();//King
      if (this.Name == "King")
        fillPossibleTripsKing();
    }

    private void fillPossibleTripsSimplePawn(string colore)
    {
      var toAdd = 0;
      if(Colore == "White")
      {
        toAdd = +1;
        
      }
      else
      {
        toAdd = -1;
      }

      PossibleTrips.Add(X + (Int32.Parse(Y) + (toAdd)).ToString());
      if (_isFirstMove)
        PossibleTrips.Add(X + (Int32.Parse(Y) + (toAdd) + (toAdd)).ToString());


    }
    private void fillPossibleTripsKnight()
    {
      var avalablesPositionList = new List<string>();
        
      var xasciiCode = (int)Convert.ToChar(X);
      avalablesPositionList.Add(Convert.ToChar(xasciiCode - 1).ToString() + (Int32.Parse(Y) + 2).ToString());
      avalablesPositionList.Add(Convert.ToChar(xasciiCode + 1).ToString() + (Int32.Parse(Y) + 2).ToString());

      avalablesPositionList.Add(Convert.ToChar(xasciiCode + 2).ToString() + (Int32.Parse(Y) - 1).ToString());
      avalablesPositionList.Add(Convert.ToChar(xasciiCode + 2).ToString() + (Int32.Parse(Y) + 1).ToString());

      avalablesPositionList.Add(Convert.ToChar(xasciiCode - 1).ToString() + (Int32.Parse(Y) - 2).ToString());
      avalablesPositionList.Add(Convert.ToChar(xasciiCode + 1).ToString() + (Int32.Parse(Y) - 2).ToString());

      avalablesPositionList.Add(Convert.ToChar(xasciiCode - 2).ToString() + (Int32.Parse(Y) - 1).ToString());
      avalablesPositionList.Add(Convert.ToChar(xasciiCode - 2).ToString() + (Int32.Parse(Y) + 1).ToString());
 
      foreach (var item in avalablesPositionList)
      {
        PossibleTrips.Add(item);
      }
    }

    private void fillPossibleTripsRook()
    {
      var avalablesPositionList = new List<string>();
      for (int c = 1; c <= 8; c++)
      {
        //var vertical
       /* var firstAlierPosition = MainWindowParent.PawnListWithe.FirstOrDefault(x => x.Location == (X + c.ToString()));
        if (firstAlierPosition != null)
          return;*/
        avalablesPositionList.Add(X + c.ToString());
        avalablesPositionList.Add(Convert.ToChar(96 + c).ToString() + Y);
      }
       avalablesPositionList.Remove(this.Location);
     

      //GESTION DES COLITIONS
      if(Colore == "White")
      {
        //on cherche la position du premier pion alier sur les position lavile
        //on cherche le pion alier le plus proche
        //pour Y

        //var intY = (Int32.Parse(this.Y));
        //var yList = MainWindowParent.PawnListWhite.Where(x=> x.Location != this.Location && x.Location.Contains(X));

        var faAsk = MainWindowParent.PawnListWhite.OrderBy(x=>x.Y).FirstOrDefault(x => x.Location.Contains(X) && x.Location != this.Location);
        var faAskTint = (Int32.Parse(faAsk.Y));
        // if(faAsk)
        if(faAskTint > Int32.Parse(Y))
        {
          for (int c = faAskTint; c <= 8; c++)
          {
            // var toRemove = MainWindowParent.GetPawn(fa.X + c.ToString() );
            avalablesPositionList.Remove(faAsk.X + c.ToString());
          }
        }
          
        
       /* var yInt = (Int32.Parse(Y));
        for (int c = faTint; c >= 0; c--)
        {
          // var toRemove = MainWindowParent.GetPawn(fa.X + c.ToString() );
          avalablesPositionList.Remove(fa.X + c.ToString());
        }*/



        //pour X
        /*  fa = MainWindowParent.PawnListWhite.OrderBy(x => x.X).FirstOrDefault(x => x.Location.Contains(Y) && x.Location != this.Location);
        // var faTint = (Int32.Parse(fa.Y));
         for (int c = faTint; c <= 8; c++)
         {
           // var toRemove = MainWindowParent.GetPawn(fa.X + c.ToString() );
           avalablesPositionList.Remove(X + Convert.ToChar(97 + c).ToString());
         }
        */
        //on enleve a 2 à 8
        /* for (int i = 0; i < length; i++)
         {
           var toRemove= X + c.ToString()
           avalablesPositionList
         }
         */

      }


      foreach (var item in avalablesPositionList)
      {
        PossibleTrips.Add(item);
      }
    }
    private void fillPossibleTripsBishop()
    {
      var avalablesPositionList = new List<string>();
      var xasciiCode = (int)Convert.ToChar(X);
       for (int c = 0; c <=8; c++)
        avalablesPositionList.Add(Convert.ToChar(xasciiCode + c).ToString() + (Int32.Parse(Y) + c).ToString());

      for (int c = 0; c <= 8; c++)
        avalablesPositionList.Add(Convert.ToChar(xasciiCode - c).ToString() + (Int32.Parse(Y) + c).ToString());

      for (int c = 8; c > 0; c--)
        avalablesPositionList.Add(Convert.ToChar(xasciiCode - c).ToString() + (Int32.Parse(Y) - c).ToString());
      for (int c = 8; c > 0; c--)
        avalablesPositionList.Add(Convert.ToChar(xasciiCode + c).ToString() + (Int32.Parse(Y) - c).ToString());


      avalablesPositionList.Remove(this.Location);
      foreach (var item in avalablesPositionList)
      {
        PossibleTrips.Add(item);
      }
    }


    private void fillPossibleTripsQueen()
    {
      var avalablesPositionList = new List<string>();
      for (int c = 1; c <= 8; c++)
      {
        avalablesPositionList.Add(X + c.ToString());
        avalablesPositionList.Add(Convert.ToChar(96 + c).ToString() + Y);
      }

      var xasciiCode = (int)Convert.ToChar(X);
      for (int c = 0; c <= 8; c++)
        avalablesPositionList.Add(Convert.ToChar(xasciiCode + c).ToString() + (Int32.Parse(Y) + c).ToString());

      for (int c = 0; c <= 8; c++)
        avalablesPositionList.Add(Convert.ToChar(xasciiCode - c).ToString() + (Int32.Parse(Y) + c).ToString());

      for (int c = 8; c > 0; c--)
        avalablesPositionList.Add(Convert.ToChar(xasciiCode - c).ToString() + (Int32.Parse(Y) - c).ToString());
      for (int c = 8; c > 0; c--)
        avalablesPositionList.Add(Convert.ToChar(xasciiCode + c).ToString() + (Int32.Parse(Y) - c).ToString());


      avalablesPositionList.Remove(this.Location);
      foreach (var item in avalablesPositionList)
      {
        PossibleTrips.Add(item);
      }
    }

    private void fillPossibleTripsKing()
    {
      var avalablesPositionList = new List<string>();
      for (int c = 1; c <= 8; c++)
      {
        avalablesPositionList.Add(X + (Int32.Parse(Y) + 1).ToString());
        avalablesPositionList.Add(X + (Int32.Parse(Y) - 1).ToString());
      }

      var xasciiCode = (int)Convert.ToChar(X);
      avalablesPositionList.Add(Convert.ToChar(xasciiCode + 1).ToString() + (Int32.Parse(Y) + 1).ToString());
      avalablesPositionList.Add(Convert.ToChar(xasciiCode - 1).ToString() + (Int32.Parse(Y) + 1).ToString());
      avalablesPositionList.Add(Convert.ToChar(xasciiCode - 1).ToString() + (Int32.Parse(Y) - 1).ToString());
      avalablesPositionList.Add(Convert.ToChar(xasciiCode + 1).ToString() + (Int32.Parse(Y) - 1).ToString());
      avalablesPositionList.Add(Convert.ToChar(xasciiCode - 1).ToString() + (Int32.Parse(Y)).ToString());
      avalablesPositionList.Add(Convert.ToChar(xasciiCode + 1).ToString() + (Int32.Parse(Y)).ToString());

      avalablesPositionList.Remove(this.Location);
      foreach (var item in avalablesPositionList)
      {
        PossibleTrips.Add(item);
      }
    }


    private void buttonCase_Click(object sender, EventArgs e)
    {
      // var buttonSender = (Button)sender;

      // on reitialise toutes les case
      MainWindowParent.SetDefaultColoreAllCases();
      IsSelected =! IsSelected;
      if(!IsSelected)
      {
        MainWindowParent.SelectedPawn = null;
        return;
      }
      MainWindowParent.SelectedPawn = this;
      foreach (var item in PossibleTrips)
      {
        var avalableButton = (Button)MainWindowParent.FindName(item);
        avalableButton.Background = Brushes.Yellow;
        
      }

      //colorer les PossibleTrips
    }

    public void ColorAvaleblesCases()
    {
      // var buttonSender = (Button)sender;

      // on reitialise toutes les case
      MainWindowParent.SetDefaultColoreAllCases();
      /*IsSelected = !IsSelected;
      if (!IsSelected)
      {
        MainWindowParent.SelectedPawn = null;
        return;
      }*/
      MainWindowParent.SelectedPawn = this;
      foreach (var item in PossibleTrips)
      {
        var avalableButton = (Button)MainWindowParent.FindName(item);
        if (avalableButton != null)
          avalableButton.Background = Brushes.Yellow;

      }

      //colorer les PossibleTrips
    }

    public void Move(Case newCase)//quan on déplace un pion, en fonction de sa position; 
    {

      

      //this.Location = newCase.CaseName;

      //this.AssociateButton = newCase.ButtonCase;
      _image = null;
      _image = new Image();
      _image.Height = 60;
      _image.Width = 60;
      _image.Source = new BitmapImage(new Uri(@"/Images/" + Name + ".png", UriKind.Relative));
      var oldColor = _dockPanel.Background;
      _dockPanel.Children.Clear();
      _dockPanel = null;
      _dockPanel = new DockPanel();
      _dockPanel.Background = oldColor;
      _dockPanel.Children.Add(_image);
      //AssociateButton.Background = newCase.ButtonCase.Background;
      //AssociateButton.Content = _dockPanel;
      newCase.ButtonCase.Content = _dockPanel;
      //newCase.ButtonCase.Background = Brushes.Black;


       //MainWindowParent.SetDefaultColoreAllCases();
       //X = newCase.X;
       //Y = newCase.Y;
      _isFirstMove = false;
      this.Location = newCase.CaseName;
      this.X = newCase.X;
      this.Y = newCase.Y;

      //FillPossibleTrips();

      Debug.WriteLine("nove: new position = " + this.Location);

      MainWindowParent.FillAllPossibleTrips();



      //MainWindowParent.SelectedPawn = null;

      // AssociateButton.Click += buttonCase_Click;

      //les PossibleTrips change en fonction du type du pion et de la couleur


    }

  }
}
