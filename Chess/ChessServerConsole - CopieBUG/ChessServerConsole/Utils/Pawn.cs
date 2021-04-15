
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChessServerConsole.Utils
{
  public class Pawn
  {


    public Server MainServer { get; set; }
    public string Name { get; set; }



    private string _location;
    public string Location
    {
      get => _location;
      set
      {

        _location = value;
        X = Location[0].ToString();
        Y = Location[1].ToString();

      }
    }


    public string X { get; set; } //a-h

    public string Y { get; set; }//1-8


    public List<string> PossibleTrips { get; set; } //Les déplacement possible: liste des positions possible
    public List<int> PossibleTripsScore { get; set; }

    public string Image { get; set; }
    public string Colore { get; set; }


    public Boolean IsSelected { get; set; }


    public bool IsFirstMove{ get; set; }

    public int Value { get; set; }

    //roc
    public bool IsFirstMoveKing { get; set; }
  
    public bool IsLeftRookFirstMove { get; set; }
    public bool IsRightRookFirstMove { get; set; }
    public string MinPosition { get; set; }
    public string MaxPosition { get; set; }
    public string BestPositionAfterEmul { get; set; }

    public int MinScore { get; set; }
    public int MaxScore { get; set; }

    public bool IsMaced { get; set; }
    public Pawn()
    {

    }

    public Pawn(string name,string location,string colore,Server server)
    {


      MainServer = server;

      Name = name;


      SetValue();






      if (Name=="SimplePawn")
        IsFirstMove = true;
      if (Name == "King")
      {
        IsFirstMoveKing = true;
        IsLeftRookFirstMove = true;
        IsRightRookFirstMove = true;
      }
        
     
      Colore = colore;
      Location = location;
      X = Location[0].ToString();
      Y = Location[1].ToString();


      /*
      _dockPanel.Background = Brushes.DarkCyan;
      if (Colore== "White")
        _dockPanel.Background = Brushes.White;*/



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
          Value = 10;
          break;
        case "Queen":
          Value = 100;
          break;
        case "Rook":
          Value = 50;
          break;
        case "Bishop":
          Value = 30;
          break;
        case "Knight":
          Value = 60;
          break;
        case "King":
          Value = 1000;
          break;
      }
    }

   

    public void FillPossibleTrips(bool isFromTemps=false)
    {
     // TempsPawnList = null;
      PossibleTrips.Clear();
      if(this.Name== "SimplePawn")
        fillPossibleTripsSimplePawn(this.Colore);
      if (this.Name == "Knight")
        fillPossibleTripsKnight(isFromTemps);
      if (this.Name == "Rook")
        fillPossibleTripsRook(isFromTemps); 
      if (this.Name == "Bishop")
        fillPossibleTripsBishop(isFromTemps); 
      if (this.Name == "Queen")
        fillPossibleTripsQueen(isFromTemps);//King
      if (this.Name == "King")
        fillPossibleTripsKing(isFromTemps);

      //EvaluateScorePossibleTrips();

      //IsMaced = FindIsMaced(CurrentPawnList);

    }

   
    public bool FindIsMaced(List<Pawn> currentPawnList)
    {
      var result = false;
      foreach (var pawn in currentPawnList.Where(x=>x.Colore != Colore))
      {
        pawn.FillPossibleTrips();
        if (pawn.PossibleTrips.Contains(Location))
        {
          result = true;
          break;
        }
      }
      return result;
    }





    



    private void fillPossibleTripsSimplePawn(string colore, bool isFromTemps=false)
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
      var intY = 0;
      //Int32.Parse(Y);
      bool success = Int32.TryParse(Y, out intY);
      if (!success)
        return;

      var tripsPosition = X + (intY +(toAdd)).ToString();
      var pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
      if (pawnInTrips == null)
      {
        PossibleTrips.Add(tripsPosition);
      }
      if (IsFirstMove)
      {
         tripsPosition = X + (intY + (toAdd)).ToString();
         pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
        if(pawnInTrips==null)
        {
          tripsPosition = X + (intY + (toAdd) + (toAdd)).ToString();
          pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
          if (pawnInTrips == null)
          {
            PossibleTrips.Add(tripsPosition);
          }
        }

        
      }

      //pour les attaques des pions
      tripsPosition = Convert.ToChar(xasciiCode -1 ).ToString() + (intY + toAdd).ToString();
      pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
      if (pawnInTrips != null)
      {
        if(pawnInTrips.Colore != this.Colore)
          PossibleTrips.Add(tripsPosition);
      }
      tripsPosition = Convert.ToChar(xasciiCode + 1).ToString() + (intY + toAdd).ToString();
      pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
      if (pawnInTrips != null)
      {
        if (pawnInTrips.Colore != this.Colore)
          PossibleTrips.Add(tripsPosition);
      }








    }
    private void fillPossibleTripsKnight(bool isFromTemps=false)
    {
      var avalablesPositionList = new List<string>();
        

      var xasciiCode = (int)Convert.ToChar(X);
      var intY = Int32.Parse(Y);


      var tripsPosition = Convert.ToChar(xasciiCode - 1).ToString() + (intY + 2).ToString();
      var pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
      if (MainServer.GetCase(tripsPosition) != null)
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
       pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
      if (MainServer.GetCase(tripsPosition) != null)
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
      pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
      if (MainServer.GetCase(tripsPosition) != null)
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
      pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
      if (MainServer.GetCase(tripsPosition) != null)
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
      pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
      if (MainServer.GetCase(tripsPosition) != null)
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
      pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
      if (MainServer.GetCase(tripsPosition) != null)
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
      pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
      if (MainServer.GetCase(tripsPosition) != null)
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
      pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
      if (MainServer.GetCase(tripsPosition) != null)
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

    private void fillPossibleTripsRook(bool isFromTemps = false)
    {
      var avalablesPositionList = new List<string>();

      var intY = Int32.Parse(Y);
      for (int i = intY+1; i <= 8; i++)
      {
        var tripsPosition = (X) + i.ToString();
        var pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
        if (MainServer.GetCase(tripsPosition) == null)
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
        var pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
        if (MainServer.GetCase(tripsPosition) == null)
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
        var pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
        if (MainServer.GetCase(tripsPosition) == null)
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
        var pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
        if (MainServer.GetCase(tripsPosition) == null)
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
    private void fillPossibleTripsBishop(bool isFromTemps = false)
    {
      var avalablesPositionList = new List<string>();
      var xasciiCode = (int)Convert.ToChar(X);
      var intY = Int32.Parse(Y);
      
      for (int i = xasciiCode+1, j = intY ; i < 97 + 8 &&  j < 8; i++,j++)
      {
        var tripsPosition = Convert.ToChar(i).ToString() + (j+1).ToString();
        var pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
        if (MainServer.GetCase(tripsPosition) == null)
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
        var pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
        if (MainServer.GetCase(tripsPosition) == null)
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
        var pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
        if (MainServer.GetCase(tripsPosition) == null)
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
        var pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
        if(MainServer.GetCase(tripsPosition)==null)
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


    private void fillPossibleTripsQueen(bool isFromTemps = false)
    {
      var avalablesPositionList = new List<string>();
      var xasciiCode = (int)Convert.ToChar(X);
      var intY = Int32.Parse(Y);

      for (int i = intY + 1; i <= 8; i++)
      {
        var tripsPosition = (X) + i.ToString();
        var pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
        if (MainServer.GetCase(tripsPosition) == null)
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
        var pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
        if (MainServer.GetCase(tripsPosition) == null)
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
        var pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
        if (MainServer.GetCase(tripsPosition) == null)
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
        var pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
        if (MainServer.GetCase(tripsPosition) == null)
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
        var pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
        if (MainServer.GetCase(tripsPosition) == null)
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
        var pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
        if (MainServer.GetCase(tripsPosition) == null)
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
        var pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
        if (MainServer.GetCase(tripsPosition) == null)
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
        var pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
        if (MainServer.GetCase(tripsPosition) == null)
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

    private void fillPossibleTripsKing(bool isFromTemps = false)
    {
      var avalablesPositionList = new List<string>();
      var xasciiCode = (int)Convert.ToChar(X);
      var intY = Int32.Parse(Y);

     
        var tripsPosition = (X) + (intY + 1).ToString();
      var pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
      if (MainServer.GetCase(tripsPosition) != null)
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
       pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
      if (MainServer.GetCase(tripsPosition) != null)
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
      if (MainServer.GetCase(tripsPosition) != null)
      {
        pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
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
      pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
      if (MainServer.GetCase(tripsPosition) != null)
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
      pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
      if (MainServer.GetCase(tripsPosition) != null)
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
      pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
      if (MainServer.GetCase(tripsPosition) != null)
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
      pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
      if (MainServer.GetCase(tripsPosition) != null)
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
      pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
      if (MainServer.GetCase(tripsPosition) != null)
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
      var opignonPawnList = MainServer.GetOpignonPawnList(this.Colore);
      //Ajout du roc
      //si le roi ne s'est pas encore deplacer
      if (Name == "King")
      {
        if (IsFirstMoveKing)//si le roi n'a pas bouger
        {
          if (IsLeftRookFirstMove)//si le fou n'as pas bouger
          {
            var isFree = true;
            for (int i = 101; i > 98; i--)//si il n'y a rein entre le roi et le fou
            {
              var location = Convert.ToChar(i - 1).ToString() + Y;
              var pawnInposition = MainServer.GetPawnFromPawnList(location);
              if (pawnInposition != null)
              {
                isFree = false;
                break;
              }

              //si en rocant, le rois ne passe pas par une case de déplacement possible de l'adversaire
              //si la case n'est pas cible des pions adverses
              
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
              var pawnInposition = MainServer.GetPawnFromPawnList(location);
              //si en rocant, le rois ne passe pas par une case de déplacement possible de l'adversaire
              //si la case n'est pas cible des pions adverses


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

      var opignonPawnPosibleTips = new List<string>();  
      foreach (var opignonPawn in opignonPawnList)
      {
        if(opignonPawn.Name!="King")
        {
          opignonPawn.FillPossibleTrips();
          opignonPawnPosibleTips.AddRange(opignonPawn.PossibleTrips);
        }

      }

      for (int i = 0; i < avalablesPositionList.Count; i++)
      {
        var avalablePosition = avalablesPositionList[i];
        if (opignonPawnPosibleTips.Contains(avalablePosition))
          avalablesPositionList.Remove(avalablePosition);
      }

   
     

      PossibleTrips.AddRange(avalablesPositionList);
      if (Colore == "White" && Location =="e2")
      {
        var t_ = PossibleTrips;
      }
    }

    private bool isChess()
    {
      if(this.Name=="King")
      {
        var opignonPawnList= MainServer.GetOpignonPawnList(this.Colore);
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


   

 
   

  }
}
