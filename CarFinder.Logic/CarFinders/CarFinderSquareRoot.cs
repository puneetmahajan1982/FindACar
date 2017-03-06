using System;

namespace CarFinder.Logic
{
    class CarFinderSquareRoot : ICarFinder
    {
        private string _description;
        private ICarFinderScore _searchScore;

        public CarFinderSquareRoot()
        {
            _description = "CarFinder - SquareRoot";
            _searchScore = new CarFinderScore();
        }

        public string Description
        {
            get { return _description; }
        }

        public void GuessPosition(uint tick, ICar car)
        {
            long position = (long) Math.Sqrt(tick);
            if (position == car.CurrentPosition)
            {
                _searchScore.IsSuccess = true;
                _searchScore.Position = car.CurrentPosition;
                _searchScore.SuccessTick = car.Tick;
            }
        }

        public ICarFinderScore SearchScore
        {
            get { return _searchScore; }
        }
    }
}