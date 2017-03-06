using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFinder.Logic
{
    public class Helpers : IHelpers
    {
        /// <summary>
        /// Gets pluggable carfinders from all assemblies
        /// </summary>
        public IEnumerable<ICarFinder> GetCarFinders()
        {
            var allCarFinders = AppDomain.CurrentDomain.GetAssemblies()
              .SelectMany(x => x.GetTypes())
              .Where(x => typeof(ICarFinder).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
              .Select(x => Activator.CreateInstance(x) as ICarFinder);

            return allCarFinders;
        }

        /// <summary>
        /// Gets scores for carfinders
        /// </summary>
        public DataTable GetCarFinderScores(IEnumerable<ICarFinder> carFinders)
        {
            DataTable scores = new DataTable();
            scores.Columns.Add("Car Finder");
            scores.Columns.Add("Success");
            scores.Columns.Add("Time");
            scores.Columns.Add("Position");
            foreach (ICarFinder carFinder in carFinders)
            {
                DataRow dr = scores.NewRow();
                dr["Car Finder"] = carFinder.Description;
                dr["Success"] = carFinder.SearchScore.IsSuccess;
                dr["Time"] = carFinder.SearchScore.IsSuccess ? carFinder.SearchScore.SuccessTick.ToString() : string.Empty;
                dr["Position"] = carFinder.SearchScore.IsSuccess ? carFinder.SearchScore.Position.ToString() : string.Empty;

                scores.Rows.Add(dr);
            }

            return scores;
        }

        /// <summary>
        /// Reads parameters from file
        /// </summary>
        public void ReadParameters(out short initialVelocity, out short initialPosition, string configFile = "config.txt")
        {
            if (string.IsNullOrEmpty(configFile))
            {
                throw new ArgumentException("Invalid config file name");
            }

            //create initial file, for easier code review
            File.WriteAllText(configFile, "100,200");

            string configLine = File.ReadLines(configFile).First();

            if (!short.TryParse(configLine.Split(',')[0], out initialVelocity) &
                short.TryParse(configLine.Split(',')[1], out initialPosition))
            {
                throw new Exception("Invalid config");
            }
        }

        /// <summary>
        /// Writes carfinder scores to an output file
        /// </summary>
        public void WriteScoresToFile(IEnumerable<ICarFinder> carFinders, string configFile = "output.txt")
        {
            if (string.IsNullOrEmpty(configFile))
            {
                throw new ArgumentException("Output file not supplied");
            }

            StringBuilder scores = new StringBuilder();
            scores.AppendLine("Car Finder" + "," + "Success" + "," +"Time" + "," + "Position");
            foreach (ICarFinder carFinder in carFinders)
            {
                scores.AppendLine(carFinder.Description  + "," + (carFinder.SearchScore.IsSuccess ? carFinder.SearchScore.SuccessTick.ToString() : string.Empty) + "," + (carFinder.SearchScore.IsSuccess ? carFinder.SearchScore.Position.ToString() : string.Empty));
            }

            File.WriteAllText(configFile, scores.ToString());
        }

    }
}
