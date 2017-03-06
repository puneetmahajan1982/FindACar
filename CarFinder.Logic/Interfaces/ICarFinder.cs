namespace CarFinder.Logic
{
    public interface ICarFinder
    {
        string Description { get; }
        void GuessPosition(uint tick, ICar car);
        ICarFinderScore SearchScore {get;}
    }
}