namespace CarFinder.Logic
{
    public class GameProgressEventArgs
    {
        private string _progress;

        public string Progress
        {
            get { return _progress; }
        }

        public GameProgressEventArgs(uint ticksProcessed, uint maxTicks)
        {
            _progress = "Progress : " + ticksProcessed + "/" + maxTicks;
        }
    }
}