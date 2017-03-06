namespace CarFinder.Logic
{
    class CarFinderScore : ICarFinderScore
    {
        public bool IsSuccess { get; set; }
        public uint SuccessTick { get; set; }
        public long Position { get; set; }
    }
}