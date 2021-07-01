using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Chess.Entity;
using Chess.Utils;

namespace Chess.Model
{
  public class ServiceChessDB
  {

    
    public bool CreateNewGamePart(string gamePartLabel,DateTime startingDateTime)
    {
      try
      {
        using (var chessBDEntitie = new ChessDBEntities())
        {
          var gamePart = new GamePart();
          gamePart.GamePartLabel = gamePartLabel;
          gamePart.GamePartStartDateTime = startingDateTime;
          chessBDEntitie.GamePart.Add(gamePart);
          chessBDEntitie.SaveChanges();
        }
        return true;

      }
      catch (Exception ex)
      {

        MessageBox.Show($"Error DB {ex.ToString()}");
        return false;
      }
    }

    public GamePart GetLastGamePart()
    {
      try
      {
        using (var chessBDEntitie = new ChessDBEntities())
        {
          return chessBDEntitie.GamePart.ToList().Last();
        }
      }
      catch (Exception ex)
      {

        MessageBox.Show($"Error DB {ex.ToString()}");
        return null;
      }
    }
  
    public GamePartHistory InserNewGamePartHistory(long gamePartParentID)
    {
      try
      {
        using (var chessBDEntitie = new ChessDBEntities())
        {
          var newGamePartHistory = new GamePartHistory();
          newGamePartHistory.GamePart = chessBDEntitie.GamePart.First(x=>x.GamePartID == gamePartParentID);
          chessBDEntitie.GamePartHistory.Add(newGamePartHistory);
          chessBDEntitie.SaveChanges();
          return chessBDEntitie.GamePartHistory.ToList().Last();
        }
        
      }
      catch (Exception ex)
      {

        MessageBox.Show($"Error DB {ex.ToString()}");
        return null;
      }
    }

    public void InserTurn(long gamePartHistoryParentID,List<Pawn> pawnList,string currentTurn)
    {
      try
      {
        using (var chessBDEntitie = new ChessDBEntities())
        {
          var newTurn= new Turn();
          newTurn.GamePartHistory = chessBDEntitie.GamePartHistory.First(x => x.GamePartHistoryID== gamePartHistoryParentID);
          newTurn.TurnColor = currentTurn;
          var pawnListStr = "";
          foreach (var pawn in pawnList)
          {
            pawnListStr+=$"{pawn.Name};{pawn.Location};{pawn.Colore};{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}\n";
          }
          newTurn.PawnListStr = pawnListStr;

          newTurn.GamePartHistory.Turn.Add(newTurn);
          chessBDEntitie.SaveChanges();
        }
      }
      catch (Exception ex)
      {

        MessageBox.Show($"Error DB {ex.ToString()}");
      }
    }
    public GamePartHistory GetGamePartHistory(long gamePartHistoryID)
    {
      try
      {
        using (var chessBDEntitie = new ChessDBEntities())
        {
          return chessBDEntitie.GamePartHistory.FirstOrDefault(x => x.GamePartHistoryID == gamePartHistoryID);
        }
      }
      catch (Exception ex)
      {

        MessageBox.Show($"Error DB {ex.ToString()}");
        return null;
      }
    }

    public List<Turn> GetGameTurns(long gamePartHistoryID)
    {
      try
      {
        using (var chessBDEntitie = new ChessDBEntities())
        {
          return chessBDEntitie.Turn.Where(x => x.GamePartHistoryID == gamePartHistoryID).ToList();
        }
      }
      catch (Exception ex)
      {

        MessageBox.Show($"Error DB {ex.ToString()}");
        return null;
      }
    }
  }
}
