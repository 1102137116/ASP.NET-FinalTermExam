using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace aspnetFinalTermExam.Models
{
    public class EmployeeService
    {
        
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

        /// <summary>
        /// 新增員工
        /// </summary>
        /// <param name="order"></param>
        /// <returns>員工編號</returns>
        public int InsertEmp(Models.Employee emp)
        {
            String sql = @" Insert INTO HR.Employees
						 (
							EmployeeID,LastName,FirstName,Title,TitleOfCourtesy,BirthDate,
							HireDate,Address,City,Region,Country,Phone,ManagerID,Gender,
                            MonthlyPayment,YearlyPayment
						)
						VALUES
						(
							@EmployeeID,@LastName,@FirstName,@Title,@TitleOfCourtesy,@BirthDate,
							@HireDate,@Address,@City,@Region,@Country,@Phone,@ManagerID,@Gender,
                            @MonthlyPayment,@YearlyPayment
						)
						Select SCOPE_IDENTITY()
						";
            int orderId;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", emp.EmployeeID));
                cmd.Parameters.Add(new SqlParameter("@LastName", emp.LastName));
                cmd.Parameters.Add(new SqlParameter("@FirstName", emp.FirstName));
                cmd.Parameters.Add(new SqlParameter("@Title", emp.Title));
                cmd.Parameters.Add(new SqlParameter("@TitleOfCourtesy", emp.TitleOfCourtesy));
                cmd.Parameters.Add(new SqlParameter("@BirthDate", emp.BirthDate));
                cmd.Parameters.Add(new SqlParameter("@HireDate", emp.HireDate));
                cmd.Parameters.Add(new SqlParameter("@Address", emp.Address));
                cmd.Parameters.Add(new SqlParameter("@City", emp.City));
                cmd.Parameters.Add(new SqlParameter("@Region", emp.Region));
                cmd.Parameters.Add(new SqlParameter("@Country", emp.Country));
                cmd.Parameters.Add(new SqlParameter("@Phone", emp.Phone));
                cmd.Parameters.Add(new SqlParameter("@ManagerID", emp.ManagerID));
                cmd.Parameters.Add(new SqlParameter("@Gender", emp.Gender));
                cmd.Parameters.Add(new SqlParameter("@MonthlyPayment", emp.MonthlyPayment));
                cmd.Parameters.Add(new SqlParameter("@YearlyPayment", emp.YearlyPayment));

                orderId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return orderId;

        }

        /// <summary>
        /// 刪除員工
        /// </summary>
        public void DeleteEmpById(String EmployeeID)
        {
            try
            {
                string sql = "Delete FROM HR.Employees Where EmployeeID=@EmployeeID";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@EmployeeID", EmployeeID));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新員工
        /// </summary>
        public void UpdateOrder(Models.Employee emp)
        {
            try
            {
                string sql = @"UPDATE HR.Employees SET 
	                        CustomerID=@custid,EmployeeID=@empid,orderdate=@orderdate,requireddate=@requireddate,
                            shippeddate=@shippeddate,shipperid=@shipperid,freight=@freight,shipname=@shipname,
                            shipaddress=@shipaddress,shipcity=@shipcity,shipregion=@shipregion,
                            shippostalcode=@shippostalcode,shipcountry=@shipcountry                          
                            WHERE OrderId=@orderid";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@EmployeeID", emp.EmployeeID));
                    cmd.Parameters.Add(new SqlParameter("@LastName", emp.LastName));
                    cmd.Parameters.Add(new SqlParameter("@FirstName", emp.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@Title", emp.Title));
                    cmd.Parameters.Add(new SqlParameter("@TitleOfCourtesy", emp.TitleOfCourtesy));
                    cmd.Parameters.Add(new SqlParameter("@BirthDate", emp.BirthDate));
                    cmd.Parameters.Add(new SqlParameter("@HireDate", emp.HireDate));
                    cmd.Parameters.Add(new SqlParameter("@Address", emp.Address));
                    cmd.Parameters.Add(new SqlParameter("@City", emp.City));
                    cmd.Parameters.Add(new SqlParameter("@Region", emp.Region));
                    cmd.Parameters.Add(new SqlParameter("@Country", emp.Country));
                    cmd.Parameters.Add(new SqlParameter("@Phone", emp.Phone));
                    cmd.Parameters.Add(new SqlParameter("@ManagerID", emp.ManagerID));
                    cmd.Parameters.Add(new SqlParameter("@Gender", emp.Gender));
                    cmd.Parameters.Add(new SqlParameter("@MonthlyPayment", emp.MonthlyPayment));
                    cmd.Parameters.Add(new SqlParameter("@YearlyPayment", emp.YearlyPayment));

                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 依照Id 取得員工資料
        /// </summary>
        /// <returns></returns>
        public Models.Employee GetEmpById(string EmployeeID)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT 
					A.EmployeeID,A.Lastname+ A.Firstname As EmpName,A.title + B.CodeVal As Job,
					A.HireDate,A.gender,A.BirthDate
					From HR.Employees As A 
					INNER JOIN dbo.CodeTable As B ON A.title=B.CodeId
					Where  A.EmployeeID=@EmployeeID";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", EmployeeID));

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapOrderDataToList(dt).FirstOrDefault();
        }

        /// <summary>
        /// 依照條件取得員工資料
        /// </summary>
        /// <returns></returns>
        public List<Models.Employee> GetEmpByCondtioin(Models.EmployeeSearchArg arg)
        {

            DataTable dt = new DataTable();
            string sql = @"SELECT 
					A.EmployeeID,A.Lastname+ A.Firstname As EmpName,B.CodeId,A.title + B.CodeVal As Job,
					A.HireDate,A.gender,A.BirthDate
					From HR.Employees As A 
					INNER JOIN dbo.CodeTable As B ON A.title=B.CodeId
					Where (A.EmployeeID=@EmployeeID Or @EmployeeID='') AND
                          (A.EmpName=@Lastname+Firstname Or @Lastname+Firstname='') AND
                          (A.CodeId=@CodeId Or @CodeId='') AND
                          (A.HireDate=@HireDate Or @HireDate='')";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@EmpName", arg.EmpName == null ? string.Empty : arg.EmpName));
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", arg.EmployeeID == null ? string.Empty : arg.EmployeeID));
                cmd.Parameters.Add(new SqlParameter("@CodeId", arg.CodeId == null ? default(int) : arg.CodeId));
                cmd.Parameters.Add(new SqlParameter("@HireDate", arg.HireDate == null ? string.Empty : arg.HireDate));

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }


            return this.MapOrderDataToList(dt);
        }

        private List<Models.Employee> MapOrderDataToList(DataTable orderData)
        {
            List<Models.Employee> result = new List<Employee>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Employee()
                {
                    EmployeeID = (int)row["EmployeeID"],
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    EmpName = row["EmpName"].ToString(),
                    HireDate = row["HireDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["HireDate"],
                    CodeId = (int)row["CodeId"]
                    
                });
            }
            return result;
        }

    }
}