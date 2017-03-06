namespace CarFinder.Logic
{
    public interface ICar
    {
        long CurrentPosition { get; set; }
        uint Tick { get; }
        void MoveCar(uint tick, int velocity);
    }
}