//' Copyright © Microsoft Corporation.  All Rights Reserved.

//' This code released under the terms of the 

//' Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html)

using System;

using System.IO;

using System.Windows;

using System.Windows.Controls;
using System.Collections.Generic;


namespace LightSwitchApplication.UserCode
{

    public partial class SelectFileWindow : ChildWindow
    {

        public SelectFileWindow()
        {

            InitializeComponent();

        }


        private List<FileStream> documentStream;


        public List<FileStream> DocumentStream
        {

            get { return documentStream; }

            set { documentStream = value; }

        }


        /// <summary>

        /// OK Button

        /// </summary>

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {

            this.DialogResult = true;

        }


        /// <summary>

        /// Cancel button

        /// </summary>

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

            this.DialogResult = false;

        }


        /// <summary>

        /// Browse button

        /// </summary>

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Limit the dialog to only show ".csv" files,

            // modify this as necessary to allow other file types

            openFileDialog.Filter = "xml (*.xml)|*.xml";

            openFileDialog.FilterIndex = 1;

            openFileDialog.Multiselect = true;
            documentStream = new List<FileStream>();
            if (openFileDialog.ShowDialog() == true)
            {

                foreach (FileInfo file in openFileDialog.Files)
                {
                    this.FileTextBox.Text += file.Name + ",";

                    System.IO.FileStream myStream = file.OpenRead();

                    documentStream.Add(myStream);
                }
                this.FileTextBox.IsReadOnly = true;

            }

        }

    }

}

