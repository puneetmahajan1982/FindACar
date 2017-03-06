using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using CarFinder.Logic;
using CarFinder.Properties;

namespace CarFinder
{
    public class GamePresenter : IGamePresenter
    {
        private readonly IGameView _view;
        private BackgroundWorker _backgroundWorker;
        private Lazy<IEnumerable<ICarFinder>> carFinders;
        private CancellationTokenSource _cancellationTokenSource;

        public GamePresenter(IGameView view)
        {
            _view = view;
        }

        public void Setup()
        {
            _backgroundWorker = new BackgroundWorker();

            _backgroundWorker.DoWork += BackgroundWorkerOnDoWork;
            _backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            _backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.WorkerSupportsCancellation = true;

            Task.Factory.StartNew(() => { carFinders = new Lazy<IEnumerable<ICarFinder>>(new Helpers().GetCarFinders); })
                .Wait();
        }

        public void Start()
        {
            if (_backgroundWorker.IsBusy)
            {
                _view.DisplayMessage(Resources.Form1_btnStart_Click_Game_already_running);
            }
            else
            {
                if (IsNumericParameters(_view.Position, _view.Velocity))
                {
                    _backgroundWorker.RunWorkerAsync();
                    _view.UpdateStatus(Resources.Form1_btnStart_Click_Starting_Game);
                    _view.ChangeToStartMode();
                    UpdateScores(carFinders.Value);
                }
                else
                {
                    _view.DisplayMessage(Resources.Form1_btnStart_Click_Parameters_must_be_numeric);
                }
            }
        }

        public bool IsNumericParameters(string position, string velocity)
        {
            int value;
            return int.TryParse(position, out value) && int.TryParse(velocity, out value);
        }

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            ICar car = new Car(Convert.ToInt16(_view.Velocity), Convert.ToInt16(_view.Position));
            IGame game = new Game(GameMode.UI, car, carFinders);

            game.OnGameComplete += OnGameOnGameComplete;
            game.OnCarMoved += GameOnCarMoved;

            _cancellationTokenSource = new CancellationTokenSource();
            game.Start(_cancellationTokenSource);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _view.UpdateStatus(Resources.Form1_backgroundWorker_RunWorkerCompleted_Game_Complete);
            UpdateScores(carFinders.Value);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _view.UpdateStatus(e.UserState.ToString());
        }

        private void GameOnCarMoved(object sender, GameProgressEventArgs gameProgressEventArgs)
        {
            _backgroundWorker.ReportProgress(0, gameProgressEventArgs.Progress);
        }

        private void OnGameOnGameComplete(object sender, GameCompleteEventArgs e)
        {
            CancelProcess();
        }

        public void CancelProcess()
        {
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }

            if (_backgroundWorker.IsBusy)
            {
                _backgroundWorker.CancelAsync();
            }

            _view.ChangeToCancelMode();
        }

        public void Randomize()
        {
            Random rand = new Random();
            _view.Position = rand.Next(-1000, 1001).ToString();
            _view.Velocity = rand.Next(-1000, 1001).ToString();
        }

        private void UpdateScores(IEnumerable<ICarFinder> carFinders)
        {
            _view.BindScores(new Helpers().GetCarFinderScores(carFinders));
        }
    }
}