using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VolumeCalculator.Interfaces;

namespace VolumeCalculator.Misc
{
    /// <summary>
    /// Handles the file read functionalities.
    /// </summary>
    public class ReadFile : IReadFile
    {
        public List<int> Read(string path)
        {
            List<int> topHorizonList = new List<int>();

            using (var reader = new StreamReader(@"" + path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var numbers = line.Split(' ');

                    numbers.ToList().ForEach(n =>
                    {
                        topHorizonList.Add(Convert.ToInt32(n));
                    });
                }
            }

            if (topHorizonList.Count / Globals.LATERAL_X != Globals.LATERAL_Y 
                || topHorizonList.Count / Globals.LATERAL_Y != Globals.LATERAL_X)
            {
                throw new InvalidDataException();                
            }
            else if (topHorizonList.Min() < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return topHorizonList;
        }

        public bool PathIsValid(string path)
        {
            return path == "" ? true : File.Exists(@"" + path);
        }
    }
}