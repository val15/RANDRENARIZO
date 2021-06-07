using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils
{
  public class Node
  {

    public List<Pawn> CurrentLocalPawnList { get; set; }
    public int Level { get; set; }
    public int MakeCheckmateLevel { get; set; }
    public int Weight { get; set; }
    public string Location { get; set; }
    public string OldPositionName { get; set; }

    public string Colore { get; set; }

    public Node Parent { get; set; }
    public List<Node> ChildList { get; set; }

    public string BestChildPosition { get; set; }
    public  Pawn AssociatePawn { get; set; }

    //public TreeNode<CircleNode> ParentTreeNode { get; set; }

    public List<Pawn> GetCurrentLocalPawnList()
    {
      return CurrentLocalPawnList;
    }

    public List<Pawn> GetCurrentLocalPawnListAllier()
    {
      return CurrentLocalPawnList.Where(x=>x.Colore == Colore).ToList();
    }
    public Node()
    {
    }
    public Node(int level, int makeCheckmateLevel, int weight,string  location, string oldPositionName,string colore,Node parent, List<Node> childList,string bestChildPosition,Pawn associatePawn, List<Pawn> currentLocalPawnList)
    {
      Level = level;
      MakeCheckmateLevel = makeCheckmateLevel;
      Weight = weight;
      Location = location;
      OldPositionName = oldPositionName;
      Colore = colore;
      ChildList = childList;
      BestChildPosition = bestChildPosition;
      AssociatePawn = associatePawn;
      CurrentLocalPawnList = currentLocalPawnList;
    }


      public Node(List<Pawn> currentLocalPawnList)
    {
      ChildList = new List<Node>();
      BestChildPosition = "";
      CurrentLocalPawnList = null;
      CurrentLocalPawnList = new List<Pawn>();
      CurrentLocalPawnList.AddRange(currentLocalPawnList);
    }

  }
}
