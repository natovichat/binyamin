using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace LightSwitchApplication.UserCode
{
    public partial class Input : ChildWindow
    {
        public Input()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public string InputCapture
        {
            set
            {
                this.inputCapture.Text= value;
            }
            get
            {
                return inputCapture.Text;
            }
        }
        public string Value
        {
            get
            {
                return Output.Text;
            }
        }

    }
}

