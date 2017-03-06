using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarFinder.Logic;

namespace CarFinder.Console
{
    public class ConfigReader
    {
        private readonly IHelpers _helpers;

        public ConfigReader(IHelpers helpers)
        {
            // pass constructor depedency
            _helpers = helpers;
        }

        public void ReadParameters(out short initialVelocity, out short initialPosition)
        {
            _helpers.ReadParameters(out initialVelocity, out initialPosition);
        }
    }
}
