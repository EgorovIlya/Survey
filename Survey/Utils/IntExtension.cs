using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Utils
{
    public static class IntExtension
    {
        public static string GetRussianYears(this int years)
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
