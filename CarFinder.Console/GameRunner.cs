using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CarFinder.Logic;

namespace CarFinder.Console
{
    public class GameRunner
    {
        private readonly IHelpers _helpers;
        private Lazy<IEnumerable<ICarFinder>> carFinders;
        private CancellationTokenSource _cancellationTokenSource;

        public GameRunner(IHelpers helpers)
        {
            // pass constructor depedency
            _helpers = helpers;
        }

        public void RunGame(short initialVelocity, short initialPosition)
        {
            ICar car = new Car(initialVelocity, initialPosition);
            Task.Factory.StartNew(() => { carFinders = new Lazy<IEnumerable<ICarFinder>>(new Helpers().GetCarFinders); }).Wait();

            IGame game = new Game(GameMode.Console, car, carFinders);

            game.OnGameComplete += OnGameOnGameComplete;
            _cancellationTokenSource = new CancellationTokenSource();
            System.Console.WriteLine("Running Game");
            game.Start(_cancellationTokenSource);
        }

        private void OnGameOnGameComplete(object sender, GameCompleteEventArgs e)
        {
            _helpers.WriteScoresToFile(carFinders.Value);
        }
    }
}
