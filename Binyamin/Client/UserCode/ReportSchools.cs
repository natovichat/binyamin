using System;
using System.Linq;
using System.IO;
using System.Globalization;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;

namespace LightSwitchApplication
{
    public partial class ReportSchools
    {
        partial void ReportSchools_InitializeDataWorkspace(global::System.Collections.Generic.List<global::Microsoft.LightSwitch.IDataService> saveChangesTo)
        {
            DateTime begin ;
        int month = DateTime.Now.Month;
        if (month == 09 || month == 10 || month == 11 || month == 12)
        {
            begin = new DateTime(DateTime.Now.Year, 09, 1);
        }
        else
        {
            begin = new DateTime(DateTime.Now.Year - 1, 09, 1);
        }
           
          this.FromDay = begin;
            this.toDay = DateTime.Now;
        }

        partial void ReportSchools_Saved()
        {
            // Write your code here.
            this.Close(false);
           
        }

        partial void GenerateReport_Execute()
        {

            this.Application.ShowSearchReportSchoolViews(this.FromDay, this.toDay,  this.SchoolParts.SelectedItem.Id);

        }
    }
}