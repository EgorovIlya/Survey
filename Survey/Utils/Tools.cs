﻿using System;

namespace Survey.Utils
{

   public static class Tools
    {
        /// <summary>
        ///     Returns the text corresponding to the number of years
        /// </summary>
        /// <param name="years">the number of years</param>
        /// <returns>the text</returns>
        public static string GetYears(int years)
        {
            if ((years - 11) % 100 == 0)
                return "лет";

            if ((years - 12) % 100 == 0)
                return "лет";

            if ((years - 13) % 100 == 0)
                return "лет";

            if ((years - 14) % 100 == 0)
                return "лет";

            if ((years - 1) % 10 == 0)
                return "год";

            if ((years - 2) % 10 == 0)
                return "года";

            if ((years - 3) % 10 == 0)
                return "года";

            if ((years - 4) % 10 == 0)
                return "года";
            
                return "лет";
        }
    }
}
