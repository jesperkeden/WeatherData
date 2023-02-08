using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData
{
    internal class Enums
    {
        public enum MainMenu
        {
            Inside,
            Outside,
            Exit
        }
        public enum OutsideMenu
        {
            Search_For_Date,
            Sort_By_Temperature,
            Sort_By_Humidity,
            Sort_By_Risk_For_Mold,
            Metrological_Winter,
            Metrological_Fall,
            Return
        }
        public enum InsideMenu
        {
            Search_For_Date,
            Sort_By_Temperature,
            Sort_By_Humidity,
            Sort_By_Risk_For_Mold,
            Return
        }
    }
}
