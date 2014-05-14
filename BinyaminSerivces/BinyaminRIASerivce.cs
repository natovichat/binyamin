using System.ComponentModel.DataAnnotations;
using System.Data.EntityClient;
using System.Linq;
using System.ServiceModel.DomainServices.Server;
using System.Web.Configuration;
using ApplicationData.Implementation;
using System;
using System.Collections.Generic;

namespace Binyamin.Reporting
{
    public class BinyaminReport : DomainService
    {
        #region Database connection
        private ApplicationDataObjectContext m_context;
        public ApplicationDataObjectContext Context
        {
            get
            {
                if (this.m_context == null)
                {
                    
                    string connString =System.Web.Configuration.WebConfigurationManager.ConnectionStrings["_IntrinsicData"].ConnectionString;
                    EntityConnectionStringBuilder builder = new EntityConnectionStringBuilder();
                    builder.Metadata = "res://*/ApplicationData.csdl|res://*/ApplicationData.ssdl|res://*/ApplicationData.msl";
                    builder.Provider = "System.Data.SqlClient";
                    builder.ProviderConnectionString = connString;
                    this.m_context = new ApplicationDataObjectContext(builder.ConnectionString);
                }
                return this.m_context;
            }
        }
        #endregion
        /// <summary>
        /// Override the Count method in order for paging to work correctly
        /// </summary>
        protected override int Count<T>(IQueryable<T> query)
        {
            return query.Count();
        }
   
        [Query(IsDefault = true)]
        public IQueryable<SummaryYear> GetSummaryYearView()
        {
             
            
           return this.Context.Activities.GroupBy(a => a.ActivityDate.Month).Select(g =>
                     new SummaryYear()
                     {
                         NumOfLectures = g.Count(),
                         TotalNumberOfClasses = g.Sum(a => a.TotalNumberOfClasses),
                      ActivityDate =g.Key,
                          TotalNumberOfCounselor = g.Sum(a => a.NumberOfCounselor)
                     });
           

        }

        /************************************************
         * **********************************************/

        /*
        public IQueryable<SummaryYear> GetSummaryYearViewBYDate(DateTime? actDate)
        {
              return this.Context.Activities.Where(a => a.ActivityDate 
                  a.ActivityDate >= start && a.ActivityDate <= end &&
                  (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel).GroupBy
                  (a => a.ActivityDate.Month).Select(g =>
        }
        */

        /************************************************
         * **********************************************/

        public IQueryable<SummaryYear> GetSummaryYearViewBYDate(DateTime? start, DateTime? end )
        {
            return this.Context.Activities.Where(a => a.ActivityDate >= start && a.ActivityDate <= end && (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel).GroupBy(a => a.ActivityDate.Month).Select(g =>
                      new SummaryYear()
                      {
                          ActivityDate = g.Key,
                          NumOfLectures = g.Count(),
                          TotalNumberOfClasses = g.Sum(a => a.TotalNumberOfClasses),
                          TotalNumberOfCounselor = g.Sum(a => a.NumberOfCounselor)
                      });


        }
        //New Query:
        public IQueryable<SummaryYear> GetSummaryYearViewBYYear(DateTime? start, DateTime? end)
        {
            return this.Context.Activities.Where(a => a.ActivityDate >= start && a.ActivityDate <= end && (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel).GroupBy(a => a.ActivityDate.Year).Select(g =>
                      new SummaryYear()
                      {
                          ActivityDate = g.Key,
                          NumOfLectures = g.Count(),
                          TotalNumberOfClasses = g.Sum(a => a.TotalNumberOfClasses),
                          TotalNumberOfCounselor = g.Sum(a => a.NumberOfCounselor)
                      });


        }
        public IQueryable<SummaryYear> GetSummaryYearViewBYYearAndMonth(DateTime? start, DateTime? end, int month)
        {
            return this.Context.Activities.Where(a => a.ActivityDate >= start && a.ActivityDate <= end && a.ActivityDate.Month.Equals(month) && (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel).GroupBy(a => a.ActivityDate.Year).Select(g =>
                      new SummaryYear()
                      {
                          ActivityDate = g.Key,
                          NumOfLectures = g.Count(),
                          TotalNumberOfClasses = g.Sum(a => a.TotalNumberOfClasses),
                          TotalNumberOfCounselor = g.Sum(a => a.NumberOfCounselor)
                      });


        } 
    
       [Query(IsDefault = true)]
        public IQueryable<frequencyView> GetfrequencyView()
        {
            DateTime start;
            int month = DateTime.Now.Month;
            if (month == 09 || month == 10 || month == 11 || month == 12)
            {
                start = new DateTime(DateTime.Now.Year, 09, 1);
            }
            else
            {
                start = new DateTime(DateTime.Now.Year - 1, 09, 1);
            }

            return this.Context.Activities.Where(a => a.ActivityDate >= start && (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel).GroupBy(a => a.SchoolPart.School).Select(g =>
                     new frequencyView()
                     {
                         
                        SchoolName = g.Key.Titel!= null ? g.Key.Titel : "",  
                        frequencyNum = g.Count()
                               
                     });

        }  
        public IQueryable<frequencyView> GetfrequencyViewBYDate(DateTime? start, DateTime? end )
        {

            return this.Context.Activities.Where(a => a.ActivityDate >= start && a.ActivityDate <= end && (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel).GroupBy(a => a.SchoolPart.School).Select(g =>
                     new frequencyView()
                     {

                         SchoolName = g.Key.Titel != null ? g.Key.Titel : "",
                  
                         frequencyNum = g.Count()

                     });

        }
        public IQueryable<frequencyView> GetfrequencyViewByType(DateTime? start, DateTime? end, string type)
        {

            return this.Context.Activities.Where(a => a.ActivityDate >= start && a.ActivityDate <= end && a.ActivityType.T.Equals(type) && (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel).GroupBy(a => a.SchoolPart.School).Select(g =>
                     new frequencyView()
                     {

                         SchoolName = g.Key.Titel != null ? g.Key.Titel : "",

                         frequencyNum = g.Count()

                     });

        }
        public IQueryable<frequencyView> GetfrequencyViewByAud(DateTime? start, DateTime? end, string aud)
        {

            return this.Context.Activities.Where(a => a.ActivityDate >= start && a.ActivityDate <= end  && a.Audience.Title.Equals(aud) && (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel).GroupBy(a => a.SchoolPart.School).Select(g =>
                     new frequencyView()
                     {

                         SchoolName = g.Key.Titel != null ? g.Key.Titel : "",

                         frequencyNum = g.Count()

                     });

        }
        public IQueryable<frequencyView> GetfrequencyViewBYDistrictAndAud(DateTime? start, DateTime? end, string aud, string district)
        {

            return this.Context.Activities.Where(a => a.ActivityDate >= start && a.ActivityDate <= end && a.District.Title.Equals(district) && a.Audience.Title.Equals(aud) && (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel).GroupBy(a => a.SchoolPart.School).Select(g =>
                     new frequencyView()
                     {

                         SchoolName = g.Key.Titel != null ? g.Key.Titel : "",

                         frequencyNum = g.Count()

                     });

        }
        public IQueryable<frequencyView> GetfrequencyViewBYDistrict(DateTime? start, DateTime? end,string district)
        {

            return this.Context.Activities.Where(a => a.ActivityDate >= start && a.ActivityDate <= end && a.District.Title.Equals(district)).GroupBy(a => a.SchoolPart.School).Select(g =>
                     new frequencyView()
                     {

                         SchoolName = g.Key.Titel != null ? g.Key.Titel : "",

                         frequencyNum = g.Count()

                     });

        }
        public IQueryable<frequencyView> GetfrequencyViewByDistrictAndType(DateTime? start, DateTime? end, string type, string district)
        {

            return this.Context.Activities.Where(a => a.ActivityDate >= start && a.ActivityDate <= end && (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel && a.District.Title.Equals(district) && a.ActivityType.T.Equals(type))
                .GroupBy(a => a.SchoolPart.School).Select(g =>
                     new frequencyView()
                     {

                         SchoolName = g.Key.Titel != null ? g.Key.Titel : "",

                         frequencyNum = g.Count()

                     });

        }

        [Query(IsDefault = true)]
         public IQueryable<LectureView> GetLectureView()
          {
              DateTime start;
              int month = DateTime.Now.Month;
              if (month == 09 || month == 10 || month == 11 || month == 12)
              {
                  start = new DateTime(DateTime.Now.Year, 09, 1);
              }
              else
              {
                  start = new DateTime(DateTime.Now.Year - 1, 09, 1);
              }

              return this.Context.Activities.Where(a => a.ActivityDate >= start && (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel).GroupBy(a => a.Topic).Select(g =>
                       new LectureView()
                      {
                     
                         ActivityTopicCollection= g.Key.TopicCollection.Title!= null ? g.Key.TopicCollection.Title: "",
                          ActivityTopic = g.Key.Title != null ? g.Key.Title : "",
                        NumberOfActivities= g.Count(),
                      NumberOfClasses=g.Sum(a=>a.TotalNumberOfClasses),
                      NumberOfCounselors = g.Sum(a=>a.NumberOfCounselor)
                      ,ActivityType = g.Key.TopicCollection.ActivityType.T,
                         NumberOfRounds = g.Sum(a => a.NumberOfRounds)
                      });
           
          }
         public IQueryable<LectureView> GetLectureViewByDate(DateTime? start, DateTime? end)
          {

              return this.Context.Activities.Where(a => a.ActivityDate >= start && a.ActivityDate <= end && (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel).GroupBy(a => a.Topic).Select(g =>
                       new LectureView()
                       {


                           ActivityTopic = g.Key.Title != null ? g.Key.Title : "",
                           ActivityTopicCollection = g.Key.TopicCollection.Title != null ? g.Key.TopicCollection.Title : "",
                           NumberOfActivities = g.Count(),
                           NumberOfClasses = g.Sum(a => a.TotalNumberOfClasses),
                           NumberOfCounselors = g.Sum(a => a.NumberOfCounselor) ,
                           NumberOfRounds = g.Sum(a => a.NumberOfRounds),
                           ActivityType = g.Key.TopicCollection.ActivityType.T,

                       });

          } 

         public IQueryable<LectureView> GetLectureViewBydistrict(DateTime? start, DateTime? end, string district)
          {

              return this.Context.Activities.Where(a => a.ActivityDate >= start && a.ActivityDate <= end && a.District.Title.Equals(district) && (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel).GroupBy(a => a.Topic).Select(g =>
                       new LectureView()
                       {


                           ActivityTopic = g.Key.Title != null ? g.Key.Title : "",
                           ActivityTopicCollection = g.Key.TopicCollection.Title != null ? g.Key.TopicCollection.Title : "",
                           NumberOfActivities = g.Count(),
                           NumberOfClasses = g.Sum(a => a.TotalNumberOfClasses),
                           NumberOfCounselors = g.Sum(a => a.NumberOfCounselor)
                       ,
                           NumberOfRounds = g.Sum(a => a.NumberOfRounds),
                           ActivityType = g.Key.TopicCollection.ActivityType.T,

                       });

          }
         public IQueryable<LectureView> GetLectureViewByAudience(DateTime? start, DateTime? end, string audience)
          {

              return this.Context.Activities.Where(a => a.ActivityDate >= start && a.ActivityDate <= end && a.Audience.Title.Equals(audience) && (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel).GroupBy(a => a.Topic).Select(g =>
                       new LectureView()
                       {


                           ActivityTopic = g.Key.Title != null ? g.Key.Title : "",
                           ActivityTopicCollection = g.Key.TopicCollection.Title != null ? g.Key.TopicCollection.Title : "",
                           NumberOfActivities = g.Count(),
                           NumberOfClasses = g.Sum(a => a.TotalNumberOfClasses),
                           NumberOfCounselors = g.Sum(a => a.NumberOfCounselor)
                       ,
                           NumberOfRounds = g.Sum(a => a.NumberOfRounds),
                           ActivityType = g.Key.TopicCollection.ActivityType.T,

                       });

          }
         public IQueryable<LectureView> GetLectureViewByCity(DateTime? start, DateTime? end, string city)

          {

              return this.Context.Activities.Where(a => a.ActivityDate >= start && a.ActivityDate <= end && a.SchoolPart.City.Title.Equals(city) && (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel).GroupBy(a => a.Topic).Select(g =>
                       new LectureView()
                       {


                           ActivityTopic = g.Key.Title != null ? g.Key.Title : "",
                           ActivityTopicCollection = g.Key.TopicCollection.Title != null ? g.Key.TopicCollection.Title : "",
                           NumberOfActivities = g.Count(),
                           NumberOfClasses = g.Sum(a => a.TotalNumberOfClasses),
                           NumberOfCounselors = g.Sum(a => a.NumberOfCounselor)
                       ,
                           NumberOfRounds = g.Sum(a => a.NumberOfRounds),
                           ActivityType = g.Key.TopicCollection.ActivityType.T,

                       });

          }
         public IQueryable<LectureView> GetLectureViewBydistrictAndAudience(DateTime? start, DateTime? end, string district, string audience)
          {

              return this.Context.Activities.Where(a => a.ActivityDate >= start && a.ActivityDate <= end &&
                  a.District.Title.Equals(district) && a.Audience.Title.Equals(audience) && (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel).GroupBy(a => a.Topic).Select(g =>
                       new LectureView()
                       {


                           ActivityTopic = g.Key.Title != null ? g.Key.Title : "",
                           ActivityTopicCollection = g.Key.TopicCollection.Title != null ? g.Key.TopicCollection.Title : "",
                           NumberOfActivities = g.Count(),
                           NumberOfClasses = g.Sum(a => a.TotalNumberOfClasses),
                           NumberOfCounselors = g.Sum(a => a.NumberOfCounselor)
                       ,
                           NumberOfRounds = g.Sum(a => a.NumberOfRounds),
                           ActivityType = g.Key.TopicCollection.ActivityType.T,

                       });

          }


         public IQueryable<LectureView> GetLectureViewByAudienceAndCity(DateTime? start, DateTime? end, string audience, string city)
         {

             return this.Context.Activities.Where(a => a.ActivityDate >= start && a.ActivityDate <= end &&
                 a.Audience.Title.Equals(audience) && a.SchoolPart.City.Title.Equals(city) && (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel).GroupBy(a => a.Topic).Select(g =>
                      new LectureView()
                      {


                          ActivityTopic = g.Key.Title != null ? g.Key.Title : "",
                          ActivityTopicCollection = g.Key.TopicCollection.Title != null ? g.Key.TopicCollection.Title : "",
                          NumberOfActivities = g.Count(),
                          NumberOfClasses = g.Sum(a => a.TotalNumberOfClasses),
                          NumberOfCounselors = g.Sum(a => a.NumberOfCounselor)
                      ,
                          NumberOfRounds = g.Sum(a => a.NumberOfRounds),
                          ActivityType = g.Key.TopicCollection.ActivityType.T,

                      });

         }

         public IQueryable<LectureView> GetLectureViewBydistrictAndCity(DateTime? start, DateTime? end, string district, string city)
         {

             return this.Context.Activities.Where(a => a.ActivityDate >= start && a.ActivityDate <= end &&
                 a.District.Title.Equals(district) && a.SchoolPart.City.Title.Equals(city) && (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel).GroupBy(a => a.Topic).Select(g =>
                      new LectureView()
                      {


                          ActivityTopic = g.Key.Title != null ? g.Key.Title : "",
                          ActivityTopicCollection = g.Key.TopicCollection.Title != null ? g.Key.TopicCollection.Title : "",
                          NumberOfActivities = g.Count(),
                          NumberOfClasses = g.Sum(a => a.TotalNumberOfClasses),
                          NumberOfCounselors = g.Sum(a => a.NumberOfCounselor)
                      ,
                          NumberOfRounds = g.Sum(a => a.NumberOfRounds),
                          ActivityType = g.Key.TopicCollection.ActivityType.T,

                      });

         }
         public IQueryable<LectureView> GetLectureViewBydistrictAndAudienceAndCity(DateTime? start, DateTime? end,string district, string audience, string city)
         {

             return this.Context.Activities.Where(a => a.ActivityDate >= start && a.ActivityDate <= end &&
                 a.District.Title.Equals(district) && a.Audience.Title.Equals(audience) && a.SchoolPart.City.Title.Equals(city) && (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel).GroupBy(a => a.Topic).Select(g =>
                      new LectureView()
                      {


                          ActivityTopic = g.Key.Title != null ? g.Key.Title : "",
                          ActivityTopicCollection = g.Key.TopicCollection.Title != null ? g.Key.TopicCollection.Title : "",
                          NumberOfActivities = g.Count(),
                          NumberOfClasses = g.Sum(a => a.TotalNumberOfClasses),
                          NumberOfCounselors = g.Sum(a => a.NumberOfCounselor)
                      ,
                          NumberOfRounds = g.Sum(a => a.NumberOfRounds),
                          ActivityType = g.Key.TopicCollection.ActivityType.T,

                      });

         }

        [Query(IsDefault = true)]
        public IQueryable<ReportSchoolView> GetReportSchoolView()
        {return this.Context.Activities.Where(a => (ActivityStatusEnum)a.ActivityStatus.InternalId != ActivityStatusEnum.Cancel)
                .Select(g =>
                    new ReportSchoolView()
                    {
                        ActivityId = g.Id,
                        SchoolPartId = g.SchoolPart.Id,
                        SchoolName = g.SchoolPart != null ? g.SchoolPart.School.Titel : "",
                        SchoolPartName = g.SchoolPart != null ? g.SchoolPart.SchoolName : "",
                        ActivityDate = g.ActivityDate != null ? g.ActivityDate : DateTime.Now,
                        ActivityPlace = g.SchoolPart != null ? g.SchoolPart.City.Title : "",
                        ActivityStatus = g.ActivityStatus != null ? g.ActivityStatus.Title : "",
                        ActivityType = g.ActivityType.T,
                        Audience = g.Audience.Title,
                        Comments = g.Comments,
                        Contact = g.Contact.FullName,
                        District = g.SchoolPart != null ? g.SchoolPart.District.Title : "",
                        SchoolType = g.SchoolPart != null ? g.SchoolPart.SchoolType.Title : "",
                        Topic = g.Topic != null ? g.Topic.Title : "",
                        TopicCollection = g.Topic != null ? g.Topic.TopicCollection.Title : "",
                        TotalNumberOfClasses = g.TotalNumberOfClasses,
                        TotalNumberOfCounselor = g.NumberOfCounselor,
                        TotalNumberOfRounds = g.NumberOfRounds,
                        DistrictId = g.SchoolPart != null ? g.SchoolPart.District.Id : 0,
                        ContactPhone = g.Contact != null ? g.Contact.PhoneNumber1 : "",
                        NumberOfAssigedCounselor = g.ActivityCounselors.Where(ac => ac.Assigned).Count(),
                        IsMissingCounselors = g.ActivityCounselors.Where(ac => ac.Assigned).Count() < g.NumberOfCounselor,
                        StartTime = g.StartTime,
                        EndTime = g.EndTime,
                        CounselorInActivity = ""
                    });
        }
       
        [Query(IsDefault = true)]
        public IQueryable<ActivityView> GetActivity()
        {
            return this.Context.Activities.Where(a=>(ActivityStatusEnum)a.ActivityStatus.InternalId!= ActivityStatusEnum.Cancel)
                 .Select(g =>
                     new ActivityView()
                     {
                         ActivityId = g.Id,
                         SchoolName = g.SchoolPart!=null ? g.SchoolPart.School.Titel : "" ,
                         SchoolPartName = g.SchoolPart!=null ? g.SchoolPart.SchoolName: "",
                         ActivityDate = g.ActivityDate!=null? g.ActivityDate : DateTime.Now,
                         ActivityPlace = g.SchoolPart!=null ?g.SchoolPart.City.Title : "",
                         ActivityStatus = g.ActivityStatus!=null ? g.ActivityStatus.Title : "",
                         ActivityType = g.ActivityType.T,
                         Audience = g.Audience.Title,
                         Comments  =g.Comments,
                         Contact = g.Contact.FullName,
                         District = g.SchoolPart!= null ? g.SchoolPart.District.Title : "",
                         SchoolType =g.SchoolPart!=null ?g.SchoolPart.SchoolType.Title : "",
                         Topic = g.Topic!=null ? g.Topic.Title : "",
                         TopicCollection= g.Topic!=null ? g.Topic.TopicCollection.Title :"",
                         TotalNumberOfClasses = g.TotalNumberOfClasses,
                         TotalNumberOfCounselor = g.NumberOfCounselor,
                         TotalNumberOfRounds = g.NumberOfRounds,
                         DistrictId = g.SchoolPart!=null ?g.SchoolPart.District.Id : 0,
                         ContactPhone = g.Contact!=null ? g.Contact.PhoneNumber1  : "",
                         NumberOfAssigedCounselor = g.ActivityCounselors.Where(ac => ac.Assigned).Count(),
                         IsMissingCounselors = g.ActivityCounselors.Where(ac => ac.Assigned).Count() < g.NumberOfCounselor,
                         StartTime = g.StartTime,
                         EndTime = g.EndTime,
                         CounselorInActivity =""
                     });

        }

        [Query(IsDefault = true)]
        public IQueryable<SchoolView> GetSchools()
        {
            return this.Context.SchoolParts
                 .Select(g =>
                     new SchoolView()
                     {
                         SchoolId = g.Id,
                      SchoolFullName = g.School.Titel + " " +g.SchoolName
                     });

        }

        [Query(IsDefault = true)]
        public IQueryable<CounselorView> GetCounselor()
        {
            DateTime start;
            int month = DateTime.Now.Month;
            if (month == 09 || month == 10 || month == 11 || month == 12)
            {
                start = new DateTime(DateTime.Now.Year, 09, 1);
            }
            else
            {
                start = new DateTime(DateTime.Now.Year - 1, 09, 1);
            }

        return this.Context.Activities.Where(a => a.ActivityDate >= start && (ActivityStatusEnum)a.ActivityStatus.InternalId!= ActivityStatusEnum.Cancel).Select(g =>
              
                    new CounselorView()
                    {
                        ActivityId = g.Id,
                        SchoolName = g.SchoolPart != null ? g.SchoolPart.School.Titel : "",
                        SchoolPartName = g.SchoolPart != null ? g.SchoolPart.SchoolName : "",
                        ActivityDate = g.ActivityDate != null ? g.ActivityDate : DateTime.Now,
                        ActivityPlace = g.SchoolPart != null ? g.SchoolPart.City.Title : "",               
                        Audience = g.Audience.Title,
                        Month = g.ActivityDate.Month ,
                        Topic = g.Topic != null ? g.Topic.Title : "",
                        TopicCollection = g.Topic != null ? g.Topic.TopicCollection.Title : "",
                        CounselorInActivity =""
                    });

        }

        public IQueryable<CounselorView> GetCounselorData(int? CounselorId)
        {
            return this.Context.ActivityCounselors.Where(a => a.Counselor.Id == CounselorId && (ActivityStatusEnum)a.Activity.ActivityStatus.InternalId != ActivityStatusEnum.Cancel).Select(g =>
            
                    new CounselorView()
                    {
                        ActivityId = g.Activity.Id,
                        SchoolName = g.Activity.SchoolPart != null ? g.Activity.SchoolPart.School.Titel : "",
                        SchoolPartName = g.Activity.SchoolPart != null ? g.Activity.SchoolPart.SchoolName : "",
                        ActivityDate = g.Activity.ActivityDate != null ? g.Activity.ActivityDate : DateTime.Now,
                        ActivityPlace = g.Activity.SchoolPart != null ? g.Activity.SchoolPart.City.Title : "",       
                        Topic = g.Activity.Topic != null ? g.Activity.Topic.Title : "",
                        TopicCollection = g.Activity.Topic != null ? g.Activity.Topic.TopicCollection.Title : "", 
                        CounselorInActivity =g.Counselor.LastName + " " + g.Counselor.FirstName,
                        Month =  g.Activity.ActivityDate.Month, 
                        Audience = g.Activity.Audience.Title
                    });

        }

        private string CreateAssignedCounselor(Activity g)
        {
            string retVal = "";

            foreach (ActivityCounselor c in g.ActivityCounselors)
            {
                retVal += c.Counselor.FirstName + " " + c.Counselor.LastName;
            }

            return retVal;
        }
        public static string Convert(ActivityStatusEnum status)
        {
            switch (status)
            {
                case ActivityStatusEnum.Create:
                    return "חדש";
                case ActivityStatusEnum.Assigned:
                    return "שובץ";
                case ActivityStatusEnum.Finished:
                    return "בוצע";
                case ActivityStatusEnum.Cancel:
                    return "בוטל";
                case ActivityStatusEnum.Postponed:
                    return "נדחה";
                default:
                    throw new NotSupportedException();
            }
        }
        public enum ActivityStatusEnum
        {
            Create = 1,
            Assigned = 2,
            Finished = 3,
            Cancel = 4,
            Postponed = 5
        }
    }

      public class frequencyView
    {
          [Key] 
          public string SchoolName { get; set; }
          public int frequencyNum {get; set; }
      }
      public class SummaryYear
      {
          [Key]

         public int TotalNumberOfClasses { get; set; }
         public int TotalNumberOfCounselor { get; set; }
         public int  ActivityDate { get; set; }  
         public int NumOfLectures { get; set; }
      }
      public class CounselorView
    {
        [Key]
        public int ActivityId { get; set; }
        public string CounselorInActivity { get; set; }     
        public string TopicCollection { get; set; }
        public string Topic { get; set; }
        public string SchoolName { get; set; }
        public string SchoolPartName { get; set; }
        public string ActivityPlace { get; set; }
        public DateTime ActivityDate { get; set; }
        public string Audience { get; set; }
        public int Month { get; set; }
    }
      public class ActivityView
    {
        [Key]
        public int ActivityId { get; set; }
        public string SchoolName { get; set; }
        public string SchoolPartName { get; set; }
        public string TopicCollection { get; set; }
        public string Topic { get; set; }
        public int TotalNumberOfClasses { get; set; }
        public int TotalNumberOfCounselor { get; set; }
        public int TotalNumberOfRounds { get; set; }
        public string Comments { get; set; }
        public string ActivityPlace { get; set; }
        public string District{get;set;}
        public string ActivityStatus { get; set; }
        public DateTime ActivityDate { get; set; }
        public string ActivityType { get; set; }
        public string SchoolType { get; set; }
        public string Contact { get; set; }
        public string Audience { get; set; }
        public string ContactPhone { get; set; }
        public int DistrictId { get; set; }
        public string CounselorInActivity { get; set; }

        private int numberOfAssigedCounselor;
        public int NumberOfAssigedCounselor
        {
            get
            {
                return numberOfAssigedCounselor;
            }
            set
            {
                numberOfAssigedCounselor = value;
            }
        }
        public bool IsMissingCounselors {get;set;}
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
      public class ReportSchoolView
    {
        [Key]
        public int ActivityId { get; set; }
        public int SchoolPartId { get; set; }
        public string SchoolName { get; set; }
        public string SchoolPartName { get; set; }
        public string TopicCollection { get; set; }
        public string Topic { get; set; }
        public int TotalNumberOfClasses { get; set; }
        public int TotalNumberOfCounselor { get; set; }
        public int TotalNumberOfRounds { get; set; }
        public string Comments { get; set; }
        public string ActivityPlace { get; set; }
        public string District { get; set; }
        public string ActivityStatus { get; set; }
        public DateTime ActivityDate { get; set; }
        public string ActivityType { get; set; }
        public string SchoolType { get; set; }
        public string Contact { get; set; }
        public string Audience { get; set; }
        public string ContactPhone { get; set; }
        public int DistrictId { get; set; }
        public string CounselorInActivity { get; set; }

        private int numberOfAssigedCounselor;
      public int NumberOfAssigedCounselor
        {
            get
            {
                return numberOfAssigedCounselor;
            }
            set
            {
                numberOfAssigedCounselor = value;
            }
        }

      public bool IsMissingCounselors { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
      public class LectureView
    {
        [Key]
        public string ActivityTopic { get; set; }
        public string ActivityTopicCollection { get; set; }
        public int NumberOfActivities { get; set; }
        public int NumberOfCounselors { get; set; }
        public int NumberOfClasses { get; set; }
        public int NumberOfRounds { get; set; }
        public string ActivityType { get; set; }
      
    } 
      public class SchoolView
    {
        [Key]
        public int SchoolId { get; set; }
        public string SchoolFullName { get; set; }
    }

}
