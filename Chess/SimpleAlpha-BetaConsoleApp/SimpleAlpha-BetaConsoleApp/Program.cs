using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAlpha_BetaConsoleApp
{
  class Program
  {
    // Initial values of 
    // Aplha and Beta
    static Node MAX = new Node();
    static Node MIN = new Node();

    // Returns optimal value for
    // current player (Initially called
    // for root and maximizer)
    static Node minimax(int depth, int nodeIndex,
                    Boolean maximizingPlayer,
                    List<Node> values, Node alpha,
                    Node beta)
    {
      // Terminating condition. i.e 
      // leaf node is reached
      var currentNode = values[nodeIndex];
      if (currentNode.Level == 3)
        return currentNode;

      if (maximizingPlayer)
      {
        var best = MIN;

        // Recur for left and
        // right children
        //for (int i = 0; i < currentNode.ChildList.Count; i++)
        foreach (var node in currentNode.ChildList)
        {

          var index = values.IndexOf(node);
          var val = minimax(currentNode.Level + 1, index,
                          false, values, alpha, beta);
          best = Max(best, val);
          alpha = Max(alpha, best);

          // Alpha Beta Pruning
          if (beta.Weight <= alpha.Weight)
            break;
        }
        return best;
      }
      else
      {
        var best = MAX;

        // Recur for left and
        // right children
        foreach (var node in currentNode.ChildList)
        {
          var index = values.IndexOf(node);
          var val = minimax(currentNode.Level + 1, index,
                          true, values, alpha, beta);
          best = Min(best, val);
          beta = Min(beta, best);

          // Alpha Beta Pruning
          if (beta.Weight <= alpha.Weight)
            break;
        }
        return best;
      }
    }

    public static Node Max(Node node0,Node node1)
    {
      if (node0.Weight > node1.Weight)
        return node0;
      return node1;
    }
    public static Node Min(Node node0, Node node1)
    {
      if (node0.Weight < node1.Weight)
        return node0;
      return node1;
    }

    // Driver Code
    public static void Main(String[] args)
    {

      MAX.Location = "MAX";
      MAX.Weight = 1000;
      MIN.Location = "MIN";
      MIN.Weight = -1000;
      List<Node> tree = new List<Node>();
      var nodeA = new Node();
      nodeA.Location = "A";
      nodeA.Level = 0;


      var nodeB = new Node();
      nodeB.Location = "B";
      nodeB.Level = 1;
      nodeB.Parent = nodeA;
      var nodeC = new Node();
      nodeC.Location = "C";
      nodeC.Level = 1;
      nodeC.Parent = nodeA;

      var nodeD = new Node();
      nodeD.Location = "D";
      nodeD.Level = 2;
      nodeD.Parent = nodeB;
      var nodeE = new Node();
      nodeE.Location = "E";
      nodeE.Level = 2;
      nodeE.Parent = nodeB;
      var nodeF = new Node();
      nodeF.Location = "F";
      nodeF.Level = 2;
      nodeF.Parent = nodeC;
      var nodeG = new Node();
      nodeG.Location = "G";
      nodeG.Level = 2;
      nodeG.Parent = nodeC;


      //level 3
      var nodeD0 = new Node();
      nodeD0.Location = "D0";
      nodeD0.Weight = 3;
      nodeD0.Level = 3;
      nodeD0.Parent = nodeD;
      var nodeD1 = new Node();
      nodeD1.Location = "D1";
      nodeD1.Weight = 5;
      nodeD1.Level = 3;
      nodeD1.Parent = nodeD;
      nodeD.ChildList.Add(nodeD0);
      nodeD.ChildList.Add(nodeD1);
      var nodeE0 = new Node();
      nodeE0.Location = "E0";
      nodeE0.Weight = 6;
      nodeE0.Level = 3;
      nodeE0.Parent = nodeE;
      var nodeE1 = new Node();
      nodeE1.Location = "E1";
      nodeE1.Weight = 9;
      nodeE1.Level = 3;
      nodeE1.Parent = nodeE;
      nodeE.ChildList.Add(nodeE0);
      nodeE.ChildList.Add(nodeE1);
      var nodeF0 = new Node();
      nodeF0.Location = "F0";
      nodeF0.Weight = 1;
      nodeF0.Level = 3;
      nodeF0.Parent = nodeF;
      var nodeF1 = new Node();
      nodeF1.Location = "F1";
      nodeF1.Weight = 2;
      nodeF1.Level = 3;
      nodeF1.Parent = nodeF;
      nodeF.ChildList.Add(nodeF0);
      nodeF.ChildList.Add(nodeF1);
      var nodeG0 = new Node();
      nodeG0.Location = "G0";
      nodeG0.Weight = 0;
      nodeG0.Level = 3;
      nodeG0.Parent = nodeG;
      var nodeG1 = new Node();
      nodeG1.Location = "G1";
      nodeG1.Weight = -1;
      nodeG1.Level = 3;
      nodeG1.Parent = nodeG;

      nodeG.ChildList.Add(nodeG0);
      nodeG.ChildList.Add(nodeG1);
      nodeB.ChildList.Add(nodeD);
      nodeB.ChildList.Add(nodeE);
      nodeC.ChildList.Add(nodeF);
      nodeC.ChildList.Add(nodeG);
      nodeA.ChildList.Add(nodeB);
      nodeA.ChildList.Add(nodeC);
      

      //generation de l'arbre
      tree.Add(nodeA);

      tree.Add(nodeB);
      tree.Add(nodeC);

      tree.Add(nodeD);
      tree.Add(nodeE);
      tree.Add(nodeF);
      tree.Add(nodeG);

      tree.Add(nodeD0);
      tree.Add(nodeD1);
      tree.Add(nodeE0);
      tree.Add(nodeE1);
      tree.Add(nodeF0);
      tree.Add(nodeF1);
      tree.Add(nodeG0);
      tree.Add(nodeG1);



      foreach (var item in tree)
      {
        Console.WriteLine(item.Weight);
      }

      //int[] values = { 3, 5, 6, 9, 1, 2, 0, -1 };
      var bestNode = minimax(0, 0, true, tree, MIN, MAX);
      Console.WriteLine("The optimal value is : " +
                         bestNode.Location);
      /*var bestNode = tree.FirstOrDefault(x => x.Weight == bestValue);
      Console.WriteLine("The optimal NodeName is : " +
                         bestNode.Location + "Parent = "+ bestNode.Parent.Location);
      */
      //firstLevelParentNode

      var currentNodeParent = bestNode.Parent;

      while (currentNodeParent.Level > 1)
      {
        currentNodeParent = currentNodeParent.Parent;
      
      }


      Console.WriteLine("Best move : " + currentNodeParent.Parent.Location + "To" +
                   currentNodeParent.Location);

     
      Console.ReadLine();

    }
  }
}

