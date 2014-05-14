using System;

using System.Windows;
using System.Windows.Controls;

using System.Reflection;
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
    public partial class PartnersReportListDetail
    {
        partial void PartnersReportListDetail_Saving(ref bool handled)
        {
            // Write your code here.

        }


        partial void saveChanges_Execute()
        {
            PartnersReport entity = this.PartnersReport.SelectedItem;
            foreach (var pra in entity.PartnersReportActivities)
            {
                // pra.Delete();
                pra.Activity.PartnerReportActivities.Remove(pra);
            }
            entity.PartnersReportActivities.ToList().ForEach(entity1 => entity.PartnersReportActivities.Remove(entity1));
            //entity.PartnersReportActivities.ToList().ForEach(entity1 => entity1.Delete());
            foreach (PartnerReportActivities pra in this.DataWorkspace.ApplicationData.PartnersReportActivities)
            {
                if (pra.PartnersReport == null)
                    pra.Delete();
                //pra.Activity.PartnerReportActivities.Remove(pra);

            }
            this.DataWorkspace.ApplicationData.SaveChanges();

            foreach (Activity act in this.DataWorkspace.ApplicationData.Activities)
            {
                if (act == null || act.SchoolPart == null)
                {
                    continue;
                }
                if (act.SchoolPart.Id == entity.SchoolPart.Id)
                {
                    if (act.StartTime.Date >= entity.StartDate && act.StartTime.Date <= entity.EndDate)
                    {
                        //Perform some task on the customer entity.
                        PartnerReportActivities x = new PartnerReportActivities();
                        x.Activity = act;
                        x.PartnersReport = entity;
                        entity.PartnersReportActivities.Add(x);
                    }
                }
            }

            this.DataWorkspace.ApplicationData.SaveChanges();

        }

        partial void ExportToMail_Execute()
        {
            if (this.PartnersReport.SelectedItem == null)
            {
                this.ShowMessageBox("אנא שמור לפני ביצוע השליחה");
                return;
            }

            string strSubject = "אל עמי - הודעה בנוגע לעדכון סטטוס הזמנה";
            string strFrom = "yonidvirami@gmail.com";

            string strMessage = createMessageBody();

            string receiver = "", receiverMail = "";
           

            Boolean isEmpty = true;
            if (this.PartnersReport.SelectedItem.PartnersGroups.Partner == null)
            {
                this.ShowMessageBox("אנא בחר קבוצת נמענים");
                return;
            }

            foreach (Partners friend in this.PartnersReport.SelectedItem.PartnersGroups.Partner)
            {
                if (friend.Email == null || friend.Email == "" || !friend.Email.Contains('@'))
                    continue;

                isEmpty = false;
                receiver += friend.FirstName + " " + friend.LastName + ", ";
                receiverMail += friend.Email + ", ";
            }

            if (isEmpty)
            {
                this.ShowMessageBox("אין נמענים בקבוצה");
                return;
            }
            receiver = receiver.Substring(0, receiver.Length - 2);
            receiverMail = receiverMail.Substring(0, receiverMail.Length - 2);

            sendMail(strFrom, receiver, strSubject, strMessage, receiverMail);
            
            // Create the MailHelper class created in the Server project.
            //Emails new_entry = new Emails();
            //new_entry.Sender = strFrom;
            //new_entry.Receiver = "dvir";
            //new_entry.Subject = strSubject;
            //new_entry.Message = strMessage;
            //new_entry.ReceiverMail = "yonigins@gmail.com";
            this.DataWorkspace.ApplicationData.SaveChanges();

            this.ShowMessageBox("ההודעה נשלחה בהצלחה");
        }

        partial void SendForTest_Execute()
        {
            if (this.PartnersReport.SelectedItem == null)
            {
                this.ShowMessageBox("אנא שמור לפני ביצוע השליחה");
                return;
            }

            string strSubject = "אל עמי - הודעה בנוגע לעדכון סטטוס הזמנה";
            string strFrom = "yonidvirami@gmail.com";

            string strMessage = createMessageBody();

            sendMail(strFrom, "me", strSubject, strMessage, getMyOwnMail());
           
            // Create the MailHelper class created in the Server project.
            //Emails new_entry = new Emails();
            //new_entry.Sender = strFrom;
            //new_entry.Receiver = "dvir";
            //new_entry.Subject = strSubject;
            //new_entry.Message = strMessage;
            //new_entry.ReceiverMail = "yonigins@gmail.com";

            this.DataWorkspace.ApplicationData.SaveChanges();

            this.ShowMessageBox("ההודעה נשלחה בהצלחה");

        }

        public string getMyOwnMail()
        {
            UserPrefs latest = null;
            foreach (UserPrefs prefs in this.DataWorkspace.ApplicationData.UserPrefs)
            {
                if (null == prefs)
                {
                    continue;
                }

                if (latest == null)
                    latest = prefs;
                else if (latest.CreatedDate < prefs.CreatedDate)
                    latest = prefs;
            }

            if (latest == null)
            {
                this.ShowMessageBox("לא הוגדרו פרטים אישיים במערכת. אנא הגדר בלשונית המתאימה");
            }
            else if (latest.MailAddress == null)
            {
                this.ShowMessageBox("לא הוגדרה כתובת מייל כראוי. אנא הגדר בלשונית המתאימה");
            }
            else if (latest.Password == null)
            {
                this.ShowMessageBox("לא הוגדרה סיסמת מייל כראוי. אנא הגדר בלשונית המתאימה");
            }
            else if (latest.DisaplayName == null)
            {
                this.ShowMessageBox("לא הוגדר שם תצוגת המייל כראוי. אנא הגדר בלשונית המתאימה");
            }

            return latest.MailAddress;

        }

        public void sendMail(string sender, string receiver, string strSubject, string strMessage, string receiverMail)
        {
            // Create the MailHelper class created in the Server project.
            Emails new_entry = new Emails();
            new_entry.Sender = sender;
            new_entry.Receiver = receiver;
            new_entry.Subject = strSubject;
            new_entry.Message = strMessage;
            new_entry.ReceiverMail = receiverMail;
           
        }


        public string createMessageBody()
        {
            
            PartnersReport report = this.PartnersReport.SelectedItem;
            int defaultWidth = 400, defaultHeigth = 250;
 
            int classes_number = 0;
            string audience = "";
            string all_activites = "<table align=\"center\" border=\"0\" cellspacing=\"3\" cellpadding=\"20\" bgcolor=\"#9999FF\">"
                + "<tr bgcolor=\"#AAAAFF\" align=\"center\"> <td><b>" + "נושא הפעילות" + "</b></td> <td><b>" + "תאריך" + "</b></td> <td><b>" + "קהל" + "</b></td> </tr>";

            int size = report.PartnersReportActivities.Count();
            foreach (var activity in report.PartnersReportActivities)
            {
                all_activites = all_activites + "<tr bgcolor=\"#CACAFF\" align=\"center\">";

                all_activites = all_activites + "<td>" + activity.Activity.FullTopicTitle + "</td>  <td>" + activity.Activity.StartTime + "</td>  <td>" +
                    activity.Activity.Audience + "'</td>";
                if (!audience.Contains("*" + activity.Activity.Audience.ToString() + "*"))
                {
                    classes_number = classes_number + activity.Activity.TotalNumberOfClasses;
                    audience = audience + "*" + activity.Activity.Audience.ToString() + "*";
                }

                all_activites = all_activites + "</tr>";
            }

            all_activites = all_activites + "</table>";


            string strMessage = "<html><body dir=\"rtl\" bgcolor=\"#E8E8E8\">";

            int currentWidth, currentHeigth;

            if (null != report.Img1 && !report.Img1.Equals(""))
            {
                currentHeigth = defaultHeigth;
                currentWidth = defaultWidth;
                if (null != report.HeightImg1 && report.HeightImg1 > 0)
                    currentHeigth = (int) report.HeightImg1;

                if (null != report.WidthImg1 && report.WidthImg1 > 0)
                    currentWidth = (int) report.WidthImg1;

                strMessage = strMessage + "<p align=\"center\"><img src=\"" + report.Img1 + "\" align=\"center\" width=\"" + currentWidth + "\" height=\"" + currentHeigth +"\"></p>";
            }

            strMessage = strMessage + "<p align=\"center\"><u><b><font face=\"verdana\" size=\"5\" color=\"#59955C\" >" + report.Title + "</font> </b></u></p>";

            if (null != report.Img2 && !report.Img2.Equals(""))
            {
                currentHeigth = defaultHeigth;
                currentWidth = defaultWidth;
                if (null != report.HeightImg2 && report.HeightImg2 > 0)
                    currentHeigth = (int)report.HeightImg2;

                if (null != report.WidthImg2 && report.WidthImg2 > 0)
                    currentWidth = (int)report.WidthImg2;

                strMessage = strMessage + "<p align=\"center\"><img src=\"" + report.Img2 + "\" align=\"center\" width=\"" + currentWidth + "\" height=\"" + currentHeigth + "\"></p>";
            }

            strMessage = strMessage + "<p align=\"center\"><b><font face=\"verdana\" size=\"4\" color=\"#FF2626\" >" + "בית ספר : " + "</font>";
            strMessage = strMessage + "<font face=\"verdana\" size=\"4\" color=\"#FF4848\" >" + report.SchoolPart + "</b></p>";

            if (null != report.Img3 && !report.Img3.Equals(""))
            {
                currentHeigth = defaultHeigth;
                currentWidth = defaultWidth;
                if (null != report.HeightImg3 && report.HeightImg3 > 0)
                    currentHeigth = (int)report.HeightImg3;

                if (null != report.WidthImg3 && report.WidthImg3 > 0)
                    currentWidth = (int)report.WidthImg3;

                strMessage = strMessage + "<p align=\"center\"><img src=\"" + report.Img3 + "\" align=\"center\" width=\"" + currentWidth + "\" height=\"" + currentHeigth + "\"></p>";
            }

            strMessage = strMessage + "<p align=\"center\"><font face=\"verdana\" size=\"4\" color=\"#404040\" >" + " בין התאריכים";
            strMessage = strMessage + report.StartDate.ToShortDateString();
            strMessage = strMessage + " ו- ";
            strMessage = strMessage + report.EndDate.ToShortDateString() + " ";
            strMessage = strMessage + "השתתפו כ-";
            strMessage = strMessage + classes_number.ToString();
            strMessage = strMessage + " כיתות המונות כ-";
            strMessage = strMessage + (classes_number * 30).ToString();
            strMessage = strMessage + " תלמידים בפעילויות הבאות:" + "</font></p>";
            strMessage = strMessage + all_activites;

            if (null != report.Img4 && !report.Img4.Equals(""))
            {
                currentHeigth = defaultHeigth;
                currentWidth = defaultWidth;
                if (null != report.HeightImg4 && report.HeightImg4 > 0)
                    currentHeigth = (int)report.HeightImg4;

                if (null != report.WidthImg4 && report.WidthImg4 > 0)
                    currentWidth = (int)report.WidthImg4;

                strMessage = strMessage + "<p align=\"center\"><img src=\"" + report.Img4 + "\" align=\"center\" width=\"" + currentWidth + "\" height=\"" + currentHeigth + "\"></p>";
            }

            strMessage = strMessage + "<p align=\"center\"><font face=\"verdana\" size=\"4\" color=\"#404040\" >" + report.Text + "</font></p>";

            if (null != report.Img5 && !report.Img5.Equals(""))
            {
                currentHeigth = defaultHeigth;
                currentWidth = defaultWidth;
                if (null != report.HeightImg5 && report.HeightImg5 > 0)
                    currentHeigth = (int)report.HeightImg5;

                if (null != report.WidthImg5 && report.WidthImg5 > 0)
                    currentWidth = (int)report.WidthImg5;

                strMessage = strMessage + "<p align=\"center\"><img src=\"" + report.Img5 + "\" align=\"center\" width=\"" + currentWidth + "\" height=\"" + currentHeigth + "\"></p>";
            }


            // add number of classes and activites and multiply by 30.
            strMessage = strMessage + "</body></html>";
            return strMessage;
        }


        

    }
}
