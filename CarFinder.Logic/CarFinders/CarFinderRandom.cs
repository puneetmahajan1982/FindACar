using System;

namespace CarFinder.Logic
{
    class CarFinderRandom : ICarFinder
    {
        private string _description;
        private ICarFinderScore _searchScore;

        public CarFinderRandom()
        {
            _description = "CarFinder - Random";
            _searchScore = new CarFinderScore();
        }

        public string Description
        {
            get { return _description; }
        }

        public void GuessPosition(uint tick, ICar car)
        {
            var rand = new Random();
            long position= rand.Next();

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