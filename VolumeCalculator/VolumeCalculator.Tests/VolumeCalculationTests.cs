using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VolumeCalculator.ViewModel;
using VolumeCalculator.Interfaces;
using VolumeCalculator.Misc;
using VolumeCalculator.Model;
using System.IO;

namespace VolumeCalculator.Tests
{
    /// <summary>
    /// Test class to verify all the exceptions while reading data.
    /// </summary>
    [TestClass]
    public class VolumeCalculationTests
    {
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ReadCSVFile_FormatException()
        {
            //Arrange
            VolumeCalculatorVM vm = InitializeTest();
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Test_Files", "depthvalues_err1.csv");

            //Act
            vm.ImportText = filePath;
        }        

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ReadCSVFile_InvalidDataException()
        {
            //Arrange
            VolumeCalculatorVM vm = InitializeTest();
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Test_Files", "depthvalues_err2.csv");

            //Act
            vm.ImportText = filePath;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadCSVFile_ArgumentOutOfRangeException()
        {
            //Arrange
            VolumeCalculatorVM vm = InitializeTest();
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Test_Files", "depthvalues_err3.csv");

            //Act
            vm.ImportText = filePath;
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ReadCSVFile_OverflowException()
        {
            //Arrange            
            VolumeCalculatorVM vm = InitializeTest();
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Test_Files", "depthvalues_err4.csv");

            //Act
            vm.ImportText = filePath;
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ReadTXTFile_FormatException()
        {
            //Arrange
            VolumeCalculatorVM vm = InitializeTest();
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Test_Files", "depthvalues_err1.txt");

            //Act
            vm.ImportText = filePath;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ReadTXTFile_InvalidDataException()
        {
            //Arrange
            VolumeCalculatorVM vm = InitializeTest();
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Test_Files", "depthvalues_err2.txt");

            //Act
            vm.ImportText = filePath;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadTXTFile_ArgumentOutOfRangeException()
        {
            //Arrange
            VolumeCalculatorVM vm = InitializeTest();
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Test_Files", "depthvalues_err3.txt");

            //Act
            vm.ImportText = filePath;
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ReadTXTFile_OverflowException()
        {
            //Arrange            
            VolumeCalculatorVM vm = InitializeTest();
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Test_Files", "depthvalues_err4.txt");

            //Act
            vm.ImportText = filePath;
        }

        /// <summary>
        /// Initializes the view model for volume calculation test.
        /// </summary>
        /// <returns>Volume calculator view model object</returns>
        private static VolumeCalculatorVM InitializeTest()
        {
            IReadFile irf = new ReadFile();
            IVolumeCalculationHelper ivch = new VolumeCalculationHelper();
            IVolumeCalculatorModel ivcm = new VolumeCalculatorModel();
            VolumeCalculatorVM vm = new VolumeCalculatorVM(ivch, ivcm, irf);
            vm.InTestMode = true;
            return vm;
        }
    }
}
