using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils
{
  public class Node
  {
    public int Level { get; set; }
    public int Weight { get; set; }
    public string Location { get; set; }
    public string OldPositionName { get; set; }

    public string Colore { get; set; }

    public Node Parent { get; set; }
    public List<Node> ChildList { get; set; }

    public string BestChildPosition { get; set; }
    public  Pawn AssociatePawn { get; set; }

    public Node()
    {
      ChildList = new List<Node>();
      BestChildPosition = "";
    }
  }
}
