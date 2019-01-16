using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mte.Models
{
    public partial class Enterprises
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Ogrn { get; set; }
        public string FAdress { get; set; }
        public string YAdress { get; set; }
        public string Phones { get; set; }
    }

    public partial class Posts
    {
        [Key]
        public int Id { get; set; }

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

        public string Name { get; set; }
    }

    public partial class CarBrands
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public partial class Cars
    {
        [Key]
        public int Id { get; set; }

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

        public string Name { get; set; }
        public string ShortName { get; set; }
    }

    public partial class Points
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public int PointTypesId { get; set; }

        public virtual PointTypes PointTypes { get; set; }
    }

    public partial class RouteTypes
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public partial class Routes
    {
        [Key]
        public int Id { get; set; }

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

        public string Name { get; set; }
        public bool IsNullRun { get; set; }
        public bool IsWork { get; set; }
        public bool IsDinner { get; set; }
        public bool IsBreak { get; set; }
    }

    public partial class Boards
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public int WeekDayWorks { get; set; }

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

        public string Name { get; set; }
        public int Step { get; set; }
    }

    public partial class WayBills
    {
        [Key]
        public int Id { get; set; }

        public string WNumber { get; set; }
        public DateTime DateAdd { get; set; }
        public DateTime DateClose { get; set; }

        public int? EnterprisesId { get; set; }
        public int? WayBillStatusesId { get; set; }
        public int? CarsId { get; set; }

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

        public int WayBillsId { get; set; }

        public int NumberShift { get; set; }
        public TimeSpan TimeBegin { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public int WorkTypesId { get; set; }
        public int RoutesId { get; set; }
        public decimal RLength { get; set; }
        public bool IsBack { get; set; }

        public virtual WayBills WayBills { get; set; }
        public virtual Routes Routes { get; set; }
        public virtual WorkTypes WorkTypes { get; set; }
    }

    public partial class Smenes
    {
        [Key]
        public int Id { get; set; }

        public DateTime SmenaDate { get; set; }
        public int DispEmployersId { get; set; }
        public int ControlerEmployersId { get; set; }

        public virtual Employers DispEmployers { get; set; }
        public virtual Employers ControlerEmployers { get; set; }
    }
}