using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utils
{
  public class TreeNodeCircleNodeAndIndex
  {
    public TreeNode<CircleNode> ContentNodeList { get; set; }
    public int Index { get; set; }
    public TreeNodeCircleNodeAndIndex(TreeNode<CircleNode> treeNode,int index)
    {
      ContentNodeList = treeNode;
      Index = index;
    }
  }
}
