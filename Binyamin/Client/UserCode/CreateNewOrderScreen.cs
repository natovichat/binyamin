using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using System.Windows.Browser;



namespace LightSwitchApplication
{
    public partial class CreateNewOrderScreen
    {

        partial void CreateNewOrderScreen_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            this.screen_logo = GetImageFromAssembly("top_n.jpg");
            this.welcome_label = "ברוכים הבאים למערכת ההזמנות!";

            //this.FindControl("contact_info").ControlAvailable += contactLostFocus;
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

        partial void make_order_Execute()
        {

            if (this.audience==null || this.school_part ==null || this.order_properties_wrapper == null || this.order_properties == null || ( this.contact_info==null && this.contact_name == null) ||
                /* this.first_date == null || this.email == null || this.phone_number == null || this.rounds_number <= 0 || this.Prices == null*/ this.Topic == null)
            {
                    this.ShowMessageBox("חסרים פרטי ההזמנה, אנא השלם ונסה שוב.");
                    return;
            }

            Orders new_order = new Orders();

            new_order.Audience = this.audience;           
            new_order.Comment = this.comment;

            Boolean flag = false;
            if (null == this.contact_name)
                new_order.Contact = this.contact_info;
            else if (null == this.contact_info)
            {
                new_order.Contact = new Contact();
                new_order.Contact.FullName = this.contact_name;
                flag = true;
            }

            new_order.DT1 = this.first_date;
            new_order.DT2 = this.second_date;
            new_order.DT3 = this.third_date;
            new_order.Email = this.email;
            new_order.Phone = this.phone_number;
            new_order.RoundsNumber = this.rounds_number;
            new_order.SchoolPart = this.school_part;
            new_order.Topic = this.Topic;

            new_order.OrderClassesAndCounslers = new OrderClassesAndCounslers();
            new_order.OrderClassesAndCounslers.Price = this.order_properties.Price;
            new_order.OrderClassesAndCounslers.ClassesNumber = this.order_properties_wrapper.ClassesAmount;
            new_order.OrderClassesAndCounslers.CounslerNumber = this.order_properties.CounslersAmount;
            
            //this.DataWorkspace.ApplicationData.SaveChanges();
           

            // send mail in order to inform the manager about the new order
            createMail(new_order);
            
            // send mail to the client with the order properties in case there is an email address.
            if ( this.email != null && this.email != "" )
                createMailClient(new_order);

            if (flag)
            {
                new_order.Contact.Delete();
            }

            this.DataWorkspace.ApplicationData.SaveChanges();
            this.ShowMessageBox("ההזמנה בוצעה בהצלחה. אישור יתקבל במייל." + "\n" + " במידה ולא נוצר קשר, אנא פנה אלינו שנית דרך המערכת או התקשר אלינו.");
            // cleaning the screen
          
            this.school_part = new_order.SchoolPart;
            this.audience = new_order.Audience;
            this.contact_info = new_order.Contact;
            this.email = new_order.Email;
            this.phone_number = new_order.Phone;


            this.rounds_number = 0;
            //this.first_date = System.DateTime.Now;
            this.second_date = null;
            this.third_date = null;
            this.Topic = null;
            this.comment = null;
            this.order_properties_wrapper = null;
            this.order_properties = null;
            this.general_topic = null;


        }

        private void createMail(Orders new_order)
        {
            string strSubject = "התקבלה הזמנה חדשה - אל-עמי";

            string strMessage = "<html><body dir=\"rtl\">";
            strMessage = strMessage + String.Format("התקבלה הזמנה חדשה") + Environment.NewLine + Environment.NewLine + "<br><br>";
            strMessage = strMessage + String.Format("פרטי ההזמנה:") + Environment.NewLine + "<br><br>";

            string tab = "&nbsp; &nbsp; &nbsp; &nbsp;";

            strMessage = strMessage + tab + String.Format("בית הספר המזמין: {0}",
                new_order.SchoolPart.FullName) + Environment.NewLine + "<br>";

            strMessage = strMessage + tab + String.Format("נושא הפעילות: {0}",
                new_order.Topic.Title) + Environment.NewLine + Environment.NewLine + "<br>";

            strMessage = strMessage + tab + String.Format("קהל: {0}",
               new_order.Audience.Title) + Environment.NewLine + Environment.NewLine + "<br>";

            strMessage = strMessage + tab + String.Format("מספר כיתות: {0}",
               new_order.OrderClassesAndCounslers.ClassesNumber) + Environment.NewLine + Environment.NewLine + "<br>";

            //strMessage = strMessage + String.Format("מספר סבבים: {0}",
            //   new_order.RoundsNumber) + Environment.NewLine + Environment.NewLine + "<br><br>";

            strMessage = strMessage + tab + String.Format("מספר מדריכים: {0}",
               new_order.OrderClassesAndCounslers.CounslerNumber) + Environment.NewLine + Environment.NewLine + "<br>";

            strMessage = strMessage + tab + String.Format("מחיר: {0}",
               new_order.OrderClassesAndCounslers.Price) + Environment.NewLine + Environment.NewLine + "<br>";

           
            if (null != new_order.DT1)
            {
                strMessage = strMessage + tab + String.Format("התאריכים:") + Environment.NewLine + Environment.NewLine + "<br>";

                strMessage = strMessage + tab + String.Format("{0}",
                new_order.DT1) + Environment.NewLine + Environment.NewLine + "<br>";
            }

            if (null != new_order.DT2)
            {
                strMessage = strMessage + tab + String.Format("{0}",
                new_order.DT2) + Environment.NewLine + Environment.NewLine + "<br>";
            }
       
            if (null != new_order.DT3)
            {
                strMessage = strMessage + tab + String.Format("{0}",
                new_order.DT3) + Environment.NewLine + Environment.NewLine + "<br>";
            }
                
            strMessage = strMessage +"<br>";

            strMessage = strMessage + String.Format("פרטי המזמין:") + Environment.NewLine + "<br><br>";

            strMessage = strMessage + tab + String.Format("{0}",
                new_order.Contact) + Environment.NewLine + Environment.NewLine + "<br>";

            if (null != new_order.Email && !new_order.Email.Equals(""))
            {
                strMessage = strMessage + tab + String.Format("אימייל: {0}",
                   new_order.Email) + Environment.NewLine + Environment.NewLine + "<br>";
            }

            if (null != new_order.Phone && !new_order.Phone.Equals(""))
            {
                strMessage = strMessage + tab + String.Format("מספר טלפון: {0}",
                   new_order.Phone) + Environment.NewLine + Environment.NewLine + "<br>";
            }
            strMessage = strMessage + "<br>";

            string website = "http://binyamin.info/binyamin";
            strMessage = strMessage + String.Format("על-מנת לטפל בהזמנה כנס ל- {0}", website) + Environment.NewLine + "<br>";
            strMessage = strMessage + "</body></html>";

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
                return;
            }
            else if (latest.MailAddress == null || latest.Password == null || latest.DisaplayName == null)
            {
                return;
            }

            // Create the MailHelper class created in the Server project.
            Emails new_entry = new Emails();
            new_entry.Sender = latest.MailAddress;
            new_entry.Receiver = latest.DisaplayName;
            new_entry.Subject = strSubject;
            new_entry.Message = strMessage;
            new_entry.ReceiverMail = latest.MailAddress;

            
        }

        private void createMailClient(Orders new_order)
        {
            string strSubject = "פרטי הזמנה שבוצעה - אל-עמי";

            string strMessage = "<html><body dir=\"rtl\">";
            strMessage = strMessage + String.Format("פרטי ההזמנה:") + Environment.NewLine + "<br><br>";

            string tab = "&nbsp; &nbsp; &nbsp; &nbsp;";

            strMessage = strMessage + tab + String.Format("בית הספר המזמין: {0}",
                new_order.SchoolPart.FullName) + Environment.NewLine + "<br>";

            strMessage = strMessage + tab + String.Format("נושא הפעילות: {0}",
                new_order.Topic.Title) + Environment.NewLine + Environment.NewLine + "<br>";

            strMessage = strMessage + tab + String.Format("קהל: {0}",
               new_order.Audience.Title) + Environment.NewLine + Environment.NewLine + "<br>";

            strMessage = strMessage + tab + String.Format("מספר כיתות: {0}",
               new_order.OrderClassesAndCounslers.ClassesNumber) + Environment.NewLine + Environment.NewLine + "<br>";

            //strMessage = strMessage + String.Format("מספר סבבים: {0}",
            //   new_order.RoundsNumber) + Environment.NewLine + Environment.NewLine + "<br><br>";

            strMessage = strMessage + tab + String.Format("מספר מדריכים: {0}",
               new_order.OrderClassesAndCounslers.CounslerNumber) + Environment.NewLine + Environment.NewLine + "<br>";

            strMessage = strMessage + tab + String.Format("מחיר: {0}",
               new_order.OrderClassesAndCounslers.Price) + Environment.NewLine + Environment.NewLine + "<br>";

            if (null != new_order.DT1)
            {
                strMessage = strMessage + tab + String.Format("התאריכים:") + Environment.NewLine + Environment.NewLine + "<br>";

                strMessage = strMessage + tab + String.Format("{0}",
                new_order.DT1) + Environment.NewLine + Environment.NewLine + "<br>";
            }

            if (null != new_order.DT2)
            {
                strMessage = strMessage + tab + String.Format("{0}",
                new_order.DT2) + Environment.NewLine + Environment.NewLine + "<br>";
            }

            if (null != new_order.DT3)
            {
                strMessage = strMessage + tab + String.Format("{0}",
                new_order.DT3) + Environment.NewLine + Environment.NewLine + "<br>";
            }
            strMessage = strMessage + "<br>";


            strMessage = strMessage + String.Format("ההזמנה בוצעה בהצלחה." + "\n" + " במידה ולא נוצר קשר, אנא פנה אלינו שנית דרך המערכת או התקשר אלינו.") + Environment.NewLine + "<br>";
            strMessage = strMessage + "</body></html>";

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
                return;
            }
            else if (latest.MailAddress == null || latest.Password == null || latest.DisaplayName == null)
            {
                return;
            }

            string receiver_name = new_order.Contact.FullName;
            if (null == receiver_name || receiver_name.Equals(""))
                receiver_name = new_order.Email;

            // Create the MailHelper class created in the Server project.
            Emails new_entry = new Emails();
            new_entry.Sender = latest.MailAddress;
            new_entry.Receiver = receiver_name;
            new_entry.Subject = strSubject;
            new_entry.Message = strMessage;
            new_entry.ReceiverMail = new_order.Email;


        }

        partial void contact_info_Changed()
        {
            if (null == this.contact_info)
                return;

            if (this.contact_name != null )
                this.contact_name = null;         

            foreach (Contact current_contact in this.DataWorkspace.ApplicationData.Contacts)
            {
                
                if (this.contact_info.Id.Equals(current_contact.Id))
                {
                    this.email = "";
                    this.phone_number = "";
                    if (this.contact_info.Email != null)
                        this.email = this.contact_info.Email;
                    if (this.contact_info.PhoneNumber1 != null)
                        this.phone_number = this.contact_info.PhoneNumber1;
                    else if (this.contact_info.PhoneNumber2 != null)
                        this.phone_number = this.contact_info.PhoneNumber2;

                }

            }
        }

        //private bool contactChanged()
        //{
        //    this.email = "";
        //    this.phone_number = "";
        //    foreach (Contact current_contact in this.DataWorkspace.ApplicationData.Contacts)
        //    {
        //        if (null == this.contact_info)
        //            return false;

        //        if (current_contact.SchoolPart == this.school_part && gTxtComboText.Equals(current_contact.FullName))
        //        {
        //            if (this.contact_info.Email != null)
        //                this.email = this.contact_info.Email;
        //            if (this.contact_info.PhoneNumber1 != null)
        //                this.phone_number = this.contact_info.PhoneNumber1;
        //            else if (this.contact_info.PhoneNumber2 != null)
        //                this.phone_number = this.contact_info.PhoneNumber2;

        //            return true;
        //        }


        //    }
        //    return false;

        //}

        //private void contactLostFocus(object sender, ControlAvailableEventArgs e)
        //{
        //    ((System.Windows.Controls.Control)e.Control).LostFocus += contactFieldChanged;
        //}


        //private void contactFieldChanged(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    //Add a reference to System.Windows.Controls.Input.dll in the Client project
        //    gTxtComboText = ((System.Windows.Controls.AutoCompleteBox)sender).Text;
        //    this.Details.Dispatcher.BeginInvoke(() =>
        //    {

        //        if (!contactChanged())
        //        {

        //            Contact new_contact = this.DataWorkspace.ApplicationData.Contacts.AddNew();
        //            new_contact.FullName = gTxtComboText;
        //            new_contact.SchoolPart = this.school_part;
        //            this.contact_info = new_contact;
        //            this.contact_info.Delete();
        //        }
        //    });

        //}

        partial void school_part_Changed()
        {
            this.contact_info = null;
            this.email = "";
            this.phone_number = "";
        }

        partial void website_link_Execute()
        {
            Microsoft.LightSwitch.Threading.Dispatchers.Main.BeginInvoke(() =>
            {
                string url = "http://www.elami-elatzmi.co.il/el.asp";
                HtmlPage.Window.Navigate(new Uri(url), "_blank");
            });

        }

        partial void general_topic_Changed()
        {
            this.Topic = null;
        }

        partial void order_properties_wrapper_Changed()
        {
            this.order_properties = null;
        }

        partial void contact_name_Changed()
        {
            if (null == this.contact_name)
                return;

            this.phone_number = "";
            this.email = "";
            this.contact_info = null;
            
        }


       
        //private Boolean compareOrders(Orders order1, Orders order2)
        //{
        //    if ( null == order1.Audience || null == order2.Audience || order1.Audience == order2.Audience)
        //        return false;

        //    order1.Audience = order2.Audience;
        //    order1.ClassNumber == order2.ClassNumber;
        //    order1.Comment = order2.Comment;
        //    order1.Contact = order2.Contact;
        //    order1.CounselorsNumber = order2.CounselorsNumber;
        //    order1.DT1.Equals(order2.DT1);
        //    order1.DT2 = order2.DT2;
        //    order1.DT3 = order2.DT3;
        //    order1.Email = order2.Email;
        //    order1.Phone = order2.Phone;
        //    order1.RoundsNumber == order2.RoundsNumber;
        //    order1.SchoolPart = order2.SchoolPart;
        //    order1.TopicCollection = order2.TopicCollection;
        //}


        
    }
}
