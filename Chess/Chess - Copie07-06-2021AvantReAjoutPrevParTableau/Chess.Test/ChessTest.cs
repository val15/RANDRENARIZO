using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Chess;
using Chess.Utils;
using System.Windows.Controls;
using System.Linq;
using System.Collections.Generic;

namespace Chess.Test
{
  [TestClass]
  public class ChessTest
  {

    [TestMethod]
    public void T00aLeKnigntBlanchNeDoitPasAttaquer()
    {
      /*La cavalier blanch ne doit pas attaquer*/
      //Positions final du cavalier Blach ne doit pas etre  ni "a7" ni "c7" 

      var mainWindow = new MainWindow();
      mainWindow.ComputerColore = "White";
      if (mainWindow.Tree != null)
        mainWindow.Tree.Clear();
      mainWindow.Tree = null;
      mainWindow.CleanPawnList();
      var pawnListWhite = new List<Pawn>();
      var pawnListBlack = new List<Pawn>();


      //WHITEList
      var whiteListString = "" +
        "Rook;a1;White;False;False;False;False"+
"\nSimplePawn;a2;White;True;False;False;False"+
"\nKnight;b5;White;False;False;False;False"+
"\nSimplePawn;b2;White;True;False;False;False"+
"\nBishop;c1;White;False;False;False;False"+
"\nSimplePawn;c2;White;True;False;False;False"+
"\nQueen;d1;White;False;False;False;False"+
"\nSimplePawn;d2;White;True;False;False;False"+
"\nKing;e1;White;False;True;True;True"+
"\nSimplePawn;e2;White;True;False;False;False"+
"\nBishop;f1;White;False;False;False;False"+
"\nSimplePawn;f2;White;True;False;False;False"+
"\nKnight;g1;White;False;False;False;False"+
"\nSimplePawn;g2;White;True;False;False;False"+
"\nRook;h1;White;False;False;False;False"+
"\nSimplePawn;h2;White;True;False;False;False";
      var whiteList = whiteListString.Split('\n');
      foreach (var line in whiteList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListWhite.Add(newPawn);
      }

      //BLACKList
      var blackListString = "" +
      "SimplePawn;a7;Black;True;False;False;False"+
"\nRook;a8;Black;False;False;False;False"+
"\nSimplePawn;b7;Black;True;False;False;False"+
"\nKnight;b8;Black;False;False;False;False"+
"\nSimplePawn;c7;Black;True;False;False;False"+
"\nBishop;c8;Black;False;False;False;False"+
"\nSimplePawn;d7;Black;True;False;False;False"+
"\nQueen;d8;Black;False;False;False;False"+
"\nSimplePawn;e6;Black;False;False;False;False"+
"\nKing;e8;Black;False;True;True;True"+
"\nSimplePawn;f7;Black;True;False;False;False"+
"\nBishop;b4;Black;False;False;False;False"+
"\nSimplePawn;g7;Black;True;False;False;False"+
"\nKnight;g8;Black;False;False;False;False"+
"\nSimplePawn;h7;Black;True;False;False;False"+
"\nRook;h8;Black;False;False;False;False";
      var blackList = blackListString.Split('\n');
      foreach (var line in blackList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListBlack.Add(newPawn);
      }


      mainWindow.FillPawnListAndFillAllPossibleTrips(pawnListWhite, pawnListBlack);


      var nodeResult = mainWindow.GetBestPositionLocalNotTask(mainWindow.ComputerColore);
      //Positions final du cavalier Blach ne doit pas etre  ni "a7" ni "c7"
      Assert.AreNotEqual(nodeResult.BestChildPosition, "a7","c7");
    }

    [TestMethod]
    public void T00bLeKnigntNoirNeDoitPasAttaquer()
    {
      /*La cavalier noit ne doit pas attaquer*/
      //Positions final du cavalier noir ne doit pas etre  ni "a2" ni "c2" 

      var mainWindow = new MainWindow();
      mainWindow.ComputerColore = "Black";
      if (mainWindow.Tree != null)
        mainWindow.Tree.Clear();
      mainWindow.Tree = null;
      mainWindow.CleanPawnList();
      var pawnListWhite = new List<Pawn>();
      var pawnListBlack = new List<Pawn>();


      //WHITEList
      var whiteListString = "" +
        "Rook;a1;White;False;False;False;False" +
"\nSimplePawn;a2;White;True;False;False;False" +
"\nKnight;b5;White;False;False;False;False" +
"\nSimplePawn;b2;White;True;False;False;False" +
"\nBishop;c1;White;False;False;False;False" +
"\nSimplePawn;c2;White;True;False;False;False" +
"\nQueen;d1;White;False;False;False;False" +
"\nSimplePawn;d2;White;True;False;False;False" +
"\nKing;e1;White;False;True;True;True" +
"\nSimplePawn;e2;White;True;False;False;False" +
"\nBishop;f1;White;False;False;False;False" +
"\nSimplePawn;f2;White;True;False;False;False" +
"\nKnight;g1;White;False;False;False;False" +
"\nSimplePawn;g2;White;True;False;False;False" +
"\nRook;h1;White;False;False;False;False" +
"\nSimplePawn;h2;White;True;False;False;False";
      var whiteList = whiteListString.Split('\n');
      foreach (var line in whiteList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListWhite.Add(newPawn);
      }

      //BLACKList
      var blackListString = "" +
      "SimplePawn;a7;Black;True;False;False;False" +
"\nRook;a8;Black;False;False;False;False" +
"\nSimplePawn;b7;Black;True;False;False;False" +
"\nKnight;b8;Black;False;False;False;False" +
"\nSimplePawn;c7;Black;True;False;False;False" +
"\nBishop;c8;Black;False;False;False;False" +
"\nSimplePawn;d7;Black;True;False;False;False" +
"\nQueen;d8;Black;False;False;False;False" +
"\nSimplePawn;e6;Black;False;False;False;False" +
"\nKing;e8;Black;False;True;True;True" +
"\nSimplePawn;f7;Black;True;False;False;False" +
"\nBishop;b4;Black;False;False;False;False" +
"\nSimplePawn;g7;Black;True;False;False;False" +
"\nKnight;g8;Black;False;False;False;False" +
"\nSimplePawn;h7;Black;True;False;False;False" +
"\nRook;h8;Black;False;False;False;False";
      var blackList = blackListString.Split('\n');
      foreach (var line in blackList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListBlack.Add(newPawn);
      }


      mainWindow.FillPawnListAndFillAllPossibleTrips(pawnListWhite, pawnListBlack);


      var nodeResult = mainWindow.GetBestPositionLocalNotTask(mainWindow.ComputerColore);
      //Positions final du cavalier noir ne doit pas etre  ni "a2" ni "c2" 
      Assert.AreNotEqual(nodeResult.BestChildPosition, "a2", "c2");
    }



    [TestMethod]
    public void T01QuenLaReineNoirNeDoitPasPrendreLeCavalier()
    {
      /*La reine noir ne doit pas prendre le cavalier*/
      //Position final de la reine Noir ne doit pas etre "g5"

      var mainWindow = new MainWindow();
      mainWindow.ComputerColore = "Black";
      if (mainWindow.Tree != null)
        mainWindow.Tree.Clear();
      mainWindow.Tree = null;
      mainWindow.CleanPawnList();
      var pawnListWhite = new List<Pawn>();
      var pawnListBlack = new List<Pawn>();


      //WHITEList
      var whiteListString = "" +
        "King;g1;White;False;True;True;True"+
"\nQueen;f3;White;False;False;False;False"+
"\nRook;a1;White;False;False;False;False"+
"\nRook;f1;White;False;False;False;False"+
"\nKnight;b1;White;False;False;False;False"+
"\nKnight;g5;White;False;False;False;False"+
"\nBishop;c1;White;False;False;False;False"+
"\nBishop;b3;White;False;False;False;False"+
"\nSimplePawn;a2;White;True;False;False;False"+
"\nSimplePawn;b2;White;True;False;False;False"+
"\nSimplePawn;c2;White;True;False;False;False"+
"\nSimplePawn;d4;White;False;False;False;False"+
"\nSimplePawn;e5;White;False;False;False;False"+
"\nSimplePawn;f2;White;True;False;False;False"+
"\nSimplePawn;g2;White;True;False;False;False"+
"\nSimplePawn;h2;White;True;False;False;False";
      var whiteList = whiteListString.Split('\n');
      foreach (var line in whiteList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListWhite.Add(newPawn);
      }

      //BLACKList
      var blackListString = "" +
      "King;e8;Black;False;True;True;True"+
"\nQueen;d8;Black;False;False;False;False"+
"\nRook;a8;Black;False;False;False;False"+
"\nRook;h8;Black;False;False;False;False"+
"\nKnight;b8;Black;False;False;False;False"+
"\nBishop;c8;Black;False;False;False;False"+
"\nBishop;f8;Black;False;False;False;False"+
"\nKnight;g8;Black;False;False;False;False"+
"\nSimplePawn;a7;Black;True;False;False;False"+
"\nSimplePawn;b5;Black;False;False;False;False"+
"\nSimplePawn;c6;Black;False;False;False;False"+
"\nSimplePawn;d5;Black;False;False;False;False"+
"\nSimplePawn;e6;Black;False;False;False;False"+
"\nSimplePawn;f7;Black;True;False;False;False"+
"\nSimplePawn;g6;Black;False;False;False;False"+
"\nSimplePawn;h4;Black;False;False;False;False";
      var blackList = blackListString.Split('\n');
      foreach (var line in blackList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListBlack.Add(newPawn);
      }


      mainWindow.FillPawnListAndFillAllPossibleTrips(pawnListWhite, pawnListBlack);


      var nodeResult = mainWindow.GetBestPositionLocalNotTask(mainWindow.ComputerColore);
      //Position final de la reine Noir ne doit pas etre "g5"
      Assert.AreNotEqual(nodeResult.BestChildPosition, "g5");
    }


    [TestMethod]
    public void T02aLeRookBlanchNeDoitPasPrendresLePion()
    {
      /*Le Rook blanc ne doit pas prendre le pion*/
      //Position final du Rook blanch  ne doit pas etre "a7"

      var mainWindow = new MainWindow();
      mainWindow.ComputerColore = "White";
      if (mainWindow.Tree != null)
        mainWindow.Tree.Clear();
      mainWindow.Tree = null;
      mainWindow.CleanPawnList();
      var pawnListWhite = new List<Pawn>();
      var pawnListBlack = new List<Pawn>();


      //WHITEList
      var whiteListString = "" +
        "King;e1;White;False;True;True;True"+
"\nRook;a1;White;False;False;False;False"+
"\nKnight;b1;White;False;False;False;False"+
"\nSimplePawn;b2;White;True;False;False;False"+
"\nBishop;c1;White;False;False;False;False"+
"\nSimplePawn;c4;White;False;False;False;False"+
"\nSimplePawn;d3;White;False;False;False;False"+
"\nBishop;f1;White;False;False;False;False"+
"\nSimplePawn;f4;White;False;False;False;False"+
"\nKnight;g1;White;False;False;False;False"+
"\nSimplePawn;g2;White;True;False;False;False"+
"\nQueen;h1;White;False;False;False;False"+
"\nSimplePawn;h2;White;True;False;False;False";
      var whiteList = whiteListString.Split('\n');
      foreach (var line in whiteList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListWhite.Add(newPawn);
      }

      //BLACKList
      var blackListString = "" +
       "King;d8;Black;False;True;True;True"+
"\nRook;h8;Black;False;False;False;False"+
"\nKnight;b8;Black;False;False;False;False"+
"\nSimplePawn;g7;Black;True;False;False;False"+
"\nBishop;c8;Black;False;False;False;False"+
"\nSimplePawn;f5;Black;False;False;False;False"+
"\nSimplePawn;e6;Black;False;False;False;False"+
"\nBishop;f8;Black;False;False;False;False"+
"\nSimplePawn;c5;Black;False;False;False;False"+
"\nKnight;g8;Black;False;False;False;False"+
"\nSimplePawn;b7;Black;True;False;False;False"+
"\nQueen;a8;Black;False;False;False;False"+
"\nSimplePawn;a7;Black;True;False;False;False";
      var blackList = blackListString.Split('\n');
      foreach (var line in blackList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListBlack.Add(newPawn);
      }


      mainWindow.FillPawnListAndFillAllPossibleTrips(pawnListWhite, pawnListBlack);


      var nodeResult = mainWindow.GetBestPositionLocalNotTask(mainWindow.ComputerColore);
      //Position final du rook blanch  ne doit pas etre "a7"
      Assert.AreNotEqual(nodeResult.BestChildPosition, "a7");
    }

    [TestMethod]
    public void T02bLeRookNoirNeDoitPasPrendresLePion()
    {
      /*Le Rook noir ne doit pas prendre le pion*/
      //Position final du Rook blanch  ne doit pas etre "h2"

      var mainWindow = new MainWindow();
      mainWindow.ComputerColore = "Black";
      if (mainWindow.Tree != null)
        mainWindow.Tree.Clear();
      mainWindow.Tree = null;
      mainWindow.CleanPawnList();
      var pawnListWhite = new List<Pawn>();
      var pawnListBlack = new List<Pawn>();


      //WHITEList
      var whiteListString = "" +
        "King;e1;White;False;True;True;True" +
"\nRook;a1;White;False;False;False;False" +
"\nKnight;b1;White;False;False;False;False" +
"\nSimplePawn;b2;White;True;False;False;False" +
"\nBishop;c1;White;False;False;False;False" +
"\nSimplePawn;c4;White;False;False;False;False" +
"\nSimplePawn;d3;White;False;False;False;False" +
"\nBishop;f1;White;False;False;False;False" +
"\nSimplePawn;f4;White;False;False;False;False" +
"\nKnight;g1;White;False;False;False;False" +
"\nSimplePawn;g2;White;True;False;False;False" +
"\nQueen;h1;White;False;False;False;False" +
"\nSimplePawn;h2;White;True;False;False;False";
      var whiteList = whiteListString.Split('\n');
      foreach (var line in whiteList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListWhite.Add(newPawn);
      }

      //BLACKList
      var blackListString = "" +
       "King;d8;Black;False;True;True;True" +
"\nRook;h8;Black;False;False;False;False" +
"\nKnight;b8;Black;False;False;False;False" +
"\nSimplePawn;g7;Black;True;False;False;False" +
"\nBishop;c8;Black;False;False;False;False" +
"\nSimplePawn;f5;Black;False;False;False;False" +
"\nSimplePawn;e6;Black;False;False;False;False" +
"\nBishop;f8;Black;False;False;False;False" +
"\nSimplePawn;c5;Black;False;False;False;False" +
"\nKnight;g8;Black;False;False;False;False" +
"\nSimplePawn;b7;Black;True;False;False;False" +
"\nQueen;a8;Black;False;False;False;False" +
"\nSimplePawn;a7;Black;True;False;False;False";
      var blackList = blackListString.Split('\n');
      foreach (var line in blackList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListBlack.Add(newPawn);
      }


      mainWindow.FillPawnListAndFillAllPossibleTrips(pawnListWhite, pawnListBlack);


      var nodeResult = mainWindow.GetBestPositionLocalNotTask(mainWindow.ComputerColore);
      //Position final du rook blanch  ne doit pas etre "a7"
      Assert.AreNotEqual(nodeResult.BestChildPosition, "h2");
    }


    [TestMethod]
    public void T04LePionBlanchNeDoitPasprendreLeRookNoir()
    {
      /*Le pion blanc ne doit pas attaquer le rooknoir*/
      //Position final du pion blanch  ne doit pas etre "a3"

      var mainWindow = new MainWindow();
      mainWindow.ComputerColore = "White";
      if (mainWindow.Tree != null)
        mainWindow.Tree.Clear();
      mainWindow.Tree = null;
      mainWindow.CleanPawnList();
      var pawnListWhite = new List<Pawn>();
      var pawnListBlack = new List<Pawn>();


      //WHITEList
      var whiteListString = "" +
        "King;f2;White;False;False;True;False" +
        "\nQueen;d1;White;False;False;False;False"+
"\nRook;a1;White;False;False;False;False"+
"\nRook;h2;White;False;False;False;False"+
"\nBishop;c1;White;False;False;False;False"+
"\nBishop;f1;White;False;False;False;False"+
"\nKnight;g1;White;False;False;False;False"+
"\nSimplePawn;a2;White;True;False;False;False"+
"\nSimplePawn;b2;White;True;False;False;False"+
"\nSimplePawn;c2;White;True;False;False;False"+
"\nSimplePawn;d3;White;False;False;False;False"+
"\nSimplePawn;e3;White;False;False;False;False"+
"\nSimplePawn;f4;White;False;False;False;False"+
"\nSimplePawn;g3;White;False;False;False;False"+
"\nSimplePawn;h4;White;False;False;False;False";
      var whiteList = whiteListString.Split('\n');
      foreach (var line in whiteList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListWhite.Add(newPawn);
      }

      //BLACKList
      var blackListString = "" +
       "King;e8;Black;False;True;False;True"+
"\nQueen;d8;Black;False;False;False;False"+
"\nRook;a3;Black;False;False;False;False"+
"\nRook;h8;Black;False;False;False;False"+
"\nBishop;b7;Black;False;False;False;False"+
"\nBishop;g7;Black;False;False;False;False"+
"\nKnight;b8;Black;False;False;False;False"+
"\nKnight;h6;Black;False;False;False;False"+
"\nSimplePawn;b6;Black;False;False;False;False"+
"\nSimplePawn;c7;Black;True;False;False;False"+
"\nSimplePawn;d5;Black;False;False;False;False"+
"\nSimplePawn;e7;Black;True;False;False;False"+
"\nSimplePawn;f7;Black;True;False;False;False"+
"\nSimplePawn;g6;Black;False;False;False;False"+
"\nSimplePawn;h7;Black;True;False;False;False";
      var blackList = blackListString.Split('\n');
      foreach (var line in blackList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListBlack.Add(newPawn);
      }


      mainWindow.FillPawnListAndFillAllPossibleTrips(pawnListWhite, pawnListBlack);


      var nodeResult = mainWindow.GetBestPositionLocalNotTask(mainWindow.ComputerColore);
      //Position final du pion blanch  ne doit pas etre "a3"
      Assert.AreNotEqual(nodeResult.BestChildPosition, "a3");

    }

    [TestMethod]
    public void T05LeFousBlacheNeDoitPasPrendreLePionNois()
    {
      /*Le Fous blanc doit attaquer le pion noir
       * les noirs*/
      //Position final du Fous blanch  doit etre "a7"




      var mainWindow = new MainWindow();
      mainWindow.ComputerColore = "White";
      if (mainWindow.Tree != null)
        mainWindow.Tree.Clear();
      mainWindow.Tree = null;
      mainWindow.CleanPawnList();
      var pawnListWhite = new List<Pawn>();
      var pawnListBlack = new List<Pawn>();


      //WHITEList
      var whiteListString = "Rook;a1;White;False;False;False;False"+
"\nSimplePawn;a4;White;False;False;False;False"+
"\nKnight;b1;White;False;False;False;False"+
"\nBishop;e3;White;False;False;False;False"+
"\nSimplePawn;c2;White;True;False;False;False"+
"\nQueen;d1;White;False;False;False;False"+
"\nSimplePawn;d3;White;False;False;False;False"+
"\nKing;e1;White;False;True;True;True"+
"\nSimplePawn;e2;White;True;False;False;False"+
"\nBishop;f1;White;False;False;False;False"+
"\nSimplePawn;f2;White;True;False;False;False"+
"\nKnight;g1;White;False;False;False;False"+
"\nSimplePawn;g2;White;True;False;False;False"+
"\nRook;h1;White;False;False;False;False"+
"\nSimplePawn;h2;White;True;False;False;False";
      var whiteList = whiteListString.Split('\n');
      foreach (var line in whiteList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListWhite.Add(newPawn);
      }

      //BLACKList
      var blackListString = "" +
       "SimplePawn;a7;Black;True;False;False;False"+
"\nRook;a8;Black;False;False;False;False"+
"\nSimplePawn;b7;Black;True;False;False;False"+
"\nKnight;b8;Black;False;False;False;False"+
"\nSimplePawn;c7;Black;True;False;False;False"+
"\nBishop;c8;Black;False;False;False;False"+
"\nSimplePawn;d7;Black;True;False;False;False"+
"\nQueen;b2;Black;False;False;False;False"+
"\nSimplePawn;e6;Black;False;False;False;False"+
"\nKing;e8;Black;False;True;True;True"+
"\nSimplePawn;f7;Black;True;False;False;False"+
"\nBishop;d6;Black;False;False;False;False"+
"\nSimplePawn;g7;Black;True;False;False;False"+
"\nKnight;g8;Black;False;False;False;False"+
"\nSimplePawn;h7;Black;True;False;False;False"+
"\nRook;h8;Black;False;False;False;False";
      var blackList = blackListString.Split('\n');
      foreach (var line in blackList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListBlack.Add(newPawn);
      }


      mainWindow.FillPawnListAndFillAllPossibleTrips(pawnListWhite, pawnListBlack);


      var nodeResult = mainWindow.GetBestPositionLocalNotTask(mainWindow.ComputerColore);
      //Position final du Fous blanch  doit etre "a7"
      Assert.AreNotEqual(nodeResult.AssociatePawn.Name, "Bishop");
      Assert.AreNotEqual(nodeResult.BestChildPosition, "a7");
    }



    [TestMethod]
    public void T07aEchecRookBlancDoitAttaquerLeRoiNoir()
    {
      /*Le Rook blanc doit attaquer le roir noir pout tout de suite mettre en echec 
       * les noirs*/
      //Position final du rook blanch  doit etre "d8"




      var mainWindow = new MainWindow();
      mainWindow.ComputerColore = "White";
      if (mainWindow.Tree != null)
        mainWindow.Tree.Clear();
      mainWindow.Tree = null;
      mainWindow.CleanPawnList();
      var pawnListWhite = new List<Pawn>();
      var pawnListBlack = new List<Pawn>();


      //WHITEList
      var whiteListString = "King;h4;White;False;False;False;True" +
"\nQueen;e1;White;False;False;False;False"+
"\nRook;d5;White;False;False;False;False"+
"\nRook;h1;White;False;False;False;False"+
"\nKnight;g1;White;False;False;False;False"+
"\nSimplePawn;a3;White;False;False;False;False"+
"\nSimplePawn;c3;White;False;False;False;False"+
"\nSimplePawn;e3;White;False;False;False;False"+
"\nSimplePawn;g4;White;False;False;False;False"+
"\nSimplePawn;h3;White;False;False;False;False";
      var whiteList = whiteListString.Split('\n');
      foreach (var line in whiteList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListWhite.Add(newPawn);
      }

      //BLACKList
      var blackListString = "King;g8;Black;False;True;False;True" +
        "\nQueen;g6;Black;False;False;False;False"+
"\nRook;c6;Black;False;False;False;False"+
"\nKnight;a7;Black;False;False;False;False"+
"\nSimplePawn;c7;Black;True;False;False;False"+
"\nSimplePawn;f7;Black;True;False;False;False"+
"\nSimplePawn;g7;Black;True;False;False;False"+
"\nSimplePawn;h7;Black;True;False;False;False";
      var blackList = blackListString.Split('\n');
      foreach (var line in blackList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListBlack.Add(newPawn);
      }


      mainWindow.FillPawnListAndFillAllPossibleTrips(pawnListWhite, pawnListBlack);
      
        
        var nodeResult = mainWindow.GetBestPositionLocalNotTask(mainWindow.ComputerColore);
      //rook blanch  doit etre "d8"
      Assert.AreEqual(nodeResult.AssociatePawn.Name, "Rook");
      Assert.AreEqual(nodeResult.BestChildPosition, "d8");
    }


    [TestMethod]
    public void T07bEchecRookOuReineBlancDoitAttaquerLeRoiNoir()
    {
      /*Le Rook ou la reinne blanc doit attaquer le roir noir pout tout de suite mettre en echec 
       * les noirs*/
      //Position final  blanche  doit etre "d8" ou "e8"




      var mainWindow = new MainWindow();
      mainWindow.ComputerColore = "White";
      if (mainWindow.Tree != null)
        mainWindow.Tree.Clear();
      mainWindow.Tree = null;
      mainWindow.CleanPawnList();
      var pawnListWhite = new List<Pawn>();
      var pawnListBlack = new List<Pawn>();


      //WHITEList
      var whiteListString = "King;h4;White;False;False;False;True" +
"\nQueen;e1;White;False;False;False;False" +
"\nRook;d5;White;False;False;False;False" +
"\nRook;h1;White;False;False;False;False" +
"\nKnight;g1;White;False;False;False;False";
      var whiteList = whiteListString.Split('\n');
      foreach (var line in whiteList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListWhite.Add(newPawn);
      }

      //BLACKList
      var blackListString = "King;g8;Black;False;True;False;True" +
        "\nQueen;g6;Black;False;False;False;False" +
"\nRook;c6;Black;False;False;False;False" +
"\nKnight;a7;Black;False;False;False;False" +
"\nSimplePawn;c7;Black;True;False;False;False" +
"\nSimplePawn;f7;Black;True;False;False;False" +
"\nSimplePawn;g7;Black;True;False;False;False" +
"\nSimplePawn;h7;Black;True;False;False;False";
      var blackList = blackListString.Split('\n');
      foreach (var line in blackList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListBlack.Add(newPawn);
      }


      mainWindow.FillPawnListAndFillAllPossibleTrips(pawnListWhite, pawnListBlack);


      var nodeResult = mainWindow.GetBestPositionLocalNotTask(mainWindow.ComputerColore);
      //Position final blanche  doit etre "d8" ou "e8"
      //Assert.AreEqual(nodeResult.AssociatePawn.Name, "Rook");
      var isSucces = false;
      if (nodeResult.BestChildPosition == "d8" || nodeResult.BestChildPosition == "e8")
        isSucces = true;
      Assert.IsTrue(isSucces);
    }


    [TestMethod]
    public void T11LaReineBlancNeDoitPasAttaqueLePion()
    {
      /*La reine blanc ne doit pas attaquer le pion noir en g6*/
      //la reine blanche ne doit se mettre sur "g6"




      var mainWindow = new MainWindow();
      mainWindow.ComputerColore = "White";
      if (mainWindow.Tree != null)
        mainWindow.Tree.Clear();
      mainWindow.Tree = null;
      mainWindow.CleanPawnList();
      var pawnListWhite = new List<Pawn>();
      var pawnListBlack = new List<Pawn>();



      //WHITEList
      var whiteListString = "Rook;a1;White;False;False;False;False"+
"\nSimplePawn;a2;White;True;False;False;False"+
"\nKnight;b1;White;False;False;False;False"+
"\nSimplePawn;b2;White;True;False;False;False"+
"\nBishop;c1;White;False;False;False;False"+
"\nSimplePawn;c3;White;False;False;False;False"+
"\nQueen;c2;White;False;False;False;False"+
"\nSimplePawn;d2;White;True;False;False;False"+
"\nKing;e1;White;False;True;True;True"+
"\nSimplePawn;e2;White;True;False;False;False"+
"\nBishop;f1;White;False;False;False;False"+
"\nSimplePawn;f2;White;True;False;False;False"+
"\nKnight;g1;White;False;False;False;False"+
"\nSimplePawn;g2;White;True;False;False;False"+
"\nRook;h1;White;False;False;False;False"+
"\nSimplePawn; h2; White; True; False; False; False";
      var whiteList = whiteListString.Split('\n');
      foreach (var line in whiteList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListWhite.Add(newPawn);
      }

      //BLACKList
      var blackListString = "SimplePawn;a7;Black;True;False;False;False" +
"\nRook;a8;Black;False;False;False;False" +
"\nSimplePawn;b7;Black;True;False;False;False" +
"\nKnight;b8;Black;False;False;False;False" +
"\nSimplePawn;c7;Black;True;False;False;False" +
"\nBishop;c8;Black;False;False;False;False" +
"\nSimplePawn;d7;Black;True;False;False;False" +
"\nQueen;d8;Black;False;False;False;False" +
"\nSimplePawn;e6;Black;False;False;False;False" +
"\nKing;e8;Black;False;True;True;True" +
"\nSimplePawn;f7;Black;True;False;False;False" +
"\nBishop;f8;Black;False;False;False;False" +
"\nSimplePawn;g6;Black;False;False;False;False" +
"\nKnight;g8;Black;False;False;False;False" +
"\nSimplePawn;h7;Black;True;False;False;False" +
"\nRook;h8;Black;False;False;False;False";
      var blackList = blackListString.Split('\n');
      foreach (var line in blackList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListBlack.Add(newPawn);
      }



      mainWindow.FillPawnListAndFillAllPossibleTrips(pawnListWhite, pawnListBlack);


      var nodeResult = mainWindow.GetBestPositionLocalNotTask(mainWindow.ComputerColore);
      //la reine blanche ne doit se mettre sur "g6"
      Assert.AreNotEqual(nodeResult.BestChildPosition, "g6");
    }


    [TestMethod]
    public void T15LaReineBlanchNeDoitPasPrendreLePion()
    {
      /*La reine blanc ne doit pas attaquer le pion noir en a6*/
      ////la reine blanche ne doit se mettre sur "a6"




      var mainWindow = new MainWindow();
      mainWindow.ComputerColore = "White";
      if (mainWindow.Tree != null)
        mainWindow.Tree.Clear();
      mainWindow.Tree = null;
      mainWindow.CleanPawnList();
      var pawnListWhite = new List<Pawn>();
      var pawnListBlack = new List<Pawn>();



      //WHITEList
      var whiteListString = "" +
        "SimplePawn;a2;White;True;False;False;False" +
        "\nSimplePawn;a3;White;False;False;False;False"+
"\nSimplePawn;c3;White;False;False;False;False"+
"\nSimplePawn;d3;White;False;False;False;False"+
"\nSimplePawn;e3;White;False;False;False;False"+
"\nSimplePawn;f2;White;True;False;False;False"+
"\nSimplePawn;g2;White;True;False;False;False"+
"\nSimplePawn;h2;White;True;False;False;False"+
"\nKnight;g1;White;False;False;False;False"+
"\nBishop;b2;White;False;False;False;False"+
"\nBishop;f1;White;False;False;False;False"+
"\nRook;a1;White;False;False;False;False"+
"\nRook;h1;White;False;False;False;False"+
"\nQueen;a4;White;False;False;False;False"+
"\nKing;e2;White;False;False;True;True";
      var whiteList = whiteListString.Split('\n');
      foreach (var line in whiteList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListWhite.Add(newPawn);
      }

      //BLACKList
      var blackListString = "" +
        "SimplePawn;a6;Black;False;False;False;False" +
        "\nSimplePawn;b7;Black;True;False;False;False"+
"\nSimplePawn;c7;Black;True;False;False;False"+
"\nSimplePawn;d5;Black;False;False;False;False"+
"\nSimplePawn;e6;Black;False;False;False;False"+
"\nSimplePawn;f7;Black;True;False;False;False"+
"\nSimplePawn;g7;Black;True;False;False;False"+
"\nSimplePawn;h7;Black;True;False;False;False"+
"\nKnight;b8;Black;False;False;False;False"+
"\nKnight;f6;Black;False;False;False;False"+
"\nRook;a8;Black;False;False;False;False"+
"\nRook;f8;Black;False;False;False;False"+
"\nQueen;d8;Black;False;False;False;False"+
"\nKing;g8;Black;False;True;True;True";
      var blackList = blackListString.Split('\n');
      foreach (var line in blackList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListBlack.Add(newPawn);
      }



      mainWindow.FillPawnListAndFillAllPossibleTrips(pawnListWhite, pawnListBlack);


      var nodeResult = mainWindow.GetBestPositionLocalNotTask(mainWindow.ComputerColore);
      //la reine blanche ne doit se mettre sur "a6"
      Assert.AreNotEqual(nodeResult.BestChildPosition, "a6");
    }

    [TestMethod]
    public void T16SeulLePionDoitProtegerLeRoiNoir()
    {
      /*seul le poin doit protéger le roi noir*/
      ////le poin noir doit se mettre sur "f6"




      var mainWindow = new MainWindow();
      mainWindow.ComputerColore = "Black";
      if (mainWindow.Tree != null)
        mainWindow.Tree.Clear();
      mainWindow.Tree = null;
      mainWindow.CleanPawnList();
      var pawnListWhite = new List<Pawn>();
      var pawnListBlack = new List<Pawn>();



      //WHITEList
      var whiteListString = "" +
        "King;e1;White;False;True;True;True"+
"\nQueen;f3;White;False;False;False;False"+
"\nRook;a1;White;False;False;False;False"+
"\nRook;h1;White;False;False;False;False"+
"\nBishop;g5;White;False;False;False;False"+
"\nBishop;c4;White;False;False;False;False"+
"\nKnight;b1;White;False;False;False;False"+
"\nKnight;g1;White;False;False;False;False"+
"\nSimplePawn;a2;White;True;False;False;False"+
"\nSimplePawn;b2;White;True;False;False;False"+
"\nSimplePawn;d4;White;False;False;False;False"+
"\nSimplePawn;e4;White;False;False;False;False"+
"\nSimplePawn;f2;White;True;False;False;False"+
"\nSimplePawn;g2;White;True;False;False;False"+
"\nSimplePawn;h2;White;True;False;False;False";
      var whiteList = whiteListString.Split('\n');
      foreach (var line in whiteList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListWhite.Add(newPawn);
      }

      //BLACKList
      var blackListString = "" +
        "King;e7;Black;False;False;True;True"+
"\nQueen;d8;Black;False;False;False;False"+
"\nRook;a8;Black;False;False;False;False"+
"\nRook;h8;Black;False;False;False;False"+
"\nBishop;c8;Black;False;False;False;False"+
"\nBishop;f8;Black;False;False;False;False"+
"\nKnight;b8;Black;False;False;False;False"+
"\nKnight;g8;Black;False;False;False;False"+
"\nSimplePawn;a7;Black;True;False;False;False"+
"\nSimplePawn;b6;Black;False;False;False;False"+
"\nSimplePawn;c7;Black;True;False;False;False"+
"\nSimplePawn;d7;Black;True;False;False;False"+
"\nSimplePawn;f7;Black;True;False;False;False"+
"\nSimplePawn;g6;Black;False;False;False;False"+
"\nSimplePawn;h7;Black;True;False;False;False";
      var blackList = blackListString.Split('\n');
      foreach (var line in blackList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListBlack.Add(newPawn);
      }



      mainWindow.FillPawnListAndFillAllPossibleTrips(pawnListWhite, pawnListBlack);


      var nodeResult = mainWindow.GetBestPositionLocalNotTask(mainWindow.ComputerColore);
      ////le poin noir  doit se mettre sur "f6"
      Assert.AreEqual(nodeResult.AssociatePawn.Name, "SimplePawn");
      Assert.AreEqual(nodeResult.BestChildPosition, "f6");
    }


    [TestMethod]
    public void T17LeRoirNoirNeDoitPasAttaquer()
    {
      /*le roi noir ne doit pas attaquer */
      ////le roi noir ne doit pas se mettre sur "f6"




      var mainWindow = new MainWindow();
      mainWindow.ComputerColore = "Black";
      if (mainWindow.Tree != null)
        mainWindow.Tree.Clear();
      mainWindow.Tree = null;
      mainWindow.CleanPawnList();
      var pawnListWhite = new List<Pawn>();
      var pawnListBlack = new List<Pawn>();



      //WHITEList
      var whiteListString = "" +
        "King;e1;White;False;True;True;True" +
"\nQueen;f3;White;False;False;False;False" +
"\nRook;a1;White;False;False;False;False" +
"\nRook;h1;White;False;False;False;False" +
"\nBishop;f6;White;False;False;False;False" +
"\nBishop;c4;White;False;False;False;False" +
"\nKnight;b1;White;False;False;False;False" +
"\nKnight;g1;White;False;False;False;False" +
"\nSimplePawn;a2;White;True;False;False;False" +
"\nSimplePawn;b2;White;True;False;False;False" +
"\nSimplePawn;d4;White;False;False;False;False" +
"\nSimplePawn;e4;White;False;False;False;False" +
"\nSimplePawn;f2;White;True;False;False;False" +
"\nSimplePawn;g2;White;True;False;False;False" +
"\nSimplePawn;h2;White;True;False;False;False";
      var whiteList = whiteListString.Split('\n');
      foreach (var line in whiteList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListWhite.Add(newPawn);
      }

      //BLACKList
      var blackListString = "" +
        "King;e7;Black;False;False;True;True" +
"\nQueen;d8;Black;False;False;False;False" +
"\nRook;a8;Black;False;False;False;False" +
"\nRook;h8;Black;False;False;False;False" +
"\nBishop;c8;Black;False;False;False;False" +
"\nBishop;f8;Black;False;False;False;False" +
"\nKnight;b8;Black;False;False;False;False" +
"\nSimplePawn;a7;Black;True;False;False;False" +
"\nSimplePawn;b6;Black;False;False;False;False" +
"\nSimplePawn;c7;Black;True;False;False;False" +
"\nSimplePawn;d7;Black;True;False;False;False" +
"\nSimplePawn;f7;Black;True;False;False;False" +
"\nSimplePawn;g6;Black;False;False;False;False" +
"\nSimplePawn;h7;Black;True;False;False;False";
      var blackList = blackListString.Split('\n');
      foreach (var line in blackList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListBlack.Add(newPawn);
      }



      mainWindow.FillPawnListAndFillAllPossibleTrips(pawnListWhite, pawnListBlack);


      var nodeResult = mainWindow.GetBestPositionLocalNotTask(mainWindow.ComputerColore);
      ////le roi noir ne doit pas se mettre sur "f6"
     //Assert.AreNotEqual(nodeResult.AssociatePawn.Name, "King");
      Assert.AreNotEqual(nodeResult.BestChildPosition, "f6");
    }

    [TestMethod]
    public void T18suiteDe16LeCavalierNoirDoitPrendreLeFouBlanc()
    {
      /*le cavalier noir  doit  attaquer */
      ////le cavalier noir  doit se mettre sur "f6"




      var mainWindow = new MainWindow();
      mainWindow.ComputerColore = "Black";
      if (mainWindow.Tree != null)
        mainWindow.Tree.Clear();
      mainWindow.Tree = null;
      mainWindow.CleanPawnList();
      var pawnListWhite = new List<Pawn>();
      var pawnListBlack = new List<Pawn>();



      //WHITEList
      var whiteListString = "" +
        "King;e1;White;False;True;True;True"+
"\nQueen;f3;White;False;False;False;False"+
"\nRook;a1;White;False;False;False;False"+
"\nRook;h1;White;False;False;False;False"+
"\nBishop;f6;White;False;False;False;False"+
"\nBishop;c4;White;False;False;False;False"+
"\nKnight;b1;White;False;False;False;False"+
"\nKnight;g1;White;False;False;False;False"+
"\nSimplePawn;a2;White;True;False;False;False"+
"\nSimplePawn;b2;White;True;False;False;False"+
"\nSimplePawn;d4;White;False;False;False;False"+
"\nSimplePawn;e4;White;False;False;False;False"+
"\nSimplePawn;f2;White;True;False;False;False"+
"\nSimplePawn;g2;White;True;False;False;False"+
"\nSimplePawn;h2;White;True;False;False;False";
      var whiteList = whiteListString.Split('\n');
      foreach (var line in whiteList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListWhite.Add(newPawn);
      }

      //BLACKList
      var blackListString = "" +
        "King;e7;Black;False;False;True;True"+
"\nQueen;d8;Black;False;False;False;False"+
"\nRook;a8;Black;False;False;False;False"+
"\nRook;h8;Black;False;False;False;False"+
"\nBishop;c8;Black;False;False;False;False"+
"\nBishop;f8;Black;False;False;False;False"+
"\nKnight;b8;Black;False;False;False;False"+
"\nKnight;g8;Black;False;False;False;False"+
"\nSimplePawn;a7;Black;True;False;False;False"+
"\nSimplePawn;b6;Black;False;False;False;False"+
"\nSimplePawn;c7;Black;True;False;False;False"+
"\nSimplePawn;d7;Black;True;False;False;False"+
"\nSimplePawn;g6;Black;False;False;False;False"+
"\nSimplePawn;h7;Black;True;False;False;False";
      var blackList = blackListString.Split('\n');
      foreach (var line in blackList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListBlack.Add(newPawn);
      }



      mainWindow.FillPawnListAndFillAllPossibleTrips(pawnListWhite, pawnListBlack);


      var nodeResult = mainWindow.GetBestPositionLocalNotTask(mainWindow.ComputerColore);
      ////le cavalier noir  doit se mettre sur "f6"
      Assert.AreEqual(nodeResult.AssociatePawn.Name, "Knight");
      Assert.AreEqual(nodeResult.BestChildPosition, "f6");
    }

    [TestMethod]
    public void T19lePionBlanchDoitProtegerLeRook()
    {
      /*le pion blanch doit proteger le rook */
      ////le pion blanch doit se mettre sur "c3"




      var mainWindow = new MainWindow();
      mainWindow.ComputerColore = "White";
      if (mainWindow.Tree != null)
        mainWindow.Tree.Clear();
      mainWindow.Tree = null;
      mainWindow.CleanPawnList();
      var pawnListWhite = new List<Pawn>();
      var pawnListBlack = new List<Pawn>();



      //WHITEList
      var whiteListString = "" +
        "King;e1;White;False;True;True;True"+
"\nQueen;d1;White;False;False;False;False"+
"\nRook;a1;White;False;False;False;False"+
"\nRook;h1;White;False;False;False;False"+
"\nBishop;c1;White;False;False;False;False"+
"\nBishop;f1;White;False;False;False;False"+
"\nKnight;b1;White;False;False;False;False"+
"\nKnight;g1;White;False;False;False;False"+
"\nSimplePawn;a2;White;True;False;False;False"+
"\nSimplePawn;b3;White;False;False;False;False"+
"\nSimplePawn;c2;White;True;False;False;False"+
"\nSimplePawn;d2;White;True;False;False;False"+
"\nSimplePawn;e3;White;False;False;False;False"+
"\nSimplePawn;f3;White;False;False;False;False"+
"\nSimplePawn;g2;White;True;False;False;False"+
"\nSimplePawn;h3;White;False;False;False;False";
      var whiteList = whiteListString.Split('\n');
      foreach (var line in whiteList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListWhite.Add(newPawn);
      }

      //BLACKList
      var blackListString = "" +
        "King;e8;Black;False;True;True;True"+
"\nQueen;d6;Black;False;False;False;False"+
"\nRook;a8;Black;False;False;False;False"+
"\nRook;h8;Black;False;False;False;False"+
"\nBishop;c8;Black;False;False;False;False"+
"\nBishop;g7;Black;False;False;False;False"+
"\nKnight;b8;Black;False;False;False;False"+
"\nKnight;g8;Black;False;False;False;False"+
"\nSimplePawn;a7;Black;True;False;False;False"+
"\nSimplePawn;b7;Black;True;False;False;False"+
"\nSimplePawn;c7;Black;True;False;False;False"+
"\nSimplePawn;d5;Black;False;False;False;False"+
"\nSimplePawn;e7;Black;True;False;False;False"+
"\nSimplePawn;f7;Black;True;False;False;False"+
"\nSimplePawn;g5;Black;False;False;False;False"+
"\nSimplePawn;h7;Black;True;False;False;False";
      var blackList = blackListString.Split('\n');
      foreach (var line in blackList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListBlack.Add(newPawn);
      }



      mainWindow.FillPawnListAndFillAllPossibleTrips(pawnListWhite, pawnListBlack);


      var nodeResult = mainWindow.GetBestPositionLocalNotTask(mainWindow.ComputerColore);
      ////le cavalier noir  doit se mettre sur "f6"
      Assert.AreEqual(nodeResult.AssociatePawn.Name, "SimplePawn");
      Assert.AreEqual(nodeResult.BestChildPosition, "c3");
    }


    [TestMethod]
    public void T20LePionDoitPrendreLeCavalier()
    {
      /*le pion blanch doit prendre le cavalier */
      ////le pion blanch doit se mettre sur "d3"




      var mainWindow = new MainWindow();
      mainWindow.ComputerColore = "White";
      if (mainWindow.Tree != null)
        mainWindow.Tree.Clear();
      mainWindow.Tree = null;
      mainWindow.CleanPawnList();
      var pawnListWhite = new List<Pawn>();
      var pawnListBlack = new List<Pawn>();



      //WHITEList
      var whiteListString = "" +
        "King;e1;White;False;True;True;True"+
"\nQueen;d1;White;False;False;False;False"+
"\nRook;a1;White;False;False;False;False"+
"\nRook;h1;White;False;False;False;False"+
"\nBishop;d2;White;False;False;False;False"+
"\nBishop;f1;White;False;False;False;False"+
"\nKnight;b1;White;False;False;False;False"+
"\nKnight;g1;White;False;False;False;False"+
"\nSimplePawn;b3;White;False;False;False;False"+
"\nSimplePawn;c3;White;False;False;False;False"+
"\nSimplePawn;e2;White;True;False;False;False"+
"\nSimplePawn;h4;White;False;False;False;False";
      var whiteList = whiteListString.Split('\n');
      foreach (var line in whiteList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListWhite.Add(newPawn);
      }

      //BLACKList
      var blackListString = "" +
        "King;e8;Black;False;True;True;True"+
"\nQueen;d8;Black;False;False;False;False"+
"\nRook;a8;Black;False;False;False;False"+
"\nRook;h8;Black;False;False;False;False"+
"\nBishop;g4;Black;False;False;False;False"+
"\nKnight;d3;Black;False;False;False;False"+
"\nKnight;e4;Black;False;False;False;False"+
"\nSimplePawn;a7;Black;True;False;False;False"+
"\nSimplePawn;b7;Black;True;False;False;False"+
"\nSimplePawn;c7;Black;True;False;False;False"+
"\nSimplePawn;f7;Black;True;False;False;False"+
"\nSimplePawn;g7;Black;True;False;False;False"+
"\nSimplePawn;h7;Black;True;False;False;False";
      var blackList = blackListString.Split('\n');
      foreach (var line in blackList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListBlack.Add(newPawn);
      }



      mainWindow.FillPawnListAndFillAllPossibleTrips(pawnListWhite, pawnListBlack);


      var nodeResult = mainWindow.GetBestPositionLocalNotTask(mainWindow.ComputerColore);
      ////le pion blanch doit se mettre sur "d3"
      Assert.AreEqual(nodeResult.AssociatePawn.Name, "SimplePawn");
      Assert.AreEqual(nodeResult.BestChildPosition, "d3");
    }


    [TestMethod]
    public void T21LeRoiBlanchDoitSeMettreEnd3()
    {
      /*La roi blanch doit se mettre en d3*/
      //Positions final du roi blanch doit etre d3 

      var mainWindow = new MainWindow();
      mainWindow.ComputerColore = "White";
      if (mainWindow.Tree != null)
        mainWindow.Tree.Clear();
      mainWindow.Tree = null;
      mainWindow.CleanPawnList();
      var pawnListWhite = new List<Pawn>();
      var pawnListBlack = new List<Pawn>();


      //WHITEList
      var whiteListString = "" +
        "King;e2;White;False;False;True;False" +
"\nQueen;d1;White;False;False;False;False" +
"\nRook;a1;White;False;False;False;False" +
"\nRook;h5;White;False;False;False;False" +
"\nBishop;c4;White;False;False;False;False" +
"\nKnight;b1;White;False;False;False;False" +
"\nKnight;g1;White;False;False;False;False" +
"\nSimplePawn;a4;White;False;False;False;False" +
"\nSimplePawn;b5;White;False;False;False;False" +
"\nSimplePawn;c3;White;False;False;False;False" +
"\nSimplePawn;d5;White;False;False;False;False";
      var whiteList = whiteListString.Split('\n');
      foreach (var line in whiteList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListWhite.Add(newPawn);
      }

      //BLACKList
      var blackListString = "" +
      "King;e8;Black;False;True;True;False" +
"\nQueen;g2;Black;False;False;False;False" +
"\nRook;a8;Black;False;False;False;False" +
"\nBishop;c8;Black;False;False;False;False" +
"\nBishop;h6;Black;False;False;False;False" +
"\nSimplePawn;a5;Black;False;False;False;False" +
"\nSimplePawn;b7;Black;True;False;False;False" +
"\nSimplePawn;c7;Black;True;False;False;False" +
"\nSimplePawn;e3;Black;False;False;False;False" +
"\nSimplePawn;e7;Black;True;False;False;False" +
"\nSimplePawn;f7;Black;True;False;False;False" +
"\nSimplePawn;g4;Black;False;False;False;False";
      var blackList = blackListString.Split('\n');
      foreach (var line in blackList)
      {
        var datas = line.Split(';');
        var newPawn = new Pawn(datas[0], datas[1], new Button(), datas[2], mainWindow);
        //;{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}
        newPawn.IsFirstMove = bool.Parse(datas[3]);
        newPawn.IsFirstMoveKing = bool.Parse(datas[4]);
        newPawn.IsLeftRookFirstMove = bool.Parse(datas[5]);
        newPawn.IsRightRookFirstMove = bool.Parse(datas[6]);
        pawnListBlack.Add(newPawn);
      }


      mainWindow.FillPawnListAndFillAllPossibleTrips(pawnListWhite, pawnListBlack);


      var nodeResult = mainWindow.GetBestPositionLocalNotTask(mainWindow.ComputerColore);
      //Positions final du cavalier Blach ne doit pas etre  ni "a7" ni "c7"
      //Assert.AreNotEqual(nodeResult.BestChildPosition, "a7", "c7");
      Assert.AreEqual(nodeResult.AssociatePawn.Name, "King");
      Assert.AreEqual(nodeResult.BestChildPosition, "d3");
    }


    [TestMethod]
    public void PreviousTestC2ToC3ToC2()
    {
      var mainWindow = new MainWindow();
      var t = mainWindow.PawnList.Count;
      mainWindow.CurrentTurn = "White";
      mainWindow.ComputerColore = "White";
      var t00 = mainWindow.PawnList.Count;
      mainWindow.MoveTo("c2", "c3");
      var t0 = mainWindow.PawnList.Count;
      mainWindow.Previous();
      var t1 = mainWindow.PawnList.Count;
      var movedPawn = mainWindow.PawnList.FirstOrDefault(x => x.Location == "c2");
      Assert.AreEqual(movedPawn.Name, "SimplePawn");

    }

    [TestMethod]
    public void PreviousTestTowPrevious()
    {
      var mainWindow = new MainWindow();
      var t = mainWindow.PawnList.Count;
      mainWindow.CurrentTurn = "White";
      mainWindow.ComputerColore = "Black";
      var t00 = mainWindow.PawnList.Count;
      mainWindow.MoveTo("c2", "c3");
      var movedPawn = mainWindow.PawnList.FirstOrDefault(x => x.Location == "c3");
      Assert.AreEqual(movedPawn.Name, "SimplePawn");
      mainWindow.Save();
      mainWindow.Load();
        mainWindow.MoveTo("f7", "f6");
     // var t54 = mainWindow.HistoricalBlackList[0].FirstOrDefault(x => x.Location == "f7");
      movedPawn = mainWindow.PawnList.FirstOrDefault(x => x.Location == "f6");
      movedPawn = mainWindow.PawnList.FirstOrDefault(x => x.Location == "f7");

      movedPawn = mainWindow.PawnList.FirstOrDefault(x => x.Location == "f6");
      Assert.IsNotNull(movedPawn);
     // t54 = mainWindow.HistoricalBlackList[0].FirstOrDefault(x => x.Location == "f7");

      mainWindow.Previous();
     // t54 = mainWindow.HistoricalBlackList[0].FirstOrDefault(x => x.Location == "f7");

      movedPawn = mainWindow.PawnList.FirstOrDefault(x => x.Location == "f7");
      Assert.IsNotNull(movedPawn);
      movedPawn = mainWindow.PawnList.FirstOrDefault(x => x.Location == "c2");
      Assert.IsNull(movedPawn);


      mainWindow.Previous();

      movedPawn = mainWindow.PawnList.FirstOrDefault(x => x.Location == "c2");
      Assert.AreEqual(movedPawn.Name, "SimplePawn");


    }

    [TestMethod]
    public void PreviousTestTreePrevious()
    {
      var mainWindow = new MainWindow();
      var t = mainWindow.PawnList.Count;
      mainWindow.CurrentTurn = "White";
      mainWindow.ComputerColore = "Black";
      var t00 = mainWindow.PawnList.Count;
      mainWindow.MoveTo("c2", "c3");
      var movedPawn = mainWindow.PawnList.FirstOrDefault(x => x.Location == "c3");
      Assert.IsNotNull(movedPawn);
       movedPawn = mainWindow.PawnList.FirstOrDefault(x => x.Location == "c2");
      Assert.IsNull(movedPawn);
      mainWindow.Save();
      mainWindow.Load();
      mainWindow.MoveTo("f7", "f6");
      movedPawn = mainWindow.PawnList.FirstOrDefault(x => x.Location == "f6");
      Assert.IsNotNull(movedPawn);
      movedPawn = mainWindow.PawnList.FirstOrDefault(x => x.Location == "f7");
      Assert.IsNull(movedPawn);
      mainWindow.Save();
      mainWindow.Load();
      mainWindow.MoveTo("h2", "h3");
      movedPawn = mainWindow.PawnList.FirstOrDefault(x => x.Location == "h3");
      Assert.IsNotNull(movedPawn);
      movedPawn = mainWindow.PawnList.FirstOrDefault(x => x.Location == "h2");
      Assert.IsNull(movedPawn);

      mainWindow.Previous();
      movedPawn = mainWindow.PawnList.FirstOrDefault(x => x.Location == "h2");
      Assert.IsNotNull(movedPawn);
      movedPawn = mainWindow.PawnList.FirstOrDefault(x => x.Location == "h3");
      Assert.IsNull(movedPawn);

      mainWindow.Previous();

      movedPawn = mainWindow.PawnList.FirstOrDefault(x => x.Location == "f6");
      Assert.IsNull(movedPawn);
      movedPawn = mainWindow.PawnList.FirstOrDefault(x => x.Location == "f7");
      Assert.IsNotNull(movedPawn);






    }

  }
}
