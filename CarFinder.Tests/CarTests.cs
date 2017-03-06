using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarFinder.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarFinder.Tests
{
    [TestClass]
    public class CarTests
    {
        [TestMethod]
        public void Car_Computes_Correct_Current_Position()
        {
            //arrange 
            ICar car = new Car(2, 2);

            //act
            car.MoveCar(1,3);

            long currentPosition = car.CurrentPosition;
            long excectedPosition = 5;

            //assert
            Assert.AreEqual(excectedPosition, currentPosition);          
        }

        [TestMethod]
        public void Car_Recalculates_CurrentPosition_When_Car_Moves()
        {
            //arrange 
            ICar car = new Car(2, 2);

            //act
            long initialPosition = car.CurrentPosition;

            car.MoveCar(1, 3);

            long recalculatedPosition = car.CurrentPosition;

            //assert
            Assert.AreNotEqual(initialPosition, recalculatedPosition);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Car_Constructor_Throw_Exception_When_Initial_Velocity_Is_Less_Than_MinValue()
        {
            //arrange 
            //act
            ICar car = new Car(-2000, 0);

            //assert
            //expects exception when initial velocity is less than -1000
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Car_Constructor_Throw_Exception_When_Initial_Velocity_Is_More_Than_MaxValue()
        {
            //arrange 
            //act
            ICar car = new Car(2000, 0);

            //assert
            //expects exception when initial velocity is more than 1000
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Car_Constructor_Throw_Exception_When_Initial_Position_Is_Less_Than_MinValue()
        {
            //arrange 
            //act
            ICar car = new Car(0, -2000);

            //assert
            //expects exception when initial position is less than -1000
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Car_Constructor_Throw_Exception_When_Initial_Position_Is_More_Than_MaxValue()
        {
            //arrange 
            //act
            ICar car = new Car(0, 2000);

            //assert
            //expects exception when initial position is more than 1000
        }
    }
}
