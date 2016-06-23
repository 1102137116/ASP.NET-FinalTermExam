using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace aspnetFinalTermExam.Models
{
    public class Employee
    {

        /// <summary>
        /// 員工編號
        /// </summary>

        [DisplayName("員工編號")]
        [Required()]
        public int EmployeeID { get; set; }

        /// <summary>
        /// 員工姓氏
        /// </summary>
        [MaxLength(3)]
        [DisplayName("員工姓氏")]
        public string FirstName { get; set; }

        /// <summary>
        /// 員工名字
        /// </summary>
        [DisplayName("員工名字")]
        public string LastName { get; set; }

        // <summary>
        /// 員工姓名
        /// </summary>
        [DisplayName("員工姓名")]
        public string EmpName { get; set; }

        /// <summary>
        /// 職稱代號
        /// </summary>
        [DisplayName("職稱代號")]
        public int CodeId { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        /// 
        [DisplayName("職稱")]
        public string Title { get; set; }

        /// <summary>
        /// 稱謂
        /// </summary>
        /// 
        [DisplayName("稱謂")]
        public string TitleOfCourtesy { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        /// 
        [DisplayName("地址")]
        public string Address { get; set; }


        /// <summary>
        /// 城市
        /// </summary>
        /// 
        [DisplayName("城市")]
        public string City { get; set; }

        /// <summary>
        /// 地區
        /// </summary>
        /// 
        [DisplayName("地區")]
        public string Region { get; set; }

        /// <summary>
        /// 國家
        /// </summary>
        /// 
        [DisplayName("國家")]
        public string Country { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        /// 
        [DisplayName("電話")]
        public string Phone { get; set; }

        /// <summary>
        /// 主管
        /// </summary>
        /// 
        [DisplayName("主管")]
        public string ManagerID { get; set; }

        /// <summary>
        /// 月薪
        /// </summary>
        /// 
        [DisplayName("月薪")]
        public string MonthlyPayment { get; set; }

        /// <summary>
        /// 年薪
        /// </summary>
        /// 
        [DisplayName("年薪")]
        public string YearlyPayment { get; set; }

        /// <summary>
        /// 任職日期
        /// </summary>
        /// 
        [DisplayName("任職日期")]
        public DateTime? HireDate { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        /// 
        [DisplayName("性別")]
        public string Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        /// 
        [DisplayName("出生日期")]
        public DateTime? BirthDate { get; set; }

        
    }
}