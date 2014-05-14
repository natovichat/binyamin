using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;

using LightSwitchApplication.UserCode;

using System.Windows;

namespace LightSwitchApplication
{
    
    public partial class OrdersListDetail
    {
        //partial void sendUpdateMail_Execute()
        //{
        //    if ( null == this.Orders.SelectedItem.ChosenDate && true == this.Orders.SelectedItem.Approved )
        //       this.ShowMessageBox("לא נבחר תאריך לאישור הזמנה");
        //    else
        //        mailSender(this.Orders.SelectedItem);

        //}

        //private void mailSender(Orders entity)
        //{
        //    string strSubject = "אל עמי - הודעה בנוגע לעדכון סטטוס הזמנה";

        //    // dealing with approved and unapproved message

        //    string strMessage = String.Format("לכבוד {0},",
        //        entity.Contact.FullName) + Environment.NewLine;

        //    if ( true == entity.Approved )
        //        strMessage = strMessage + String.Format(".הזמנתך אושרה") + Environment.NewLine;
        //    else
        //        strMessage = strMessage + String.Format(".הזמנתך בוטלה") + Environment.NewLine;

        //    strMessage = strMessage + Environment.NewLine;

        //    strMessage = strMessage + String.Format("פרטי ההזמנה:") + Environment.NewLine;

        //    strMessage = strMessage + String.Format("בית הספר המזמין: {0}",
        //        entity.SchoolPart.FullName) + Environment.NewLine;

        //    strMessage = strMessage + String.Format("נושא הפעילות: {0}",
        //        entity.Topic.Title) + Environment.NewLine + Environment.NewLine;

        //    strMessage = strMessage + String.Format("קהל: {0}",
        //       entity.Audience.Title) + Environment.NewLine + Environment.NewLine;

        //    strMessage = strMessage + String.Format("מספר כיתות: {0}",
        //       entity.ClassNumber) + Environment.NewLine + Environment.NewLine;

        //    strMessage = strMessage + String.Format("מספר סבבים: {0}",
        //       entity.RoundsNumber) + Environment.NewLine + Environment.NewLine;

        //    strMessage = strMessage + String.Format("מספר מדריכים: {0}",
        //       entity.CounselorsNumber) + Environment.NewLine + Environment.NewLine;


        //    if (true == entity.Approved)
        //        strMessage = strMessage + String.Format("התאריך שאושר: {0}",
        //        entity.ChosenDate) + Environment.NewLine + Environment.NewLine;
        //    else
        //    {
        //        strMessage = strMessage + String.Format("התאריכים: {0}",
        //         entity.DT1) + Environment.NewLine + Environment.NewLine;

        //        if (null != entity.DT2)
        //        {
        //            strMessage = strMessage + String.Format("{0}",
        //            entity.DT2) + Environment.NewLine + Environment.NewLine;
        //        }
        //        if (null != entity.DT3)
        //        {
        //            strMessage = strMessage + String.Format("{0}",
        //            entity.DT3) + Environment.NewLine + Environment.NewLine;
        //        }
        //    }
            
           
        //    if ( true == entity.Approved )
        //        strMessage = strMessage + String.Format("לפרטים נוספים ויצירת קשר, אנא שלח/י מייל לכתובת זו.") + Environment.NewLine;
        //    else
        //        strMessage = strMessage + String.Format("אנו מצטערים על ביטול ההזמנה, נשמח אם תיצור/תיצרי איתנו קשר באימייל זה לקבלת פרטים נוספים.") + Environment.NewLine;

        //    strMessage = strMessage + String.Format("בברכה,") + Environment.NewLine;
        //    strMessage = strMessage + String.Format("ארגון אל-עמי") + Environment.NewLine;

        //    string strFrom = "yonidvirami@gmail.com";


        //    // Create the MailHelper class created in the Server project.
        //    Emails new_entry = new Emails();
        //    new_entry.Sender = strFrom;
        //    new_entry.Receiver = entity.Contact.FullName;
        //    new_entry.Subject = strSubject;
        //    new_entry.Message = strMessage;
        //    new_entry.ReceiverMail = entity.Email.ToString();

        //    this.DataWorkspace.ApplicationData.SaveChanges();
        //}

        //partial void ChoseDate1_Execute()
        //{
        //    this.Orders.SelectedItem.ChosenDate = this.Orders.SelectedItem.DT1;

        //}

        //partial void ChooseDate2_Execute()
        //{
        //    this.Orders.SelectedItem.ChosenDate = this.Orders.SelectedItem.DT2;
        //}


        //partial void ChooseDate3_Execute()
        //{
        //    this.Orders.SelectedItem.ChosenDate = this.Orders.SelectedItem.DT3;
        //}

        partial void sendUpdateMail_Execute()
        {
            // Write your code here.

        }
    }
}
