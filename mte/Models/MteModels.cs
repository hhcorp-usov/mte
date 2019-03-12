namespace mte.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System;
    using System.Web.Configuration;
    using System.ComponentModel;
    using System.Web.Mvc;

    public class BaseSettings
    {
        public int bs_itemsPerPage = Convert.ToInt32(WebConfigurationManager.AppSettings["itemsPerPage"]);
    }

    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }

        public string Search { get; set; }
        public string Sort_filter { get; set; }
        public string Sort_order { get; set; }
    }

    public partial class GlobalContainers
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public partial class Enterprises
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; }
        [DisplayName("ИНН")]
        public string Inn { get; set; }
        [DisplayName("КПП")]
        public string Kpp { get; set; }
        [DisplayName("ОГРН")]
        public string Ogrn { get; set; }
        [DisplayName("Фактический адрес")]
        public string FAdress { get; set; }
        [DisplayName("Юридический адрес")]
        public string YAdress { get; set; }
        [DisplayName("Телефоны")]
        public string Phones { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool _deleted { get; set; }
    }

    public partial class Posts
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public string Name { get; set; }
        public bool IsDriver { get; set; }
        public bool IsConductor { get; set; }
        public bool IsIntern { get; set; }
        public bool IsControler { get; set; }
        public bool IsDisp { get; set; }
    }

    public partial class Employers
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public string Name { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string IIdent { get; set; }

        public int? EnterprisesId { get; set; }
        public int? PostsId { get; set; }

        public virtual Enterprises Enterprises { get; set; }
        public virtual Posts Posts { get; set; }
    }

    public partial class CarTypes
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public string Name { get; set; }
    }

    public partial class CarBrands
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public string Name { get; set; }
    }

    public partial class Cars
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public string Name { get; set; }
        public string PIdent { get; set; }
        public string BIdent { get; set; }

        public int? EnterprisesId { get; set; }
        public int? CarBrandsId { get; set; }
        public int? CarTypesId { get; set; }

        public virtual Enterprises Enterprises { get; set; }
        public virtual CarTypes CarTypes { get; set; }
        public virtual CarBrands CarBrands { get; set; }
    }

    public partial class PointTypes
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public string Name { get; set; }
        public string ShortName { get; set; }
    }

    public partial class Points
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public string Name { get; set; }
        public int PointTypesId { get; set; }

        public virtual PointTypes PointTypes { get; set; }
    }

    public partial class RouteTypes
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public string Name { get; set; }
    }

    public partial class Routes
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public string Name { get; set; }
        public string BackName { get; set; }
        public string RNumber { get; set; }

        public int? EnterprisesId { get; set; }
        public int? RouteTypesId { get; set; }
        public int? PointStartId { get; set; }
        public int? PointStopId { get; set; }

        public virtual Enterprises Enterprises { get; set; }
        public virtual RouteTypes RouteTypes { get; set; }
        public virtual Points PointStart { get; set; }
        public virtual Points PointStop { get; set; }

        public virtual List<RoutePoints> RoutePoints { get; set; }

        public Routes()
        {
            RoutePoints = new List<RoutePoints>();
        }
    }

    public partial class RoutePoints
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public int RoutesId { get; set; }
        public int PointsId { get; set; }

        public decimal RLength { get; set; }
        public TimeSpan RTime { get; set; }
        public bool IsBack { get; set; }

        public virtual Routes Routes { get; set; }
        public virtual Points Points { get; set; }
    }

    public partial class WorkTypes
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool IsNullRun { get; set; }
        public bool IsWork { get; set; }
        public bool IsDinner { get; set; }
        public bool IsBreak { get; set; }
    }

    public partial class Boards
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public string Name { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public int WeekDayWorks { get; set; }

        public string RIdent { get; set; }
        public string REIdent { get; set; }

        public int? EnterprisesId { get; set; }

        public virtual Enterprises Enterprises { get; set; }

        public virtual List<BoardFlightLists> BoardFlightLists { get; set; }

        public Boards()
        {
            BoardFlightLists = new List<BoardFlightLists>();
        }
    }

    public partial class BoardFlightLists
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public int BoardsId { get; set; }

        public int NumberShift { get; set; }
        public TimeSpan TimeBegin { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public int WorkTypesId { get; set; }
        public int RoutesId { get; set; }
        public decimal RLength { get; set; }
        public bool IsBack { get; set; }

        public virtual Boards Boards { get; set; }
        public virtual Routes Routes { get; set; }
        public virtual WorkTypes WorkTypes { get; set; }
    }

    public partial class WayBillStatuses
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public string Name { get; set; }
        public int Step { get; set; }
    }

    public partial class WayBills
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public string WNumber { get; set; }
        public DateTime DateAdd { get; set; }
        public DateTime DateClose { get; set; }

        public int? EnterprisesId { get; set; }
        public int? WayBillStatusesId { get; set; }
        public int? CarsId { get; set; }

        public int OdometrStart { get; set; }
        public int OdometrStop { get; set; }

        public virtual Enterprises Enterprises { get; set; }
        public virtual WayBillStatuses WayBillStatuses { get; set; }
        public virtual Cars Cars { get; set; }

        public virtual List<WayBillTeams> WayBillTeams { get; set; }
        public virtual List<WayBillFlightLists> WayBillFlightLists { get; set; }

        public WayBills()
        {
            WayBillTeams = new List<WayBillTeams>();
            WayBillFlightLists = new List<WayBillFlightLists>();
        }
    }

    public partial class WayBillTeams
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public int WayBillsId { get; set; }
        public int EmployersId { get; set; }

        public int NumberShift { get; set; }

        public virtual WayBills WayBills { get; set; }
        public virtual Employers Employers { get; set; }
    }

    public partial class WayBillFlightLists
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public int WayBillsId { get; set; }

        public int NumberShift { get; set; }
        public TimeSpan TimeBegin { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public int WorkTypesId { get; set; }
        public int RoutesId { get; set; }
        public decimal RLength { get; set; }
        public bool IsBack { get; set; }
        public string BoardsName { get; set; }

        public virtual WayBills WayBills { get; set; }
        public virtual Routes Routes { get; set; }
        public virtual WorkTypes WorkTypes { get; set; }
    }

    public partial class Smenes
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public DateTime SmenaDate { get; set; }
        public int DispEmployersId { get; set; }
        public int ControlerEmployersId { get; set; }

        public virtual Employers DispEmployers { get; set; }
        public virtual Employers ControlerEmployers { get; set; }

        public string GetCName()
        {
            return ControlerEmployers.FirstName.Trim() + ' ' + ControlerEmployers.Name.Trim() + ' ' + ControlerEmployers.SurName.Trim();
        }

        public string GetDName() {
            return DispEmployers.FirstName.Trim() + ' ' + DispEmployers.Name.Trim() + ' ' + DispEmployers.SurName.Trim();
        }
    }

    public partial class MonthTables
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public string Name { get; set; }
        public int Month { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }

        public virtual List<MonthTablesCars> MonthTablesCars { get; set; }

        public MonthTables()
        {
            MonthTablesCars = new List<MonthTablesCars>();
        }
    }

    public partial class MonthTablesCars
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public int MonthTablesId { get; set; }
        public int CarsId { get; set; }

        public virtual MonthTables MonthTables { get; set; }
        public virtual Cars Cars { get; set; }

        public virtual List<MonthTablesTeams> MonthTablesTeams { get; set; }

        public MonthTablesCars()
        {
            MonthTablesTeams = new List<MonthTablesTeams>();
        }
    }

    public partial class MonthTablesTeams
    {
        [Key]
        public int Id { get; set; }
        public int GlobalContainersId { get; set; }

        public int MonthTablesCarsId { get; set; }
        public int EmployersId { get; set; }
        public int NumberShift { get; set; }

        public string D1_RIdent { get; set; }
        public string D1_REIdent { get; set; }
        public string D2_RIdent { get; set; }
        public string D2_REIdent { get; set; }
        public string D3_RIdent { get; set; }
        public string D3_REIdent { get; set; }
        public string D4_RIdent { get; set; }
        public string D4_REIdent { get; set; }
        public string D5_RIdent { get; set; }
        public string D5_REIdent { get; set; }
        public string D6_RIdent { get; set; }
        public string D6_REIdent { get; set; }
        public string D7_RIdent { get; set; }
        public string D7_REIdent { get; set; }
        public string D8_RIdent { get; set; }
        public string D8_REIdent { get; set; }
        public string D9_RIdent { get; set; }
        public string D9_REIdent { get; set; }
        public string D10_RIdent { get; set; }
        public string D10_REIdent { get; set; }
        public string D11_RIdent { get; set; }
        public string D11_REIdent { get; set; }
        public string D12_RIdent { get; set; }
        public string D12_REIdent { get; set; }
        public string D13_RIdent { get; set; }
        public string D13_REIdent { get; set; }
        public string D14_RIdent { get; set; }
        public string D14_REIdent { get; set; }
        public string D15_RIdent { get; set; }
        public string D15_REIdent { get; set; }
        public string D16_RIdent { get; set; }
        public string D16_REIdent { get; set; }
        public string D17_RIdent { get; set; }
        public string D17_REIdent { get; set; }
        public string D18_RIdent { get; set; }
        public string D18_REIdent { get; set; }
        public string D19_RIdent { get; set; }
        public string D19_REIdent { get; set; }
        public string D20_RIdent { get; set; }
        public string D20_REIdent { get; set; }
        public string D21_RIdent { get; set; }
        public string D21_REIdent { get; set; }
        public string D22_RIdent { get; set; }
        public string D22_REIdent { get; set; }
        public string D23_RIdent { get; set; }
        public string D23_REIdent { get; set; }
        public string D24_RIdent { get; set; }
        public string D24_REIdent { get; set; }
        public string D25_RIdent { get; set; }
        public string D25_REIdent { get; set; }
        public string D26_RIdent { get; set; }
        public string D26_REIdent { get; set; }
        public string D27_RIdent { get; set; }
        public string D27_REIdent { get; set; }
        public string D28_RIdent { get; set; }
        public string D28_REIdent { get; set; }
        public string D29_RIdent { get; set; }
        public string D29_REIdent { get; set; }
        public string D30_RIdent { get; set; }
        public string D30_REIdent { get; set; }
        public string D31_RIdent { get; set; }
        public string D31_REIdent { get; set; }

        public virtual MonthTablesCars MonthTablesCars { get; set; }
        public virtual Employers Employers { get; set; }
    }
}