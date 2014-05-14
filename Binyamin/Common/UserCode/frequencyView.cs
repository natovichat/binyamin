using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class frequencyView
    {
        public static string CSVHeader()
        {
            string CSV = "";
           
            CSV += " בית ספר" + ",";
            CSV += "תדירות לתקופה" + ",";
          


            return CSV;
        }
        public string ToCSV()
        {


            string CSV = "";
          
            CSV += SchoolName + ",";
         
            CSV += frequencyNum + ",";



            return CSV;

        }
    }
}
