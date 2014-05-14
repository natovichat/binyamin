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
    public partial class SearchGetCounselorData
    {
        IDispatcher current;
        partial void  SearchGetCounselorData_InitializeDataWorkspace(List<IDataService> saveChangesTo)
       {
            DateTime begin;
            int month = DateTime.Now.Month;
            if (month == 09 || month == 10 || month == 11 || month == 12)
            {
                begin = new DateTime(DateTime.Now.Year, 09, 1);
            }
            else
            {
                begin = new DateTime(DateTime.Now.Year - 1, 09, 1);
            }

            this.from = begin;
            this.endTime = DateTime.Now;
            this.NumberOfActivities = GetCounselorData.Count();
        }
        partial void GetCounselorData_Changed(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.NumberOfActivities = GetCounselorData.Count();
           
        }
        partial  void calculateClasses_Execute()

        {
         
            int counterSeven = 0, counterEight = 0, counterNine = 0, counterTen = 0, counterEleven = 0, counterTwelve = 0;
        
            foreach (CounselorView view in this.GetCounselorData)
            {
                
                switch (view.Audience)
                {
                    case ("ז"):
                        counterSeven++;
                        
                        break;
                    case ("ח"):
                        counterEight++;
                      
                        break;
                    case ("ט"):
                        counterNine++;
                       
                        break;

                    case ("י"):
                        counterTen++;
                       
                        break;
                    case ("יא"):
                        counterEleven++;
                       
                        break;
                    case ("יב"):
                        counterTwelve++;
                       
                        break;

                }
            }
            CounselorView viewFirst;
            viewFirst = GetCounselorData.ElementAt(0);
           
                this.ShowMessageBox("שכבת ז- קיבלה " + counterSeven + " פעילויות " + "\n" +
                    "שכבת ח-  קיבלה " + counterEight + " פעילויות " + "\n" +
                           "שכבת ט-  קיבלה " + counterNine + " פעילויות " + "\n" +
                          "שכבת י- קיבלה " + counterTen + " פעילויות " + "\n" +
                    "שכבת יא-  קיבלה " + counterEleven + " פעילויות " + "\n" +
                           "שכבת יב-  קיבלה " + counterTwelve + " פעילויות " + "\n");

        }

        partial void calculate_Months_Execute()
        {

           //second option- conatins name of month and all 12 monthes
            int counterSeven = 0, counterEight = 0, counterNine = 0, counterTen = 0, counterEleven = 0, counterTwelve = 0;
            int counter1 = 0, counter2 = 0, counter3 = 0, counter4 = 0, counter5 = 0, counter6 = 0;
            foreach (CounselorView view in this.GetCounselorData)
            {

                switch (view.Month)
                {
                    case (1):
                        counter1++;

                        break;
                    case (2):
                        counter2++;

                        break;
                    case (3):
                        counter3++;
                        break;

                    case (4):
                        counter4++;

                        break;
                    case (5):
                        counter5++;

                        break;
                    case (6):
                        counter6++;

                        break;
                    case (7):
                        counterSeven++;

                        break;
                    case (8):
                        counterEight++;

                        break;
                    case (9):
                        counterNine++;

                        break;

                    case (10):
                        counterTen++;

                        break;
                    case (11):
                        counterEleven++;

                        break;
                    case (12):
                        counterTwelve++;

                        break;

                }
            }
            CounselorView viewFirst;
            viewFirst = GetCounselorData.ElementAt(0);

            this.ShowMessageBox("בחודש ינואר (1) העביר " + counter1 + " פעילויות " + "\n" +
                "בחודש פברואר (2) העביר  " + counter2 + " פעילויות " + "\n" +
                       "בחודש מרץ (3)  העביר " + counter3 + " פעילויות " + "\n" +
                      "בחודש אפריל (4)  העביר " + counter4 + " פעילויות " + "\n" +
                "בחודש מאי (5) העביר " + counter5+ " פעילויות " + "\n" +
                       "בחודש יוני (6)  העביר " + counter6 + " פעילויות " + "\n"+
                       "בחודש יולי (7) העביר " + counterSeven + " פעילויות " + "\n" +
                "בחודש אוגוסט (8)  העביר " + counterEight + " פעילויות " + "\n" +
                       "בחודש ספטמבר (9)   העביר " + counterNine + " פעילויות " + "\n" +
                      "בחודש אוקטובר (10)  העביר " + counterTen + " פעילויות " + "\n" +
                "בחודש נובמבר (11) העביר " + counterEleven + " פעילויות " + "\n" +
                       "בחודש דצמבר (12)  העביר " + counterTwelve + " פעילויות " + "\n");

        
        }

        partial void calculateCity_Execute()
        {
            string city;
             Dictionary<string, int> hashtable = new Dictionary<string, int>();
            
         
            foreach (CounselorView view in this.GetCounselorData)
            {
                city = view.ActivityPlace;
                if (hashtable.ContainsKey(city))
                {
                    hashtable[city] = hashtable[city] + 1;
                }
                else hashtable.Add(city, 1);

            }
            List<string> list = new List<string>(hashtable.Keys);
            string cityToInsert="";
            // Loop through list
           
           foreach (string k in list)
           {
               string t = k + " " + hashtable[k] + "\n";
               cityToInsert=cityToInsert+ t;
               
            }
           this.ShowMessageBox(cityToInsert);
        }

        partial void calculate_Schools_Execute()
        {
            string city;
            Dictionary<string, int> hashtable = new Dictionary<string, int>();


            foreach (CounselorView view in this.GetCounselorData)
            {
                city = view.SchoolName;
                if (hashtable.ContainsKey(city))
                {
                    hashtable[city] = hashtable[city] + 1;
                }
                else hashtable.Add(city, 1);

            }
            List<string> list = new List<string>(hashtable.Keys);
            string cityToInsert = "";
            // Loop through list

            foreach (string k in list)
            {
                string t = k + " " + hashtable[k] + "\n";
                cityToInsert = cityToInsert + t;

            }
            this.ShowMessageBox(cityToInsert);
        }

        partial void calculateTopic_Execute()
        {
            string city;
            Dictionary<string, int> hashtable = new Dictionary<string, int>();


            foreach (CounselorView view in this.GetCounselorData)
            {
                city = view.Topic;
                if (hashtable.ContainsKey(city))
                {
                    hashtable[city] = hashtable[city] + 1;
                }
                else hashtable.Add(city, 1);

            }
            List<string> list = new List<string>(hashtable.Keys);
            string cityToInsert = "";
            // Loop through list

            foreach (string k in list)
            {
                string t = k + " " + hashtable[k] + "\n";
                cityToInsert = cityToInsert + t;

            }
            this.ShowMessageBox(cityToInsert);
        }

        partial void calculateTopicCollection_Execute()
        {
            string city;
            Dictionary<string, int> hashtable = new Dictionary<string, int>();


            foreach (CounselorView view in this.GetCounselorData)
            {
                city = view.TopicCollection;
                if (hashtable.ContainsKey(city))
                {
                    hashtable[city] = hashtable[city] + 1;
                }
                else hashtable.Add(city, 1);

            }
            List<string> list = new List<string>(hashtable.Keys);
            string cityToInsert = "";
            // Loop through list

            foreach (string k in list)
            {
                string t = k + " " + hashtable[k] + "\n";
                cityToInsert = cityToInsert + t;

            }
            this.ShowMessageBox(cityToInsert);
        }

        partial void calculateDays_Execute()
        {
            string city;
            Dictionary<string, int> hashtable = new Dictionary<string, int>();


            foreach (CounselorView view in this.GetCounselorData)
            {
                city = view.ActivityDate.ToString("dddd");
                if (hashtable.ContainsKey(city))
                {
                    hashtable[city] = hashtable[city] + 1;
                }
                else hashtable.Add(city, 1);

            }
            List<string> list = new List<string>(hashtable.Keys);
            string cityToInsert = "";
            // Loop through list

            foreach (string k in list)
            {
                string t = k + " " + hashtable[k] + "\n";
                cityToInsert = cityToInsert + t;

            }
            this.ShowMessageBox(cityToInsert);
        }

       
      
        partial void export_Execute()
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

            csv.AppendFormat(CounselorView.CSVHeader() + System.Environment.NewLine);

            foreach ( CounselorView view in this.GetCounselorData)
            {

                csv.AppendFormat(view.ToCSV() + System.Environment.NewLine, view);
            }

            return csv.ToString(0, csv.Length - 1);
        }

        
    }
}
