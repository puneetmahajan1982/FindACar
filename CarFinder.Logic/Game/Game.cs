using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarFinder.Logic
{
    public class Game : IGame
    {
        private const uint _maxTicks = 5000000;
        private readonly GameMode _mode;
        private readonly ICar _car;
        private readonly Lazy<IEnumerable<ICarFinder>> _carFinders;
        private SortedDictionary<uint, int> _velocitySet;

        public event EventHandler<GameProgressEventArgs> OnCarMoved;
        public event EventHandler<GameCompleteEventArgs> OnGameComplete;

        public Game(GameMode mode, ICar car, Lazy<IEnumerable<ICarFinder>> carFinders)
        {
            _mode = mode;
            _car = car;
            _carFinders = carFinders;
        }

        private void PopulateVelocity()
        {
            _velocitySet = new SortedDictionary<uint, int>();
            Random rand = new Random();
            for (uint i = 0; i < _maxTicks; i++)
            {
                uint id = i;
                _velocitySet[id] = rand.Next(-1000, 1001);
            }
        }

        public void Start(CancellationTokenSource cancellationTokenSource)
        {
            ParallelOptions po = new ParallelOptions();
            po.CancellationToken = cancellationTokenSource.Token;
            po.MaxDegreeOfParallelism = Environment.ProcessorCount;

            try
            {
                Task taskGame = Task.Factory.StartNew(PopulateVelocity)
                    .ContinueWith(
                        new Action<Task>(task =>
                            Parallel.ForEach(_velocitySet, po, x => ProcessTick(x, po))));

                taskGame.Wait(cancellationTokenSource.Token);
            }
            catch (OperationCanceledException e)
            {
                if (OnGameComplete != null)
                {
                    OnGameComplete(this,
                        new GameCompleteEventArgs() {Success = false, CarFinders = _carFinders.Value.ToList()});
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (OnGameComplete != null)
                OnGameComplete(this,
                    new GameCompleteEventArgs() {Success = false, CarFinders = _carFinders.Value.ToList()});
        }

        private void ProcessTick(KeyValuePair<uint, int> velocity, ParallelOptions parallelOptions)
        {
            parallelOptions.CancellationToken.ThrowIfCancellationRequested();

            _car.MoveCar(velocity.Key, velocity.Value);

            if (OnCarMoved != null)
            {
                OnCarMoved(this, new GameProgressEventArgs(velocity.Key, _maxTicks));
            }

            ConcurrentBag<Task> tasks = new ConcurrentBag<Task>();
            foreach (ICarFinder carFinder in _carFinders.Value)
            {
                ICarFinder finder = carFinder;
                if (!finder.SearchScore.IsSuccess)
                {
                    tasks.Add(Task.Factory.StartNew(() => finder.GuessPosition(velocity.Key, _car)));
                }
            }
            Task.WaitAll(tasks.ToArray());

            bool isSuccess = _carFinders.Value.All(x => x.SearchScore.IsSuccess);
            if (isSuccess)
            {
                if (OnGameComplete != null)
                {
                    OnGameComplete(this, new GameCompleteEventArgs() {Success = true, CarFinders = _carFinders.Value.ToList()});
                }
            }
        }

    }
}
