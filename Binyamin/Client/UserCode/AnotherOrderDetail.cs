using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;

namespace LightSwitchApplication
{
    public partial class AnotherOrderDetail
    {

        partial void AnotherOrderDetail_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            this.screen_logo = GetImageFromAssembly("top_n.jpg");
            this.welcome_label = "ברוכים הבאים למערכת ההזמנות!";
        }

        private byte[] GetImageFromAssembly(string fileName) 
        { 
            
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

            String[] str = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
            
            fileName = "LightSwitchApplication.Resources." + fileName;
            Stream stream = assembly.GetManifestResourceStream(fileName);
            int streamLength = Convert.ToInt32(stream.Length);
            byte[] fileData = new byte[streamLength];
            stream.Read(fileData, 0, streamLength);
            stream.Close();
            return fileData;
        }
    }
}
