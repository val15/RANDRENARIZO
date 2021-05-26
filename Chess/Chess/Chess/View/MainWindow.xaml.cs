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
using System.IO;
using System.ComponentModel;
using Chess.View;
using System.Net;
using System.Net.Sockets;
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using Notifications.Wpf;

namespace Chess
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {

   
   




    public List<Case> CaseList { get; set; }
    public List<Pawn> PawnList { get; set; }
    private List<string> _deadList;
    private bool _graveyardIsLoaded;
    private int _blackPoint =0;
    private int _whitePoint =0;
    private Node _lastBestNode;

    private bool _isInLocalEgine;


    Notifier Notifier { get; set; }
    NotificationManager NotificationManagerWindows { get; set; }


    // public List<Pawn> TempsPawnList { get; set; }
    public List<Pawn> PawnListWhite { get; set; }
    public List<Pawn> PawnListBlack { get; set; }

    public Pawn SelectedPawn { get; set; }

    public string FromPosition { get; set; }
    public string ToPosition { get; set; }

    private string _currentTurn;

    public string ComputerColore
    {
      get => _computerColore;
      set
      {
        _computerColore = value;
        
      }
    }

    public string CurrentTurn
    {
      get => _currentTurn;
      set
      {
        lbBlackScore.Content = GetScore("Black");
        lbWhiteScore.Content = GetScore("White");
        //DoSomething();
        _currentTurn = value;
        if (CurrentTurn == "Black")
        {
          BlackTurnButton.Visibility = Visibility.Visible;
          WhiteTurnButton.Visibility = Visibility.Hidden;
          if (_whiteTimer!=null)
            _whiteTimer.Stop();
          startOrContinuBlackTimer();
        }
          
        if (CurrentTurn == "White")
        {
          BlackTurnButton.Visibility = Visibility.Hidden;
          WhiteTurnButton.Visibility = Visibility.Visible;
          if (_blackTimer != null)
            _blackTimer.Stop();
          startOrContinuWhiteTimer();
        }
          
      }
    }

    

    //private int deepStep = 0;
    private int cumputerLevel = 3;//max 21
    private int deepBlackLevel = 1;//5;//5;//4;
    private int deepWhiteLevel = 1;//1;//2;
   // private int levele = 0;

    public List<Node> Tree { get; set; }
    public List<List<Node>> AllCumputerPawnTreeList { get; set; }

    private string _computerColore;
    private int blackTurnNumber = 0;
    private int whiteTurnNumber = 0;

    //Timers
    private DispatcherTimer _cpuTimer;
    private DispatcherTimer _blackTimer;
    private DispatcherTimer _whiteTimer;
    private DateTime _cpuStartTime;
    private DateTime _blackCountTime;
    private DateTime _whiteCountTime;

    //pour les cimetiaire
    private int _simplePawnBlackDeadNumber;
    private int _simplePawnWhiteDeadNumber;
    private int _bishopBlackDeadNumber;
    private int _bishopWhiteDeadNumber;
    private int _knighBlackDeadNumber;
    private int _knighWhiteDeadNumber;
    private int _rookBlackDeadNumber;
    private int _rookWhiteDeadNumber;
    private int _queenPawnBlackDeadNumber;
    private int _queenPawnWhiteDeadNumber;

    public MainWindow()
    {
      InitializeComponent();
      _isInLocalEgine = true;
      SwithToServerEngineButon.Visibility = Visibility.Collapsed;
      SwithToLocalEngineButon.Visibility = Visibility.Visible;

      //pour les notification
      Notifier = new Notifier(cfg =>
      {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.MainWindow,
            corner: Corner.TopRight,
            offsetX: 10,
            offsetY: 10);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(3),
            maximumNotificationCount: MaximumNotificationCount.FromCount(5));

        cfg.Dispatcher = Application.Current.Dispatcher;
      });
      //NotificationManagerWindows
      NotificationManagerWindows = new NotificationManager();
      



      //Creations des fichier

      var whiteListFile = "./WHITEList.txt";
      var blackListFile = "./BLACKList.txt";
      var whiteListOldFile = "./WHITEListOld.txt";
      var blackListOldFile = "./BLACKListOld.txt";
      var deadListFile = "./Graveyard.txt";

      if (!File.Exists(whiteListFile))
      {
        using (var streamWriter = new StreamWriter(whiteListFile))
        {

        }
      }

      if (!File.Exists(blackListFile))
      {
        using (var streamWriter = new StreamWriter(blackListFile))
        {

        }
      }

      if (!File.Exists(whiteListOldFile))
      {
        using (var streamWriter = new StreamWriter(whiteListOldFile))
        {

        }
      }

      if (!File.Exists(blackListOldFile))
      {
        using (var streamWriter = new StreamWriter(blackListOldFile))
        {

        }
      }

      if (!File.Exists(deadListFile))
      {
        using (var streamWriter = new StreamWriter(deadListFile))
        {

        }
      }


      _deadList = new List<string>();
      _graveyardIsLoaded = false;
      _blackCountTime = new DateTime();
      _whiteCountTime = new DateTime();

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

      //Tree = new List<Node>();
      AllCumputerPawnTreeList = new List<List<Node>>();



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

      /* if(deepWhiteLevel> deepBlackLevel)
       {
         PawnList.AddRange(PawnListWhite);
         PawnList.AddRange(PawnListBlack);
       }
       else
       {
         PawnList.AddRange(PawnListBlack);
         PawnList.AddRange(PawnListWhite);

       }*/

      
      
      PawnList.AddRange(PawnListBlack);
      PawnList.AddRange(PawnListWhite);



    }

    /*tsiry;07-05-2021
     * implémentation du cimetière
     * */

    public void AddDeadList(string deadPawn)
    {
      
      fillGraveyard(deadPawn);
      //SaveGraveyardList for Load
      _deadList.Add(deadPawn);

      //calcule des point
      calculatePoint();
    }
    private void calculatePoint()
    {
      var whiteDeadList = _deadList.Where(x=> x.Contains("White"));
      var point = 0;
      foreach (var pawnString in whiteDeadList)
      {
        var pawnName = pawnString.Replace("White", "");
        
        switch (pawnName)
        {
          case "SimplePawn":
            point += 1;
            break;
          case "Knight":
            point += 3;
            break;
          case "Bishop":
            point += 3;
            break;
          case "Rook":
            point += 5;
            break;
          case "Queen":
            point += 9;
            break;
        }
      }

      _blackPoint = point;

      var blackDeadList = _deadList.Where(x => x.Contains("Black"));
      point = 0;
      foreach (var pawnString in blackDeadList)
      {
        var pawnName = pawnString.Replace("Black", "");

        switch (pawnName)
        {
          case "SimplePawn":
            point += 1;
            break;
          case "Knight":
            point += 3;
            break;
          case "Bishop":
            point += 3;
            break;
          case "Rook":
            point += 5;
            break;
          case "Queen":
            point += 9;
            break;
        }
      }

      _whitePoint = point;
      BlackPointLabel.Content = "";
      WhitePointLabel.Content = "";

      if (_blackPoint == _whitePoint)
        return;
      if (_blackPoint > _whitePoint)
      {
        var diffPoint = _blackPoint - _whitePoint;
        BlackPointLabel.Content = $"+{diffPoint}";
        return;
      }

      if (_whitePoint > _blackPoint)
      {
        var diffPoint = _whitePoint - _blackPoint;
        WhitePointLabel.Content = $"+{diffPoint}";
        return;
      }

    }
    private void fillGraveyard(string deadPawn)
    {
      try
      {
     

        //BLACK
        if (deadPawn.Contains("Black"))
        {

          if (deadPawn.Contains("SimplePawn"))
          {
            //SimplePawnBlack
            _simplePawnBlackDeadNumber++;
            if (_simplePawnBlackDeadNumber > 1)
              SimplePawnBlackDeadNumberLabel.Content = "* " + _simplePawnBlackDeadNumber.ToString();
            else
            {
              var dockPanel = new DockPanel();
              var image = new Image();
              image.Height = 60;
              image.Width = 60;
              image.Source = new BitmapImage(new Uri(@"/Images/" + "SimplePawnBlack.png", UriKind.Relative));
              dockPanel.Children.Add(image);
              SimplePawnBlackDeadButton.Content = dockPanel;
            }
          }
          if (deadPawn.Contains("Bishop"))
          {
            //BishopBlack
            _bishopBlackDeadNumber++;
            if (_bishopBlackDeadNumber > 1)
              BishopBlackDeadNumberLabel.Content = "* " + _bishopBlackDeadNumber.ToString();
            else
            {
              var dockPanel = new DockPanel();
              var image = new Image();
              image.Height = 60;
              image.Width = 60;
              image.Source = new BitmapImage(new Uri(@"/Images/" + "BishopBlack.png", UriKind.Relative));
              dockPanel.Children.Add(image);
              BishopBlackDeadButton.Content = dockPanel;
            }
          }

          if (deadPawn.Contains("Knight"))
          {
            //KnightBlack
            _knighBlackDeadNumber++;
            if (_knighBlackDeadNumber > 1)
              KnightBlackDeadNumberLabel.Content = "* " + _knighBlackDeadNumber.ToString();
            else
            {
              var dockPanel = new DockPanel();
              var image = new Image();
              image.Height = 60;
              image.Width = 60;
              image.Source = new BitmapImage(new Uri(@"/Images/" + "KnightBlack.png", UriKind.Relative));
              dockPanel.Children.Add(image);
              KnightBlackDeadButton.Content = dockPanel;
            }
          }

          if (deadPawn.Contains("Rook"))
          {
            //RookBlack
            _rookBlackDeadNumber++;
            if (_rookBlackDeadNumber > 1)
              RookBlackDeadNumberLabel.Content = "* " + _rookBlackDeadNumber.ToString();
            else
            {
              var dockPanel = new DockPanel();
              var image = new Image();
              image.Height = 60;
              image.Width = 60;
              image.Source = new BitmapImage(new Uri(@"/Images/" + "RookBlack.png", UriKind.Relative));
              dockPanel.Children.Add(image);
              RookBlackDeadButton.Content = dockPanel;
            }
          }

          if (deadPawn.Contains("Queen"))
          {
            //QueenBlack
            _queenPawnBlackDeadNumber++;
            if (_queenPawnBlackDeadNumber > 1)
              QueenBlackDeadNumberLabel.Content = "* " + _queenPawnBlackDeadNumber.ToString();
            else
            {
              var dockPanel = new DockPanel();
              var image = new Image();
              image.Height = 60;
              image.Width = 60;
              image.Source = new BitmapImage(new Uri(@"/Images/" + "QueenBlack.png", UriKind.Relative));
              dockPanel.Children.Add(image);
              QueenBlackDeadButton.Content = dockPanel;
            }
          }

        }

       /* dockPanel = null;
        image = null;
        dockPanel = new DockPanel();
        image = new Image();
        image.Height = 60;
        image.Width = 60;*/

        //WHITE
        if (deadPawn.Contains("White"))
        {
          if (deadPawn.Contains("SimplePawn"))
          {
            //SimplePawnWhite
            _simplePawnWhiteDeadNumber++;
            if (_simplePawnWhiteDeadNumber > 1)
              SimplePawnWhiteDeadNumberLabel.Content = "* " + _simplePawnWhiteDeadNumber.ToString();
            else
            {
              var dockPanel = new DockPanel();
              var image = new Image();
              image.Height = 60;
              image.Width = 60;
              image.Source = new BitmapImage(new Uri(@"/Images/" + "SimplePawnWhite.png", UriKind.Relative));
              dockPanel.Children.Add(image);
              SimplePawnWhiteDeadButton.Content = dockPanel;
            }

            
          }


          if (deadPawn.Contains("Bishop"))
          {
            //BishopWhite
            _bishopWhiteDeadNumber++;
            if (_bishopWhiteDeadNumber > 1)
              BishopWhiteDeadNumberLabel.Content = "* " + _bishopWhiteDeadNumber.ToString();
            else
            {
              var dockPanel = new DockPanel();
              var image = new Image();
              image.Height = 60;
              image.Width = 60;
              image.Source = new BitmapImage(new Uri(@"/Images/" + "BishopWhite.png", UriKind.Relative));
              dockPanel.Children.Add(image);
              BishopWhiteDeadButton.Content = dockPanel;
            }


          }

          if (deadPawn.Contains("Knight"))
          {
            //KnightWhite
            _knighWhiteDeadNumber++;
            if (_knighWhiteDeadNumber > 1)
              KnightWhiteDeadNumberLabel.Content = "* " + _knighWhiteDeadNumber.ToString();
            else
            {
              var dockPanel = new DockPanel();
              var image = new Image();
              image.Height = 60;
              image.Width = 60;
              image.Source = new BitmapImage(new Uri(@"/Images/" + "KnightWhite.png", UriKind.Relative));
              dockPanel.Children.Add(image);
              KnightWhiteDeadButton.Content = dockPanel;
            }
          }

          if (deadPawn.Contains("Rook"))
          {
            //RookWhite
            _rookWhiteDeadNumber++;
            if (_rookWhiteDeadNumber > 1)
              RookWhiteDeadNumberLabel.Content = "* " + _rookWhiteDeadNumber.ToString();
            else
            {
              var dockPanel = new DockPanel();
              var image = new Image();
              image.Height = 60;
              image.Width = 60;
              image.Source = new BitmapImage(new Uri(@"/Images/" + "RookWhite.png", UriKind.Relative));
              dockPanel.Children.Add(image);
              RookWhiteDeadButton.Content = dockPanel;
            }
          }

          if (deadPawn.Contains("Queen"))
          {
            //QueenWhite
            _queenPawnWhiteDeadNumber++;
            if (_queenPawnWhiteDeadNumber > 1)
              QueenWhiteDeadNumberLabel.Content = "* " + _queenPawnWhiteDeadNumber.ToString();
            else
            {
              var dockPanel = new DockPanel();
              var image = new Image();
              image.Height = 60;
              image.Width = 60;
              image.Source = new BitmapImage(new Uri(@"/Images/" + "QueenWhite.png", UriKind.Relative));
              dockPanel.Children.Add(image);
              QueenWhiteDeadButton.Content = dockPanel;
            }


          }

        }
      }
      catch (Exception ex)
      {
        WriteInLog(ex.ToString());
      }
      


    }

    private void ThreadCountCPUReflectionTime()
    {
      _cpuTimer = null;
      this.Dispatcher.BeginInvoke(new Action(() =>
      {
        _cpuTimer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 50), DispatcherPriority.Background,
                cpuTimer_Tick, Dispatcher.CurrentDispatcher); _cpuTimer.IsEnabled = true;
        _cpuStartTime = DateTime.Now;
        //lbCPUReflectionTime.Content = "Hello Geeks !";

      }));

    }
    private void cpuTimer_Tick(object sender, EventArgs e)
    {
      lbCPUReflectionTime.Content = Convert.ToString(DateTime.Now - _cpuStartTime);
    }

    private void startOrContinuBlackTimer()
    {
      Thread threadTimer = new Thread(ThreadStartOrContinuBlackTimer);
      threadTimer.Start();
    }

    private void startOrContinuWhiteTimer()
    {
      Thread threadTimer = new Thread(ThreadStartOrContinuWhiteTimer);
      threadTimer.Start();
    }

    private void ThreadStartOrContinuBlackTimer()
    {

      _blackTimer = null;
      this.Dispatcher.BeginInvoke(new Action(() =>
      {
        _blackTimer = new DispatcherTimer(new TimeSpan(0, 0, 0,1), DispatcherPriority.Background,
                blackTimer_Tick, Dispatcher.CurrentDispatcher); _blackTimer.IsEnabled = true;
      }));

    }
    private void blackTimer_Tick(object sender, EventArgs e)
    {
      _blackCountTime = _blackCountTime.AddSeconds(1);
      lbBlackTime.Content = String.Format("{0:HH:mm:ss}", _blackCountTime);
    }

    private void ThreadStartOrContinuWhiteTimer()
    {

      _whiteTimer = null;
      this.Dispatcher.BeginInvoke(new Action(() =>
      {
        _whiteTimer = new DispatcherTimer(new TimeSpan(0, 0, 0, 1), DispatcherPriority.Background,
                whiteTimer_Tick, Dispatcher.CurrentDispatcher); _whiteTimer.IsEnabled = true;
      }));

    }
    private void whiteTimer_Tick(object sender, EventArgs e)
    {
      _whiteCountTime = _whiteCountTime.AddSeconds(1);
      lbWhiteTime.Content = String.Format("{0:HH:mm:ss}", _whiteCountTime);
    }


    public void ReActive()
    {
      //this.IsActive = true;
      if (this.WindowState == WindowState.Minimized)
        this.WindowState = WindowState.Normal;
    }

    public async Task SwithTurnAsync()
    {
      if (this.WindowState == WindowState.Minimized)
      {
        if (_computerColore == CurrentTurn)
        {
          //Notification sur l'application
          //Notifier.ShowInformation("Move completed");

          //notification dans la bare de tache
          NotificationManagerWindows.Show(new NotificationContent
          {
            Title = "Chess notification",
            Message = $"CPU ({_computerColore}) move completed",
            Type = NotificationType.Information
          }, onClick: () => ReActive()/*,
               onClose: () => ReActive()*/);

          /* NotificationManagerWindows.Show("String notification", onClick: () => ReActive(),
                  onClose: () => ReActive());*/
        }


      }



      if (CurrentTurn == "White")
      {
        whiteTurnNumber++;
        CurrentTurn = "Black";
        
        if(_computerColore == CurrentTurn)
        {
          //await Task.Delay(100);
          GetBestPositionAndMoveFor(CurrentTurn);
        }
        


      }
      else
      {
        blackTurnNumber++;
        CurrentTurn = "White";
        //searchAndExecuteBestMove(PawnListBlack);
        if (_computerColore == CurrentTurn)
        {
          //await Task.Delay(100);
          GetBestPositionAndMoveFor(CurrentTurn);
        }
      
      }

      
      
      Save();
      Load();
    /*  var listBlack = new List<Pawn>();
      var listWhite= new List<Pawn>();
      listBlack.AddRange(PawnListBlack);
      listWhite.AddRange(PawnListWhite);

      //on recherge
      CleanPawnList();
      foreach (var item in listBlack)
      {
        //var bt = (Button)this.FindName(item.Location);
        var newPawn = new Pawn(item.Name, item.Location, item.AssociateButton, item.Colore, this);
        newPawn.IsFirstMove = item.IsFirstMove;
        newPawn.IsFirstMoveKing= item.IsFirstMoveKing;
        newPawn.IsLeftRookFirstMove = item.IsLeftRookFirstMove;
        newPawn.IsRightRookFirstMove = item.IsRightRookFirstMove;
        PawnListBlack.Add(newPawn);
      }
      foreach (var item in listWhite)
      {
        //var bt = (Button)this.FindName(item.Location);
        var newPawn = new Pawn(item.Name, item.Location, item.AssociateButton, item.Colore, this);
        newPawn.IsFirstMove = item.IsFirstMove;
        newPawn.IsFirstMoveKing = item.IsFirstMoveKing;
        newPawn.IsLeftRookFirstMove = item.IsLeftRookFirstMove;
        newPawn.IsRightRookFirstMove = item.IsRightRookFirstMove;
        PawnListWhite.Add(newPawn);
      }



      //PawnListBlack.AddRange()
      PawnList.AddRange(listBlack);
      PawnList.AddRange(listWhite);
      FillAllPossibleTrips();*/





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
    public Pawn GetPawn(string location,bool isFromTemps=false)
    {

      /*if(isFromTemps)
       */
     /* if (TempsPawnList != null)
        return TempsPawnList.FirstOrDefault(x => x.Location == location);
      else
        return null;///PawnList.FirstOrDefault(x => x.Location == location);
      /* else*/
         return PawnList.FirstOrDefault(x => x.Location == location);

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
      //Tree = null;
     // Tree = new List<Node>();
      //Tree.Clear();
      DebugTextBlock.Text = "";
      var computerPawnList = PawnList.Where(x => x.Colore == colore).ToList();
      /*var king = computerPawnList.FirstOrDefault(x => x.Name == "King");
      if (king.FindIsMaced())//si le roi est menacer
      {

        //soit on attaque ce qui menace



        computerPawnList.Clear();
        //soit on bouge le roi
        computerPawnList.Add(king);
        //soit on on protege le roi

        
      }*/






      
      foreach (var pawn in computerPawnList)
      {
        //var deep = 0;
        Tree = null;
         Tree = new List<Node>();


        Node newNode = new Node(PawnList);
        newNode.Location = pawn.Location;
        newNode.OldPositionName = "";
       /* if (_computerColore == "Black")
          newNode.Weight = evaluateScoreForBlack(colore, computerPawnList, pawn);
        if (_computerColore == "White")
          newNode.Weight = evaluateScoreForWhite(colore, computerPawnList, pawn);*/
        newNode.Weight = -10000000;
        newNode.Level = 0;
        newNode.Colore = colore;
        newNode.AssociatePawn = pawn;

        Tree.Add(newNode);



        //pawn.EvaluateScorePossibleTrips();

        if (pawn.Name == "King")
        { 

          var t_p = pawn;
          var t_king = pawn.PossibleTrips;
        }

        
        
        for (int i = 0; i < pawn.PossibleTrips.Count; i++)
        {
          //deep++;
          GenerateThread(pawn.Location, pawn.PossibleTrips[i], colore, newNode, pawn, deepLevel);
          //deepStep = 0;
          foreach (var item in PawnList)
          {
            item.EmulateAllPossibleTips();
          }
        }
        //Chaque pion a son arbre(Tree)
        AllCumputerPawnTreeList.Add(Tree);




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

      //foreach (var node in Tree.Where(x => x.Level >= 3 && (x.AssociatePawn.Name == "Rook"  || x.AssociatePawn.Name == "King") && (x.Location == "e1" )))
      //foreach (var node in Tree.Where(x => (x.AssociatePawn.Name == "King" && x.Location == "e1" && x.Level == 3) || (x.AssociatePawn.Name == "King" && x.Level ==1) || (x.AssociatePawn?.Name == "Rook" && x.Location == "e1" && x.Level == 2 && x.Parent.Location == "d2")).OrderBy(x => x.Level))
      //foreach (var node in Tree.Where(x => (x.AssociatePawn.Name == "Rook"  && x.Level == 2 && x.Location == "e1" /*&& x.ChildList.Count>0*/) || (x.AssociatePawn.Name == "King" && x.Level == 3 && x.Location == "e1" && x.Parent.Location =="e1")).OrderBy(x=>x.Level))// || (x.AssociatePawn?.Name == "Rook" && x.Location == "e1" && x.Level == 2 && x.Parent.Location == "d2")).OrderBy(x => x.Level))
      /* {

         Debug.WriteLine(node.Location);
         DebugTextBlock.Text += "postition : " + node.Location + "   score : " + node.Weight.ToString() + "  level : "  + node.Level +
            "    number of child : " + node.ChildList.Count() + "    OldPosition : " + node.OldPositionName + "   parent :"+node.Parent?.AssociatePawn.Name +" "+ node.Parent?.AssociatePawn.Location+ " " + node.Parent?.AssociatePawn.Colore + "    Colore : " + node.Colore  + "    Pawn : " + node.AssociatePawn?.Name + "   BSP : " + node?.BestChildPosition +"\n";

         //}
       }*/
      /* foreach (var node in Tree.Where(x => x.AssociatePawn.Name=="Rook" && x.Level==2 && x.Location == "e1" && x.Parent.Location == "d2" && x.Parent.AssociatePawn.Name == "King"))
       {
         DebugTextBlock.Text += "postition : " + node.Location + "   score : " + node.Weight.ToString() + "  level : " + node.Level +
            "    number of child : " + node.ChildList.Count() + "    OldPosition : " + node.OldPositionName + "   parent :" + node.Parent?.AssociatePawn.Name + " " + node.Parent?.AssociatePawn.Location + " " + node.Parent?.AssociatePawn.Colore + "    Colore : " + node.Colore + "    Pawn : " + node.AssociatePawn?.Name + "   BSP : " + node?.BestChildPosition + "\n";

         foreach (var item in node.ChildList)
         {
           DebugTextBlock.Text += "postition : " + item.Location + "   score : " + item.Weight.ToString() + "  level : " + item.Level +
           "    number of child : " + item.ChildList.Count() + "    OldPosition : " + item.OldPositionName + "   parent :" + item.Parent?.AssociatePawn.Name + " " + item.Parent?.AssociatePawn.Location + " " + item.Parent?.AssociatePawn.Colore + "    Colore : " + item.Colore + "    Pawn : " + item.AssociatePawn?.Name + "   BSP : " + item?.BestChildPosition + "\n";

         }*/

     /* foreach (var node in Tree)
      {
        DebugTextBlock.Text += "postition : " + node.Location +"    Pawn : " + node.AssociatePawn?.Name + "   score : " + node.Weight + "  level : " + node.Level +
           "    number of child : " + node.ChildList.Count() + "    OldPosition : " + node.OldPositionName + "   parent :" + node.Parent?.AssociatePawn.Name + " " + node.Parent?.AssociatePawn.Location + " " + node.Parent?.AssociatePawn.Colore + "    Colore : " + node.Colore + "   BSP : " 
           + node?.BestChildPosition+"    NUmberPawn : "+ node.GetCurrentLocalPawnList().Count +" "+node.GetCurrentLocalPawnListAllier().Count + "\n";


      }*/


    }

    private int[] evaluateScoreForBlack(string colore, List<Pawn> actualPawnList,Pawn movingPawn)
    {

      var finalScore = 0;
      var makeCheckmateLevel = 0;

      if (opinionIsCheckmate("Black", actualPawnList))
        makeCheckmateLevel = 1;



      if (actualPawnList.FirstOrDefault(x => x.Name == "King" && x.Colore == "Black") == null)
      {
        var resultArray0 = new int[2] { -9999999, makeCheckmateLevel };
        return resultArray0;
      }
      if (actualPawnList.FirstOrDefault(x => x.Name == "King" && x.Colore == "White") == null)
      {
        var resultArray0 = new int[2] { 9999999, makeCheckmateLevel };
        return resultArray0;
      }
      if (actualPawnList.FirstOrDefault(x => x.Name == "Queen" && x.Colore == "Black") == null)
      {
        var resultArray0 = new int[2] { -9000, makeCheckmateLevel };
        return resultArray0;
      }
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
      if (movingPawn.Colore == colore)
        finalScore = blackScore - whiteScore;
      else
        finalScore = whiteScore - blackScore ;

      var resultArray = new int[2] { finalScore, makeCheckmateLevel };
      return resultArray;
    }



    private int[] evaluateScoreForWhite(string colore, List<Pawn> actualPawnList, Pawn movingPawn)
    {
      var finalScore = 0;
      var makeCheckmateLevel = 0;

      if(opinionIsCheckmate("White",actualPawnList))
        makeCheckmateLevel = 1;

      if (actualPawnList.FirstOrDefault(x => x.Name == "King" && x.Colore == "White") == null)
      {
        var resultArray0 = new int[2] { -9999999, makeCheckmateLevel };
        return resultArray0;
      }
        
      if (actualPawnList.FirstOrDefault(x => x.Name == "King" && x.Colore == "Black") == null)
      {
        var resultArray0 = new int[2] { 9999999, makeCheckmateLevel };
        return resultArray0;
      }
      if (actualPawnList.FirstOrDefault(x => x.Name == "Queen" && x.Colore == "White") == null)
      {
        var resultArray0 = new int[2] { -9000, makeCheckmateLevel };
        return resultArray0;
      }
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
        finalScore=whiteScore - blackScore;
      else
        finalScore=blackScore - whiteScore;

      var resultArray = new int[2] { finalScore, makeCheckmateLevel };
      return resultArray;




    }


    private bool opinionIsCheckmate(string curentColor, List<Pawn> actualPawnList)
    {
      var opinionKing = actualPawnList.FirstOrDefault(x => x.Colore != curentColor && x.Name == "King");
      if (opinionKing == null)
        return false;

      var opinionKingLocation = opinionKing.Location;
      var alierPawnList = actualPawnList.Where(x => x.Colore == curentColor);
      foreach (var pawn in alierPawnList)
      {
        if (pawn.PossibleTrips.Contains(opinionKingLocation))
          return true;
      }

      return false;

    }


    private void MinMax()
    {

      //pour chaque arbre, on amplique le MinMax
      foreach (var tree in AllCumputerPawnTreeList)
      {
        for (int i = tree.Count - 1; i >= 0; i--)
        {
          var node = tree[i];
          var parent = tree[i].Parent;
          if (parent == null)
            continue;
          parent.ChildList.Add(node);

          /*if(node.Weight == 9999999)
          {
            parent.Weight = 9999999;
            parent.BestChildPosition = node.Location;
            continue;
          }*/
         

          if ((node.Level % 2) != 0)//Max
          {
            //on remonte le max
            if (parent.Weight < node.Weight)
            {
              parent.Weight = node.Weight - node.Level;
              parent.MakeCheckmateLevel = node.MakeCheckmateLevel;

              if (parent.Level == 0)
              {
                parent.BestChildPosition = node.Location;
              }
                

            }
          }
          else //Min
          {

            if (parent.Weight > node.Weight)
            {
              parent.Weight = node.Weight - node.Level;
              parent.MakeCheckmateLevel = node.MakeCheckmateLevel;
            }
         }
        }
      }

      

    }
    private void GenerateThread(string initialPosition, string evaluatePosition,string actualColore, Node parentNode,Pawn associatePawn,int deepLevel)
    {

      List<Pawn> depPawnsList = new List<Pawn>();
      depPawnsList.AddRange(parentNode.GetCurrentLocalPawnList());
      Pawn selectedPawn = new Pawn();
      selectedPawn = pawnGetPawnsInList(depPawnsList, initialPosition);
      if(selectedPawn == null)
      {
        //return
        var tN = selectedPawn;
      }
      var destinationPawn = pawnGetPawnsInList(depPawnsList, evaluatePosition);
      if (destinationPawn != null)
      {
        if( destinationPawn.Colore=="White" && destinationPawn.Name == "King")
        {
          var tdk = destinationPawn;
        }

        if(selectedPawn.Colore == "Black" && destinationPawn.Colore=="White")
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

      

      if (_computerColore == "Black")
      {
        var weightAndMakeCheckmateLevel = evaluateScoreForBlack(actualColore, depPawnsList, selectedPawn);
        newNode.Weight = weightAndMakeCheckmateLevel[0];
        newNode.MakeCheckmateLevel = weightAndMakeCheckmateLevel[1];
      }
        
     if (_computerColore == "White")
      {
        var weightAndMakeCheckmateLevel = evaluateScoreForWhite(actualColore, depPawnsList, selectedPawn);
        newNode.Weight = weightAndMakeCheckmateLevel[0];
        newNode.MakeCheckmateLevel = weightAndMakeCheckmateLevel[1];
      }
        
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
      originalPawnList.AddRange(PawnList);

      var opignionPawnList = new List<Pawn>();
      foreach (var item in depPawnsList)
      {
        if (item.Colore != actualColore)
        {

          //A VERIFIER
         // PawnList = depPawnsList;
          item.FillPossibleTrips();
          opignionPawnList.Add(item);
          if(item.Name == "Rook" && newNode.Level == 2 && item.Location == "d2" && newNode.AssociatePawn.Name == "Rook" )
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
      if (parentNode.Level < cumputerLevel-1)
      {

        if (newNode.AssociatePawn.Name == "Rook" && newNode.Location == "e1" && newNode.Level == 2 && newNode.Parent.AssociatePawn.Name == "King" && newNode.Parent.AssociatePawn.Location =="d2")
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
            GenerateThread(pawn.Location, pawn.PossibleTrips[i], pawn.Colore, newNode, pawn,deepLevel);
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

      /*
      var maxWeight = Tree.Where(x => x.Level == 0).OrderByDescending(x => x.Weight).First().Weight;
      var bestNodeList = Tree.Where(x => x.Level == 0 && x.Weight == maxWeight);
      Node bestNode = new Node(new List<Pawn>());
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
      //var blackScore = GetScore("Black");
      //var whiteScore = GetScore("White");
      lbInfo.Content = "Best node for "+ bestNode.Colore +" : " + bestNode.AssociatePawn.Name + "  " + bestNode.Weight +"Position : "+ bestNode.Location+ " to " + bestNode.BestChildPosition;
      //  +"    Black score : "+ blackScore+" ("+blackTurnNumber+" turn) "+"    White score : "+ whiteScore+" ("+ whiteTurnNumber + " turn)";
      return bestNode;*/
      DebugTextBlock.Text = "";
      //  bestNode = new Node();


      var bestNodeList = new List<Node>();

      //on prend les milleurs arbre
      foreach (var tree   in AllCumputerPawnTreeList)
      {
        bestNodeList.Add(tree.First());
      }
      var maxWeiht = bestNodeList.OrderByDescending(x => x.Weight).First().Weight;

      var maxMakeCheckmateLevel = bestNodeList.OrderByDescending(x => x.MakeCheckmateLevel).First().MakeCheckmateLevel;

      //on prend tout les maxMakeCheckmateLevel
     var bestMaxWeithNodeList = new List<Node>();
      var temp = bestNodeList.Where(x => x.Weight == maxWeiht && x.MakeCheckmateLevel == maxMakeCheckmateLevel).ToList();
      if(temp.Count >0)
      {
        bestMaxWeithNodeList = temp;
      }
      else
      {
        if (maxMakeCheckmateLevel == 1)
          bestMaxWeithNodeList = bestNodeList.Where(x => x.MakeCheckmateLevel == maxMakeCheckmateLevel).ToList();
        else
          //si maxMakeCheckmateLevel = 0 , on prend les maxWeiht
          bestMaxWeithNodeList = bestNodeList.Where(x => x.Weight == maxWeiht).ToList();
      }
     // var bestMaxWeithNodeList = bestNodeList.Where(x => x.Weight == maxWeiht ).ToList();




      foreach (var node in bestMaxWeithNodeList)
      {
        DebugTextBlock.Text += $"Level:{node.Level} Name:{node.AssociatePawn.Name} Weight:{node.Weight} BestChildPosition:{node.BestChildPosition} MakeCheckmateLevel:{node.MakeCheckmateLevel} \n";
      }
      var t = DebugTextBlock.Text;
      if (bestMaxWeithNodeList.Count() == 1)
      {
        var node = bestMaxWeithNodeList.First(); ;
        DebugTextBlock.Text += $"BEST : {node.Level} {node.AssociatePawn.Name} {node.Weight} {node.BestChildPosition}" +
         $" {node.Location} to {node.BestChildPosition}\n";
        return node;
      }

      //si il y a plusieur meilleurs arbre
      //On Simule les noeuds qui on les milleurs score

      DebugTextBlock.Text += "Next level : \n";
      AllCumputerPawnTreeList = null;
      AllCumputerPawnTreeList = new List<List<Node>>();


      foreach (var node in bestMaxWeithNodeList)
      {
        Tree = null;
        Tree = new List<Node>();
        var pawn = node.AssociatePawn;
        pawn.FillPossibleTrips();


        for (int i = 0; i < pawn.PossibleTrips.Count; i++)
        {
          //deep++;
          GenerateThread(pawn.Location, pawn.PossibleTrips[i], pawn.Colore, node, pawn, cumputerLevel);
          //deepStep = 0;
          foreach (var item in PawnList)
          {
            item.EmulateAllPossibleTips();
          }
        }
        //tC = Tree.Count;
        AllCumputerPawnTreeList.Add(Tree);
      }


      //var tl = bestMaxWeithNodeList.Count();
      var tl = AllCumputerPawnTreeList.Count;
      MinMax();
      var tl0 = AllCumputerPawnTreeList.Count;
      foreach (var tree in AllCumputerPawnTreeList)
      {
        if(tree.Count == 0)
        {
          if (_computerColore == "Black")
            Win("White");
          else
            Win("Black");
        }

        var node = tree.First();
        //DebugTextBlock.Text += $"{node.Level} {node.AssociatePawn.Name} {node.Weight} {node.BestChildPosition} \n";
        DebugTextBlock.Text += $"Level:{node.Level} Name:{node.AssociatePawn.Name} Weight:{node.Weight} BestChildPosition:{node.BestChildPosition} MakeCheckmateLevel:{node.MakeCheckmateLevel} \n";

      }

      var bestNodeListSecond = new List<Node>();

      foreach (var tree in AllCumputerPawnTreeList)
      {
        bestNodeListSecond.Add(tree.First());
      }
       var maxWeihtSecond = bestNodeListSecond.OrderByDescending(x => x.Weight).First().Weight;

      var bestMaxWeithNodeListSecond = bestNodeListSecond.Where(x => x.Weight == maxWeihtSecond);


      var dzeere = bestMaxWeithNodeListSecond.Count();
      if (bestMaxWeithNodeListSecond.Count() == 1)
      {
        var nodeSeconde = bestMaxWeithNodeListSecond.First();
        //var t = nodeSeconde.AssociatePawn;
        //var tl0 = bestMaxWeithNodeList.Count();
        var node = bestMaxWeithNodeList.FirstOrDefault(x => x.AssociatePawn.Name == nodeSeconde.AssociatePawn.Name);

        DebugTextBlock.Text += $"BEST : {node.Level} {node.AssociatePawn.Name} {node.Weight} {node.BestChildPosition}" +
          $" {node.Location} to {node.BestChildPosition}\n";
        return node;
      }
      DebugTextBlock.Text += $"NO BEST FOND\n";

      //quand il n'y a pas de milleurs apres une seconde simulation
      //on prend au hazard

      Random rnd = new Random();
      int index = rnd.Next(0, bestMaxWeithNodeListSecond.Count());
      var nodeSecondeRandom = bestMaxWeithNodeListSecond.ElementAt(index);
      //bestMaxWeithNodeList.RemoveAll(x => x.AssociatePawn.Name == "King" || x.AssociatePawn.Name == "Queen");
      var nodeRandomList = bestMaxWeithNodeList.Where(x => (x.AssociatePawn.Name == nodeSecondeRandom.AssociatePawn.Name)).ToList();
      Random rand = new Random();
      var randomIndex = rand.Next(0, nodeRandomList.Count);

      var nodeRandom = nodeRandomList.ElementAt(randomIndex);


      DebugTextBlock.Text += $"RANDOM : {nodeRandom.Level} {nodeRandom.AssociatePawn.Name} {nodeRandom.Weight} {nodeRandom.BestChildPosition}" +
          $" {nodeRandom.Location} to {nodeRandom.BestChildPosition}\n";

      
      


      return nodeRandom;

    }



    public int GetScore(string colore)
    {

      if(colore=="Black")
      {

        return PawnList.Where(x => x.Colore == "Black").Sum(x => x.Value);
      }
      else
      {
        return PawnList.Where(x => x.Colore == "White").Sum(x => x.Value);
      }

      
    }

    private void simulProgression()
    {
      //pbCalculationProgress.Value = 0;

      BackgroundWorker worker = new BackgroundWorker();
      worker.WorkerReportsProgress = true;
      worker.DoWork += worker_DoWork;
      worker.ProgressChanged += worker_ProgressChanged;
      worker.RunWorkerCompleted += worker_RunWorkerCompleted;
      worker.RunWorkerAsync(10000);
    }
    void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      //pbCalculationProgress.Value = e.ProgressPercentage;
     /* if (e.UserState != null)
        lbResults.Items.Add(e.UserState);*/
    }

    void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      MessageBox.Show("Numbers between 0 and 10000 divisible by 7: " + e.Result);
    }


    void worker_DoWork(object sender, DoWorkEventArgs e)
    {
      int max = (int)e.Argument;
      int result = 0;
      for (int i = 0; i < max; i++)
      {
        int progressPercentage = Convert.ToInt32(((double)i / max) * 100);
        if (i % 42 == 0)
        {
          result++;
          (sender as BackgroundWorker).ReportProgress(progressPercentage, i);
        }
        else
          (sender as BackgroundWorker).ReportProgress(progressPercentage);
        System.Threading.Thread.Sleep(1);

      }
      e.Result = result;
    }


    public Node GetBestPositionAndMoveFor(string colore)
    {
      Thread threadTimer = new Thread(ThreadCountCPUReflectionTime);
      threadTimer.Start();

      var node = new Node();

      //simulProgression();
      
      if(_isInLocalEgine)
      {
        //utilisation du moteur local
        Thread sherchThread = new Thread(() => node = ThreadGetBestMove(colore));
        sherchThread.Start();
      }
      else
      {
        //utilisation du moteur du server
        Thread sherchThread = new Thread(() => ThreadGetBestMoveFromServer(colore));
        sherchThread.Start();
      }



      

     // Notifier.ShowInformation("Move completed");

      return node;
    }

    /*tsiry;26-05-2021
     * copie de GetBestPositionAndMoveFor mais pour les tests
     * */
    public Node GetBestPositionLocalNotTask(string colore)
    {
      return ThreadGetBestMoveNotTask(colore);
    }

    private Node ThreadGetBestMove(string color)
    {

      Thread.Sleep(TimeSpan.FromSeconds(5));
      var node = new Node();
      this.Dispatcher.BeginInvoke(new Action(() =>
      {

        Tree = null;
        Tree = new List<Node>();
        AllCumputerPawnTreeList = null;
        AllCumputerPawnTreeList = new List<List<Node>>();
        if (CurrentTurn == "White")
          GenereTree(color, deepWhiteLevel);
        if (CurrentTurn == "Black")
          GenereTree(color, deepBlackLevel);

        var t_tree = Tree;
        var bestNode = GetBestNodePostion();
        MoveTo(bestNode.Location, bestNode.BestChildPosition);
        _cpuTimer.Stop();

        _lastBestNode = bestNode;
        node= bestNode;





      }));
      return node;


    }

    /*tsiry;26-05-2021
     * copie de ThreadGetBestMove pour les test
     * */
    public Node ThreadGetBestMoveNotTask(string color)
    {



        Tree = null;
        Tree = new List<Node>();
        AllCumputerPawnTreeList = null;
        AllCumputerPawnTreeList = new List<List<Node>>();
        GenereTree(color, 3);
        var t_tree = Tree;
        return GetBestNodePostion();
       





    }


    public static string StartClient(string message)
    {
      byte[] bytes = new byte[1024];

      try
      {
        // Connect to a Remote server  
        // Get Host IP Address that is used to establish a connection  
        // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
        // If a host has multiple addresses, you will get a list of addresses  
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

        // Create a TCP/IP  socket.    
        Socket sender = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);

        // Connect the socket to the remote endpoint. Catch any errors.    
        try
        {
          // Connect to Remote EndPoint  
          sender.Connect(remoteEP);

          Console.WriteLine("Socket connected to {0}",
              sender.RemoteEndPoint.ToString());

          // Encode the data string into a byte array.    
          byte[] msg = Encoding.ASCII.GetBytes(message + "<EOF>");

          // Send the data through the socket.    
          int bytesSent = sender.Send(msg);

          // Receive the response from the remote device.    
          int bytesRec = sender.Receive(bytes);
          Console.WriteLine("Echoed test = {0}",
              Encoding.ASCII.GetString(bytes, 0, bytesRec));
          var result= Encoding.ASCII.GetString(bytes, 0, bytesRec);

          // Release the socket.    
          sender.Shutdown(SocketShutdown.Both);
          sender.Close();
          return result;

        }
        catch (ArgumentNullException ane)
        {
          Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
          return  ane.ToString();
        }
        catch (SocketException se)
        {
          Console.WriteLine("SocketException : {0}", se.ToString());
          return se.ToString();
        }
        catch (Exception e)
        {
          Console.WriteLine("Unexpected exception : {0}", e.ToString());
          return e.ToString();
        }

      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
        return e.ToString();
      }
    }
    private void ThreadGetBestMoveFromServer(string color)
    {
      Thread.Sleep(TimeSpan.FromSeconds(5));
      var pawnStringList = new List<string>();

      foreach (var pawn in PawnList)
      {
        pawnStringList.Add(($"{pawn.Name};{pawn.Location};{pawn.Colore};{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}"));

      }

      var message = String.Join(".", pawnStringList);

      this.Dispatcher.BeginInvoke(new Action(() =>
      {
        var answer = StartClient(color + "." + cumputerLevel.ToString() + "." + message);
        //return answer;
        var answerList = answer.Split(';');
        var location = answerList[0];
        var bestPosition = answerList[1];


        MoveTo(location, bestPosition);
        _cpuTimer.Stop();






      }));


     

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
     // PawnList.AddRange(PawnListBlack);
      //PawnList.AddRange(PawnListWhite);
      //FillAllPossibleTrips();

      if (_computerColore == "Black")
        return;
      if(!String.IsNullOrEmpty(_computerColore))
      {
        var bestNode = GetBestPositionAndMoveFor(_computerColore);
        Save();
       Load();
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

      
      PawnList.AddRange(PawnListWhite);
      PawnList.AddRange(PawnListBlack);
      FillAllPossibleTrips();

      if (_computerColore == "White")
        return;
      if (!String.IsNullOrEmpty(_computerColore))
      {
        var bestNode = GetBestPositionAndMoveFor(_computerColore);
        Save();
        Load();
      }


    }



    private void saveButton_Click(object sender, RoutedEventArgs e)
    {

      Save();
    }

    public void Save()
    {
      var whiteListFile = "./WHITEList.txt";
      var blackListFile = "./BLACKList.txt";
      var whiteListOldFile = "./WHITEListOld.txt";
      var blackListOldFile = "./BLACKListOld.txt";
      //implemenation de preview
      File.Copy(whiteListFile, whiteListOldFile, true);
      File.Copy(blackListFile, blackListOldFile, true);
      using (var writer = new StreamWriter(whiteListFile))
      {

        foreach (var pawn in PawnListWhite)
        {
          writer.WriteLine($"{pawn.Name};{pawn.Location};{pawn.Colore};{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}");
        }
      }
      using (var writer = new StreamWriter(blackListFile))
      {

        foreach (var pawn in PawnListBlack)
        {
          writer.WriteLine($"{pawn.Name};{pawn.Location};{pawn.Colore};{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}");
        }
      }

      //sauvegarde du cemetiaire

      var deadListFile = "./Graveyard.txt";
      using (var writer = new StreamWriter(deadListFile))
      {

        foreach (var item in _deadList)
        {
          writer.WriteLine($"{item}");
        }
      }


    }


    public void CleanPawnList()
    {
      foreach (var  pawn in PawnList)
      {
        pawn.Clean();
      }

      foreach (var pawn in PawnListWhite)
      {
        pawn.Clean();
      }

      foreach (var pawn in PawnListBlack)
      {
        pawn.Clean();
      }

      /* PawnListWhite = new List<Pawn>();
       PawnListBlack = new List<Pawn>();*/


      /* PawnListWhite.Clear();
       PawnListBlack.Clear();
       PawnList.Clear();*/
      PawnListWhite = null;
      PawnListBlack = null;
      PawnList = null;
      PawnListWhite = new List<Pawn>();
      PawnListBlack = new List<Pawn>();
      PawnList = new List<Pawn>();
    }

    private void Load(string old="")
    {
      if(Tree!= null)
        Tree.Clear();
      Tree = null;
      CleanPawnList();

      var pawnListWhite = new List<Pawn>();
      var pawnListBlack =new List< Pawn > ();


      var readText = File.ReadAllText("./WHITEList"+old+".txt");

      using (StringReader sr = new StringReader(readText))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          //Debug.WriteLine(line);

          var datas = line.Split(';');
          var bt = (Button)this.FindName(datas[1]);
          var newPawn = new Pawn(datas[0], datas[1], bt, datas[2], this);
          //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
          newPawn.IsFirstMove = bool.Parse(datas[3]);
          newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
          newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
          newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
          pawnListWhite.Add(newPawn);

        }
      }

      readText = File.ReadAllText("./BLACKList"+old+".txt");

      using (StringReader sr = new StringReader(readText))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          //Debug.WriteLine(line);

          var datas = line.Split(';');
          var bt = (Button)this.FindName(datas[1]);
          var newPawn = new Pawn(datas[0], datas[1], bt, datas[2], this);
          //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
          newPawn.IsFirstMove = bool.Parse(datas[3]);
          newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
          newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
          newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
          pawnListBlack.Add(newPawn);

        }
      }

      /*if(_computerColore=="White")
     {
       PawnList.AddRange(PawnListWhite);
       PawnList.AddRange(PawnListBlack);
     }
     if (_computerColore == "Black")
     {
       PawnList.AddRange(PawnListBlack);
       PawnList.AddRange(PawnListWhite);

     }*/
      /*PawnList.AddRange(PawnListWhite);
      PawnList.AddRange(PawnListBlack);*/
      /*if (_currentTurn == "White")
      {
        PawnList.AddRange(PawnListWhite);
        PawnList.AddRange(PawnListBlack);
      }
      if (_currentTurn == "Black")
      {
        PawnList.AddRange(PawnListBlack);
        PawnList.AddRange(PawnListWhite);

        }*/



      FillPawnListAndFillAllPossibleTrips(pawnListWhite, pawnListBlack) ;






    }

    public void FillPawnListAndFillAllPossibleTrips(List<Pawn> pawnListWhite, List<Pawn> pawnListBlack)
    {
      
      PawnList.AddRange(pawnListWhite);
      PawnList.AddRange(pawnListBlack);


      PawnListWhite = PawnList.Where(x => x.Colore == "White").ToList();
      PawnListBlack = PawnList.Where(x => x.Colore == "Black").ToList();





      var whiteScore = GetScore("White");
      var blackScore = GetScore("Black");
      lbWhiteScore.Content = whiteScore;
      lbBlackScore.Content = blackScore;
      /*if (blackScore> whiteScore)
      {
        PawnList = PawnList.OrderByDescending(x => x.Value).ToList();
      }
      else
      {
        PawnList = PawnList.OrderBy(x => x.Value).ToList();
      }*/
      /*PawnList = PawnList.OrderByDescending(x => x.Value).ToList();

      if(_computerColore == "Black")
        PawnList = PawnList.OrderByDescending(x => x.Value).ToList();
      else*/
      // PawnList = PawnList.OrderByDescending(x => x.Value).ToList();


      
      //OK=>T05,T11
      //NOK=>T02
      if(whiteScore<=blackScore)
        PawnList = PawnList.OrderByDescending(x => x.Value).ToList();
      else
        PawnList = PawnList.OrderBy(x => x.Value).ToList();

      //
      //T07EchecRookBlancDoitAttaquerLeRoiNoir
      // if (_computerColore == "White")
      // PawnList = PawnList.OrderBy(x => x.Value).ToList();
      //PawnList

      FillAllPossibleTrips();

    }


    /*tsiry;10-05-2021
     * lecture du cemetiére
     * */

    public void LoadGraveyardFile()
    {
      //lecture du cimetiére
      if (!_graveyardIsLoaded)
      {
        var readText = File.ReadAllText("./Graveyard.txt");

        using (StringReader sr = new StringReader(readText))
        {
          string line;
          while ((line = sr.ReadLine()) != null)
          {
            //Debug.WriteLine(line);

            fillGraveyard(line);
            _deadList.Add(line);

          }
        }
        calculatePoint();
      }
       
      _graveyardIsLoaded = true;


    }

    private void loadButton_Click(object sender, RoutedEventArgs e)
    {
      Load();
      
    }

    private void ChoseWhiteForCoputerButon_Click(object sender, RoutedEventArgs e)
    {
      _computerColore = "White";
      ChoseBlackForCoputerButon.IsEnabled = false;
      deepWhiteLevel = cumputerLevel;//5;//5;//4;

    }

    private void ChoseBlackForCoputerButon_Click(object sender, RoutedEventArgs e)
    {
      _computerColore = "Black";
      ChoseWhiteForCoputerButon.IsEnabled = false;
      deepBlackLevel = cumputerLevel;//5;//5;//4;
    }

    private void PreviousButon_Click(object sender, RoutedEventArgs e)
    {
      Load("Old");
      if (CurrentTurn == "Black")
        CurrentTurn = "White";
      if (CurrentTurn == "White")
        CurrentTurn = "Black";
      LoadGraveyardFile();
    }

    private void GrapheButon_Click(object sender, RoutedEventArgs e)
    {

      if (Tree == null)
        return;
      //Load("Old");
      _computerColore = "Black";
      GenereTree("Black", cumputerLevel);
      var t_t = AllCumputerPawnTreeList;
      var treeGrapheForm = new TreeGrapheForm(Tree);
      treeGrapheForm.Show();

       

      


    }

    public void WriteInLog(string logMessage)
    {
      var whiteListFile = "./LOG.txt";
      using (var writer = new StreamWriter(whiteListFile))
      {
        writer.WriteLine($"{logMessage}");
      }
    }

    private void SwithToServerEngineButon_Click(object sender, RoutedEventArgs e)
    {
      _isInLocalEgine = true;
      SwithToServerEngineButon.Visibility = Visibility.Collapsed;
      SwithToLocalEngineButon.Visibility = Visibility.Visible;
    }

    private void SwithToLocalEngineButon_Click(object sender, RoutedEventArgs e)
    {
      _isInLocalEgine = false;
      SwithToServerEngineButon.Visibility = Visibility.Visible;
      SwithToLocalEngineButon.Visibility = Visibility.Collapsed;
    }
  }
}
