using FootballNeighborhood.Domain.Consts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballNeighborhood.Domain.Extentions.Common
{
    public static class DateTimeExtensions
    {
        public static string DateTimeToString(this DateTime target)
        {
            return target.ToString(DateTimeSettings.DateTimeFormat);
        }
    }
}
