using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace binaryTreeVisualitator
{
    internal class Node
    {
        private int val;
        private Node left;
        private Node right;
        public Node(int val, Node left = null, Node right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
        public int Val
        {
            get { return val; }
            set { val = value; }
        }
        public Node Left
        {
            get { return left; }
            set { left = value; }
        }
        public Node Right
        {
            get { return right; }
            set { right = value; }
        }
    }
    internal class BST
    {
        //BST will have 2 field 
        private Node root;//root of the tree , the most important
     
            public BST()
        {
            root = null;
            //arrayToBalance = null;
        }//constructor


        private Node insert(Node root, int val)
        {
            if (root == null) return new Node(val);

            if (root.Val > val)
            {
                root.Left = insert(root.Left, val);
            }
            else if (root.Val < val)
            {
                root.Right = insert(root.Right, val);
            }

            return root;

        }//insert helper
            public void InsertNode()
        {
            Console.WriteLine("input your node value: ");
            int val=int.Parse(Console.ReadLine());
            root = insert(root, val);
        }//insert method


        private void print(Node root)//preorder , this will print from the smallet number to biggest
        {

            if (root == null) return;
            print(root.Left);
            Console.Write(root.Val + " ");
            print(root.Right);

        }
            public void PrintTree()
        {
            Console.WriteLine("all element in a tree: \n");
            print(root);
            Console.WriteLine();
        }//print tree method

        private bool SearchForNode(Node root, int val)
        {
            if (root == null) return false;
            if(root.Val == val) return true;
            else if(root.Val < val)return SearchForNode(root.Right, val); 
            else if(root.Val > val)return SearchForNode(root.Left, val);
            return false;
        }//search helper
            public void SearchNode()
        {
            Console.WriteLine("input your node that you want to find: ");
            int val = int.Parse(Console.ReadLine());
            string answer = (SearchForNode(root, val)) ? "found" : "not found";
            Console.WriteLine(answer);
        }

        private Node delete(int val, Node root)
        {
            if (root == null) return null;
            else if (root.Val > val)
            {
                root.Left = delete(val, root.Left);
            }
            else if (root.Val < val)
            {
                root.Right = delete(val, root.Right);
            }
            else//if we find the value
            {
                
                if (root.Left == null && root.Right == null) return null;//case 1 : leaf
                else if (root.Right != null && root.Left != null)//case 3: if neither is null
                {
                    Node rootMin = MinElement(root.Right);//find the min element of the right subtree
                    root.Val = rootMin.Val;//asign the value to the node we delete
                    root.Right = delete(root.Val, rootMin);//delete the root that we have just found
                    return root;
                }
                else if (root.Left != null)//case 2 : one is null
                {
                    return root.Left;
                }
                else if (root.Right != null)//case 2: one is null
                {
                    return root.Right;
                }
            }
            return root;
        }//delete helper
        private Node MinElement(Node root)
        {
            while (root.Left != null)
            {
                root = root.Left;
            }
            return root;
        }//find the min element of the tree
            public void DeleteNode()
        {
            Console.WriteLine("node want to delete: ");
            int val = int.Parse(Console.ReadLine());
            root = delete(val, root);
        }//delete method


        private Node GenerateBalanceTree(int[] array)
        {
            if (array.Length == 0) return null;
            int mid = array.Length / 2;

            int[] arrayLeft = array[..mid];
            int[] arrayRight = array[(mid + 1)..];

            Node create = new Node(array[mid]);
            create.Left = GenerateBalanceTree(arrayLeft);
            create.Right = GenerateBalanceTree(arrayRight);

            return create;
        }//convert array to balance BST (leetcode approved)
        private void TraverseAndPopulateList(Node root, List<int> list)
        {
            if (root == null) return;
            TraverseAndPopulateList(root.Left, list);
            list.Add(root.Val);
            TraverseAndPopulateList(root.Right, list);
        }//create array from BST
        private void DeleteTree(Node root)
        {
            if (root == null) return;
            DeleteTree(root.Left);
            DeleteTree(root.Right);
            root.Left = root.Right = null;
        }//delete the tree
        private void ConvertToBalanceTree()
        {
            //create an array form of BST
            List<int> list = new List<int>();
            TraverseAndPopulateList(root, list);
            //then delete the unbalance tree
            DeleteTree(root);
            //generate the tree again into balance tree
            int[] array = new int[list.Count];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = list[i];
            }
            this.root = GenerateBalanceTree(array);

        }

        
        private int x = 0;
        private void vizualization(int y,Node root)
        {
           if(root == null)
           {
                Console.SetCursorPosition(x, y);
                Console.WriteLine("N");
                x++;
                return;
           }

           vizualization(y + 2, root.Left);
           Console.SetCursorPosition(x, y + 1);
           Console.WriteLine('/');
           x++;


           Console.SetCursorPosition(x, y);
           Console.WriteLine(root.Val);
           x++;

           Console.SetCursorPosition(x, y + 1);
           Console.WriteLine('\\');
           x++;
           vizualization(y + 2, root.Right);
        }
            public void TreeVizualization()
        {
            x = 0;
            Console.WriteLine("press any key to continue");
            int y = 1;
            ConvertToBalanceTree();
            vizualization(y, root);
            
        }

    }
    public class UserInterface
    {
        BST bst = null;
        public UserInterface()
        {
            bst = new BST();
        }
        public void menu()
        {
            string MENU = @"
            --------------------------    
            |1.input a node          |
            |2.delete a node         |
            |3.print all value       |
            |4.find value            |
            |5.vizualize the tree.   |
            |0.exit                  |
            --------------------------
your input: ";

            bool escape = false;
            while (escape == false)
            {
                int input = -1;
                try
                {
                    Console.Clear();
                    Console.WriteLine(MENU);
                     input = int.Parse(Console.ReadLine());
                }
                catch(Exception e)
                {
                    
                }
                Console.Clear();
               
                switch (input)
                {
                    case 1:bst.InsertNode();
                        Console.WriteLine("press any key to continue");
                        Console.ReadKey();
                        break;
                    case 2:bst.DeleteNode();
                        Console.WriteLine("press any key to continue");
                        Console.ReadKey();
                        break;
                    case 3:bst.PrintTree();
                        Console.WriteLine("press any key to continue");
                        Console.ReadKey();
                        break;
                    case 4:bst.SearchNode();
                        Console.WriteLine("press any key to continue");
                        Console.ReadKey();
                        break;
                    case 5:bst.TreeVizualization();
                        Console.ReadKey();
                        break;
                    case 0:escape = true;break;
                }
            }

        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
           UserInterface usf = new UserInterface();
            usf.menu();
        }
    }
}
