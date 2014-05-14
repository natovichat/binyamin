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

        public class FileWindowArgument
        {
            public string FileFilter { get; set; }
            public FileAccess FileAccess { get; set; }
            public string FileName { get; set; }
            public bool Multiselect { get; set; }

        }
        protected FileWindowArgument Argument { get; set; }
        public SelectFileWindow(FileWindowArgument argument)
        {
            Argument = argument;
            InitializeComponent();

        }


        private List<Stream> documentStream;


        public List<Stream> DocumentStream
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

            if (Argument.FileAccess == FileAccess.Read)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = Argument.FileFilter; //"xml (*.xml)|*.xml";

                openFileDialog.FilterIndex = 1;

                openFileDialog.Multiselect = Argument.Multiselect;

                documentStream = new List<Stream>();
                if (openFileDialog.ShowDialog() == true)
                {

                    foreach (FileInfo file in openFileDialog.Files)
                    {
                        this.FileTextBox.Text += file.Name + ",";

                        System.IO.FileStream myStream = null;

                        if (Argument.FileAccess == FileAccess.Read)
                        {
                            myStream = file.OpenRead();
                        }
                        else if (Argument.FileAccess == FileAccess.Write || Argument.FileAccess == FileAccess.ReadWrite)
                        {
                            myStream = file.OpenWrite();
                        }

                        documentStream.Add(myStream);
                    }
                    this.FileTextBox.IsReadOnly = true;

                }
            }
            else
            {
                SaveFileDialog openFileDialog = new SaveFileDialog();

                openFileDialog.Filter = Argument.FileFilter; //"xml (*.xml)|*.xml";

                openFileDialog.FilterIndex = 1;

                documentStream = new List<Stream>();

                if (openFileDialog.ShowDialog() == true)
                {
                        this.FileTextBox.Text += openFileDialog.SafeFileName; 
                    
                        System.IO.Stream myStream = null;

                        myStream = openFileDialog.OpenFile();
                        
                        documentStream.Add(myStream);
                    }
                    this.FileTextBox.IsReadOnly = true;

            }


        }

    }

}

