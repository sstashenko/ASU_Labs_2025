namespace lab6
{
    public class BinarySearchTree
    {
        public Node? Root { get; private set; }


        public void Add(int value)
        {
            Root = AddRecursive(Root, value);
        }

        private Node AddRecursive(Node? current, int value)
        {
            if (current == null)
            {
                return new Node(value);
            }

            if (value < current.Value)
            {
                current.Left = AddRecursive(current.Left, value);
            }
            else if (value > current.Value)
            {
                current.Right = AddRecursive(current.Right, value);
            }

            return current;
        }
        public void Delete(int value)
        {
            Root = DeleteRecursive(Root, value);
        }

        private Node? DeleteRecursive(Node? current, int value)
        {
            if (current == null) return null;

            if (value < current.Value)
            {
                current.Left = DeleteRecursive(current.Left, value);
            }
            else if (value > current.Value)
            {
                current.Right = DeleteRecursive(current.Right, value);
            }
            else
            {
                if (current.Left == null && current.Right == null)
                {
                    return null;
                }
                if (current.Left == null) return current.Right;
                if (current.Right == null) return current.Left;

                int smallestValue = FindMinValue(current.Right);
                current.Value = smallestValue;
                current.Right = DeleteRecursive(current.Right, smallestValue);
            }
            return current;
        }

        private int FindMinValue(Node node)
        {
            return node.Left == null ? node.Value : FindMinValue(node.Left);
        }

        public void PrintSorted()
        {
            Console.WriteLine("Відсортований вміст дерева (симетричний обхід):");
            InOrderTraversal(Root);
            Console.WriteLine();
        }
        
        private void InOrderTraversal(Node? node)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left);
                Console.Write(node.Value + " ");
                InOrderTraversal(node.Right);
            }
        }
        public void PrintTree()
        {
            Console.WriteLine("Структура дерева:");
            PrintTreeRecursive(Root, "", true);
        }

        private void PrintTreeRecursive(Node? node, string indent, bool isLast)
        {
            if (node != null)
            {
                Console.Write(indent);
                if (isLast)
                {
                    Console.Write("└──");
                    indent += "   ";
                }
                else
                {
                    Console.Write("├──");
                    indent += "│  ";
                }
                Console.WriteLine(node.Value);
                PrintTreeRecursive(node.Left, indent, node.Right == null);
                PrintTreeRecursive(node.Right, indent, true);
            }
        }

        public void FindNodesWithCloseChildren()
        {
            Console.WriteLine("\nВузли, у яких значення синів відрізняються не більше ніж на 5:");
            FindNodesWithCloseChildrenRecursive(Root);
        }

        private void FindNodesWithCloseChildrenRecursive(Node? node)
        {
            if (node == null) return;

            FindNodesWithCloseChildrenRecursive(node.Left);

            if (node.Left != null && node.Right != null)
            {
                if (Math.Abs(node.Left.Value - node.Right.Value) <= 5)
                {
                    Console.WriteLine($"Вузол: {node.Value} (Лівий: {node.Left.Value}, Правий: {node.Right.Value})");
                }
            }

            FindNodesWithCloseChildrenRecursive(node.Right);
        }
        
        public void FindNodesWithLargeSubtreeSumDifference()
        {
            Console.WriteLine("\nВузли, у яких різниця сум піддерев більша за 20:");
            FindNodesWithLargeSubtreeSumDifferenceRecursive(Root);
        }

        private void FindNodesWithLargeSubtreeSumDifferenceRecursive(Node? node)
        {
            if (node == null) return;
            
            FindNodesWithLargeSubtreeSumDifferenceRecursive(node.Left);

            int leftSum = SumSubtree(node.Left);
            int rightSum = SumSubtree(node.Right);

            if (Math.Abs(leftSum - rightSum) > 20)
            {
                Console.WriteLine($"Вузол: {node.Value} -> |{rightSum} - {leftSum}| = {Math.Abs(rightSum - leftSum)} > 20");
                Console.WriteLine($"  - Сума лівого піддерева: {leftSum}");
                Console.WriteLine($"  - Сума правого піддерева: {rightSum}");
            }

            FindNodesWithLargeSubtreeSumDifferenceRecursive(node.Right);
        }

        /// <summary>
        /// Допоміжний метод для рекурсивного розрахунку суми всіх вузлів у піддереві.
        /// </summary>
        private int SumSubtree(Node? node)
        {
            if (node == null)
            {
                return 0;
            }
            return node.Value + SumSubtree(node.Left) + SumSubtree(node.Right);
        }
    }
}