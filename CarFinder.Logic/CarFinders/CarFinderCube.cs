using System;

namespace CarFinder.Logic
{
    class CarFinderCube : ICarFinder
    {
        private string _description;
        private ICarFinderScore _searchScore;

        public CarFinderCube()
        {
            _description = "CarFinder - Cube";
            _searchScore = new CarFinderScore();
        }

        public string Description
        {
            get { return _description; }
        }

        public void GuessPosition(uint tick, ICar car)
        {
            uint position = tick*tick*tick;

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