using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Common;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Configuration;
using System.ServiceModel;

namespace Contract
{

    public interface IServerFacadeWrapper : ICity, ICounselors, IOrder, IActivity, ISchool, ITopic
    {

    }
    [ServiceContract]
    public interface IActivity
    {
        [OperationContract(Name = "SaveOpens")]
        void Save(Activity activity, List<OpenToAssign> opens);

        [OperationContract(Name = "SaveProposals")]
        void Save(Activity currentActivity, List<Proposal> Proposals);

        [OperationContract]
        List<Activity> Activites();

        [OperationContract]
        Activity GetActivity(int id);

        [OperationContract(Name = "SaveActivity")]
        bool Save(Activity activity);

        [OperationContract]
        Activity CreateNewActivity();

        [OperationContract]
        void DeleteActivity(Activity activity);

        [OperationContract]
        void DuplicateActivity(Activity activity, int howManyToDuplicate);
    }
    [ServiceContract]
    public interface ISchool
    {
        [OperationContract]
        List<School> Schools();
        [OperationContract]
        School GetSchool(int id);
        [OperationContract(Name = "SaveSchool")]
        bool Save(School school);

        List<SchoolType> SchoolTypes { [OperationContract] get; }

        List<SchoolCollection> SchoolCollections { [OperationContract] get; }
        [OperationContract]
        School CreatNewSchool();

        List<Role> Roles { [OperationContract] get; }
        [OperationContract]
        Contacte CreatNewContacte(School school);
        [OperationContract]
        void AddCity(City city);
        [OperationContract]
        void AddSchoolCollection(SchoolCollection schoolCollection);
        [OperationContract(Name = "SaveSchoolCollection")]
        bool Save(SchoolCollection SelectedSchoolCollection);
        [OperationContract]
        void DeleteSchool(School school);
        [OperationContract]
        void MoveSchool(int p, int p_2, School currentSchool);
    }
    [ServiceContract]
    public interface ITopic
    {
        [OperationContract]
        List<Topic> Topics();
        List<TopicsCollection> TopicCollections { [OperationContract]get; }
    }
    [ServiceContract]
    public interface ICounselors
    {

        List<Counselor> Counselors { [OperationContract] get; }
        [OperationContract]
        List<Counselor> CounselorsNotProposes(Activity CurrentActivity);

    }

    [ServiceContract]
    public interface ICity
    {
        List<City> Cities { [OperationContract]get; }
        List<District> Districts { [OperationContract] get; }
        [OperationContract]
        City GetCity(int id);
        [OperationContract]
        List<City> GetCitiesOfSchools();

    }
    [ServiceContract]
    public interface IOrder
    {

        List<Order> AllOrder { [OperationContract] get; }
        [OperationContract]
        Order CreateNewOrder();
    }



}





