using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using System.Windows.Controls;

namespace LightSwitchApplication
{
    public partial class PartnersGroupsListDetail
    {
        partial void SendMail_Execute()
        {
            this.FindControl("SendingMessage").ControlAvailable += PostsListDetail_ControlAvailable;
            
            Email = new Emails();
            Email.ReceiverMail = this.Partner.SelectedItem.Email;
            Email.Receiver = this.Partner.SelectedItem.FirstName + " " + this.Partner.SelectedItem.LastName;
            Email.Sender = "me";
            //Email.Subject = "";
            //Email.Message = "";
            this.OpenModalWindow("SendingMessage");

        }

        void PostsListDetail_ControlAvailable(object sender, ControlAvailableEventArgs e)
        {
            ChildWindow window = (ChildWindow)e.Control;
            window.Closed += new EventHandler(MessageNew_Closed);
        }
      
        void MessageNew_Closed(object sender, EventArgs e)
        {
            ChildWindow window = (ChildWindow)sender;
            if (!(window.DialogResult.HasValue))
            {
                this.Details.Dispatcher.BeginInvoke(() =>
                {
                    Email.Delete();
                });

                Email.Details.DiscardChanges();
                this.CloseModalWindow("SendingMessage");
                //// Remove unsaved records           
                //foreach (Message message in this.DataWorkspace.ApplicationData.Details.GetChanges()
                //    .AddedEntities.OfType<message>())
                //{
                //    message.Details.DiscardChanges();
                //}
            }
        }


        partial void SendEmail_Execute()
        {
            //Emails new_entry = new Emails();
            //new_entry.Sender = Email.Sender;
            //new_entry.Receiver = Email.Receiver;
            //new_entry.Subject = Email.Subject;
            //new_entry.Message = Email.Message;
            //new_entry.ReceiverMail = Email.ReceiverMail;

            if (Email.Subject == null || Email.Subject.Equals(""))
            {
                System.Windows.MessageBoxResult result_subject = this.ShowMessageBox("לא צויין נושא ההודעה: "
                                                                + "\n"
                                                                + "האם אתה רוצה לשלוח את ההודעה ללא הנושא?",
                                                                "חריגה בשליחת הודעה", MessageBoxOption.YesNo);
                if (result_subject.Equals(System.Windows.MessageBoxResult.Yes))
                {
                    Email.Subject = "";
                }
                else
                {
                    return;
                }
            }

            if (Email.Message == null || Email.Message.Equals(""))
            {
                System.Windows.MessageBoxResult result_message = this.ShowMessageBox("לא צויין גוף ההודעה: "
                                                                + "\n"
                                                                + "האם אתה רוצה לשלוח את ההודעה ללא תוכן?",
                                                                "חריגה בשליחת הודעה", MessageBoxOption.YesNo);
                if (result_message.Equals(System.Windows.MessageBoxResult.Yes))
                {
                    Email.Message = "";
                }
                else
                {
                    return;
                }
            }

            this.DataWorkspace.ApplicationData.SaveChanges();
            this.ShowMessageBox("ההודעה נשלחה בהצלחה");
            this.CloseModalWindow("SendingMessage");
        }

        partial void SendMails_Execute()
        {
            this.FindControl("SendingMessage").ControlAvailable += PostsListDetail_ControlAvailable;

            Email = new Emails();

            Boolean isEmpty = true;
            string receiver = "", receiverMail = "";
            foreach (Partners friend in this.PartnersGroups.SelectedItem.Partner)
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

            Email.Receiver = receiver;
            Email.ReceiverMail = receiverMail;
            Email.Sender = "me";
            this.OpenModalWindow("SendingMessage");

        }
    }
}
