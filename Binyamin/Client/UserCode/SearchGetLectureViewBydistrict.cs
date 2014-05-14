﻿using System;
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
    public partial class SearchGetLectureViewBydistrict
    {
        IDispatcher current;
        partial void SearchGetLectureViewBydistrict_InitializeDataWorkspace(List<IDataService> saveChangesTo)
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

            this.LectureViewstart = begin;
            this.LectureViewc_end = DateTime.Now;
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

            csv.AppendFormat(LectureView.CSVHeader() + System.Environment.NewLine);

            foreach (LectureView view in this.GetLectureViewBydistrict)
            {

                csv.AppendFormat(view.ToCSV() + System.Environment.NewLine, view);
            }

            return csv.ToString(0, csv.Length - 1);
        }
    }
}
