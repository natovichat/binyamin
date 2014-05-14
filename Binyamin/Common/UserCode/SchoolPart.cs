using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class SchoolPart
    {
        partial void FullName_Compute(ref string result)
        {
            result = this.SchoolType + " " + this.School.Titel;
            if (this.SchoolName != null && this.SchoolName != "")
            {
                result += " " + this.SchoolName;
            }
            if (this.City != null)
            {
                result += " " + this.City.Title;
            }
        }
    }
}
