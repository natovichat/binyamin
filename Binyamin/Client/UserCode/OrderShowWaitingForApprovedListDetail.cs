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
    public partial class OrderShowWaitingForApprovedListDetail
    {
        partial void sendMessageBack_Execute()
        {
            if (null == this.OrderShowWaitingForApproved.SelectedItem.Approved)
            {
                this.ShowMessageBox("לא נבחר סטטוס להזמנה (אישור/ביטול)");
                return;
            }
            if (null == this.OrderShowWaitingForApproved.SelectedItem.ChosenDate && true == this.OrderShowWaitingForApproved.SelectedItem.Approved)
            {
                this.ShowMessageBox("לא נבחר תאריך לאישור הזמנה");
                return;
            }
            else
                mailSender(this.OrderShowWaitingForApproved.SelectedItem);
            this.Refresh();
        }

        private void mailSender(Orders entity)
        {
            string strSubject = "אל עמי - הודעה בנוגע לעדכון סטטוס הזמנה";

            // dealing with approved and unapproved message
            string strMessage = "<html><body dir=\"rtl\">"; 
            strMessage = strMessage + String.Format("לכבוד {0},",
                entity.Contact.FullName) + Environment.NewLine;
            strMessage = strMessage + "<br>";

            if (true == entity.Approved)
                strMessage = strMessage +  String.Format("הזמנתך אושרה.") + Environment.NewLine + "<br>";
            else
                strMessage = strMessage + String.Format("הזמנתך בוטלה.") + Environment.NewLine + "<br>";

            strMessage = strMessage + Environment.NewLine + "<br>";

            strMessage = strMessage + String.Format("פרטי ההזמנה:") + Environment.NewLine + "<br><br>";

            string tab = "&nbsp; &nbsp; &nbsp; &nbsp;";

            strMessage = strMessage + tab + String.Format("בית הספר המזמין: {0}",
                entity.SchoolPart.FullName) + Environment.NewLine + "<br>";

            strMessage = strMessage + tab + String.Format("נושא הפעילות: {0}",
                entity.Topic.Title) + Environment.NewLine + "<br>";

            strMessage = strMessage + tab + String.Format("קהל: {0}",
               entity.Audience.Title) + Environment.NewLine + Environment.NewLine + "<br>";

            strMessage = strMessage + tab + String.Format("מספר כיתות: {0}",
               entity.OrderClassesAndCounslers.ClassesNumber) + Environment.NewLine + Environment.NewLine + "<br>";

            //strMessage = strMessage + String.Format("מספר סבבים: {0}",
            //   entity.RoundsNumber) + Environment.NewLine + Environment.NewLine + "<br><br>";

            strMessage = strMessage + tab + String.Format("מספר מדריכים: {0}",
               entity.OrderClassesAndCounslers.CounslerNumber) + Environment.NewLine + Environment.NewLine + "<br>";


            if (true == entity.Approved)
                strMessage = strMessage + tab + String.Format("התאריך שאושר: {0}",
                entity.ChosenDate) + Environment.NewLine + Environment.NewLine + "<br>";
            else
            {
                if (null != entity.DT1)
                {
                    strMessage = strMessage + tab + String.Format("התאריכים:") + Environment.NewLine + Environment.NewLine + "<br>";

                    strMessage = strMessage + tab + String.Format("{0}",
                    entity.DT1) + Environment.NewLine + Environment.NewLine + "<br>";
                }

                if (null != entity.DT2)
                {
                    strMessage = strMessage + tab + String.Format("{0}",
                    entity.DT2) + Environment.NewLine + Environment.NewLine + "<br>";
                }

                if (null != entity.DT3)
                {
                    strMessage = strMessage + tab + String.Format("{0}",
                    entity.DT3) + Environment.NewLine + Environment.NewLine + "<br>";
                }

                
            }
            strMessage = strMessage + "<br>";

            if (true == entity.Approved)
                strMessage = strMessage + String.Format("לפרטים נוספים ויצירת קשר, אנא שלח/י מייל לכתובת זו.") + Environment.NewLine;
            else
                strMessage = strMessage + String.Format("אנו מצטערים על ביטול ההזמנה, נשמח אם תיצור/תיצרי איתנו קשר באימייל זה לקבלת פרטים נוספים.") + Environment.NewLine;

            strMessage = strMessage + String.Format("בברכה,") + Environment.NewLine + "<br>";
            strMessage = strMessage + String.Format("ארגון אל-עמי") + Environment.NewLine + "<br>";

            strMessage = strMessage + "</body></html>";

            string strFrom = "yonidvirami@gmail.com";


            // Create the MailHelper class created in the Server project.
            Emails new_entry = new Emails();
            new_entry.Sender = strFrom;
            new_entry.Receiver = entity.Contact.FullName;
            new_entry.Subject = strSubject;
            new_entry.Message = strMessage;
            new_entry.ReceiverMail = entity.Email.ToString();

            this.DataWorkspace.ApplicationData.SaveChanges();

            this.ShowMessageBox("ההודעה נשלחה בהצלחה");
        }


        partial void chose_date1_Execute()
        {
            if (this.OrderShowWaitingForApproved.SelectedItem.DT1 == null)
                return;
            this.OrderShowWaitingForApproved.SelectedItem.ChosenDate = this.OrderShowWaitingForApproved.SelectedItem.DT1;
        }

        partial void chose_date2_Execute()
        {
            if (this.OrderShowWaitingForApproved.SelectedItem.DT2 == null)
                return;
            this.OrderShowWaitingForApproved.SelectedItem.ChosenDate = this.OrderShowWaitingForApproved.SelectedItem.DT2;
        }


        partial void chose_date3_Execute()
        {
            if (this.OrderShowWaitingForApproved.SelectedItem.DT3 == null)
                return;
            this.OrderShowWaitingForApproved.SelectedItem.ChosenDate = this.OrderShowWaitingForApproved.SelectedItem.DT3;
        }


    }
}
