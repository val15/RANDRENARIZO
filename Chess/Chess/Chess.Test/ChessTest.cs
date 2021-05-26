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
    public void T07EchecRookBlancDoitAttaquerLeRoiNoir()
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
      var blackListString = "King;f8;Black;False;True;False;True" +
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


  }
}
