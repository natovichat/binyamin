﻿using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using ServerSide;
using Microsoft.LightSwitch.Threading;
using System.Runtime.Serialization;
using System.Xml;
using System.Windows;
using LightSwitchApplication.UserCode;
using System.Text;
using System.Windows.Controls;
using System.Collections.Specialized;

namespace LightSwitchApplication
{
    public partial class SearchActivityViews
    {
        #region Import

        partial void Import_Execute()
        {
            PromptAndImportEntities();

        }
        IDispatcher current;
        public void PromptAndImportEntities()
        {

            //files.Add(new FileInfo(@"C:\database1.xml"));
            //files.Add(new FileInfo(@"C:\database2.xml"));
            //files.Add(new FileInfo(@"C:\database3.xml"));

            //  DeleteData();
            current = Dispatchers.Current;
            Dispatchers.Main.BeginInvoke(() =>
            {
                SelectFileWindow.FileWindowArgument Argument = new SelectFileWindow.FileWindowArgument { FileAccess = System.IO.FileAccess.Read, Multiselect=true , FileFilter = "xml (*.xml)|*.xml" };
                SelectFileWindow selectFileWindow = new SelectFileWindow(Argument);


                selectFileWindow.Closed += new EventHandler(selectFileWindow_Closed);

                selectFileWindow.Show();

            });




        }

        void selectFileWindow_Closed(object sender, EventArgs e)
        {

            current.BeginInvoke(() =>
            {
                try
                {

                    SelectFileWindow selectFileWindow = (SelectFileWindow)sender;

                    List<Stream> files = selectFileWindow.DocumentStream;


                    DeleteData();

                    TestServerFacadeWrapper data = DeSerilizeServerSide(files[0]);


                    Dictionary<int, ActivityType> activityTypeById = new Dictionary<int, ActivityType>();
                    Dictionary<int, Topic> topicById = new Dictionary<int, Topic>();
                    Dictionary<int, Audience> audienceById = new Dictionary<int, Audience>();
                    Dictionary<int, ActivityStatus> activityStatusById = new Dictionary<int, ActivityStatus>();

                    activityTypeById.Add(1, InsertActivityType(Common.ActivityType.Lecture));
                    activityTypeById.Add(2, InsertActivityType(Common.ActivityType.WorkshopRegular));
                    activityTypeById.Add(3, InsertActivityType(Common.ActivityType.WorkshopHoleGrade));
                    activityTypeById.Add(4, InsertActivityType(Common.ActivityType.WorkshopEvening));
                    activityTypeById.Add(5, InsertActivityType(Common.ActivityType.ParentsAndChildren));
                    activityTypeById.Add(6, InsertActivityType(Common.ActivityType.Bitmidrash));
                    activityTypeById.Add(7, InsertActivityType(Common.ActivityType.SoundAndLightShow));
                    activityTypeById.Add(8, InsertActivityType(Common.ActivityType.Trip));
                    activityTypeById.Add(9, InsertActivityType(Common.ActivityType.Show));
                    activityTypeById.Add(0, InsertActivityType(Common.ActivityType.Unknown));


                    audienceById.Add(1, InsertAudience(Common.Grade.ז));
                    audienceById.Add(2, InsertAudience(Common.Grade.ח));
                    audienceById.Add(3, InsertAudience(Common.Grade.ט));
                    audienceById.Add(4, InsertAudience(Common.Grade.י));
                    audienceById.Add(5, InsertAudience(Common.Grade.יא));
                    audienceById.Add(6, InsertAudience(Common.Grade.יב));
                    audienceById.Add(7, InsertAudience(Common.Grade.הורים));
                    audienceById.Add(8, InsertAudience(Common.Grade.מורים));
                    audienceById.Add(9, InsertAudience(Common.Grade.אחר));

                    activityStatusById.Add((int)Common.ActivityStatus.Create, InsertActivityStatus((int)Common.ActivityStatus.Create));
                    activityStatusById.Add((int)Common.ActivityStatus.Assigned, InsertActivityStatus((int)Common.ActivityStatus.Assigned));
                    activityStatusById.Add((int)Common.ActivityStatus.Finished, InsertActivityStatus((int)Common.ActivityStatus.Finished));
                    activityStatusById.Add((int)Common.ActivityStatus.Cancel, InsertActivityStatus((int)Common.ActivityStatus.Cancel));
                    activityStatusById.Add((int)Common.ActivityStatus.Postponed, InsertActivityStatus((int)Common.ActivityStatus.Postponed));



                    DataWorkspace.ApplicationData.SaveChanges();

                    foreach (Common.TopicsCollection topicCollection in data.TopicCollections)
                    {
                        InsertTopicCollection(activityTypeById, topicCollection, ref topicById);
                    }
                    DataWorkspace.ApplicationData.SaveChanges();
                    Dictionary<int, SchoolType> schoolTypeById = new Dictionary<int, SchoolType>();
                    Dictionary<int, Role> roleById = new Dictionary<int, Role>();
                    Dictionary<int, District> districtById = new Dictionary<int, District>();
                    Dictionary<string, City> cityById = new Dictionary<string, City>();
                    Dictionary<string, SchoolPart> schoolById = new Dictionary<string, SchoolPart>();
                    Dictionary<string, Contact> contactById = new Dictionary<string, Contact>();
                    Dictionary<int, Counselor> counselorById = new Dictionary<int, Counselor>();
                    Dictionary<string, School> schooCollectionlByid = new Dictionary<string, School>();

                    foreach (Common.District d in data.Districts)
                    {
                        District newd = DataWorkspace.ApplicationData.Districts.AddNew();
                        newd.Title = d.Title;
                        districtById.Add(d.Id, newd);
                    }
                    DataWorkspace.ApplicationData.SaveChanges();
                    foreach (Common.SchoolType schoolType in data.SchoolTypes)
                    {
                        schoolTypeById.Add(schoolType.Id, InsertSchoolType(schoolType));
                    }
                    foreach (Common.Role role in data.Roles)
                    {
                        roleById.Add(role.Id, InsertRole(role));
                    }
                    DataWorkspace.ApplicationData.SaveChanges();

                    bool first = true;
                    int i = 1;
                    foreach (FileStream f in files)
                    {


                        if (!first)
                        {

                            data = DeSerilizeServerSide(f);

                        }

                        first = false;

                        TestServerFacadeWrapper._district = i;



                        foreach (Common.City city in data.Cities)
                        {
                            if (!cityById.ContainsKey(city.Id.ToString() + city.Title))
                            {
                                cityById.Add(city.Id + city.Title, InsertCity(districtById, city));
                            }
                        }

                        DataWorkspace.ApplicationData.SaveChanges();


                        foreach (Common.SchoolCollection schoolCollection in data.SchoolCollections)
                        {
                            if (schoolCollection.Schools != null && schoolCollection.Schools.Count > 0 && schoolCollection.Schools[0].District.Id == TestServerFacadeWrapper._district)
                            {
                                int schoolCollectionId = schoolCollection.Id;

                                List<Common.SchoolCollection> existSchoolCollection = data.SchoolCollections.Where(s => s.Schools != null && s.Schools.Count > 0 && s.Schools[0].District.Id == TestServerFacadeWrapper._district && s.Id == schoolCollectionId).ToList();

                                string key = schoolCollection.Id + "," + schoolCollection.Schools[0].District.Id;

                                if (!schooCollectionlByid.ContainsKey(key))
                                {
                                    schooCollectionlByid.Add(key, InsertSchoolCollection(schoolTypeById, roleById, districtById, cityById, existSchoolCollection, ref schoolById, ref contactById));
                                }


                            }

                        }
                        DataWorkspace.ApplicationData.SaveChanges();
                        foreach (Common.Counselor counselor in data.counselors)
                        {
                            counselorById.Add(counselor.Id, InsertCounselor(cityById, districtById, counselor));
                        }

                        DataWorkspace.ApplicationData.SaveChanges();

                        

                        foreach (Common.Activity activity in data.Activites())
                        {
                            InsertActivity(audienceById, counselorById, topicById, activityTypeById, schoolTypeById, roleById, districtById, cityById, schoolById, contactById, activityStatusById, activity);
                        }
                        DataWorkspace.ApplicationData.SaveChanges();

                        i++;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
                );
        }

        private ActivityStatus InsertActivityStatus(int p)
        {
            ActivityStatus status = this.DataWorkspace.ApplicationData.ActivityStatus.AddNew();
            status.InternalId = (int)ConvertToLWEnum((Common.ActivityStatus)p);
            status.Title = Convert(ConvertToLWEnum((Common.ActivityStatus)p));
            return status;

        }

        public static string Convert(LightSwitchApplication.Activity.ActivityStatusEnum status)
        {
            switch (status)
            {
                case LightSwitchApplication.Activity.ActivityStatusEnum.Create:
                    return "חדש";
                case LightSwitchApplication.Activity.ActivityStatusEnum.Assigned:
                    return "שובץ";
                case LightSwitchApplication.Activity.ActivityStatusEnum.Finished:
                    return "בוצע";
                case LightSwitchApplication.Activity.ActivityStatusEnum.Cancel:
                    return "בוטל";
                case LightSwitchApplication.Activity.ActivityStatusEnum.Postponed:
                    return "נדחה";
                default:
                    throw new NotSupportedException();
            }
        }
        private void DeleteData()
        {
            //DataWorkspace.ApplicationData.SaveChanges();


            foreach (ActivityCounselor activity in DataWorkspace.ApplicationData.ActivityCounselors)
            {
                activity.Delete();
                DataWorkspace.ApplicationData.SaveChanges();
            }

            foreach (Activity activity in DataWorkspace.ApplicationData.Activities)
            {
                activity.Delete();
                DataWorkspace.ApplicationData.SaveChanges();
            }
            DataWorkspace.ApplicationData.SaveChanges();
            foreach (Contact activity in DataWorkspace.ApplicationData.Contacts)
            {
                activity.Delete();
            }
            DataWorkspace.ApplicationData.SaveChanges();
            foreach (SchoolPart activity in DataWorkspace.ApplicationData.SchoolParts)
            {
                activity.Delete();
            }
            DataWorkspace.ApplicationData.SaveChanges();
            foreach (School activity in DataWorkspace.ApplicationData.Schools)
            {
                activity.Delete();
            }
            DataWorkspace.ApplicationData.SaveChanges();
            foreach (Topic activity in DataWorkspace.ApplicationData.Topics)
            {
                activity.Delete();
            }
            DataWorkspace.ApplicationData.SaveChanges();

            foreach (TopicCollection activity in DataWorkspace.ApplicationData.TopicCollections)
            {
                activity.Delete();
            }
            DataWorkspace.ApplicationData.SaveChanges();
            foreach (Counselor activity in DataWorkspace.ApplicationData.Counselors)
            {
                activity.Delete();
            }
            DataWorkspace.ApplicationData.SaveChanges();
            foreach (Role activity in DataWorkspace.ApplicationData.Roles)
            {
                activity.Delete();
            }
            DataWorkspace.ApplicationData.SaveChanges();

            foreach (ActivityType activity in DataWorkspace.ApplicationData.ActivityTypes)
            {
                activity.Delete();
            }
            DataWorkspace.ApplicationData.SaveChanges();

            foreach (Audience activity in DataWorkspace.ApplicationData.Audiences)
            {
                activity.Delete();
            }

            DataWorkspace.ApplicationData.SaveChanges();

            foreach (City activity in DataWorkspace.ApplicationData.Cities)
            {
                activity.Delete();
            }
            DataWorkspace.ApplicationData.SaveChanges();

            foreach (District activity in DataWorkspace.ApplicationData.Districts)
            {
                activity.Delete();
            }
            DataWorkspace.ApplicationData.SaveChanges();

            foreach (SchoolType activity in DataWorkspace.ApplicationData.SchoolTypes)
            {
                activity.Delete();
            }
            DataWorkspace.ApplicationData.SaveChanges();

            foreach (ActivityStatus activity in DataWorkspace.ApplicationData.ActivityStatus)
            {
                activity.Delete();
            }

            DataWorkspace.ApplicationData.SaveChanges();



        }

        private Audience InsertAudience(Common.Grade grade)
        {
            Audience newAutdience = DataWorkspace.ApplicationData.Audiences.AddNew();
            newAutdience.Title = grade.ToString();
            return newAutdience;
        }

        private City InsertCity(Dictionary<int, District> districtById, Common.City city)
        {
            if (city.Title != null && city.Title != string.Empty)
            {
                City newCity = DataWorkspace.ApplicationData.Cities.AddNew();
                newCity.Title = city.Title != null ? city.Title : "";
                if (newCity.DefaultDistrict != null) newCity.DefaultDistrict = districtById[city.DefaultDistrict.Id];

                DataWorkspace.ApplicationData.SaveChanges();

                return newCity;
            }
            return null;
        }

        private ActivityType InsertActivityType(Common.ActivityType activityType)
        {

            ActivityType type = DataWorkspace.ApplicationData.ActivityTypes.AddNew();

            type.T = Convert(activityType);
            type.Id = (int)activityType;

            return type;
        }

        public string Convert(Common.ActivityType activityType)
        {
            switch (activityType)
            {
                case Common.ActivityType.Lecture:
                    return "הרצאה";
                    break;
                case Common.ActivityType.WorkshopRegular:
                    return "סדנה";
                    break;
                case Common.ActivityType.WorkshopHoleGrade:
                    return "סדנה שכבתית";
                    break;
                case Common.ActivityType.WorkshopEvening:
                    return "סדנת ערב";
                    break;
                case Common.ActivityType.SoundAndLightShow:
                    return "מייצג";
                    break;
                case Common.ActivityType.Trip:
                    return "סיור";
                    break;
                case Common.ActivityType.Bitmidrash:
                    return "בית מדרש ערב";
                    break;
                case Common.ActivityType.ParentsAndChildren:
                    return "פעילות הורים וילדים";
                    break;
                case Common.ActivityType.Show:
                    return "מופע";
                    break;
                case Common.ActivityType.Unknown:
                    return "לא ידוע";
                default:
                    return "";
                    break;
            }
        }


        private void InsertActivity(Dictionary<int, Audience> audiencById, Dictionary<int, Counselor> counselorById, Dictionary<int, Topic> topicById, Dictionary<int, ActivityType> activityTypeById, Dictionary<int, SchoolType> schoolTypeById,
                Dictionary<int, Role> roleById,
                Dictionary<int, District> districtById,
                Dictionary<string, City> cityById,
                Dictionary<string, SchoolPart> schoolPartById,
                Dictionary<string, Contact> contactById,Dictionary<int,ActivityStatus> activityStatusById, Common.Activity activity)
        {

            try
            {
                if (activity.School == null && activity.Contacte == null) return;
                Activity newActivity = DataWorkspace.ApplicationData.Activities.AddNew();
                if (activity.Rounds != null)
                {
                    newActivity.NumberOfRounds = activity.Rounds.Count;
                }
                newActivity.ActivityDate = activity.Date;
                newActivity.ActivityStatus = activityStatusById[(int)activity.ActivityStatus];
                newActivity.ActivityType = activityTypeById[(int)activity.ActivityType];
                newActivity.OrderComments = activity.OrderCommnet != null ? activity.OrderCommnet : "";
                newActivity.Audience = audiencById[(int)(activity.Grade)];
                newActivity.NumberOfCounselor = activity.NumberOfRequireCounselors;
                newActivity.TotalNumberOfClasses = activity.TotalNumberOfClass;
                newActivity.District = districtById[TestServerFacadeWrapper._district];
                if (activity.School != null)
                {
                    string key = activity.School.Id + "," + activity.School.District.Id;
                    if (schoolPartById.ContainsKey(key))
                    {
                        schoolPartById[key].Activities.Add(newActivity);
                        newActivity.SchoolPart = schoolPartById[key];
                        if (activity.Contacte != null)
                        {
                            string contactkey = activity.School.Id.ToString() + "," + activity.Contacte.Id.ToString() + "," + activity.School.District.Id;
                            newActivity.Contact = contactById[contactkey];
                            contactById[contactkey].Activities.Add(newActivity);
                        }
                    }
                    else
                    {
                        MessageBox.Show("activity has school with id =" + activity.School.Id + " not exist");
                    }
                }

                if (activity.Topic != null && topicById.ContainsKey(activity.Topic.Id))
                {
                    newActivity.Topic = topicById[activity.Topic.Id];
                }

                newActivity.Comments = activity.Comments;


                if (newActivity.Rounds.Count() > 0)
                {
                    foreach (Round round in newActivity.Rounds)
                    {
                        round.Delete();
                    }

                    foreach (Common.Round r in activity.Rounds)
                    {
                        Round newRound = newActivity.Rounds.AddNew();
                        newRound.StartTime = newActivity.ActivityDate.Add(r.StratTime);
                        newRound.StopTime = newActivity.ActivityDate.Add(r.StopTime);
                        newRound.NumberOfMeetingsInRound = r.NumberOfMeeting;
                        newRound.NumberOfStudentInMeeting = r.NumberOfStudentInMeeting;
                        newRound.Activity = newActivity;
                        foreach (Common.Meeting meeting in r.Meetings)
                        {
                            Meeting newMeeting = newRound.Meetings.AddNew();
                            if (meeting.MeetingSubject != null) newMeeting.Topic = topicById[meeting.MeetingSubject.Id];
                            if (meeting.Counselor != null)
                            {
                                if (counselorById.ContainsKey(meeting.Counselor.Id))
                                {
                                    newMeeting.Counselor = counselorById[meeting.Counselor.Id];
                                }
                                else
                                {

                                }
                            }
                            newMeeting.ClassNumber = meeting.NumOfClass;
                            if (meeting.Grade.HasValue) newMeeting.Audience = audiencById[(int)meeting.Grade];
                            else newMeeting.Audience = audiencById[(int)Common.Grade.אחר];

                            newMeeting.Round = newRound;
                            try
                            {
                                DataWorkspace.ApplicationData.SaveChanges();
                            }
                            catch(Exception ex2)
                            {

                            }
                        }

                    }
                }
                if (activity.Rounds.Count > 0)
                {
                    newActivity.StartTime = newActivity.ActivityDate.Add(activity.Rounds.First().StratTime);
                    newActivity.EndTime = newActivity.ActivityDate.Add(activity.Rounds.Last().StopTime);
                }

                foreach (Common.Proposal proposal in activity.ProposalsToCounselors)
                {
                    if (proposal.Counselor != null)
                    {
                        ActivityCounselor newActivityCounselor = DataWorkspace.ApplicationData.ActivityCounselors.AddNew();
                        newActivityCounselor.Activity = newActivity;
                        newActivityCounselor.Assigned = proposal.IsApprovedByManager;
                        newActivityCounselor.Counselor = counselorById[proposal.Counselor.Id];
                        newActivityCounselor.AsPermanent = proposal.IsAssingedAsPermenet;
                        newActivityCounselor.NumberOfRound = newActivity.NumberOfRounds;
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                int f = activity.Id;
            }
    

    

            DataWorkspace.ApplicationData.SaveChanges();


        }

        private LightSwitchApplication.Activity.ActivityStatusEnum ConvertToLWEnum(Common.ActivityStatus activityStatus)
        {
            LightSwitchApplication.Activity.ActivityStatusEnum s = Activity.ActivityStatusEnum.Create;
            switch (activityStatus)
            {
                case Common.ActivityStatus.Create:
                    s = LightSwitchApplication.Activity.ActivityStatusEnum.Create;
                    break;
                case Common.ActivityStatus.Assigned:
                    s = LightSwitchApplication.Activity.ActivityStatusEnum.Assigned;
                    break;
                case Common.ActivityStatus.Finished:
                    s = LightSwitchApplication.Activity.ActivityStatusEnum.Finished;
                    break;
                case Common.ActivityStatus.Cancel:
                    s = LightSwitchApplication.Activity.ActivityStatusEnum.Cancel;
                    break;
                case Common.ActivityStatus.Postponed:
                    s = LightSwitchApplication.Activity.ActivityStatusEnum.Postponed;
                    break;
                default:
                    break;
            }
            return s;
        }

        private Counselor InsertCounselor(Dictionary<string, City> cityById, Dictionary<int, District> districtById, Common.Counselor counselor)
        {
            Counselor newCounselor = DataWorkspace.ApplicationData.Counselors.AddNew();
            newCounselor.Adress = counselor.Adress;
            if (counselor.City != null && cityById.ContainsKey(counselor.City.Id.ToString() + counselor.City.Title)) newCounselor.City = cityById[counselor.City.Id.ToString() + counselor.City.Title];
            newCounselor.Email = counselor.Mail;
            newCounselor.PhoneNumber1 = counselor.PhoneNumber1;
            newCounselor.PhoneNumber2 = counselor.PhoneNumber2;
            newCounselor.LastName = counselor.LastName;
            newCounselor.FirstName = counselor.FirstName;
            newCounselor.District = districtById[TestServerFacadeWrapper._district];
            return newCounselor;
        }



        private Role InsertRole(Common.Role role)
        {
            Role newrole = DataWorkspace.ApplicationData.Roles.AddNew();

            newrole.Title = role.Title;

            return newrole;

        }

        private School InsertSchoolCollection(Dictionary<int, SchoolType> schoolTypeById, Dictionary<int, Role> roleById, Dictionary<int, District> districtById, Dictionary<string, City> cityById, List<Common.SchoolCollection> schoolCollections, ref Dictionary<string, SchoolPart> schoolPartById, ref Dictionary<string, Contact> contactById)
        {
            School school = DataWorkspace.ApplicationData.Schools.AddNew();
            school.Titel = schoolCollections[0].Title;

            foreach (Common.SchoolCollection schoolCollection in schoolCollections)
            {

                if (school.Titel == null || school.Titel == "") school.Titel = " ";

                // DataWorkspace.ApplicationData.SaveChanges();

                foreach (Common.School importSchool in schoolCollection.Schools)
                {

                    SchoolPart schoolPart = school.SchoolParts.AddNew();

                    //schoolPart.City 
                    schoolPart.SchoolName = importSchool.SchoolName;
                    schoolPart.Comments = importSchool.Comments != null ? importSchool.Comments : ""; ;
                    schoolPart.Adress = importSchool.Adress;
                    schoolPart.PhoneNumber1 = importSchool.PhoneNumber1;
                    schoolPart.PhoneNumber2 = importSchool.PhoneNumber2;
                    schoolPart.FaxNumber = importSchool.FaxNumber;
                    schoolPart.District = districtById[importSchool.District.Id];
                    districtById[importSchool.District.Id].SchoolParts.Add(schoolPart);
                    schoolPart.SchoolType = schoolTypeById[importSchool.SchoolType.Id];
                    schoolTypeById[importSchool.SchoolType.Id].SchoolParts.Add(schoolPart);
                    schoolPart.City = cityById[importSchool.City.Id.ToString() + importSchool.City.Title];
                    schoolPart.School = school;
                    school.SchoolParts.Add(schoolPart);
                    schoolPart.School = school;
                    string key = importSchool.Id + "," + importSchool.District.Id;
                    schoolPartById.Add(key, schoolPart);

                    DataWorkspace.ApplicationData.SaveChanges();


                    foreach (Common.Contacte c in importSchool.Conactees)
                    {
                        Contact newContact = DataWorkspace.ApplicationData.Contacts.AddNew();
                        newContact.FullName = c.Name;
                        newContact.PhoneNumber1 = c.PhoneNumber1;
                        newContact.PhoneNumber2 = c.PhoneNumber2;
                        newContact.Email = c.Mail;
                        if (c.Role != null) newContact.Role = roleById[c.Role.Id];
                        contactById.Add(importSchool.Id.ToString() + "," + c.Id.ToString() + "," + importSchool.District.Id, newContact);
                        schoolPart.Contacts.Add(newContact);
                        newContact.SchoolPart = schoolPart;
                    }
                    DataWorkspace.ApplicationData.SaveChanges();
                }
            }
            return school;
            // DataWorkspace.ApplicationData.SaveChanges();
        }

        private SchoolType InsertSchoolType(Common.SchoolType schoolType)
        {
            SchoolType type = DataWorkspace.ApplicationData.SchoolTypes.AddNew();
            type.Id = schoolType.Id;
            type.Title = schoolType.Title;

            return type;
        }

        private void InsertTopicCollection(Dictionary<int, ActivityType> activityTypeById, Common.TopicsCollection topicCollection, ref Dictionary<int, Topic> topicById)
        {
            TopicCollection importItem = DataWorkspace.ApplicationData.TopicCollections.AddNew();

            importItem.ActivityType = activityTypeById[(int)topicCollection.CollectionType];

            importItem.Title = topicCollection.Title;

            foreach (Common.Topic topic in topicCollection.Topics)
            {
                Topic importTopic = DataWorkspace.ApplicationData.Topics.AddNew();

                importTopic.Title = topic.Title;

                importTopic.TopicCollection = importItem;

                importItem.Topics.Add(importTopic);

                topicById.Add(topic.Id, importTopic);
            }

            DataWorkspace.ApplicationData.SaveChanges();








        }
        public TestServerFacadeWrapper DeSerilizeServerSide(Stream file)
        {



            DataContractSerializer serializer = new DataContractSerializer(typeof(TestServerFacadeWrapper));

            XmlReader reader = XmlReader.Create(file);

            object serverSide = serializer.ReadObject(reader);


            TestServerFacadeWrapper serverFacade = (TestServerFacadeWrapper)serverSide;

            if (file != null) file.Close();


            return serverFacade;


        }

        #endregion

        partial void EditSelectedActivity_Execute()
        {
            if (ActivityViews.SelectedItem != null)
            {
                Application.ShowCreateNewOrEditActivity(ActivityViews.SelectedItem.ActivityId);
            }

        }

        partial void AddNewActivity_Execute()
        {
            //this.OpenModalWindow("CreateNewOrEditActivity");
            Application.ShowCreateNewOrEditActivity(null);
            

        }

        partial void FinishAssiged_Execute()
        {
            if (ActivityViews.SelectedItem != null)
            {
                Application.ShowRoundsListDetail(ActivityViews.SelectedItem.ActivityId);
            }


        }

        partial void DuplicateActivities_Execute()
        {
            current = Dispatchers.Current;
            if (this.ActivityViews.SelectedItem != null)
            {
                
                Dispatchers.Main.BeginInvoke(() =>
                {
                    Input f = new Input();
                    f.Title = "שכפול פעילות";
                    f.InputCapture = "כמה פעמים תרצה לשכפל?";
                    f.Closed += new EventHandler(f_Closed);
                    f.Show();

                    
                    
                });



            }
        }

        void f_Closed(object sender, EventArgs e)
        {
            Input form = (Input)sender;

            int howmuchToDuplicate = 0;

            if (form.DialogResult.Value == true)
            {
                int.TryParse(form.Value, out howmuchToDuplicate);

                current.BeginInvoke(() =>
                {
                    Activity duplicateFrom = this.DataWorkspace.ApplicationData.Activities_Single(ActivityViews.SelectedItem.ActivityId);
                    if (duplicateFrom != null)
                    {
                        for (int i = 0; i < howmuchToDuplicate; i++)
                        {
                            Activity duplicateTo = this.DataWorkspace.ApplicationData.Activities.AddNew();
                            duplicateFrom.DuplicateActivity(duplicateTo);

                            this.Application.ShowCreateNewOrEditActivity(duplicateTo.Id);

                        }

                        this.Refresh();
                    }
                });

            }


        }

       
        partial void AssignActivity_Execute()
        {
            if (this.ActivityViews.SelectedItem != null)
            {
                Application.ShowAssigningProcess(this.ActivityViews.SelectedItem.ActivityId,this.ActivityViews.SelectedItem.DistrictId,this.ActivityViews.SelectedItem.ActivityDate);
            }

        }

        partial void SearchActivityViews_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            this.ActivityViewFromDate = DateTime.Now;

            this.ActivityViewToDate = DateTime.Now.AddMonths(1);

            saveChangesTo.Add(DataWorkspace.ApplicationData);

        }

        partial void Export_Execute()
        {
            current = Dispatchers.Current;
            Dispatchers.Main.BeginInvoke(() =>
            {
                SelectFileWindow.FileWindowArgument Argument = new SelectFileWindow.FileWindowArgument { FileAccess = System.IO.FileAccess.Write, FileFilter = "csv (*.csv)|*.csv" };

                SelectFileWindow selectFileWindow = new SelectFileWindow(Argument);

                selectFileWindow.Closed+=new EventHandler(selectFileWindowForExport_Closed);

                selectFileWindow.Show();

            });


        }
        void selectFileWindowForExport_Closed(object sender, EventArgs e)
        {
            SelectFileWindow selectFileWindow = (SelectFileWindow)sender;


            StreamWriter stream = new StreamWriter(selectFileWindow.DocumentStream[0],Encoding.UTF8);
            stream.Write(GetCSV());
            stream.Close();
            

        }
        private string GetCSV()
        {
            StringBuilder csv = new StringBuilder();

            csv.AppendFormat(ActivityView.CSVHeader() + System.Environment.NewLine);

            foreach (ActivityView view in this.ActivityViews)
            {
                
                csv.AppendFormat(view.ToCSV() +System.Environment.NewLine
                                    , view);
            }

            return csv.ToString(0, csv.Length - 1);
        }

        partial void Method_Execute()
        {
            this.ActivityViews.Refresh();

        }

        partial void DeleteActivity_Execute()
        {
            if (ActivityViews.SelectedItem!=null)
            {
                MessageBoxResult result = MessageBoxResult.No;

                Dispatchers.Main.Invoke(() =>
                {

                    result = MessageBox.Show("הפעילות עומדת להמחק, האם את/ה מאשר?", "מחיקת פעילות", MessageBoxButton.OKCancel);
                    
                });

                if (result == MessageBoxResult.OK)
                {
                    Activity activity = DataWorkspace.ApplicationData.Activities_SingleOrDefault(ActivityViews.SelectedItem.ActivityId);

                    if (activity != null)
                    {
                    
                     
                        activity.ActivityStatus = this.DataWorkspace.ApplicationData.ActivityByInternalId((int)Activity.ActivityStatusEnum.Cancel).Execute().ToList()[0];
                        this.DataWorkspace.ApplicationData.SaveChanges();
                    }
                }

                
                
            }

        }

        partial void SearchActivityViews_Saving(ref bool handled)
        {
            DataWorkspace.ApplicationData.SaveChanges();
            handled = true;
            this.ActivityViews.Refresh();

        }

        partial void newEdit_Execute()
        {
            if (ActivityViews.SelectedItem != null)
            {
                Application.ShowCreateNewActivity(ActivityViews.SelectedItem.ActivityId);
            }

        }

        partial void ActivityViews_Changed(NotifyCollectionChangedEventArgs e)
        {
            this.NumberOfActivity = ActivityViews.Count;
        }



      
    }
}