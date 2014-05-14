using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
//using SampleServiceClient;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Configuration;
using Contract;
using System.ServiceModel;

namespace ServerSide
{

    public class TestServerFacadeWrapper : IServerFacadeWrapper, IDisposable
    {
        string CurrentDirectory { get; set; }

        List<Order> orders = new List<Order>();

        //List<School> schools = new List<School>();
        public List<School> Schools()
        {
            List<School> schools = new List<School>();

            foreach (SchoolCollection schoolcollection in SchoolCollections)
            {
                schools.AddRange(schoolcollection.Schools.Where(school => school.District.Id == _district));
            }

            return schools;
        }
        public List<School> AllSchools()
        {

            List<School> schools = new List<School>();

            foreach (SchoolCollection schoolcollection in SchoolCollections)
            {
                schools.AddRange(schoolcollection.Schools);
            }

            return schools;
        }



        public List<Counselor> counselors = new List<Counselor>();
        List<District> districts = new List<District>();
        List<City> cities = new List<City>();
        List<Role> roles = new List<Role>();
        List<SchoolType> schoolTypes = new List<SchoolType>();
        List<Contacte> contactees = new List<Contacte>();
        List<TopicsCollection> topicCollectionList = new List<TopicsCollection>();
        List<SchoolCollection> schoolCollections = new List<SchoolCollection>();

        public TestServerFacadeWrapper()
        {
        }

        #region Init


        public void Init()
        {
            try
            {

                CurrentDirectory = Environment.CurrentDirectory;

                //ImportSchool();

                //ImportTopics();

                //ImportCounselors();


            }
            catch (Exception ex)
            {
                throw new Exception("problem in Init in ServerFacadeWrapper" + ex.Message);
            }


        }

        //public void ImportSchool()
        //{
        //    string fileFullPath = CurrentDirectory + "\\schools.xls";

        //    ExcelReaderInterop excel = new ExcelReaderInterop();

        //    SchoolData data = excel.ImportSchool(fileFullPath);

        //    districts = data.districts.Values.ToList();

        //    roles = data.RolesById.Values.ToList();

        //    schoolTypes = data.schoolTypes.Values.ToList();


        //    schoolCollections = data.schoolCollections.Values.ToList();

        //    cities = data.cities.Values.ToList();

        //}

        //public void ImportTopics()
        //{

        //    string topicFilePath = CurrentDirectory + "\\topics.xls";
        //    ExcelReaderInterop excel = new ExcelReaderInterop();

        //    //c + "\\topics.xls"
        //    TopicData topicData = excel.ImportTopics(topicFilePath);

        //    topicCollectionList = topicData.TopicCollections;

        //}

        //public void ImportCounselors()
        //{

        //    string fileFullPath = CurrentDirectory + "\\counselors.xls";

        //    ExcelReaderInterop excel = new ExcelReaderInterop();

        //    CounselorsData counselorData = excel.ImportCounselors(ref cities, fileFullPath);

        //    counselors = counselorData.Counselors;
        //}

        private List<Contacte> GeneratwContactees()
        {
            List<Contacte> Contactees = new List<Contacte>();
            for (int i = 0; i < 10; i++)
            {
                Contacte contact = new Contacte();
                contact.Id = 1;
                contact.IsCenteral = RandomBool(i);
                contact.Mail = RandomMail();
                contact.Name = RandomFirstName(i);
                contact.PhoneNumber1 = RandomNumber();
                contact.PhoneNumber2 = RandomNumber();
                contact.Role = RandomRole();
                contactees.Add(contact);
            }

            return contactees;
        }

        //private List<Order> GenerateOrders()
        //{
        //    List<Order> orders = new List<Order>();
        //    for (int i = 0; i < 10; i++)
        //    {
        //        Order order = new Order();
        //        order.IsSchoolApprove = RandomBool(i);
        //        order.School = RandSchool();

        //        order.Activities = new Activity[2];
        //        order.Activities[0] = GenerateRandomActivity(i);
        //        order.Activities[1] = GenerateRandomActivity(i + 10);
        //        orders.Add(order);
        //    }
        //    return orders;
        //}

        //private Activity GenerateRandomActivity(int i)
        //{
        //    Activity activity = new Activity();
        //    activity.Date = DateTime.Now;
        //    activity.Date += new TimeSpan(new Random(i).Next(30), 0, 0);
        //    activity.DayInWeek = activity.Date.DayOfWeek;
        //    activity.Comments ="פעילות + " + new Random(i).Next(50);
        //    activity.Id = i;
        //    activity.NumberOfRequireCounselors = new Random(i).Next(40);
        //    activity.Rounds = new Round[3];
        //    activity.School = schools[new Random(i).Next(schools.Count)];
        //    activity.ActivityStatus = new ActivityStatus();
        //    activity.ProposalsToCounselors = new Proposal[0];
        //    activity.CounselorsAssigned = new Counselor[0];
        //    activity.CounselorsApproved = new Counselor[0];
        //    activity.ActivityStatus = (ActivityStatus)new Random(i).Next(5);
        //    activity.NumberOfMissingCounselors = activity.NumberOfRequireCounselors - activity.CounselorsAssigned.Count();
        //    Topic topic = new Topic();
        //    topic.Id = 1;
        //    topic.Title = "חנוכה1";
        //    activity.Topic = topic;
        //    Round r1 = new Round();
        //    r1.NumberOfMeeting = 3;
        //    r1.Meetings = new Meeting[3];

        //    Meeting metting1 = new Meeting();
        //    metting1.AgeOfStudent = 9;
        //    metting1.NumOfClass = 7;
        //    metting1.MeetingSubject = topic;

        //    Meeting metting2 = new Meeting();
        //    metting2.AgeOfStudent = 9;
        //    metting2.NumOfClass = 8;
        //    metting2.MeetingSubject = topic;

        //    Meeting metting3 = new Meeting();
        //    metting3.AgeOfStudent = 9;
        //    metting3.NumOfClass = 9;
        //    metting3.MeetingSubject = topic;

        //    r1.Meetings[0] = metting1;
        //    r1.Meetings[1] = metting2;
        //    r1.Meetings[2] = metting3;

        //    Round r2 = new Round();
        //    r2.NumberOfMeeting = 3;
        //    r2.Meetings = new Meeting[3];

        //    metting1 = new Meeting();
        //    metting1.AgeOfStudent = 9;
        //    metting1.NumOfClass = 1;
        //    metting1.MeetingSubject = topic;

        //    metting2 = new Meeting();
        //    metting2.AgeOfStudent = 9;
        //    metting2.NumOfClass = 2;
        //    metting2.MeetingSubject = topic;

        //    metting3 = new Meeting();
        //    metting3.AgeOfStudent = 9;
        //    metting3.NumOfClass = 3;
        //    metting3.MeetingSubject = topic;

        //    r2.Meetings[0] = metting1;
        //    r2.Meetings[1] = metting2;
        //    r2.Meetings[2] = metting3;

        //    Round r3 = new Round();
        //    r3.NumberOfMeeting = 3;
        //    r3.Meetings = new Meeting[3];

        //    metting1 = new Meeting();
        //    metting1.AgeOfStudent = 9;
        //    metting1.NumOfClass = 4;
        //    metting1.MeetingSubject = topic;

        //    metting2 = new Meeting();
        //    metting2.AgeOfStudent = 9;
        //    metting2.NumOfClass = 5;
        //    metting2.MeetingSubject = topic;

        //    metting3 = new Meeting();
        //    metting3.AgeOfStudent = 9;
        //    metting3.NumOfClass = 6;
        //    metting3.MeetingSubject = topic;

        //    r3.Meetings[0] = metting1;
        //    r3.Meetings[1] = metting2;
        //    r3.Meetings[2] = metting3;

        //    activity.Rounds[0] = r1;
        //    activity.Rounds[1] = r2;
        //    activity.Rounds[2] = r3;


        //    return activity;
        //}

        private List<SchoolType> GenerateSchoolTypes()
        {
            List<SchoolType> schoolTypes = new List<SchoolType>();
            //generate schoolType             
            SchoolType schoolType = new SchoolType();
            schoolType.Id = 1;
            schoolType.Title = "חט''ב";
            schoolTypes.Add(schoolType);

            return schoolTypes;

        }



        private List<School> GenerateRandomSchool()
        {
            List<School> schools = new List<School>();
            for (int i = 0; i < 10; i++)
            {
                School school = new School();
                school.Adress = "";
                school.City = RandomCity(i);
                school.District = RandomDistrict();
                school.FaxNumber = RandomNumber();
                school.Id = i;
                school.PhoneNumber1 = RandomNumber();
                school.PhoneNumber2 = RandomNumber();
                school.Conactees = new List<Contacte>();
                school.Conactees.Add(RandomContacte(i));
                school.SchoolType = RandomSchoolType(i);
                //                school.Title = RandomSchoolTitle(i);
                schools.Add(school);
            }
            return schools;
        }

        private string RandomSchoolTitle(int i)
        {
            return "בית ספר" + new Random(i).Next(30);
        }

        private Contacte RandomContacte(int i)
        {
            Random random = new Random(i);
            int index = random.Next(contactees.Count);
            return contactees[index];
        }

        private SchoolType RandomSchoolType(int i)
        {
            Random random = new Random(i);
            int index = random.Next(schoolTypes.Count);
            return schoolTypes[index];
        }

        private string RandomNumber()
        {
            return new Random().Next(9999999).ToString();
        }

        private District RandomDistrict()
        {
            Random random = new Random();
            int index = random.Next(districts.Count);
            return districts[index];
        }

        private List<Role> GenerateRole()
        {
            List<Role> roles = new List<Role>();
            Role role = new Role();
            role.Id = 1;
            role.Title = "מדריך";
            roles.Add(role);
            return roles;
        }



        private City RandomCity(int i)
        {
            Random random = new Random(i + 1);
            int index = random.Next(cities.Count);
            return cities[index];
        }

        private List<City> GenerateRandomCity()
        {
            List<City> cities = new List<City>();
            for (int i = 0; i < 10; i++)
            {
                City city = new City();
                city.Id = i;
                city.Title = RandomCityTitle(i);
                cities.Add(city);
            }
            return cities;
        }

        private string RandomCityTitle(int i)
        {
            return "עיר מספר" + new Random(i + 2).Next(30);
        }

        private List<Counselor> GenerateRandonCounselors()
        {
            List<Counselor> counselors = new List<Counselor>();

            for (int i = 0; i < 60; i++)
            {

                PermanentInfo info = new PermanentInfo();
                info.IsPermanent = RandomBool(i);
                info.PermanentDay = DayOfWeek.Sunday;

                Counselor counselor = new Counselor();
                counselor.PermanentInfo = info;
                counselor.Id = i;



                counselor.FreeDays = GetRandomDayOfWeekArray(i);
                counselor.FlexDays = GetRandomDayOfWeekArray(i + 10);

                counselor.Adress = RandomAdress(i);
                counselor.City = RandomCity(i);
                counselor.District = RandomDistrict();
                counselor.HasCar = RandomBool(i);
                counselor.Mail = RandomMail();
                counselor.PhoneNumber1 = RandomNumber();
                counselor.PhoneNumber2 = RandomNumber();
                counselor.FirstName = RandomFirstName(i);
                counselor.LastName = RandomLastName(i);
                counselor.FullName = counselor.FirstName + " " + counselor.LastName;
                counselors.Add(counselor);

            }
            return counselors;
        }

        private DayOfWeek[] GetRandomDayOfWeekArray(int i)
        {
            DayOfWeek[] days = new DayOfWeek[3];
            for (int k = 0; k < days.Length; k++)
            {
                days[k] = GetRandDay(i + k);
            }
            return days;

        }

        private DayOfWeek GetRandDay(int p)
        {
            Random random = new Random(p);
            int index = random.Next(6);
            return (DayOfWeek)index;
        }

        private Role RandomRole()
        {
            Random random = new Random();
            int index = random.Next(roles.Count);
            return roles[index];
        }

        private string RandomMail()
        {
            return "aviad.natovich@gmail.com" + new Random().Next(20);
        }

        private string RandomLastName(int i)
        {
            return "שם משפחה" + new Random(i + 3).Next(30);
        }

        private string RandomFirstName(int i)
        {
            return "שם פרטי" + new Random(i).Next(30);
        }

        private string RandomAdress(int i)
        {
            return "בן גוריון" + new Random(i + 4).Next(20);
        }

        private bool RandomBool(int j)
        {
            Random r = new Random(j);
            int i = r.Next(2);
            return i == 0 ? true : false;
        }

        #endregion

        #region ICity Members

        public List<City> Cities
        {
            get
            {
                return cities;
            }
        }

        public City GetCity(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICounselors Members

        public List<Counselor> Counselors
        {
            get
            {
                List<Counselor> counselorsInDistrict = counselors.Where(c => c.District.Id == _district).ToList();

                foreach (Counselor counselor in counselorsInDistrict)
                {
                    City city = cities.FirstOrDefault(c => c.Id == counselor.CityId);

                    if (city != null)
                    {
                        counselor.City = city;
                    }
                    else
                    {
                        counselor.City = new City(-1, "");
                    }
                }
                return counselorsInDistrict;
            }
            set
            {
                counselors = value;
            }
        }

        #endregion

        #region IOrder Members

        public List<Order> AllOrder
        {
            get { return orders; }
        }

        #endregion

        #region IActivity Members

        public List<Activity> Activites()
        {
            List<Activity> activities = new List<Activity>();
            foreach (Order order in orders)
            {
                foreach (Activity activity in order.Activities)
                {
                    if (activity != null)
                    {
                        activity.School = GetSchool(activity.SchoolId);

                        activity.Topic = Topics().FirstOrDefault(topic => topic.Id == activity.TopicId);

                        activity.CounselorsApproved = new List<Counselor>();

                        foreach (int counselorsApprovedId in activity.CounselorsApprovedIds)
                        {
                            Counselor counselor = counselors.FirstOrDefault(c => c.Id == counselorsApprovedId);

                            if (counselor != null)
                            {
                                activity.CounselorsApproved.Add(counselor);
                            }
                        }

                        activity.CounselorsAssigned = new List<Counselor>();

                        foreach (int counselorsAssignedId in activity.CounselorsAssignedIds)
                        {
                            Counselor counselor = counselors.FirstOrDefault(c => c.Id == counselorsAssignedId);
                            if (counselor != null)
                            {
                                activity.CounselorsAssigned.Add(counselor);
                            }
                        }
                        if (activity.School != null)
                        {
                            Contacte contactee = activity.School.Conactees.FirstOrDefault(c => c.Id == activity.ContacteId);

                            if (contactee != null)
                            {
                                activity.Contacte = contactee;
                            }
                        }
                        if (activity.ProposalsToCounselors != null)
                        {
                            foreach (Proposal proposal in activity.ProposalsToCounselors)
                            {
                                Counselor counselor = counselors.FirstOrDefault(c => c.Id == proposal.CounselorId);
                                if (counselor != null)
                                {
                                    proposal.Counselor = counselor;
                                }
                            }

                        }

                        activities.Add(activity);


                    }
                }

            }
            activities.Sort(new DateCompare());
            return activities;
        }
        public class DateCompare : IComparer<Activity>
        {

            #region IComparer<Activity> Members

            public int Compare(Activity x, Activity y)
            {
                if (x.Date == y.Date)
                {
                    if (x.Rounds.Count == 0 && y.Rounds.Count > 0)
                    {
                        return -1;
                    }
                    else if (x.Rounds.Count > 0 && y.Rounds.Count == 0)
                    {
                        return 1;
                    }
                    else if (x.Rounds.Count == 0 && y.Rounds.Count == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return x.Rounds[0].StratTime.CompareTo(y.Rounds[0].StratTime);
                    }
                }
                else
                {
                    return x.Date.CompareTo(y.Date);
                }
            }

            #endregion
        }
        public Activity GetActivity(int id)
        {
            return (from Activity a in Activites()
                    where a.Id == id
                    select a).First();
        }

        public Activity CreateNewActivity()
        {

            int maxId = 0;

            var activityId = (from Activity act in Activites()
                              select act.Id);
            if (activityId.Count() > 0) maxId = activityId.Max();

            Activity activity = new Activity(maxId + 1);

            Order order = CreateNewOrder();

            order.Activities.Add(activity);

            activity.OrderId = order.Id;

            orders.Add(order);


            return activity;
        }

        public bool Save(Activity activity)
        {
            activity.ModifyTime = DateTime.Now;

            if (activity.School != null)
            {
                activity.SchoolId = activity.School.Id;
            }
            if (activity.Topic != null)
            {
                activity.TopicId = activity.Topic.Id;
            }
            if (activity.Contacte != null)
            {
                activity.ContacteId = activity.Contacte.Id;
            }

            activity.CounselorsApprovedIds.Clear();

            foreach (Counselor c in activity.CounselorsApproved)
            {
                activity.CounselorsApprovedIds.Add(c.Id);
            }

            activity.CounselorsAssignedIds.Clear();

            foreach (Counselor c in activity.CounselorsAssigned)
            {
                activity.CounselorsAssignedIds.Add(c.Id);
            }

            foreach (Round r in activity.Rounds)
            {
                foreach (Meeting m in r.Meetings)
                {
                    if (!m.Grade.HasValue)
                    {
                        m.Grade = activity.Grade;
                    }
                }
            }

            if (activity.Topic != null)
            {
                foreach (Round r in activity.Rounds)
                {
                    foreach (Meeting m in r.Meetings)
                    {
                        m.MeetingSubject = activity.Topic;
                    }
                }
            }

            activity.ReCalculatePayment();

            //TO DO: send the new/changed activity to server side;
            //TO DO: if the activity send the new/changed Order

            SerilizeServerSide();

            return true;
        }

        public void SerilizeServerSide()
        {



            FileStream fileStream = null;

            try
            {

                File.Delete(filename);

                fileStream = new FileStream(filename, FileMode.OpenOrCreate);

                XmlDictionaryWriter reader = XmlDictionaryWriter.CreateTextWriter(fileStream, Encoding.UTF8);

                DataContractSerializer serilize = new DataContractSerializer(typeof(TestServerFacadeWrapper));

                serilize.WriteObject(fileStream, this);


            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
            return;

        }
        private static string filename;
        public static int _district = 0;
        public static TestServerFacadeWrapper DeSerilizeServerSide(int district)
        {
            filename = "database";
            _district = district;
            filename = filename + district + ".xml";
            FileStream fileStream = null;
            try
            {
                if (File.Exists(filename))
                {
                    fileStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);

                    //XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas());

                    DataContractSerializer serializer = new DataContractSerializer(typeof(TestServerFacadeWrapper));

                    object serverSide = serializer.ReadObject(fileStream);

                    TestServerFacadeWrapper serverFacade = (TestServerFacadeWrapper)serverSide;

                    serverFacade.CurrentDirectory = Environment.CurrentDirectory;

                    // serverFacade.ImportTopics();

                    //serverFacade.ImportCounselors();

                    return serverFacade;
                }
                else
                {
                    TestServerFacadeWrapper newServerSide = new TestServerFacadeWrapper();

                    newServerSide.Init();
                    return newServerSide;

                }
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }


        }

        #endregion

        #region IServerFacadeWrapper Members

        public List<Counselor> CounselorsNotProposes(Activity CurrentActivity)
        {
            List<Counselor> counselorsPropose = (from Proposal p in CurrentActivity.ProposalsToCounselors
                                                 select p.Counselor).ToList();

            List<Counselor> counselorsNotPropose = (from Counselor c in Counselors
                                                    where !counselorsPropose.Contains(c)
                                                    select c).ToList();

            return counselorsNotPropose;
        }

        #endregion

        #region IServerFacadeWrapper Members

        //the opens should not contain counselor that already had proposal
        public void Save(Activity activity, List<OpenToAssign> opens)
        {
            //activity.ModifyTime = DateTime.Now;

            //Activity working_activity = Activites().Find(a => a.Id == activity.Id);

            //List<Proposal> currentProposals = working_activity.ProposalsToCounselors.ToList();

            //foreach (OpenToAssign open in opens)
            //{
            //    Proposal propose = new Proposal(open.Counselor);

            //    propose.NumOfRoundPropose = 3;
            //    if (!working_activity.ProposalsToCounselors.Contains(propose))
            //    {
            //        propose.IsAssingedAsPermenet = open.Counselor.PermanentInfo.IsPermanent;
            //    }

            //    currentProposals.Add(propose);
            //}

            //working_activity.ReCalculatePayment();
            //working_activity.ProposalsToCounselors = currentProposals;

            //SerilizeServerSide();


        }





        #endregion

        #region IServerFacadeWrapper Members


        public void Save(Activity currentActivity, List<Proposal> Proposals)
        {
            //currentActivity.ModifyTime = DateTime.Now;

            //Activity working_activity = Activites().Find(a => a.Id == currentActivity.Id);

            //working_activity.ProposalsToCounselors = Proposals;

            //working_activity.CounselorsAssigned = Proposals.Where(p => p.IsApprovedByManager).Select(p => p.Counselor).ToList();
            //working_activity.CounselorsApproved = Proposals.Where(p => p.CounselorAnswer == CounselorAnswer.Approve).Select(p => p.Counselor).ToList();

            //working_activity.ReCalculatePayment();

            //SerilizeServerSide();

        }

        #endregion

        #region ISchool Members



        public School GetSchool(int id)
        {
            return Schools().FirstOrDefault(s => s.Id == id);
        }

        public bool Save(School school)
        {
            //int cityId = school.City != null ? school.City.Id : -1;

            //School working_school = Schools().Find(a => a.Id == school.Id);

            //if (cityId != -1)
            //{
            //    working_school.CityId = cityId;
            //}
            //working_school = school;

            return true;

        }
        public List<SchoolType> SchoolTypes
        {
            get
            {
                return schoolTypes;
            }
        }
        #endregion

        #region ITopic Members

        public List<Topic> Topics()
        {
            List<Topic> topics = new List<Topic>();
            foreach (TopicsCollection collection in topicCollectionList)
            {
                topics.AddRange(collection.Topics);
            }

            return topics;
        }
        public List<TopicsCollection> TopicCollections
        {
            get { return topicCollectionList; }
        }

        #endregion

        #region IServerFacadeWrapper Members


        public List<District> Districts
        {
            get
            {
                return districts;
            }

        }


        #endregion

        #region IOrder Members


        public Order CreateNewOrder()
        {
            Order order = new Order();

            int maxId = 1;
            var ordersId = from Order o in orders
                           select o.Id;
            if (ordersId.Count() > 0)
            {
                maxId = ordersId.Max();
            }

            order.Id = maxId + 1;

            order.Activities = new List<Activity>();

            return order;
        }

        #endregion

        #region IActivity Members

        //public ObservableCollection<Activity> GetActivityByCounselorId(int id)
        //{
        //    ObservableCollection<Activity> activities = new ObservableCollection<Activity>();

        //    foreach (Activity activity in Activites())
        //    {
        //        foreach (Proposal propose in activity.ProposalsToCounselors)
        //        {
        //            if (propose.Counselor.Id == id)
        //            {
        //                activities.Add(activity);
        //                break;
        //            }
        //        }
        //    }
        //    return activities;
        //}
        #endregion

        #region IActivity Members


        //public List<OrderOfEquiment> GetEquipment()
        //{
        //    List<OrderOfEquiment> equipment = new List<OrderOfEquiment>();

        //    foreach (Activity activity in Activites())
        //    {

        //        List<OrderOfEquiment> activityEquipment = GetEquipment(activity);

        //        equipment.AddRange(activityEquipment);
        //    }





        //    return equipment;

        //}

        //private List<OrderOfEquiment> GetEquipment(Activity activity)
        //{
        //    Dictionary<int, OrderOfEquiment> ordersByTopicId = new Dictionary<itn, OrderOfEquiment>();
        //    foreach (var round in activity.Rounds)
        //    {
        //        foreach (var meeting in round.Meetings)
        //        {

        //        }

        //    }
        //}

        #endregion

        #region IActivity Members


        public void DeleteActivity(Activity activity)
        {
            //Order order = orders.First(ord => ord.Activities.FindAll(act => act.Id == activity.Id).Count() > 0);
            //if (order != null)
            //{
            //    order.Activities.Remove(activity);
            //}
            //SerilizeServerSide();

        }

        #endregion

        #region ISchool Members


        public List<SchoolCollection> SchoolCollections
        {
            get
            {
                foreach (SchoolCollection c in schoolCollections)
                {
                    foreach (School school in c.Schools)
                    {
                        school.SchoolCollectionName = c.Title;

                        school.City = cities.FirstOrDefault(city => city.Id == school.CityId);
                    }
                }
                return schoolCollections;
            }
        }

        #endregion

        #region ISchool Members


        public School CreatNewSchool()
        {
            

            School school = new School();

            school.Id = GetNewSchoolId();

            return school;
        }

        private int GetNewSchoolId()
        {
            int maxId = 1;

            var schoolIds = (from School school in AllSchools()
                             select school.Id);
            if (schoolIds.Count() > 0) maxId = schoolIds.Max();

            return maxId + 1;
        }

        private int GetNewCityId()
        {
            int maxId = 1;

            var cityIds = (from City city in cities
                           select city.Id);
            if (cityIds.Count() > 0) maxId = cityIds.Max();

            return maxId + 1;
        }


        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.SerilizeServerSide();
        }

        #endregion

        #region ISchool Members


        public List<Role> Roles
        {
            get { return roles; }
        }

        #endregion

        #region ISchool Members


        public Contacte CreatNewContacte(School school)
        {
            int contacteeId = 1;
            if (school.Conactees.Count > 0)
            {
                contacteeId = (from School _school in Schools()
                               from Contacte contact in _school.Conactees
                               select contact.Id).Max();
            }
            Contacte newContact = new Contacte();
            newContact.Id = contacteeId + 1;

            return newContact;
        }

        #endregion

        #region ISchool Members


        public void AddCity(City city)
        {
            city.Id = GetNewCityId();

            cities.Add(city);
        }

        #endregion

        #region ISchool Members


        public void AddSchoolCollection(SchoolCollection schoolCollection)
        {
            schoolCollection.Id = GetNewSchoolCollectionId();

            SchoolCollections.Add(schoolCollection);
        }

        private int GetNewSchoolCollectionId()
        {
            int maxId = 1;

            var schoolCollectionIds = (from SchoolCollection sc in SchoolCollections
                                       select sc.Id);
            if (schoolCollectionIds.Count() > 0) maxId = schoolCollectionIds.Max();

            return maxId + 1;
        }

        #endregion

        #region ISchool Members


        public bool Save(SchoolCollection selectedSchoolCollection)
        {
            //SchoolCollection schoolCollection = SchoolCollections.Find(a => a.Id == selectedSchoolCollection.Id);

            //schoolCollection = selectedSchoolCollection;

            return true;
        }

        #endregion

        #region ICity Members


        public List<City> GetCitiesOfSchools()
        {
            var retVal = (from School school in Schools()
                          select school.City).Distinct().ToList();
            return retVal;
        }

        #endregion

        #region ISchool Members


        public void DeleteSchool(School school)
        {
            SchoolCollection schoolCollection = schoolCollections.FirstOrDefault(col => col.Id == school.SchoolCollectionId);
            if (schoolCollection != null)
            {
                schoolCollection.Schools.Remove(school);
            }
            SerilizeServerSide();

        }

        #endregion

        #region ISchool Members


        public void MoveSchool(int schoolCollectionOld, int schoolCollectionNew, School currentSchool)
        {
            SchoolCollection oldCollection = SchoolCollections.FirstOrDefault(item => item.Id == schoolCollectionOld);
            SchoolCollection newCollection = SchoolCollections.FirstOrDefault(item => item.Id == schoolCollectionNew);

            if (oldCollection != null)
            {
                oldCollection.Schools.Remove(currentSchool);

            }
            if (newCollection != null)
            {

                newCollection.Schools.Add(currentSchool);
            }

            this.SerilizeServerSide();
        }

        #endregion

        #region IActivity Members


        public void DuplicateActivity(Activity activity, int howManyToDuplicate)
        {
            for (int i = 0; i < howManyToDuplicate; i++)
            {

                Activity duplicateActivity = CreateNewActivity();

                duplicateActivity.ActivityStatus = activity.ActivityStatus;
                duplicateActivity.ActivityType = activity.ActivityType;
                duplicateActivity.Comments = activity.Comments;
                duplicateActivity.ContacteId = activity.ContacteId;
                duplicateActivity.Date = activity.Date;
                duplicateActivity.EnterTime = DateTime.Now;
                duplicateActivity.Grade = activity.Grade;
                duplicateActivity.ModifyTime = DateTime.Now;
                duplicateActivity.NumberOfRequireCounselors = activity.NumberOfRequireCounselors;
                duplicateActivity.SchoolId = activity.SchoolId;
                duplicateActivity.TopicId = activity.TopicId;
                duplicateActivity.TotalNumberOfClass = activity.TotalNumberOfClass;
                duplicateActivity.Rounds = new List<Round>();
                foreach (Round round in activity.Rounds)
                {
                    Round r = new Round();
                    r.NumberOfMeeting = round.NumberOfMeeting;
                    r.NumberOfStudentInMeeting = round.NumberOfStudentInMeeting;
                    r.StopTime = round.StopTime;
                    r.StratTime = round.StratTime;
                    duplicateActivity.Rounds.Add(r);
                }

            }
        }

        #endregion

        #region IServerFacadeWrapper Members

        public int Add(int a, int b)
        {
            throw new NotImplementedException();
        }

        public int Sub(int a, int b)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}