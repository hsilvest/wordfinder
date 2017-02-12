using System;
using System.Collections.Generic;
using System.Linq;

namespace WordFinder
{
    public class WordFinder
    {
        #region Properties
        public char[,] Matrix { get; private set; }
        public List<string> Dictionary { get; private set; }

        private Coordinate[] directions = {
            new Coordinate(-1, 0), 
            new Coordinate(0, -1), 
            new Coordinate(1, 0),  
            new Coordinate(0, 1), 
        };
        #endregion

        #region Contructor
        public WordFinder(IEnumerable<string> dictionary)
        {
            Dictionary = dictionary.ToList();
        }
        #endregion

        #region Main Methods
        public IList<string> Find(IEnumerable<string> src)
        {
            GenerateMatrix(src);

            List<string> result = new List<string>();

            for (int word = 0; word < Dictionary.Count(); word++)
            {
                for (int y = 0; y < Matrix.GetLength(0); y++)
                {
                    for (int x = 0; x < Matrix.GetLength(1); x++)
                    {
                        if (Matrix[y, x] == Dictionary.ToList()[word][0])
                        {
                            var start = new Coordinate(x, y);
                            var end = SearchEachDirection(Dictionary.ToList()[word], x, y);
                            if (end != null)
                            {
                                result.Add(Dictionary.ToList()[word]);
                            }
                        }
                    }
                }
            }
            return result.ToList(); ;
        }
        #endregion

        #region Auxiliar Methods
        public static T[,] CreateRectangularArray<T>(List<T[]> arrays)
        {
            int minorLength = arrays[0].Length;
            T[,] ret = new T[arrays.Count, minorLength];
            for (int i = 0; i < arrays.Count; i++)
            {
                var array = arrays[i];
                if (array.Length != minorLength)
                {
                    throw new ArgumentException
                        ("All arrays must be the same length");
                }
                for (int j = 0; j < minorLength; j++)
                {
                    ret[i, j] = array[j];
                }
            }
            return ret;
        }

        private void GenerateMatrix(IEnumerable<string> src)
        {
            List<char[]> _aux = new List<char[]>();

            foreach (var item in src)
            {
                _aux.Add(item.ToCharArray());
            }

            Matrix = CreateRectangularArray(_aux);
        }

        private Coordinate SearchEachDirection(string word, int x, int y)
        {
            char[] chars = word.ToCharArray();
            // 4 because we only search words from: left to rigth and top to bottom
            for (int direction = 0; direction < 4; direction++)
            {
                var coordinate = SearchDirection(chars, x, y, direction);
                if (coordinate != null)
                {
                    return coordinate;
                }
            }
            return null;
        }

        private Coordinate SearchDirection(char[] chars, int x, int y, int direction)
        {
            // check limits of given matrix
            if (x < 0 || y < 0 || x >= Matrix.GetLength(1) || y >= Matrix.GetLength(0))
                return null;

            if (Matrix[y, x] != chars[0])
                return null;

            if (chars.Length == 1)
                return new Coordinate(x, y);

            // search next character in the direction
            char[] next = new char[chars.Length - 1];
            Array.Copy(chars, 1, next, 0, chars.Length - 1);
            return SearchDirection(next, x + directions[direction].X, y + directions[direction].Y, direction);
        }

        #endregion
    }
    public class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
