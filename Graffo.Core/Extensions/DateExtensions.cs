using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graffo.Core.Extensions
{
    public static class DateExtensions
    {
        public static DateTime InicioDoDia(this DateTime data)
        {
            return new DateTime(data.Year, data.Month, data.Day, 0, 0, 0);
        }

        public static DateTime FimDoDia(this DateTime data)
        {
            return new DateTime(data.Year, data.Month, data.Day, 23, 59, 59);
        }
    } 
}
