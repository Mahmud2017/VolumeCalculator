using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using VolumeCalculator.Interfaces;
using VolumeCalculator.Misc;
using System.Collections.Generic;
using System.Linq;

namespace VolumeCalculator.Tests
{
    /// <summary>
    /// To test the file reading functionality.
    /// </summary>
    [TestClass]
    public class ReadFileTests
    {
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ReadCSVFile_FormatException()
        {
            //Arrange
            IReadFile irf = new ReadFile();
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Test_Files", "depthvalues_err1.csv");

            //Act
            irf.Read(filePath);
        }        

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ReadCSVFile_InvalidDataException()
        {
            //Arrange
            IReadFile irf = new ReadFile();
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Test_Files", "depthvalues_err2.csv");

            //Act
            irf.Read(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadCSVFile_ArgumentOutOfRangeException()
        {
            //Arrange
            IReadFile irf = new ReadFile();
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Test_Files", "depthvalues_err3.csv");

            //Act
            irf.Read(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ReadCSVFile_OverflowException()
        {
            //Arrange            
            IReadFile irf = new ReadFile();
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Test_Files", "depthvalues_err4.csv");

            //Act
            irf.Read(filePath);
        }

        [TestMethod]
        public void ReadCSVFile_WithoutError()
        {
            //Arrange
            IReadFile irf = new ReadFile();
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Test_Files", "depthvalues.csv");
            List<int> expectedData = new List<int>();

            using (var reader = new StreamReader(@"" + filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var numbers = line.Split(' ');

                    numbers.ToList().ForEach(n =>
                    {
                        expectedData.Add(Convert.ToInt32(n));
                    });
                }
            }

            //Act
            List<int> actualData = irf.Read(filePath);

            //Assert
            CollectionAssert.AreEqual(expectedData, actualData);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ReadTXTFile_FormatException()
        {
            //Arrange
            IReadFile irf = new ReadFile();
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Test_Files", "depthvalues_err1.txt");

            //Act
            irf.Read(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ReadTXTFile_InvalidDataException()
        {
            //Arrange
            IReadFile irf = new ReadFile();
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Test_Files", "depthvalues_err2.txt");

            //Act
            irf.Read(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadTXTFile_ArgumentOutOfRangeException()
        {
            //Arrange
            IReadFile irf = new ReadFile();
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Test_Files", "depthvalues_err3.txt");

            //Act
            irf.Read(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ReadTXTFile_OverflowException()
        {
            //Arrange            
            IReadFile irf = new ReadFile();
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Test_Files", "depthvalues_err4.txt");

            //Act
            irf.Read(filePath);
        }

        [TestMethod]
        public void ReadTXTFile_WithoutError()
        {
            //Arrange
            IReadFile irf = new ReadFile();
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Test_Files", "depthvalues.txt");
            List<int> expectedData = new List<int>();

            using (var reader = new StreamReader(@"" + filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var numbers = line.Split(' ');

                    numbers.ToList().ForEach(n =>
                    {
                        expectedData.Add(Convert.ToInt32(n));
                    });
                }
            }

            //Act
            List<int> actualData = irf.Read(filePath);

            //Assert
            CollectionAssert.AreEqual(expectedData, actualData);
        }
    }
}
