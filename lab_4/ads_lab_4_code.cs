using System;
using static System.Console;

namespace connected_arrays_ads
{
    class SLList
    {
        public Node head;
        public Node tail;
        public class Node
        {
            public string data;
            public Node next;
            public Node(string data)
            {
                this.data = data;
            }
            public Node(string data, Node next)
            {
                this.data = data;
                this.next = next;
            }
        }
        public SLList(string data)
        {
            head = new Node(data);
            tail = head;
        }
        public void AddFirst(string data)
        {
            Node newNode = new Node(data, head);
            head = newNode;
        }
        public void AddToPosition(string data, int position)
        {
            Node current = head;

            for (int i = 1; i < position - 1; i++)
            {
                if (current == null)
                {
                    Write("There is no such position. First element was added.");
                    AddFirst(data);
                    return;
                }
                current = current.next;
                    
            }

            Node newNode = new Node(data, current.next);
            current.next = newNode;
        }
        public void AddLast(string data)
        {
            tail.next = new Node(data);
            tail = tail.next;
            
        }
        public void DeleteFirst()
        {
            head = head.next;
        }
        public void DeleteFromPosition(int position)
        {
            Node current = head;

            for (int i = 1; i < position - 3; i++)
            {       
                if (current.next == null)
                {
                    Write("There is no such position. First element was deleted.");
                    DeleteFirst();
                    return;
                }
                current = current.next;
            }

            current.next = current.next.next;
        }
        public void DeleteLast()
        {
            Node current = head;

            while (current.next.next != null)
                current = current.next;

            tail = current;
            tail.next = null;
        }
        public void Print()
        {
            Node current = head;
            WriteLine();
            while (current.next != null)
            {
                Write(current.data + "-");
                current = current.next;
            }
            Write(current.data);
            WriteLine();
        }
        public void personalTask(string data)
        {
            Node current = head;
            int counter = 0;
            while (current != null)
            {
                current = current.next;
                counter++;
            }

            if (counter % 2 == 0)
                AddToPosition(data, counter / 2);
            else
                AddToPosition(data, 1);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var node = new SLList("0");
            while (true)
            {
                Console.WriteLine("[ 1 ] Add First");
                Console.WriteLine("[ 2 ] Add To Position");
                Console.WriteLine("[ 3 ] Add Last");
                Console.WriteLine("[ 4 ] Delete First");
                Console.WriteLine("[ 5 ] Delete From Position");
                Console.WriteLine("[ 6 ] Delete Last");
                Console.WriteLine("[ 7 ] Personal Task (№9)");
                Console.WriteLine("[ 8 ] Exit");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        node.AddFirst(askForData());
                        break;
                    case "2":
                        node.AddToPosition(askForData(), askForPos());
                        break;
                    case "3":
                        node.AddLast(askForData());
                        break;
                    case "4":
                        node.DeleteFirst();
                        break;
                    case "5":
                        node.DeleteFromPosition(askForPos());
                        break;
                    case "6":
                        node.DeleteLast();
                        break;
                    case "7":
                        node.personalTask(askForData());
                        break;
                    case "8":
                        Environment.Exit(0);
                        break;

                }
                node.Print();
            }
        }
        static string askForData()
        {
            Write("Input data: "); string data = ReadLine();
            return data;
        }
        static int askForPos()
        {
            int pos = 0;
            while (true)
            {
                Write("Input position: ");
                try
                {
                    pos = Convert.ToInt32(ReadLine());
                    return pos;
                }
                catch
                {
                    WriteLine("Input int format!");
                }
            }          
        }
    }
}
