using System;
using System.Collections.Generic;

namespace ComparableDemo
{
    //public static class ClassesExtensionMethods
    //{
    //    public static IEnumerable<T> EveryOther<T>(this IEnumerable<T> enumerable)
    //    {
    //        bool retNext = true;
    //        foreach (T t in enumerable)
    //        {
    //            if (retNext) yield return t;
    //            retNext = !retNext;
    //        }
    //    }

    //}


    public class ClassesAndGenerics
    {
        /*
        #region "1.2 Making a Type Sortable"
        public class Square : IComparable<Square>
        {
            public Square() { }
            public Square(int height, int width)
            {
                this.Height = height;
                this.Width = width;


            }
            public int Height { get; set; }
            public int Width { get; set; }
            public int CompareTo(object obj)
            {
                Square square = obj as Square;
                if (square != null)
                    return CompareTo(square);
                throw
                    new ArgumentException("Both object being compared must be of type square.");

            }

            public override string ToString() => ($"Height:{this.Height}   Width:{this.Width}");

            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;
                Square square = obj as Square;
                if (square != null)
                    return this.Height == square.Height;
                return false;
            }

            public override int GetHashCode()
            {
                return this.Height.GetHashCode() | this.Width.GetHashCode();

            }

            public static bool operator ==(Square x, Square y) => x.Equals(y);
            public static bool operator !=(Square x, Square y) => !(x == y);
            public static bool operator <(Square x, Square y) => (x.CompareTo(y) < 0);
            public static bool operator >(Square x, Square y) => (x.CompareTo(y) > 0);

            public int CompareTo(Square other)
            {
                long area1 = this.Height * this.Width;
                long area2 = other.Height * other.Width;
                if (area1 == area2)
                    return 0;
                else if (area1 > area2)
                    return 1;
                else if (area1 < area2)
                    return -1;
                else
                    return -1;
            }
        }

        public class CompareHeight : IComparer<Square>
        {
            public int Compare(object firstSquare, object secondSquare)
            {
                Square square1 = firstSquare as Square;
                Square square2 = secondSquare as Square;
                if (square1 == null || square2 == null)
                    throw (new ArgumentException("Both parameters must be of type Square."));
                else
                    return Compare(firstSquare, secondSquare);
            }

            public int Compare(Square x, Square y)
            {
                if (x.Height == y.Height)
                    return 0;
                else if (x.Height > y.Height)
                    return 1;
                else if (x.Height < y.Height)
                    return -1;
                else
                    return -1;
            }
        }

        public static void Main()
        {
            List<Square> listOfSquares = new List<Square>(){
                                        new Square(1,3),
                                        new Square(4,3),
                                        new Square(2,1),
                                        new Square(6,1)};

            // Test a List<String>
            Console.WriteLine("List<String>");
            Console.WriteLine("Original list");
            foreach (Square square in listOfSquares)
            {
                Console.WriteLine(square.ToString());
            }


            Console.WriteLine();
            IComparer<Square> heightCompare = new CompareHeight();
            listOfSquares.Sort(heightCompare);
            Console.WriteLine("Sorted list using IComparer<Square>=heightCompare");
            foreach (Square square in listOfSquares)
            {
                Console.WriteLine(square.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("Sorted list using IComparable<Square>");
            listOfSquares.Sort();
            foreach (Square square in listOfSquares)
            {
                Console.WriteLine(square.ToString());
            }


            // Test a SORTEDLIST
            var sortedListOfSquares = new SortedList<int, Square>(){
                                    { 0, new Square(1,3)},
                                    { 2, new Square(3,3)},
                                    { 1, new Square(2,1)},
                                    { 3, new Square(6,1)}};

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("SortedList<Square>");
            foreach (KeyValuePair<int, Square> kvp in sortedListOfSquares)
            {
                Console.WriteLine($"{kvp.Key} : {kvp.Value}");
            }
        }
        #endregion

    */

        #region"1.3 test read"
        static void Main(string[] args)
        {
            string a = System.Console.ReadLine();
            //char b = (char)a;
            Console.WriteLine(a);
        }





        #endregion
    }
}
