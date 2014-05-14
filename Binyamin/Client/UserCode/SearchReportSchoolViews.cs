using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using LightSwitchApplication.UserCode;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using Microsoft.LightSwitch.Threading;
using ServerSide;
namespace LightSwitchApplication
{
    public partial class SearchReportSchoolViews
    {
        IDispatcher current;
        partial void ReportSchoolViews_Changed(NotifyCollectionChangedEventArgs e)
        {
            
            int SumNumberOfCounselors = 0,SumNumberOfClasses=0;

            foreach (ReportSchoolView activity in ReportSchoolViews)
            {

                SumNumberOfCounselors = SumNumberOfCounselors + activity.TotalNumberOfCounselor;
                SumNumberOfClasses = SumNumberOfClasses + activity.TotalNumberOfClasses;
            }
            this.NumberOfActivity = ReportSchoolViews.Count;
            this.NumberOfClasses = SumNumberOfClasses;
            this.NumberOfCounselors = SumNumberOfCounselors;

        }

        partial void Export_Execute()
        {
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

            csv.AppendFormat(ReportSchoolView.CSVHeader() + System.Environment.NewLine);

            foreach (ReportSchoolView view in this.ReportSchoolViews)
            {

                csv.AppendFormat(view.ToCSV() + System.Environment.NewLine , view);
            }

            return csv.ToString(0, csv.Length - 1);
        }
        partial void Calculate_Classes_Execute()
        {
            int Maxseven = 0, Maxeight = 0, Maxnine = 0, Maxten = 0, Maxeleven = 0, Maxtwelve = 0, numOfClasses, numOfCounselors, numOfRounds;
            int counterSeven = 0, counterEight = 0, counterNine = 0, counterTen = 0, counterEleven = 0, counterTwelve = 0;
            foreach (ReportSchoolView view in this.ReportSchoolViews)
            {
                numOfClasses = view.TotalNumberOfClasses;
                numOfCounselors = view.TotalNumberOfCounselor;
                numOfRounds = view.TotalNumberOfRounds;
                switch (view.Audience)
                {
                    case ("ז"):
                        counterSeven++;
                        if (numOfClasses > Maxseven)
                            if (numOfCounselors == 0 && numOfRounds > 1)
                            { //do nothing yet
                            }
                            else
                            {
                                Maxseven = numOfClasses;
                            }
                        break;
                    case ("ח"):
                        counterEight++;
                        if (numOfClasses > Maxeight)
                            if (numOfCounselors == 0 && numOfRounds > 1)
                            { //do nothing yet
                            }
                            else
                            {
                                Maxeight = numOfClasses;
                            }
                        break;
                    case ("ט"):
                        counterNine++;
                        if (numOfClasses > Maxnine)
                            if (numOfCounselors == 0 && numOfRounds > 1)
                            { //do nothing yet
                            }
                            else
                            {
                                Maxnine = numOfClasses;
                            }
                        break;

                    case ("י"):
                        counterTen++;
                        if (numOfClasses > Maxten)
                            if (numOfCounselors == 0 && numOfRounds > 1)
                            { //do nothing yet
                            }
                            else
                            {
                                Maxten = numOfClasses;
                            }
                        break;
                    case ("יא"):
                        counterEleven++;
                        if (numOfClasses > Maxeleven)
                            if (numOfCounselors == 0 && numOfRounds > 1)
                            { //do nothing yet
                            }
                            else
                            {
                                Maxeleven = numOfClasses;
                            }
                        break;
                    case ("יב"):
                        counterTwelve++;
                        if (numOfClasses > Maxtwelve)
                            if (numOfCounselors == 0 && numOfRounds > 1)
                            { //do nothing yet
                            }
                            else
                            {
                                Maxtwelve = numOfClasses;
                            }
                        break;

                }
            }
                ReportSchoolView viewFirst;
                viewFirst = ReportSchoolViews.ElementAt(0);
                string schoolType = viewFirst.SchoolType;
                if (schoolType.Equals("תיכון"))
                {

                    this.ShowMessageBox("שכבת י- " + Maxten +" כיתות קיבלה " + counterTen+" פעילויות "+ "\n" +
                        "שכבת יא- " + Maxeleven + " כיתות קיבלה " + counterEleven + " פעילויות " + "\n" +
                               "שכבת יב- " + Maxtwelve + " כיתות קיבלה " + counterTwelve + " פעילויות " + "\n");

                }
                else if (schoolType.Equals("שש שנתי"))
                {
                    this.ShowMessageBox("שכבת ז- " + Maxseven + " כיתות קיבלה " + counterSeven + " פעילויות " + "\n" +
                        "שכבת ח- " + Maxeight + " כיתות קיבלה " + counterEight + " פעילויות " + "\n" +
                               "שכבת ט- " + Maxnine + " כיתות קיבלה " + counterNine + " פעילויות " + "\n"+ 
                              "שכבת י- " + Maxten +" כיתות קיבלה " + counterTen+" פעילויות "+ "\n" +
                        "שכבת יא- " + Maxeleven + " כיתות קיבלה " + counterEleven + " פעילויות " + "\n" +
                               "שכבת יב- " + Maxtwelve + " כיתות קיבלה " + counterTwelve + " פעילויות " + "\n");

                }
                else
                {
                    this.ShowMessageBox("שכבת ז- " + Maxseven + " כיתות קיבלה " + counterSeven + " פעילויות " + "\n" +
                       "שכבת ח- " + Maxeight + " כיתות קיבלה " + counterEight + " פעילויות " + "\n" +
                              "שכבת ט- " + Maxnine + " כיתות קיבלה " + counterNine + " פעילויות " + "\n");
                 
                }
           
        }


     
    }
     
}
