using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Security.Server;
using System.Linq.Expressions;
using LightSwitchApplication.UserCode;
using LightSwitchApplication;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;





namespace LightSwitchApplication
{
    public partial class ApplicationDataService
    {
        partial void CounselorsByActivty_PreprocessQuery(int? ActivityId, ref IQueryable<Counselor> query)
        {

            query = query.Where<Counselor>(a => a.ActivityCounselors.Where(activity => (activity.Activity.Id == ActivityId.Value && activity.Assigned )).Count() > 0);

        }


        partial void CounselorsByDistrict_PreprocessQuery(int? DistrictId, ref IQueryable<Counselor> query)
        {
            if (DistrictId.HasValue)
            {
                query = query.Where<Counselor>(c => c.District == null || c.District.Id == DistrictId.Value);
            }
        }

        partial void CityByDistrict_PreprocessQuery(int? DistrictId, ref IQueryable<City> query)
        {
            var schools = (from school in this.SchoolParts
                           select school);

            if (DistrictId.HasValue)
            {
                schools = schools.Where(s => s.District.Id == DistrictId);
            }

            List<int> citiesId = new List<int>();
            foreach (SchoolPart school in schools)
            {
                citiesId.Add(school.City.Id);
            }


            query = query.Where(c => citiesId.Contains(c.Id));
        }


        //partial void SearchActivity_PreprocessQuery(DateTime? FromDate, DateTime? ToDate, int? SchoolPartDistrictId, string SearchText, ref IQueryable<Activity> query)
        //{
        //    if (SearchText != null && SearchText != "")
        //    {
        //        //IEnumerable<SchoolPart> schoolParts = this.SchoolParts.Search(new SearchTerm(SearchText)).Execute();
        //        //IEnumerable<Topic> topics = this.Topics.Search(new SearchTerm(SearchText)).Execute();
        //        //IEnumerable<ActivityType> activityTypes = this.ActivityTypes.Search(new SearchTerm(SearchText)).Execute();
        //        //IEnumerable<City> cities = this.Cities.Search(new SearchTerm(SearchText)).Execute();
        //        //IEnumerable<Counselor> counselors = this.Counselors.Search(new SearchTerm(SearchText)).Execute();
        //        //IEnumerable<Contact> contacts = this.Contacts.Search(new SearchTerm(SearchText)).Execute();

        //        Func<Activity,bool> method = CreateDelegate(SearchText);


        //        query = query.Where(
        //            a => a.SchoolPart!= null.FullName.Contains(SearchText) ||
        //                 a.Topic.topics.Contains(a.Topic) ||
        //                 activityTypes.Contains(a.ActivityType) ||
        //                 (a.SchoolPart!=null && cities.Contains(a.SchoolPart.City)) ||
        //                 a.AssignedCounselors.Contains(SearchText) ||
        //                 contacts.Contains(a.Contact)

        //            );
        //    }
        //}

        //private bool Pass(string search, List<EntityObject> obj)
        //{

        //}




        //partial void SearchActivity_PreprocessQuery(DateTime? FromDate, DateTime? ToDate, int? SchoolPartDistrictId, string SearchText, ref IQueryable<Activity> query)
        //{
        //    List<SchoolPart> schoolsPart = this.SchoolParts.Where(s => s.SchoolName.Contains(SearchText)).Execute().ToList();

        //    if (SearchText != null && SearchText != "")
        //    {

        //        query = query.Where(
        //            a => a.DataForSearch.Contains(a.SchoolPart)
        //                 //a.Topic!=null && a.Topic.Title.Contains(SearchText) ||
        //                 //a.ActivityType !=null && a.ActivityType.T.Contains(SearchText) ||
        //                 //a.SchoolPart != null && a.SchoolPart.City!=null && a.SchoolPart.City.Title.Contains(SearchText) ||
        //                 //a.AssignedCounselors.Contains(SearchText) ||
        //                 //a.Contact!=null && a.Contact.FullName.Contains(SearchText)
        //            );
        //    }
        //}


        partial void Activities_Validate(Activity entity, EntitySetValidationResultsBuilder results)
        {
            
            //if (entity.Contact != null && entity.Contact.SchoolPart.Id !=entity.SchoolPart.Id)
            //{
            //    results.AddEntityError("איש הקשר שנבחר לא תואם את בית הספר שנבחר");
            //}
            //if (entity.Contact == null && entity.SchoolPart == null)
            //{
            //    results.AddEntityError("חייבים לבחור אחד מבין השדות הבאים : בית ספר, איש קשר");
            //}
            //else if (entity.SchoolPart == null)
            //{
            //    entity.SchoolPart = entity.Contact.SchoolPart;
            //}

            //if (entity.Topic != null)
            //{
            //    entity.ActivityType = entity.Topic.TopicCollection.ActivityType;
            //}

            
        }

        partial void SearchSchool_PreprocessQuery(string SearchText, ref IQueryable<SchoolPart> query)
        {
            if (SearchText != null && SearchText != "")
            {
                query = from  school in query
                        where school.SchoolName.Contains(SearchText) ||  school.School.Titel.Contains(SearchText) ||
                        school.Contacts.Any(c=>c.FullName.Contains(SearchText))
                        select school;

            }
        }

        partial void SchoolContacts_PreprocessQuery(int? SchoolPartId, ref IQueryable<Contact> query)
        {
            if (SchoolPartId.HasValue)
            {
                query = query.Where(c => c.SchoolPart.Id == SchoolPartId);
            }
        }

        partial void ActivityCounselorsByDate_PreprocessQuery(int? CounselorId, DateTime? FromDate, DateTime? ToDate, ref IQueryable<ActivityCounselor> query)
        {
            query = query.OrderBy<ActivityCounselor, DateTime>(d => d.Activity.ActivityDate);
        }

        partial void FixCR_PreprocessQuery(ref IQueryable<CRs> query)
        {
            query = query.Where(cr => cr.Type==null || cr.Type == "" || cr.Type == "Fix");
        }

        partial void PartnersReport_Inserted(PartnersReport entity)
        {

        }

        partial void PartnersReport_Inserting(PartnersReport entity)
        {
            //this.PartnersReportActivities.RemoveAll();
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
            
        }

        partial void ActivitiesByCounselor_PreprocessQuery(int? CounselorId, ref IQueryable<Activity> query)
        {
            query = from act in query
                    from actCons in act.ActivityCounselors
                    where actCons.Counselor.Id == CounselorId
                    //where actCons.Activity.ActivityDate < DateTime.Now
                    where actCons.Assigned == true
                    select act;
        }

        partial void PastActivitiesByCounselor_PreprocessQuery(int? CounselorId, ref IQueryable<Activity> query)
        {
            query = from act in query
                    where act.ActivityDate < DateTime.Now
                    select act;
        }

        partial void FutureActivitiesByCounselor_PreprocessQuery(int? CounselorId, ref IQueryable<Activity> query)
        {
            query = from act in query
                    where act.ActivityDate >= DateTime.Now
                    select act;
        }




        partial void Emails_Inserting(Emails entity)
        {
            if (entity.Message == null)
            {
                entity.Message = "";
            }
            if (entity.Subject == null)
            {
                entity.Subject = "";
            }

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


            MailHelper mailHelper =
                new MailHelper(
                    latest.DisaplayName,
                    entity.ReceiverMail,
                    entity.Receiver,
                    entity.Subject,
                    entity.Message,
                    latest);

            // Send Email
            mailHelper.SendMail();
        }

        //private AlternateView buildWithImages(Emails entity)
        //{
        //    string strMessage = entity.Message;
        //    AlternateView avHtml = null;
        //    LinkedResource pic1 = null;


        //    if (null != entity.Image1)
        //    {

        //        //string base64Str = Convert.ToBase64String(entity.Image1);
        //        //string base64Str = entity.Image1.ToString();
        //        Stream s = new MemoryStream(entity.Image1);
        //        pic1 = new LinkedResource(s, "image/jpeg");
        //        pic1.ContentId = "Pic1";
        //        pic1.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

        //        int length = strMessage.Length;
        //        string start_string = strMessage.Substring(0, entity.Image1index);
        //        string end_string = strMessage.Substring(entity.Image1index, strMessage.Length - start_string.Length);
        //        //strMessage = start_string + "<img src=\"data:image/png;base64," + base64Str + "\" width=\"200\" height=\"50\" alt=\"\"/>" +end_string;
        //        strMessage = start_string + "<img src=\"http://www.digitaltrends.com/wp-content/uploads/2013/02/10-google-search-tricks-you-might-not-know-982f430daf.jpg\" width=\"200\" height=\"50\">" + end_string;
        //    }
           
        //    avHtml = AlternateView.CreateAlternateViewFromString(strMessage, null, MediaTypeNames.Text.Html);

        //    if (null == pic1)
        //        avHtml.LinkedResources.Add(pic1);

        //    return avHtml;
        //}

        partial void ContactsBySchool_PreprocessQuery(int? school_id, ref IQueryable<Contact> query)
        {

        }

        partial void OrderClassesAndCounslersSet_Inserting(OrderClassesAndCounslers entity)
        {
            entity.Summary = "מספר כיתות: " + entity.ClassesNumber + ", " + "מספר מדריכים: " + entity.CounslerNumber + ", " + "מחיר: " + entity.Price;
        }

        partial void OrderClassesAndCounslersSet_Updating(OrderClassesAndCounslers entity)
        {
            entity.Summary = "מספר כיתות: " + entity.ClassesNumber + ", " + "מספר מדריכים: " + entity.CounslerNumber + ", " + "מחיר: " + entity.Price;
        }

        partial void CounselorByUserPass_PreprocessQuery(string Username, string Password, ref IQueryable<Counselor> query)
        {
            query = from cons in query
                    where cons.Username == Username
                    where cons.Password == Password
                    select cons;
        }

        partial void Activities_Updated(Activity entity)
        {

            //CalanderUtil.Calender.InsertActivity(entity.Topic + " " + entity.ActivityType, entity.ActivityDate, entity.ActivityPlace, entity.Comments);
        }



        
        


    }

    public class Calender
    {
        public static void InsertActivity(string title, DateTime when, string where, string commnets)
        {

            //var service = new CalendarService();
            //CalendarsResource calendars = service.Calendars;



            /*
            // Create a CalenderService and authenticate
            CalendarService myService = new CalendarService("exampleCo-exampleApp-1");
            myService.setUserCredentials("elatzmi@gmail.com", "tzahala");

            //calendarquery query = new calendarquery();
            //query.uri = new uri("https://www.google.com/calendar/feeds/default/allcalendars/full");
            //calendarfeed resultfeed = (calendarfeed)myservice.query(query);
            //console.writeline("your calendars:\n");
            //foreach (calendarentry entry in resultfeed.entries)
            //{
            //    console.writeline(entry.title.text + "\n");
            //}

            EventEntry eventEntry = new EventEntry();

            // Set the title and content of the entry.
            eventEntry.Title.Text = title;
            eventEntry.Content.Content = commnets;

            // Set a location for the event.
            Where eventLocation = new Where();
            eventLocation.ValueString = where;
            eventEntry.Locations.Add(eventLocation);

            When eventTime = new When(when,when.AddHours(2));
            eventEntry.Times.Add(eventTime);

            Uri postUri = new Uri("https://www.google.com/calendar/feeds/default/private/full");

            // Send the request and receive the response:
            AtomEntry insertedEntry = myService.Insert(postUri, eventEntry);
            
            */
        }

        
    }
}

