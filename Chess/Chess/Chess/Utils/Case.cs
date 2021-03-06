using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Chess.Utils
{
  public class Case
  {
    public MainWindow MainWindowParent { get; set; }
    public string X { get; set; } //a - h
    public string Y { get; set; } //1 - 8
    public Button ButtonCase { get; set; }
    public string CaseName { get; set; }

   

    public Case(string x,string y, Button buttonCase, MainWindow mainWindowParent)
    {
      MainWindowParent = mainWindowParent;
      X = x;
      Y = y;
      CaseName = X + Y;
      ButtonCase = buttonCase;

      SetDefaultColore();




      /*Image image = new Image();
      image.Source = new BitmapImage(new Uri(@"Images\SimplePawn.png", UriKind.Relative));
      ButtonCase.Content = image;*/


      ButtonCase.Click += buttonCase_Click;
    }

    public void SetDefaultColore()
    {
      ButtonCase.Background = Brushes.Blue;
      var xasciiCode = (int)Convert.ToChar(X);
      var yint = Int32.Parse(Y);
      if ((xasciiCode % 2) == 1 && (yint % 2) == 0)
        ButtonCase.Background = Brushes.Red;
      if ((xasciiCode % 2) == 0 && (yint % 2) == 1)
        ButtonCase.Background = Brushes.Red;
    }

    private void buttonCase_Click(object sender, EventArgs e)
    {
      //MOVE dase de depar, case d'arriver
      var buttonSender = (Button)sender;
      var senderGetPawn = MainWindowParent.GetPawn(buttonSender.Name);

     

      if(senderGetPawn != null)//selection d'un pion ou attaque sur in pion adverse
      {
        //case de depar
        // MainWindowParent.ToPosition ="";

        var currentPawn = MainWindowParent.GetPawn(MainWindowParent.FromPosition);
        var opinonPawn = MainWindowParent.GetPawn(buttonSender.Name);
        if(currentPawn != null && opinonPawn !=null)//attaque
        {
            moveTo(MainWindowParent.FromPosition, buttonSender.Name);
            return;
        }

        MainWindowParent.FromPosition = buttonSender.Name;
        senderGetPawn.ColorAvaleblesCases();
        return;
      }
      else
      {
        MainWindowParent.SetDefaultColoreAllCases();
        MainWindowParent.ToPosition = buttonSender.Name;
      }

      if(!String.IsNullOrEmpty(MainWindowParent.FromPosition) && !String.IsNullOrEmpty(MainWindowParent.ToPosition))
      {
        moveTo(MainWindowParent.FromPosition, MainWindowParent.ToPosition);
      }

      



      //si case de depar est vide
      //set case de depar
      //coloration

      //si non case = case d'arriver
      //prendre selection
      //movement
      //selection = vide
    }

    private void moveTo(string fromPosition, string toPosition)
    {

      var t = 0;

      var fromPawn = MainWindowParent.GetPawn(fromPosition);
      var toCase = MainWindowParent.GetCase(toPosition);

      if (fromPawn.PossibleTrips.Contains(CaseName))
        MainWindowParent.SelectedPawn.Move(toCase);


      MainWindowParent.FromPosition = "";
      MainWindowParent.ToPosition = "";
      /*

      var buttonSender = (Button)sender;


      var s = MainWindowParent.GetPawn(buttonSender.Name);



      if (s == null)//si on selectionne une case vide
      {
        MainWindowParent.SetDefaultColoreAllCases();

        if (MainWindowParent.SelectedPawn == null)
          return;

        //MainWindowParent.SelectedPawn = s;
        if (MainWindowParent.SelectedPawn.PossibleTrips.Contains(CaseName))
        {
          MainWindowParent.SelectedPawn.Move(this);
        }
        //return;
      }
      else
      {

        MainWindowParent.SelectedPawn = s;
        MainWindowParent.SelectedPawn.ColorAvaleblesCases();
      }
      */


    }
  }
}
