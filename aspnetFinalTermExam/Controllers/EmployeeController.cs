using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspnetFinalTermExam.Controllers
{
    public class EmployeeController : Controller
    {
        /// <summary>
        /// 員工管理系統首頁
        /// </summary>
        /// <returns></returns>
        Models.CodeService codeService = new Models.CodeService();
        Models.EmployeeService employeeService = new Models.EmployeeService();
        public ActionResult Index()
        {
            ViewBag.EmpCodeData = this.codeService.GetEmp();
            //ViewBag.ShipCodeData = this.codeService.GetShipper();
            return View();
        }

        /// <summary>
        /// 取得員工查詢結果
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult Index(Models.EmployeeSearchArg arg)
        {
            ViewBag.EmpCodeData = this.codeService.GetEmp();
            Models.EmployeeService employeeService = new Models.EmployeeService();
            ViewBag.SearchResult = employeeService.GetEmpByCondtioin(arg);
            return View("Index");
        }

        /// <summary>
        /// 新增員工的畫面
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult InsertEmp()
        {
            ViewBag.EmpCodeData = this.codeService.GetEmp();
            
            return View(new Models.Employee());
        }

        /// <summary>
        /// 新增員工
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult InsertEmp(Models.Employee emp)
        {
            if (ModelState.IsValid)
            {
                Models.EmployeeService employeeService = new Models.EmployeeService();
                int insert = employeeService.InsertEmp(emp);
                return RedirectToAction("Index/" + insert);
            }

            return View(emp);
            //return View();
        }

        /// <summary>
        /// 更新員工畫面
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult UpdateEmp(string id)
        {
            Models.Employee emp = this.employeeService.GetEmpById(id);
            ViewBag.EmpData = employeeService.GetEmpById(id);
            ViewBag.EmpCodeData = this.codeService.GetEmp();
            ViewBag.HireDate = string.Format("{0:yyyy-MM-dd}", emp.HireDate);
            ViewBag.EmployeeData = emp;
            return View(emp);
        }

        /// <summary>
        /// 更新員工
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateEmp(Models.Employee emp)
        {
            Models.EmployeeService employeeService = new Models.EmployeeService();
            ViewBag.EmpCodeData = this.codeService.GetEmp();
            employeeService.UpdateOrder(emp);
            return View("Index");
        }


        /// <summary>
        /// 刪除員工
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost()]
        public JsonResult DeleteEmp(string EmployeeID)
        {
            try
            {
                Models.EmployeeService employeeService = new Models.EmployeeService();
                employeeService.DeleteEmpById(EmployeeID);
                return this.Json(true);
            }
            catch (Exception)
            {

                return this.Json(false);
            }
        }
        public ActionResult index2()
        { return View(); }
    }
}