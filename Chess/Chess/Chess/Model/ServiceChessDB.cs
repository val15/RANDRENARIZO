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

    
    public bool CreateNewGamePart(string gamePartLabel,DateTime startingDateTime,string gamePartMode)
    {
      try
      {
        using (var chessBDEntitie = new ChessDBEntities())
        {
          var newGamePart = new GamePart();
          newGamePart.GamePartLabel = gamePartLabel;
          newGamePart.GamePartStartDateTime = startingDateTime;
          newGamePart.GamePartMode = gamePartMode;
          chessBDEntitie.GamePart.Add(newGamePart);
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
  
   /* public GamePartHistory InserNewGamePartHistory(long gamePartParentID)
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
   */
    public int InserTurn(long gamePartID,List<Pawn> pawnList,string currentTurn)
    {
      try
      {
        using (var chessBDEntitie = new ChessDBEntities())
        {
          var newTurn= new Turns();
          newTurn.GamePart = chessBDEntitie.GamePart.First(x => x.GamePartID== gamePartID);
          newTurn.TurnColor = currentTurn;
          var pawnListStr = "";
          foreach (var pawn in pawnList)
          {
            pawnListStr+=$"{pawn.Name};{pawn.Location};{pawn.Colore};{pawn.IsFirstMove};{pawn.IsFirstMoveKing};{pawn.IsLeftRookFirstMove};{pawn.IsRightRookFirstMove}\n";
          }
          newTurn.PawnListStr = pawnListStr;

          newTurn.GamePart.Turns.Add(newTurn);
          chessBDEntitie.SaveChanges();
          return newTurn.GamePart.Turns.Count();
        }
      }
      catch (Exception ex)
      {

        MessageBox.Show($"Error DB {ex.ToString()}");
        return -1;
      }
    }
    public GamePart GetGamePart(long gamePartID)
    {
      try
      {
        using (var chessBDEntitie = new ChessDBEntities())
        {
          return chessBDEntitie.GamePart.FirstOrDefault(x => x.GamePartID == gamePartID);
        }
      }
      catch (Exception ex)
      {

        MessageBox.Show($"Error DB {ex.ToString()}");
        return null;
      }
    }
    
    public List<GamePart> GetValidGameParts(string gamePartMode)
    {
      try
      {
        using (var chessBDEntitie = new ChessDBEntities())
        {
          return chessBDEntitie.GamePart.Where(x=>x.GamePartMode == gamePartMode && x.Turns.Count>0).OrderByDescending(x=>x.GamePartID).ToList();
        }
      }
      catch (Exception ex)
      {

        MessageBox.Show($"Error DB {ex.ToString()}");
        return null;
      }
    }

    public List<Turns> GetGameTurns(long gamePartID)
    {
      try
      {
        using (var chessBDEntitie = new ChessDBEntities())
        {
          return chessBDEntitie.Turns.Where(x => x.GamePartID == gamePartID).ToList();
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
