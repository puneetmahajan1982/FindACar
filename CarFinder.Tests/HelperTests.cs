using System;
using System.Collections.Generic;
using CarFinder.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarFinder.Tests
{
    [TestClass]
    public class HelperTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadParameters_Throws_Exception_When_Config_File_Is_Null()
        {
            //arrange
            var helper = new Helpers();

            //act
            short velocity;
            short position;
            helper.ReadParameters(out velocity, out position, null);

            //assert

            //Expects ArgumentException as config file path is null
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadParameters_Throws_Exception_When_Config_File_Is_Empty()
        {
            //arrange
            var helper = new Helpers();

            //act
            short velocity;
            short position;
            helper.ReadParameters(out velocity, out position, string.Empty);

            //assert

            //Expects ArgumentException as config file path is empty
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WriteScoresToFile_Throws_Exception_When_Output_File_Is_Null()
        {
            //arrange
            var helper = new Helpers();

            //act
            helper.WriteScoresToFile(new List<ICarFinder>(), null);

            //assert

            //Expects ArgumentException as output file path is null
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WriteScoresToFile_Throws_Exception_When_Output_File_Is_Empty()
        {
            //arrange
            var helper = new Helpers();

            //act
            helper.WriteScoresToFile(new List<ICarFinder>(), string.Empty);

            //assert

            //Expects ArgumentException as output file path is empty
        }

    }
}
