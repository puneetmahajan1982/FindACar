namespace CarFinder.Logic
{
    public interface ICarFinderScore
    {
        bool IsSuccess { get; set; }
        uint SuccessTick { get; set; }
        long Position { get; set; }
    }
}