using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chess.Utils;

namespace Chess.View
{
  
  public partial class TreeGrapheForm : Form
  {
    private List<Node> _tree;
    private TreeNode<CircleNode> _root;
    private Node _kingRoot;
    private List<TreeNodeCircleNodeAndIndex> treeNodeCircleNodeAndIndexList;
    public TreeGrapheForm(List<Node> tree)
    {
      InitializeComponent();
      _tree = tree;
      _tree = _tree.Where(x => x.Colore == "White").ToList();
      _kingRoot  = _tree.FirstOrDefault(x => x.AssociatePawn?.Name == "Queen" && x.Level == 0);
      _root =new TreeNode<CircleNode>(new CircleNode($"{_kingRoot.Level};{_kingRoot.AssociatePawn.Name};{_kingRoot.AssociatePawn.Location};to {_kingRoot.BestChildPosition}\n { _kingRoot.Weight.ToString() }"));
    }

    // The root node.






    // Make a tree.
    private void TreeGrapheForm_Load(object sender, EventArgs e)
    {
      /*TreeNode<CircleNode> a_node = new TreeNode<CircleNode>(new CircleNode("A"));
      TreeNode<CircleNode> b_node = new TreeNode<CircleNode>(new CircleNode("B"));
      TreeNode<CircleNode> c_node = new TreeNode<CircleNode>(new CircleNode("C"));
      TreeNode<CircleNode> d_node = new TreeNode<CircleNode>(new CircleNode("D"));
      TreeNode<CircleNode> e_node = new TreeNode<CircleNode>(new CircleNode("E"));
      TreeNode<CircleNode> f_node = new TreeNode<CircleNode>(new CircleNode("F"));
      TreeNode<CircleNode> g_node = new TreeNode<CircleNode>(new CircleNode("G"));
      TreeNode<CircleNode> h_node = new TreeNode<CircleNode>(new CircleNode("H"));

      _root.AddChild(a_node);
      _root.AddChild(b_node);
      a_node.AddChild(c_node);
      a_node.AddChild(d_node);
      b_node.AddChild(e_node);
      b_node.AddChild(f_node);
      b_node.AddChild(g_node);
      e_node.AddChild(h_node);


      */
     
      foreach (var item in _tree.Where(x=>x.AssociatePawn?.Name== "Queen" && x.Level == 1/* && x.Location == "d2"*/))
      {
        TreeNode<CircleNode> newGraphe = new TreeNode<CircleNode>(new CircleNode($"{item.Level};{item.AssociatePawn.Name};{item.Location}\n { item.Weight.ToString() }"));
        //var newTreeAndInde = new TreeNodeCircleNodeAndIndex(newGraphe, item.Level);
        _root.AddChild(newGraphe);
/*
        foreach (var itemChild in item.ChildList.Where(x =>  x.AssociatePawn.Name == "Rook" && x.Location =="e1") )  
        {
          var newGrapheL1 = new TreeNode<CircleNode>(new CircleNode($"{itemChild.Level};{itemChild.AssociatePawn.Name};{itemChild.Location}\n { itemChild.Weight.ToString() }"));
          newGraphe.AddChild(newGrapheL1);
          foreach (var itemL2 in itemChild.ChildList)
          {
            var newGrapheL2 = new TreeNode<CircleNode>(new CircleNode($"{itemL2.Level};{itemL2.AssociatePawn.Name};{itemL2.Location}\n { itemL2.Weight.ToString() }"));
            newGrapheL1.AddChild(newGrapheL2);

          }
        }*/
      }




      /* foreach (var item in treeNodeCircleNodeAndIndexList.Where(x=> x.Index > 0))
       {
         treeNodeCircleNodeAndIndexList[item.Index-1].ContentNodeList.AddChild(item.ContentNodeList);
       }*/

      //var t = treeNodeCircleNodeAndIndexList.Where(x=>x.Index ==1);
      //var l = 1;
      /*while (l<4)
      {
        var lst= treeNodeCircleNodeAndIndexList.Where(x => x.Index == l);
        foreach (var item in lst)
        {
          treeNodeCircleNodeAndIndexList[l - 1].ContentNodeList.AddChild(item.ContentNodeList);

        }
        
        l++;
      }
      */

     /* foreach (var item in treeNodeCircleNodeAndIndexList.Where(x=>x.Index==0))
      {
        var lst = treeNodeCircleNodeAndIndexList.Where(x => x.Index == 1);
        foreach (var il in lst)
        {
          item.ContentNodeList.AddChild(il.ContentNodeList);

        }
      }

      foreach (var item in treeNodeCircleNodeAndIndexList.Where(x => x.Index == 1))
      {
        var lst = treeNodeCircleNodeAndIndexList.Where(x => x.Index == 2);
        foreach (var il in lst)
        {
          item.ContentNodeList.AddChild(il.ContentNodeList);

        }
      }
     */


      ArrangeTree();
      //treeNodeCircleNodeAndIndexList




      //addNodeInGraphe(1, _root);
      /*foreach (var node in _tree)
      {
        var grapheNode = new TreeNode<CircleNode>(new CircleNode($"{node.AssociatePawn.Name};{node.Location}\n { node.Weight.ToString() }"));
        // rootChild.AddChild(grapheNode);

        //var shildNode = new TreeNode<CircleNode>(new CircleNode("A"));
        _root.AddChild(grapheNode);
      }

      */

      //_tree[0].ParentTreeNode = _root;
      // ArrangeTree();

      //TreeNode<CircleNode> rootChild = new TreeNode<CircleNode>();
      /*foreach (var item in _kingRoot.ChildList)
      {
        var grapheNode = new TreeNode<CircleNode>(new CircleNode($"{item.AssociatePawn.Name};{item.Location}\n { item.Weight.ToString() }"));
        // rootChild.AddChild(grapheNode);

        //var shildNode = new TreeNode<CircleNode>(new CircleNode("A"));
        _root.AddChild(grapheNode);


        foreach (var childNode in item.ChildList)
        {
          if(childNode.AssociatePawn.Name=="King" && childNode.Parent.Weight > 0 )
          {
            var newTreeNode= new TreeNode<CircleNode>(new CircleNode($"{childNode.AssociatePawn.Name};{childNode.Location}\n { childNode.Weight.ToString() }"));
            grapheNode.AddChild(newTreeNode);

            foreach (var it in childNode.ChildList)
            {
              newTreeNode.AddChild(new TreeNode<CircleNode>(new CircleNode($"{it.AssociatePawn.Name};{it.Location}\n { it.Weight.ToString() }")));

            }
          }
            

          

        }
      }
      // Position the tree.

     
      ArrangeTree();*/


    }

    private void addNodeInGraphe(int level, TreeNode<CircleNode> contentTreeNode)
    {
      
        foreach (var item in _tree.Where(x=>x.Level== level && x.AssociatePawn.Name =="King"))
        {
          var grapheNode = new TreeNode<CircleNode>(new CircleNode($"{item.AssociatePawn.Name};{item.Location}\n { item.Weight.ToString() }"));
          // rootChild.AddChild(grapheNode);

          //var shildNode = new TreeNode<CircleNode>(new CircleNode("A"));
          contentTreeNode.AddChild(grapheNode);
        }
       
      
       

      ArrangeTree();
      
    }

    // Draw the tree.
    private void picTree_Paint(object sender, PaintEventArgs e)
    {
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
      _root.DrawTree(e.Graphics);
    }

    // Center the tree on the form.
    private void picTree_Resize(object sender, EventArgs e)
    {
      ArrangeTree();
    }
    private void ArrangeTree()
    {
      using (Graphics gr = picTree.CreateGraphics())
      {
        if (_root.Orientation == TreeNode<CircleNode>.Orientations.Horizontal)
        {
          // Arrange the tree once to see how big it is.
          float xmin = 0, ymin = 0;
          _root.Arrange(gr, ref xmin, ref ymin);

          // Arrange the tree again to center it horizontally.
          xmin = (picTree.ClientSize.Width - xmin) / 2;
          ymin = 10;
          _root.Arrange(gr, ref xmin, ref ymin);
        }
        else
        {
          // Arrange the tree.
          float xmin = _root.Indent;//@
          float ymin = xmin;
          _root.Arrange(gr, ref xmin, ref ymin);
        }
      }

      picTree.Refresh();
    }

    // The currently selected node.
    private TreeNode<CircleNode> SelectedNode;

    // Display the text of the node under the mouse.
    private void picTree_MouseMove(object sender, MouseEventArgs e)
    {
      // Find the node under the mouse.
      FindNodeUnderMouse(e.Location);

      // If there is a node under the mouse,
      // display the node's text.
      if (SelectedNode == null)
      {
        lblNodeText.Text = "";
      }
      else
      {
        lblNodeText.Text = SelectedNode.Data.Text;
      }
    }

    // If this is a right button down and the
    // mouse is over a node, display a context menu.
    private void picTree_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right) return;

      // Find the node under the mouse.
      FindNodeUnderMouse(e.Location);

      // If there is a node under the mouse,
      // display the context menu.
      if (SelectedNode != null)
      {
        // Don't let the user delete the _root node.
        // (The TreeNode class can't do that.)
        ctxNodeDelete.Enabled = (SelectedNode != _root);

        // Display the context menu.
        ctxNode.Show(this, e.Location);
      }
    }

    // Set SelectedNode to the node under the mouse.
    private void FindNodeUnderMouse(PointF pt)
    {
      using (Graphics gr = picTree.CreateGraphics())
      {
        SelectedNode = _root.NodeAtPoint(gr, pt);
      }
    }

   

    // Delete this node from the tree.
    private void ctxNodeDelete_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Are you sure you want to delete this node?",
          "Delete Node?", MessageBoxButtons.YesNo,
          MessageBoxIcon.Question) == DialogResult.Yes)
      {
        // Delete the node and its subtree.
        _root.DeleteNode(SelectedNode);

        // Rearrange the tree to show the new structure.
        ArrangeTree();
      }
    }

    // Change the tree's orientation.
    private void radHorizontal_Click(object sender, EventArgs e)
    {
      _root.SetTreeDrawingParameters(5, 10, 20, 5,
          TreeNode<CircleNode>.Orientations.Horizontal);
      ArrangeTree();
    }

    private void radVertical_Click(object sender, EventArgs e)
    {
      _root.SetTreeDrawingParameters(5, 2, 20, 5,
          TreeNode<CircleNode>.Orientations.Vertical);
      ArrangeTree();
    }

    private void radVertical_CheckedChanged(object sender, EventArgs e)
    {

    }
  }


}
