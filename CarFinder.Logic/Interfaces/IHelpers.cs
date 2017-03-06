using System.Collections.Generic;
using System.Data;

namespace CarFinder.Logic
{
    public interface IHelpers
    {
        /// <summary>
        /// Gets pluggable carfinders from all assemblies
        /// </summary>
        IEnumerable<ICarFinder> GetCarFinders();

        /// <summary>
        /// Gets scores for carfinders
        /// </summary>
        DataTable GetCarFinderScores(IEnumerable<ICarFinder> carFinders);

        /// <summary>
        /// Reads parameters from file
        /// </summary>
        void ReadParameters(out short initialVelocity, out short initialPosition, string configFile = "config.txt");

        /// <summary>
        /// Writes carfinder scores to an output file
        /// </summary>
        void WriteScoresToFile(IEnumerable<ICarFinder> carFinders, string configFile = "output.txt");
    }
}