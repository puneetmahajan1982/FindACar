using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarFinder.Tests
{
    [TestClass]
    public class GameViewTests
    {
        [TestMethod]
        public void UpdateStatus_Changes_Status_Label_Text()
        {
            //arrange
            IGameView gameView = new GameView();
            string status = "NewStatus";
            gameView.UpdateStatus(status);

            //act
            string readStatus = gameView.GetStatus();

            //assert
            Assert.AreEqual(readStatus, status);
        }
    }
}
