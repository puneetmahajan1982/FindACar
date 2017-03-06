namespace CarFinder.Logic
{
    class CarFinderDouble : ICarFinder
    {
        private string _description;
        private ICarFinderScore _searchScore;

        public CarFinderDouble()
        {
            _description = "CarFinder - Double";
            _searchScore = new CarFinderScore();
        }

        public string Description
        {
            get { return _description; }
        }

        public void GuessPosition(uint tick, ICar car)
        {
            uint position = tick * 2;

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