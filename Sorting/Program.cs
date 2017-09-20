using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting {

    class Pair<A, B> {

        public A First {
            get;
            set;
        }

        public B Second {
            get;
            set;
        }

        public Pair (A a, B b) {
            this.First = a;
            this.Second = b;
        }


        public override string ToString () {
            return string.Format("A = {0}, B = {1}", First.ToString(), Second.ToString());
        }



    }

    class PrevNode {

        public Node Node {
            get;
            set;
        }

        public PrevNode (Node p) {
            this.Node = p;
        }

        public static implicit operator Node (PrevNode p) {
            return p.Node;
        }

        public static implicit operator PrevNode (Node n) {
            return new PrevNode(n);
        }

    }

    class NextNode {

        public Node Node {
            get;
            set;
        }

        public NextNode (Node n) {
            this.Node = n;
        }

        public static implicit operator Node (NextNode n) {
            return n.Node;
        }

        public static implicit operator NextNode (Node n) {
            return new NextNode(n);
        }

    }

    class Node {
        public int Value {
            get;
            set;
        }

        public Node Next {
            get;
            set;
        }

        public Node Previous {
            get;
            set;
        }

        public Node () {

        }

        public Node (int v) : this() {
            this.Value = v;
        }

        public Node (NextNode n, int v) : this(v) {
            this.Next = n;
        }

        public Node (PrevNode p, int v) : this(v) {
            this.Previous = p;
        }

        public Node (Node p, Node n, int v) : this(v) {
            this.Previous = p;
            this.Next = n;
        }

        public Node LinkNode (Node p, Node n) {

            if (p != null) {
                this.Previous = p;
                p.Next = this;
            }
            if (n != null) {
                this.Next = n;
                n.Previous = this;
            }
            return this;
        }

        public static Pair<Node, Node> NodeList (int x) {
            Node start = null;
            Node current = null;
            for (int i = 0;i < x;i++) {
                if (start == null) {
                    start = new Node(int.Parse(Console.ReadLine()));
                } else {
                    current = start.Next == null ?
                        start.LinkNode(null, new Node((PrevNode)start, int.Parse(Console.ReadLine()))).Next :
                        current.LinkNode(current.Previous, new Node((PrevNode)current, int.Parse(Console.ReadLine()))).Next;
                }
            }
            return new Pair<Node, Node>(start, current);
        }
        public static void PrintList (Node n) {
            if (n != null) {
                Console.WriteLine(n.Value);
                PrintList(n.Next);
            }
        }
        public override string ToString () {
            return string.Format("{0}, {1}, {2}", this.Previous != null ? true : false, this.Value, this.Next != null ? true : false);
        }
    }

    class NodeIterator {
        public Node Previous {
            get { return Current.Previous; }
            set {
                Current.Previous = value;
                Current.Previous.Next = Current;
            }
        }

        public Node Current {
            get;
            set;
        }

        public Node Next {
            get { return Current.Next; }
            set {
                Current.Next = value;
                Current.Next.Previous = Current;
            }
        }

        public NodeIterator (Node n) {
            Current = n;
        }

        private static void AddFirst (NodeIterator i, Node n) {
            if (i.Previous == null) {
                i.Previous = n;
            } else {
                AddFirst(--i, n);
            }
        }

        private static void AddLast (NodeIterator i, Node n) {
            if (i.Next == null) {
                i.Next = n;
            } else {
                AddLast(++i, n);
            }
        }

        public void AddFirst (Node n) {
            Node copy = Current;
            AddFirst(this, n);
            Current = copy;
        }

        public void AddLast (Node n) {
            Node copy = Current;
            AddLast(this, n);
            Current = copy;
        }

        public static NodeIterator operator ++ (NodeIterator i) {
            if (i.Next != null) i.Current = i.Next;
            return i;
        }

        public static NodeIterator operator -- (NodeIterator i) {
            if (i.Previous != null) i.Current = i.Previous;
            return i;
        }

    }

    class Program {

        

        static void Main (string[] args) {

            //var meme = Node.NodeList( 5 );

            NodeIterator m = new NodeIterator(new Node(2));
            m.AddFirst(new Node(1));
            m.AddLast(new Node(3));
            m.AddLast(new Node(4));

            //Node.PrintList( meme.First );

            //int[] x = { 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
            //bubbleSort( x );
        }

        public static void bubbleSort (int[] arr) {
            int count = 0;
            bool sorted = false;
            for (int i = arr.Length;i >= 0 && !sorted;i--) {
                sorted = true;
                for (int j = 0;j < arr.Length - 1;j++) {
                    count++;
                    if (arr[j] > arr[j + 1]) {
                        int h = arr[j];
                        Swap(ref arr[j], ref arr[j + 1]);
                        sorted = false;
                    }
                }
            }
        }

        public static void bubbleDown (int[] arr) {
            bool sorted = false;
            for (int i = 0;i <= arr.Length && !sorted;i++) {
                sorted = true;
                for (int j = arr.Length - 1;j > i;j--) {
                    if (arr[j] > arr[j - 1]) {
                        int h = arr[j];
                        Swap(ref arr[j], ref arr[j - 1]);
                        sorted = false;
                    }
                }
            }
        }

        public static void Swap (ref int x, ref int y) {
            int z = x;
            x = y;
            y = z;
        }
    }
}