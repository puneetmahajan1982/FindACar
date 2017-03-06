namespace CarFinder.Logic
{
    class CarFinderSquare : ICarFinder
    {
        private string _description;
        private ICarFinderScore _searchScore;

        public CarFinderSquare()
        {
            _description = "CarFinder - Square";
            _searchScore = new CarFinderScore();
        }

        public string Description
        {
            get { return _description; }
        }

        public void GuessPosition(uint tick, ICar car)
        {
            long position = tick*tick;

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