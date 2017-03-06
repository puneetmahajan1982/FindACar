using System.Data;

namespace CarFinder
{
    public interface IGameView
    {
        void DisplayMessage(string message);

        void UpdateStatus(string status);

        string GetStatus();

        void BindScores(DataTable data);

        string Velocity { get; set; }

        string Position { get; set; }

        void ChangeToStartMode();

        void ChangeToCancelMode();
    }
}