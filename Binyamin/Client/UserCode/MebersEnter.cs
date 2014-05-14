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
    public partial class MebersEnter
    {
        partial void EnterMembersMenu_Execute()
        {
            if (this.IsConnected == false && this.Id == -1)
            {
                int num = 0, id;
                bool flag = false, emptyflag = false;

                Counselor cons = null;
                //  cons = this.DataWorkspace.ApplicationData.CounselorByUserPass(this.AuthenticationDetailProperty.UserName.ToString(), this.AuthenticationDetailProperty.Password.ToString()).First<Counselor>();
                if (this.Password == null || this.UserName == null)
                {
                    emptyflag = true;
                }
                if (emptyflag == false)
                {
                    foreach (Counselor cons1 in this.DataWorkspace.ApplicationData.CounselorByUserPass(this.UserName.ToString(), this.Password.ToString()))
                    {
                        num++;
                    }
                    if (num > 0)
                    {
                        cons = this.DataWorkspace.ApplicationData.CounselorByUserPass(this.UserName.ToString(), this.Password.ToString()).First<Counselor>();
                        this.Id = cons.Id;
                        id = cons.Id;
                        this.DistrictId = cons.District.Id;
                        flag = true;
                    }
                    else
                    {
                        this.ShowMessageBox("שם משתמש ו/או ססמא לא נכונים","בעיית התחברות",MessageBoxOption.Ok);
                        flag = false;

                    }

                    if (flag != false)
                    {
                        this.IsConnected = true;
                        this.ShowMessageBox("התחברות בוצעה!","ברוכים הבאים",MessageBoxOption.Ok);
                        this.CounselorDetails = "מחובר כ: " + cons.FullName;
                        //  this.Application.ShowPastActivitysOfCounselor(this.Id);
                    }
                }
                else
                {
                    this.ShowMessageBox("נא הזן שם משתמש וססמא", "בעיית התחברות", MessageBoxOption.Ok);
                }
            }

        }

        partial void OpenActivityCounselorPropose_Execute()
        {
            if (this.IsConnected == true)
            {
                this.Application.ShowActivityCounselorPropose(this.Id, this.DistrictId);
            }

        }

        partial void OpenPastActivitysOfCounselor_Execute()
        {
            if (this.IsConnected == true)
            {
              this.Application.ShowPastActivitysOfCounselor(this.Id);
            }

        }

        partial void OpenFutureActivitysOfCounselor_Execute()
        {
            if (this.IsConnected == true)
            {
                this.Application.ShowFutureActivitysOfCounselor(this.Id);
            }
        }

        partial void ExitMembersMenu_Execute()
        {
            if (this.IsConnected == true)
            {
                this.IsConnected = false;
                this.UserName = null;
                this.Password = null;
                this.CounselorDetails = "";
                this.IsConnected = false;
                this.ShowMessageBox("התנתקת בהצלחה!","להתראות",MessageBoxOption.Ok);
                this.Id = -1;
                
            }
            

        }

        partial void MebersEnter_Activated()
        {
            //this.IsConnected = false;

        }

        partial void MebersEnter_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            this.Id = -1;
            this.ImageLogo= GetImageFromAssembly("top_n.jpg");

        }


        ////////////////////
      

        //this.FindControl("contact_info").ControlAvailable += contactLostFocus;
        

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
