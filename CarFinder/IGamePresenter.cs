namespace CarFinder
{
    public interface IGamePresenter
    {
        void Setup();
        void Start();
        void CancelProcess();
        void Randomize();
        bool IsNumericParameters(string position, string velocity);
    }
}