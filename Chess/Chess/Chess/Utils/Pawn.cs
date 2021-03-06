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

      var xasciiCode = (int)Convert.ToChar(X);
      var intY = Int32.Parse(Y);

      var tripsPosition = X + (intY +(toAdd)).ToString();
      var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (pawnInTrips == null)
      {
        PossibleTrips.Add(tripsPosition);
      }
      if (_isFirstMove)
      {
        tripsPosition = X + (intY + (toAdd)+ (toAdd)).ToString();
        pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (pawnInTrips == null)
        {
          PossibleTrips.Add(tripsPosition);
        }
      }

      //pour les attaques des pions
      tripsPosition = Convert.ToChar(xasciiCode -1 ).ToString() + (intY + toAdd).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (pawnInTrips != null)
      {
        PossibleTrips.Add(tripsPosition);
      }
      tripsPosition = Convert.ToChar(xasciiCode + 1).ToString() + (intY + toAdd).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (pawnInTrips != null)
      {
        PossibleTrips.Add(tripsPosition);
      }








    }
    private void fillPossibleTripsKnight()
    {
      var avalablesPositionList = new List<string>();
        

      var xasciiCode = (int)Convert.ToChar(X);
      var intY = Int32.Parse(Y);


      var tripsPosition = Convert.ToChar(xasciiCode - 1).ToString() + (intY + 2).ToString();
      var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (pawnInTrips == null)
      {
        avalablesPositionList.Add(tripsPosition);
      }
      else
      {
        if (pawnInTrips.Colore != this.Colore)//pion alier
        {
          avalablesPositionList.Add(tripsPosition);
        }
      }
       tripsPosition = Convert.ToChar(xasciiCode + 1).ToString() + (intY + 2).ToString();
       pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (pawnInTrips == null)
      {
        avalablesPositionList.Add(tripsPosition);
      }
      else
      {
        if (pawnInTrips.Colore != this.Colore)//pion alier
        {
          avalablesPositionList.Add(tripsPosition);
        }
      }
      tripsPosition = Convert.ToChar(xasciiCode + 2).ToString() + (intY -1).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (pawnInTrips == null)
      {
        avalablesPositionList.Add(tripsPosition);
      }
      else
      {
        if (pawnInTrips.Colore != this.Colore)//pion alier
        {
          avalablesPositionList.Add(tripsPosition);
        }
      }
      tripsPosition = Convert.ToChar(xasciiCode + 2).ToString() + (intY + 1).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (pawnInTrips == null)
      {
        avalablesPositionList.Add(tripsPosition);
      }
      else
      {
        if (pawnInTrips.Colore != this.Colore)//pion alier
        {
          avalablesPositionList.Add(tripsPosition);
        }
      }

      tripsPosition = Convert.ToChar(xasciiCode - 1).ToString() + (intY -2 ).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (pawnInTrips == null)
      {
        avalablesPositionList.Add(tripsPosition);
      }
      else
      {
        if (pawnInTrips.Colore != this.Colore)//pion alier
        {
          avalablesPositionList.Add(tripsPosition);
        }
      }

      tripsPosition = Convert.ToChar(xasciiCode + 1).ToString() + (intY - 2).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (pawnInTrips == null)
      {
        avalablesPositionList.Add(tripsPosition);
      }
      else
      {
        if (pawnInTrips.Colore != this.Colore)//pion alier
        {
          avalablesPositionList.Add(tripsPosition);
        }
      }

      tripsPosition = Convert.ToChar(xasciiCode - 2).ToString() + (intY - 1).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (pawnInTrips == null)
      {
        avalablesPositionList.Add(tripsPosition);
      }
      else
      {
        if (pawnInTrips.Colore != this.Colore)//pion alier
        {
          avalablesPositionList.Add(tripsPosition);
        }
      }

      tripsPosition = Convert.ToChar(xasciiCode - 2).ToString() + (intY + 1).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (pawnInTrips == null)
      {
        avalablesPositionList.Add(tripsPosition);
      }
      else
      {
        if (pawnInTrips.Colore != this.Colore)//pion alier
        {
          avalablesPositionList.Add(tripsPosition);
        }
      }

     PossibleTrips.AddRange(avalablesPositionList);

    }

    private void fillPossibleTripsRook()
    {
      var avalablesPositionList = new List<string>();

      var intY = Int32.Parse(Y);
      for (int i = intY+1; i <= 8; i++)
      {
        var tripsPosition = (X) + i.ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if(pawnInTrips== null)
        {
          avalablesPositionList.Add(tripsPosition);
          continue;
        }
        if(pawnInTrips.Colore == this.Colore)//pion alier
        {
          break;
        }
        else
        {
          avalablesPositionList.Add(tripsPosition);
          break;
        }
      }
      for (int i = intY - 1; i > 0; i--)
      {
        var tripsPosition = (X) + i.ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (pawnInTrips == null)
        {
          avalablesPositionList.Add(tripsPosition);
          continue;
        }
        if (pawnInTrips.Colore == this.Colore)//pion alier
        {
          break;
        }
        else
        {
          avalablesPositionList.Add(tripsPosition);
          break;
        }
      }


      var xasciiCode = (int)Convert.ToChar(X);
      for (int i = 1; i <= 97+8; i++)
      {
        var tripsPosition = (Convert.ToChar(xasciiCode + i)).ToString() + Y;
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (pawnInTrips == null)
        {
          avalablesPositionList.Add(tripsPosition);
          continue;
        }
        if (pawnInTrips.Colore == this.Colore)//pion alier
        {
          break;
        }
        else
        {
          avalablesPositionList.Add(tripsPosition);
          break;
        }
      }
      for (int i = xasciiCode-1; i >= 97; i--)
      {
        var tripsPosition = (Convert.ToChar(i)).ToString() + Y;
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (pawnInTrips == null)
        {
          avalablesPositionList.Add(tripsPosition);
          continue;
        }
        if (pawnInTrips.Colore == this.Colore)//pion alier
        {
          break;
        }
        else
        {
          avalablesPositionList.Add(tripsPosition);
          break;
        }
      }


     PossibleTrips.AddRange(avalablesPositionList);
    }
    private void fillPossibleTripsBishop()
    {
      var avalablesPositionList = new List<string>();
      var xasciiCode = (int)Convert.ToChar(X);
      var intY = Int32.Parse(Y);

      for (int i = 1, j = intY ; i <= 97 + 8 ||  j <= 8; i++,j++)
      {
        var tripsPosition = Convert.ToChar(xasciiCode + i).ToString() + (j+1).ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (pawnInTrips == null)
        {
          avalablesPositionList.Add(tripsPosition);
          continue;
        }
        if (pawnInTrips.Colore == this.Colore)//pion alier
        {
          break;
        }
        else
        {
          avalablesPositionList.Add(tripsPosition);
          break;
        }
      }
      for (int i = xasciiCode - 1, j = intY; i >= 97 || j >= 1 ; i--, j--)
      {
        var tripsPosition = (Convert.ToChar(i)).ToString() + (j - 1).ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (pawnInTrips == null)
        {
          avalablesPositionList.Add(tripsPosition);
          continue;
        }
        if (pawnInTrips.Colore == this.Colore)//pion alier
        {
          break;
        }
        else
        {
          avalablesPositionList.Add(tripsPosition);
          break;
        }
      }

      for (int i = xasciiCode - 1, j = intY; i >= 97 || j <= 8  ; i--, j++)
      {
        var tripsPosition = (Convert.ToChar(i)).ToString() + (j + 1).ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (pawnInTrips == null)
        {
          avalablesPositionList.Add(tripsPosition);
          continue;
        }
        if (pawnInTrips.Colore == this.Colore)//pion alier
        {
          break;
        }
        else
        {
          avalablesPositionList.Add(tripsPosition);
          break;
        }
      }

      for (int i = 1, j = intY; i <= 97 || j >= 8; i++, j--)
      {
        var tripsPosition = Convert.ToChar(xasciiCode + i).ToString() + (j - 1).ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (pawnInTrips == null)
        {
          avalablesPositionList.Add(tripsPosition);
          continue;
        }
        if (pawnInTrips.Colore == this.Colore)//pion alier
        {
          break;
        }
        else
        {
          avalablesPositionList.Add(tripsPosition);
          break;
        }
      }



      PossibleTrips.AddRange(avalablesPositionList);
    }


    private void fillPossibleTripsQueen()
    {
      var avalablesPositionList = new List<string>();
      var xasciiCode = (int)Convert.ToChar(X);
      var intY = Int32.Parse(Y);

      for (int i = intY + 1; i <= 8; i++)
      {
        var tripsPosition = (X) + i.ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (pawnInTrips == null)
        {
          avalablesPositionList.Add(tripsPosition);
          continue;
        }
        if (pawnInTrips.Colore == this.Colore)//pion alier
        {
          break;
        }
        else
        {
          avalablesPositionList.Add(tripsPosition);
          break;
        }
      }
      for (int i = intY - 1; i > 0; i--)
      {
        var tripsPosition = (X) + i.ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (pawnInTrips == null)
        {
          avalablesPositionList.Add(tripsPosition);
          continue;
        }
        if (pawnInTrips.Colore == this.Colore)//pion alier
        {
          break;
        }
        else
        {
          avalablesPositionList.Add(tripsPosition);
          break;
        }
      }
      for (int i = 1; i <= 97 + 8; i++)
      {
        var tripsPosition = (Convert.ToChar(xasciiCode + i)).ToString() + Y;
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (pawnInTrips == null)
        {
          avalablesPositionList.Add(tripsPosition);
          continue;
        }
        if (pawnInTrips.Colore == this.Colore)//pion alier
        {
          break;
        }
        else
        {
          avalablesPositionList.Add(tripsPosition);
          break;
        }
      }
      for (int i = xasciiCode - 1; i >= 97; i--)
      {
        var tripsPosition = (Convert.ToChar(i)).ToString() + Y;
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (pawnInTrips == null)
        {
          avalablesPositionList.Add(tripsPosition);
          continue;
        }
        if (pawnInTrips.Colore == this.Colore)//pion alier
        {
          break;
        }
        else
        {
          avalablesPositionList.Add(tripsPosition);
          break;
        }
      }
      for (int i = 1, j = intY; i <= 97 + 8 || j <= 8; i++, j++)
      {
        var tripsPosition = Convert.ToChar(xasciiCode + i).ToString() + (j + 1).ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (pawnInTrips == null)
        {
          avalablesPositionList.Add(tripsPosition);
          continue;
        }
        if (pawnInTrips.Colore == this.Colore)//pion alier
        {
          break;
        }
        else
        {
          avalablesPositionList.Add(tripsPosition);
          break;
        }
      }
      for (int i = xasciiCode - 1, j = intY; i >= 97 || j >= 1; i--, j--)
      {
        var tripsPosition = (Convert.ToChar(i)).ToString() + (j - 1).ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (pawnInTrips == null)
        {
          avalablesPositionList.Add(tripsPosition);
          continue;
        }
        if (pawnInTrips.Colore == this.Colore)//pion alier
        {
          break;
        }
        else
        {
          avalablesPositionList.Add(tripsPosition);
          break;
        }
      }
      for (int i = xasciiCode - 1, j = intY; i >= 97 || j <= 8; i--, j++)
      {
        var tripsPosition = (Convert.ToChar(i)).ToString() + (j + 1).ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (pawnInTrips == null)
        {
          avalablesPositionList.Add(tripsPosition);
          continue;
        }
        if (pawnInTrips.Colore == this.Colore)//pion alier
        {
          break;
        }
        else
        {
          avalablesPositionList.Add(tripsPosition);
          break;
        }
      }
      for (int i = 1, j = intY; i <= 97 || j >= 8; i++, j--)
      {
        var tripsPosition = Convert.ToChar(xasciiCode + i).ToString() + (j - 1).ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (pawnInTrips == null)
        {
          avalablesPositionList.Add(tripsPosition);
          continue;
        }
        if (pawnInTrips.Colore == this.Colore)//pion alier
        {
          break;
        }
        else
        {
          avalablesPositionList.Add(tripsPosition);
          break;
        }
      }





      PossibleTrips.AddRange(avalablesPositionList);
    }

    private void fillPossibleTripsKing()
    {
      var avalablesPositionList = new List<string>();
      var xasciiCode = (int)Convert.ToChar(X);
      var intY = Int32.Parse(Y);

     
        var tripsPosition = (X) + (intY + 1).ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (pawnInTrips == null)
        {
          avalablesPositionList.Add(tripsPosition);
        }
        else
        {
          if(pawnInTrips.Colore != this.Colore)//pion alier
          {
            avalablesPositionList.Add(tripsPosition);
          }
        }
        

       tripsPosition = (X) + (intY - 1).ToString();
       pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (pawnInTrips == null)
      {
        avalablesPositionList.Add(tripsPosition);
      }
      else
      {
        if (pawnInTrips.Colore != this.Colore)//pion alier
        {
          avalablesPositionList.Add(tripsPosition);
        }
      }
      


      
      tripsPosition = (Convert.ToChar(xasciiCode + 1)).ToString() + Y;
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (pawnInTrips == null)
      {
        avalablesPositionList.Add(tripsPosition);
      }
      else
      {
        if (pawnInTrips.Colore != this.Colore)//pion alier
        {
          avalablesPositionList.Add(tripsPosition);
        }
      }
      tripsPosition = (Convert.ToChar(xasciiCode - 1)).ToString() + Y;
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (pawnInTrips == null)
      {
        avalablesPositionList.Add(tripsPosition);
      }
      else
      {
        if (pawnInTrips.Colore != this.Colore)//pion alier
        {
          avalablesPositionList.Add(tripsPosition);
        }
      }


      tripsPosition = Convert.ToChar(xasciiCode + 1).ToString() + (intY + 1).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (pawnInTrips == null)
      {
        avalablesPositionList.Add(tripsPosition);
      }
      else
      {
        if (pawnInTrips.Colore != this.Colore)//pion alier
        {
          avalablesPositionList.Add(tripsPosition);
        }
      }
      tripsPosition = Convert.ToChar(xasciiCode - 1).ToString() + (intY + 1).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (pawnInTrips == null)
      {
        avalablesPositionList.Add(tripsPosition);
      }
      else
      {
        if (pawnInTrips.Colore != this.Colore)//pion alier
        {
          avalablesPositionList.Add(tripsPosition);
        }
      }
      tripsPosition = Convert.ToChar(xasciiCode + 1).ToString() + (intY - 1).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (pawnInTrips == null)
      {
        avalablesPositionList.Add(tripsPosition);
      }
      else
      {
        if (pawnInTrips.Colore != this.Colore)//pion alier
        {
          avalablesPositionList.Add(tripsPosition);
        }
      }
      tripsPosition = Convert.ToChar(xasciiCode - 1).ToString() + (intY - 1 ).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (pawnInTrips == null)
      {
        avalablesPositionList.Add(tripsPosition);
      }
      else
      {
        if (pawnInTrips.Colore != this.Colore)//pion alier
        {
          avalablesPositionList.Add(tripsPosition);
        }
      }
      PossibleTrips.AddRange (avalablesPositionList);
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
