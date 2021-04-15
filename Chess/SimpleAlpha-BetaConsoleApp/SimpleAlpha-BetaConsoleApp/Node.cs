using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAlpha_BetaConsoleApp
{
  public class Node
  {

    //private List<Pawn> _currentLocalPawnList;
    public int Level { get; set; }
    public int Weight { get; set; }
    public string Location { get; set; }
    public string OldPositionName { get; set; }

    public string Colore { get; set; }

    public Node Parent { get; set; }
    public List<Node> ChildList { get; set; }

    public string BestChildPosition { get; set; }
    public Node()
    {
      ChildList = new List<Node>();
    }

  
    // public  Pawn AssociatePawn { get; set; }

    //public TreeNode<CircleNode> ParentTreeNode { get; set; }

    /* public List<Pawn> GetCurrentLocalPawnList()
     {
       return _currentLocalPawnList;
     }

     public List<Pawn> GetCurrentLocalPawnListAllier()
     {
       return _currentLocalPawnList.Where(x=>x.Colore == Colore).ToList();
     }

     public Node(List<Pawn> currentLocalPawnList)
     {
       ChildList = new List<Node>();
       BestChildPosition = "";
       _currentLocalPawnList = null;
       _currentLocalPawnList = new List<Pawn>();
       _currentLocalPawnList.AddRange(currentLocalPawnList);
     }*/

  }
}
