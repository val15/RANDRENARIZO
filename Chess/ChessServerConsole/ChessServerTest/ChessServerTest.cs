using ChessServerConsole.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessServerTest
{
  [TestClass]
  public class ChessServerTest
  {
    [TestMethod]
    public void GeneratePawnListFromStringListTestMethod()
    {
      var enterStringList = new List<string>();
      enterStringList.Add("SimplePawn;a7;Black;True;False;False;False");
      enterStringList.Add("King;e8;Black;False;True;True;True");
      enterStringList.Add("King;e1;White;False;True;True;True");
      var server = new Engine("White");
      server.GeneratePawnList(enterStringList);
      var pawList = server.PawnList;
      var firstPaw = pawList.First();

      var kingBlackPawn = server.GetPawnFromPawnList("e8");

      var kingWhitePawn = server.GetPawnFromPawnList("e1");
      var kingWhitePawnPossibleTips = kingWhitePawn.PossibleTrips;

      Assert.AreEqual(firstPaw.Name, "SimplePawn");
      Assert.AreEqual(kingBlackPawn.Name, "King");
      Assert.AreEqual(firstPaw.Colore, "Black");
      Assert.AreEqual(kingBlackPawn.Colore, "Black");

      Assert.AreEqual(kingWhitePawn.Name, "King");
      Assert.AreEqual(kingWhitePawn.Colore, "White");

      Assert.IsTrue(kingWhitePawnPossibleTips.Contains("e2"));
      Assert.IsTrue(kingWhitePawnPossibleTips.Contains("f1"));
      Assert.IsTrue(kingWhitePawnPossibleTips.Contains("d1"));
      Assert.IsTrue(kingWhitePawnPossibleTips.Contains("f2"));
      Assert.IsTrue(kingWhitePawnPossibleTips.Contains("d2"));






    }
  }
}
