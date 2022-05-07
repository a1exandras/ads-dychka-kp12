using System;
using System.Linq;
using System.Collections.Generic;

namespace ads_lab_6
{
    public class StackNode
    {
        public Node head;
        public Node tail;
        public class Node
        {
            public char val;
            public Node next;
            public Node(char val)
            {
                this.val = val;
            }
            public Node(char val, Node next)
            {
                this.val = val;
                this.next = next;
            }
        }
        public StackNode(char val)
        {
            head = new Node(val);
            tail = head;
        }
        public void Push(char val)
        {
            tail.next = new Node(val);
            tail = tail.next;
        }
        public char Pop()
        {
            char tmp = tail.val;

            Node current = head;
            while(current.next.next != null)
                current = current.next;
            tail = current;
            tail.next = null;
            return tmp;
        }
        public char Peek()
        {
            return tail.val;
        }
        public void Print()
        {
            Node current = head;
            while(current.next != null)
            {
                Console.Write(current.val.ToString() + ' ');
                current = current.next;
            }
            Console.Write(current.val.ToString() + "\n");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] stringos = { "3*x*y+(y-5/(x*y))", "a+(b-c)*d", "a+f-((g*2)" };
            string str = stringos[0];
            if(str.Count(i => (i == '(')) != str.Count(i => (i == ')')))
            {
                Console.WriteLine("Incorrect Input!");
                return;
            }
            var weights = new Dictionary<char, int>
            {
                {'+', 1},
                {'-', 1},
                {'/', 2},
                {'*', 2},
                {'^', 3},
                {'(', 0},
                {' ', -100}
            };

            var node = new StackNode(' ');
            string output = String.Empty;
            foreach(char ch in str)
            {
                if (char.IsLetterOrDigit(ch))
                {
                    output += ch;
                }
                else
                {
                    if (ch == ')')
                    {
                        while(node.Peek() != '(')
                        {
                            output += node.Pop();
                        }
                        node.Pop();
                    }
                    else
                    {
                        if(ch == '(')
                        {
                            node.Push(ch);
                        }
                        else
                        {
                            while(node.Peek() != ' ' && weights[ch] <= weights[node.Peek()])
                            {
                                output += node.Pop();
                            }
                            node.Push(ch);
                        }
                    }
                }
                Console.Write("String: {0, 15}, Stack: ", output);
                node.Print();
            }
            while(node.Peek() != ' ')
            {
                output += node.Pop();
            }
            Console.WriteLine("Final result: {0}", output);
        }
    }
}
