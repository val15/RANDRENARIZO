using Chess.View;
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
    public List<int> PossibleTripsScore { get; set; }

    public string Image { get; set; }
    public string Colore { get; set; }
    public Button AssociateButton { get; set; }

    public Boolean IsSelected { get; set; }

    private DockPanel _dockPanel;
    private Image _image;
    private bool _isFirstMove;

    public int Value { get; set; }

    //roc
    public bool IsFirstMoveKing { get; set; }
  
    public bool IsLeftRookFirstMove { get; set; }
    public bool IsRightRookFirstMove { get; set; }

    public Pawn(string name,string location,string x,string y , Button associateButton,string colore, MainWindow mainWindowParent)
    {

     

      MainWindowParent = mainWindowParent;
      Name = name;

      SetValue();






      if (Name=="SimplePawn")
        _isFirstMove = true;
      if (Name == "King")
      {
        IsFirstMoveKing = true;
        IsLeftRookFirstMove = true;
        IsRightRookFirstMove = true;
      }
        
     
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
      PossibleTripsScore = new List<int>();
     // fillPossibleTrips();




      /* Image image = new Image();
       image.Source = new BitmapImage(new Uri(@"Images\SimplePawn.png", UriKind.Relative));
       AssociateButton.Content = image;*/

      //<DockPanel Margin="0,0,0,0">
      //  <Image  Height="59"  Width="58" Source="../Images/SimplePawn.png"/>
      //</DockPanel>


      //AssociateButton.Click += buttonCase_Click;

    }

    public void SetValue()
    {
      switch (Name)
      {
        case "SimplePawn":
          Value = 1;
          break;
        case "Queen":
          Value = 9;
          break;
        case "Rook":
          Value = 5;
          break;
        case "Bishop":
          Value = 3;
          break;
        case "Knight":
          Value = 5;
          break;
        case "King":
          Value = 1000;
          break;
      }
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

      //EvaluateScorePossibleTrips();
    }

    public void EvaluateScorePossibleTrips()//liste des position possibles avec leurs score respective
    {
      PossibleTripsScore.Clear();
      foreach (var position in PossibleTrips)
      {
        var score = 0;
        var pawn = MainWindowParent.GetPawn(position);
        if (pawn == null)
        {
          score = 0;
        }
        else
        {
          if (pawn.Colore != this.Colore)
          {
            score += pawn.Value;
          }
            
        }
        var possibleCase= MainWindowParent.GetCase(position);

        if(possibleCase!=null)
          possibleCase.Score = score;

        PossibleTripsScore.Add(score);
      }
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
      if (MainWindowParent.GetCase(tripsPosition) != null)
      {
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
      }
        


       tripsPosition = Convert.ToChar(xasciiCode + 1).ToString() + (intY + 2).ToString();
       pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (MainWindowParent.GetCase(tripsPosition) != null)
      {
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
      }


      tripsPosition = Convert.ToChar(xasciiCode + 2).ToString() + (intY -1).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (MainWindowParent.GetCase(tripsPosition) != null)
      {
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
      }


      tripsPosition = Convert.ToChar(xasciiCode + 2).ToString() + (intY + 1).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (MainWindowParent.GetCase(tripsPosition) != null)
      {
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
      }

      tripsPosition = Convert.ToChar(xasciiCode - 1).ToString() + (intY -2 ).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (MainWindowParent.GetCase(tripsPosition) != null)
      {
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
      }

      tripsPosition = Convert.ToChar(xasciiCode + 1).ToString() + (intY - 2).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (MainWindowParent.GetCase(tripsPosition) != null)
      {
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
      }

      tripsPosition = Convert.ToChar(xasciiCode - 2).ToString() + (intY - 1).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (MainWindowParent.GetCase(tripsPosition) != null)
      {
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
      }

      tripsPosition = Convert.ToChar(xasciiCode - 2).ToString() + (intY + 1).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (MainWindowParent.GetCase(tripsPosition) != null)
      {
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
        if (MainWindowParent.GetCase(tripsPosition) == null)
          break;
        if (pawnInTrips== null)
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
        if (MainWindowParent.GetCase(tripsPosition) == null)
          break;
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
      for (int i = 1; i <= 97+8 && xasciiCode < 104; i++)
      {
        var tripsPosition = (Convert.ToChar(xasciiCode + i)).ToString() + Y;
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (MainWindowParent.GetCase(tripsPosition) == null)
          break;
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
        if (MainWindowParent.GetCase(tripsPosition) == null)
          break;
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
      
      for (int i = xasciiCode+1, j = intY ; i < 97 + 8 &&  j < 8; i++,j++)
      {
        var tripsPosition = Convert.ToChar(i).ToString() + (j+1).ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (MainWindowParent.GetCase(tripsPosition) == null)
          break;
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
      for (int i = xasciiCode, j = intY; i > 97 && j > 1 ; i--, j--)
      {
        var tripsPosition = (Convert.ToChar(i-1)).ToString() + (j - 1).ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (MainWindowParent.GetCase(tripsPosition) == null)
          break;
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
      
     for (int i = xasciiCode - 1, j = intY; i >= 97 && j <= 8  ; i--, j++)
      {
        var tripsPosition = (Convert.ToChar(i)).ToString() + (j + 1).ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (MainWindowParent.GetCase(tripsPosition) == null)
          break;
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
     
      for (int i = xasciiCode, j = intY; i < 97+7 && j > 1; i++, j--)
      {
        var tripsPosition = Convert.ToChar(i+1).ToString() + (j - 1).ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if(MainWindowParent.GetCase(tripsPosition)==null)
          break;
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
        if (MainWindowParent.GetCase(tripsPosition) == null)
          break;
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
        if (MainWindowParent.GetCase(tripsPosition) == null)
          break;
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
      for (int i = 1; i <= 97 + 8 && xasciiCode < 104; i++)
      {
        var tripsPosition = (Convert.ToChar(xasciiCode + i)).ToString() + Y;
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (MainWindowParent.GetCase(tripsPosition) == null)
          break;
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
        if (MainWindowParent.GetCase(tripsPosition) == null)
          break;
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



      for (int i = xasciiCode + 1, j = intY; i < 97 + 8 && j < 8; i++, j++)
      {
        var tripsPosition = Convert.ToChar(i).ToString() + (j + 1).ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (MainWindowParent.GetCase(tripsPosition) == null)
          break;
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
      for (int i = xasciiCode, j = intY; i > 97 && j > 1; i--, j--)
      {
        var tripsPosition = (Convert.ToChar(i - 1)).ToString() + (j - 1).ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (MainWindowParent.GetCase(tripsPosition) == null)
          break;
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

      for (int i = xasciiCode - 1, j = intY; i >= 97 && j <= 8; i--, j++)
      {
        var tripsPosition = (Convert.ToChar(i)).ToString() + (j + 1).ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (MainWindowParent.GetCase(tripsPosition) == null)
          break;
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

      for (int i = xasciiCode, j = intY; i < 97 + 7 && j > 1; i++, j--)
      {
        var tripsPosition = Convert.ToChar(i + 1).ToString() + (j - 1).ToString();
        var pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
        if (MainWindowParent.GetCase(tripsPosition) == null)
          break;
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
      if (MainWindowParent.GetCase(tripsPosition) != null)
      {
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
      }
      
        
        

       tripsPosition = (X) + (intY - 1).ToString();
       pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (MainWindowParent.GetCase(tripsPosition) != null)
      {
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
      }
      


      
      tripsPosition = (Convert.ToChar(xasciiCode + 1)).ToString() + Y;
      if (MainWindowParent.GetCase(tripsPosition) != null)
      {
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
      }

      tripsPosition = (Convert.ToChar(xasciiCode - 1)).ToString() + Y;
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (MainWindowParent.GetCase(tripsPosition) != null)
      {
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
      }


      tripsPosition = Convert.ToChar(xasciiCode + 1).ToString() + (intY + 1).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (MainWindowParent.GetCase(tripsPosition) != null)
      {
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
      }

      tripsPosition = Convert.ToChar(xasciiCode - 1).ToString() + (intY + 1).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (MainWindowParent.GetCase(tripsPosition) != null)
      {
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
      }


      tripsPosition = Convert.ToChar(xasciiCode + 1).ToString() + (intY - 1).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (MainWindowParent.GetCase(tripsPosition) != null)
      {
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
      }

      tripsPosition = Convert.ToChar(xasciiCode - 1).ToString() + (intY - 1 ).ToString();
      pawnInTrips = MainWindowParent.GetPawn(tripsPosition);
      if (MainWindowParent.GetCase(tripsPosition) != null)
      {
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
      }

      //Ajout du roc
      //si le roi ne s'est pas encore deplacer
      if(Name == "King")
      {
        if (IsFirstMoveKing)//si le roi n'a pas bouger
        {
          if (IsLeftRookFirstMove)//si le fou n'as pas bouger
          {
            var isFree = true;
            for (int i = 101; i > 98; i--)//si il n'y a rein entre le roi et le fou
            {
              var location = Convert.ToChar(i - 1).ToString() + Y;
              var pawnInposition = MainWindowParent.GetPawn(location);
              if (pawnInposition != null)
              {
                isFree = false;
                break;
              }

              //si en rocant, le rois ne passe pas par une case de déplacement possible de l'adversaire
              //si la case n'est pas cible des pions adverses
              var opignonPawnList = MainWindowParent.GetOpignonPawnList(this.Colore);
              if (opignonPawnList != null)
              {
                foreach (var item in opignonPawnList)
                {
                  if (item.PossibleTrips.Contains(location))
                  {
                    isFree = false;
                    break;
                  }
                }
              }
            }

            //si le roi est sous la menache d'un echec



            if (isFree && !isChess())
            {
              tripsPosition = Convert.ToChar(xasciiCode - 2).ToString() + (intY).ToString();
              avalablesPositionList.Add(tripsPosition);
            }
          }

          if (IsRightRookFirstMove)
          {

            //si il n'y a rien entre la tour et le roi
            var isFree = true;
            for (int i = 1; i < 3 ; i++)
            {
              var location = Convert.ToChar(xasciiCode + i).ToString() + Y;
              var pawnInposition = MainWindowParent.GetPawn(location);
              //si en rocant, le rois ne passe pas par une case de déplacement possible de l'adversaire
              //si la case n'est pas cible des pions adverses

              var opignonPawnList = MainWindowParent.GetOpignonPawnList(this.Colore);
              foreach (var item in opignonPawnList)
              {
                if (item.PossibleTrips.Contains(location))
                {
                  isFree = false;
                  break;
                }
              }
              if (pawnInposition != null)
              {
                isFree = false;
                break;
              }
                
            }
            if(isFree && !isChess())
            {
              tripsPosition = Convert.ToChar(xasciiCode + 2).ToString() + (intY).ToString();
              avalablesPositionList.Add(tripsPosition);
            }

          }
        
        }
      }
      PossibleTrips.AddRange (avalablesPositionList);
    }

    private bool isChess()
    {
      if(this.Name=="King")
      {
        var opignonPawnList= MainWindowParent.GetOpignonPawnList(this.Colore);
        foreach (var item in opignonPawnList)
        {
          if(item.Location == this.Location)
          {
            return true;
          }
        } 
      }
      return false;
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
        var avaleblesCases = MainWindowParent.GetCase(item);
        var avalableButton = (Button)MainWindowParent.FindName(item);
        if (avalableButton != null)
        {
          avalableButton.Background = Brushes.Yellow;
          avalableButton.ToolTip = avaleblesCases.Score.ToString();
        }
        



      }

      //colorer les PossibleTrips
    }

    public void Move(Case newCase)//quan on déplace un pion, en fonction de sa position; 
    {

      


      if (MainWindowParent.CurrentTurn != this.Colore)
        return;


      //attaque
      

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
      AssociateButton = newCase.ButtonCase;
      //newCase.ButtonCase.Background = Brushes.Black;

      var deletedPawn = MainWindowParent.GetPawn(newCase.CaseName);
      if (deletedPawn != null)
      {
        //suppression
        MainWindowParent.PawnList.Remove(deletedPawn);
        MainWindowParent.PawnListBlack = MainWindowParent.PawnList.Where(x => x.Colore == "Black").ToList();
        MainWindowParent.PawnListWhite = MainWindowParent.PawnList.Where(x => x.Colore == "White").ToList();
        /*if(deletedPawn.Colore == "White")
          MainWindowParent.PawnListBlack.Remove(deletedPawn);
        else
          MainWindowParent.PawnListWhite.Remove(deletedPawn);*/
        if (deletedPawn.Name == "King")
          MainWindowParent.Win(this.Colore);
      }

      //X = newCase.X;
      //Y = newCase.Y;
      _isFirstMove = false;
      this.Location = newCase.CaseName;
      this.X = newCase.X;
      this.Y = newCase.Y;


      
      
     
       

      Debug.WriteLine("nove: new position = " + this.Location);



      //Rank
      if (this.Name == "SimplePawn" && (Y == "1" || Y == "8"))
      {
        var riseInRankWindow = new RiseInRankWindow(this);
        riseInRankWindow.ShowDialog();
      }

      //Roc
      if(this.Name =="King" && this.IsFirstMoveKing && this.IsLeftRookFirstMove && this.X=="c")
      {
        var rook = MainWindowParent.GetLeftRook(this.Colore);
        rook.Move(MainWindowParent.GetCase("d" + Y));
        MainWindowParent.SwithTurn();
      }
      if (this.Name == "King" && this.IsFirstMoveKing && this.IsRightRookFirstMove && this.X == "g")
      {
        var rook = MainWindowParent.GetRightRook(this.Colore);
        rook.Move(MainWindowParent.GetCase("f" + Y));
        MainWindowParent.SwithTurn();
      }

      if (this.Name == "King")
        IsFirstMoveKing = false;


      
      //si le roi se deplace de deux case vers la gauche ou la droite
      //et le fou se place à droite ou à gauche du roi

      MainWindowParent.FillAllPossibleTrips();
      MainWindowParent.SetDefaultColoreAllCases();
      MainWindowParent.SwithTurn();

    }

 
    public void SwithTo(string name)
    {
      this.Name = name;
      SetValue();
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
      this.AssociateButton.Content = _dockPanel;

    }

  }
}
