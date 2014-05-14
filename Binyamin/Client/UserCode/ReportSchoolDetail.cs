using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using ServerSide;
using Microsoft.LightSwitch.Threading;
using System.Runtime.Serialization;
using System.Xml;
using System.Windows;
using LightSwitchApplication.UserCode;
using System.Text;
using System.Windows.Controls;
using System.Collections.Specialized;
namespace LightSwitchApplication
{
    public partial class ReportSchoolDetail
    {
        partial void SchoolPart_Loaded(bool succeeded)
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.SchoolPart);
        }

        partial void SchoolPart_Changed()
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.SchoolPart);
        }

        partial void ReportSchoolDetail_Saved()
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.SchoolPart);
        }

        partial void sumOfCounselors_Validate(ScreenValidationResultsBuilder results)
        {
            int sum = 0;
            DateTime timeNow = this.fromDate;
            DateTime timeEnd = this.toDate;
            List<Activity> list = this.SchoolPart.Activities.ToList();
            foreach (Activity activity in list)
            {
                if (activity.ActivityDate >= fromDate && activity.ActivityDate <= toDate)
                    sum = sum + activity.NumberOfCounselor;

            }
            sumOfCounselors = sum;

        }

        partial void sumOfClasses_Validate(ScreenValidationResultsBuilder results)
        {
            int sum = 0;
            List<Activity> list = this.SchoolPart.Activities.ToList();
            foreach (Activity activity in list)
            {
                if (activity.ActivityDate >= fromDate && activity.ActivityDate <= toDate)
                    sum = sum + activity.TotalNumberOfClasses;

            }
            sumOfClasses = sum;

        }

        partial void sumOfActivities_Validate(ScreenValidationResultsBuilder results)
        {
            sumOfActivities = 0;
            List<Activity> list = this.SchoolPart.Activities.ToList();
            foreach (Activity activity in list)
            {
                if (activity.ActivityDate >= fromDate && activity.ActivityDate <= toDate)
                    sumOfActivities = sumOfActivities + 1;
            }
        }

        partial void import_Execute()
        {
            IDispatcher current;
            current = Dispatchers.Current;
            Dispatchers.Main.BeginInvoke(() =>
            {
                SelectFileWindow.FileWindowArgument Argument = new SelectFileWindow.FileWindowArgument { FileAccess = System.IO.FileAccess.Write, FileFilter = "csv (*.csv)|*.csv" };

                SelectFileWindow selectFileWindow = new SelectFileWindow(Argument);

                selectFileWindow.Closed += new EventHandler(selectFileWindowForExport_Closed);

                selectFileWindow.Show();

            });
        }
        void selectFileWindowForExport_Closed(object sender, EventArgs e)
        {
            SelectFileWindow selectFileWindow = (SelectFileWindow)sender;


            StreamWriter stream = new StreamWriter(selectFileWindow.DocumentStream[0], Encoding.UTF8);
            stream.Write(GetCSV());
            stream.Close();


        }
        private string GetCSV()
        {
            StringBuilder csv = new StringBuilder();

            csv.AppendFormat(ActivityView.CSVHeader() + System.Environment.NewLine);

            foreach (Activity view in this.Activities)
            {

                csv.AppendFormat(view.ToCSV() + System.Environment.NewLine
                                    , view);
            }

            return csv.ToString(0, csv.Length - 1);
        }

    }
}