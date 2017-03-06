using System;
using CarFinder.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarFinder.Tests
{
    [TestClass]
    public class GamePresenterTests
    {
        [TestMethod]
        public void Randomize_Set_View_With_Random_Initial_Parameters()
        {
            //arrange
            IGameView gameView = new GameView();
            IGamePresenter gamePresenter = new GamePresenter(gameView);

            gameView.Position = string.Empty;
            gameView.Velocity = string.Empty;

            string initialVelocity = gameView.Velocity;
            string initialPosition = gameView.Position;

            //act
            gamePresenter.Randomize();

            //assert
            Assert.AreEqual(string.Empty, initialVelocity);
            Assert.AreEqual(string.Empty, initialPosition);
            Assert.AreNotEqual(initialPosition, gameView.Position);
            Assert.AreNotEqual(initialVelocity, gameView.Velocity);
        }

        [TestMethod]
        public void Randomize_Set_View_With_Random_Valid_Initial_Parameters()
        {
            //arrange
            IGameView gameView = new GameView();
            IGamePresenter gamePresenter = new GamePresenter(gameView);

            //act
            gamePresenter.Randomize();

            //assert
            Assert.IsTrue(gamePresenter.IsNumericParameters(gameView.Position, gameView.Velocity));
        }

        [TestMethod]
        public void IsNumericParameters_Returns_False_When_Position_Is_Not_Numeric()
        {
            //arrange
            IGamePresenter gamePresenter = new GamePresenter(null);

            //act
            bool isNumericParameters = gamePresenter.IsNumericParameters("aaa", 0.ToString());

            //assert
            Assert.IsFalse(isNumericParameters);
        }

        [TestMethod]
        public void IsNumericParameters_Returns_False_When_Velocity_Is_Not_Numeric()
        {
            //arrange
            IGamePresenter gamePresenter = new GamePresenter(null);

            //act
            bool isNumericParameters = gamePresenter.IsNumericParameters(0.ToString(), "aaa");

            //assert
            Assert.IsFalse(isNumericParameters);
        }

        [TestMethod]
        public void IsNumericParameters_Returns_False_When_Velocity_And_Position_Are_Not_Numeric()
        {
            //arrange
            IGamePresenter gamePresenter = new GamePresenter(null);

            //act
            bool isNumericParameters = gamePresenter.IsNumericParameters("aaa", "aaa");

            //assert
            Assert.IsFalse(isNumericParameters);
        }

        [TestMethod]
        public void IsNumericParameters_Returns_True_When_Velocity_And_Position_Are_Numeric()
        {
            //arrange
            IGamePresenter gamePresenter = new GamePresenter(null);

            //act
            bool isNumericParameters = gamePresenter.IsNumericParameters(0.ToString(), 0.ToString());

            //assert
            Assert.IsTrue(isNumericParameters);
        }

    }
}
