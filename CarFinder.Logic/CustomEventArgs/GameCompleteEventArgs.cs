using System;
using System.Collections.Generic;

namespace CarFinder.Logic
{
    public class GameCompleteEventArgs:EventArgs
    {
        public bool Success { get; set; }
        public List<ICarFinder> CarFinders { get; set; }
    }
}