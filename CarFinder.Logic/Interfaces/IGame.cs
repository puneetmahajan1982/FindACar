using System;
using System.Threading;

namespace CarFinder.Logic
{
    public interface IGame
    {
        event EventHandler<GameProgressEventArgs> OnCarMoved;
        event EventHandler<GameCompleteEventArgs> OnGameComplete;
        void Start(CancellationTokenSource cancellationTokenSource);
    }
}