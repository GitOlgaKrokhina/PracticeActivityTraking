using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticeActivityTraking.Controllers
{
    public class HRController : Controller
    {
        // GET: HR
        public ActionResult Index()
        {
            return View();
        }
        // GET: HR
        public ActionResult HRLog()
        {
            return View();
        }
        public static string cs = @"data source=(localdb)\MSSQLLocalDB;initial catalog=ActivityTracking;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        public ActionResult RangEmployeeExcel()
        {
            Microsoft.Office.Interop.Excel.Application app =
        new Microsoft.Office.Interop.Excel.Application();

            app.Visible = true;

            Workbook wb = app.Workbooks.Add();
            Worksheet ws = wb.Worksheets[1];

            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT CONCAT(Employee.Surname,' ' ,Employee.Name,' ' ,Employee.Patronymic) as ФИО, 
            Employee.PassportID as [Паспорт], Employee.Phone as [Телефон], Sum(CodifierActivity.Score) AS [Сумма баллов]
            FROM (CodifierActivity INNER JOIN (Activity  INNER JOIN (Employee INNER JOIN EmployeeToActivity 
            ON Employee.PassportID = EmployeeToActivity.PassportID) 
            ON Activity.ActivityID = EmployeeToActivity.ActivityID) 
            ON CodifierActivity.ActivityTypeID = Activity.ActivityTypeID) 
            GROUP BY Employee.Surname, Employee.Name, Employee.Patronymic, Employee.PassportID, Employee.Phone
            ORDER BY Sum(CodifierActivity.Score) DESC;", conn);

            SqlDataReader reader = cmd.ExecuteReader();

            for(int j = 0; j<4; j++){ws.Cells[1, j+1].Value = reader.GetName(j);}
            int i = 2;
            while (reader.Read())
            {
                for (int h = 0; h < 4; h++){ws.Cells[i, h+1].Value = reader[h];}
                i++;
            }

            reader.Close();
            conn.Close();
            return RedirectToAction("Index");
        }
        public ActionResult EmployeeAndActivityExcel()
        {
            Microsoft.Office.Interop.Excel.Application app =
        new Microsoft.Office.Interop.Excel.Application();

            app.Visible = true;

            Workbook wb = app.Workbooks.Add();
            Worksheet ws = wb.Worksheets[1];

            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT CONCAT(Employee.Surname,' ' ,Employee.Name,' ' ,Employee.Patronymic) as ФИО, Employee.PassportID as [Паспорт],
                                            Activity.ActivityID as [Код активности], CodifierActivity.ActivityName as [Название активности], Activity.Date as [Дата], 
                                            Event.EventName as [Название мероприятия], CodifierActivity.Score as [Балл]
                                            FROM(CodifierActivity INNER JOIN Activity ON CodifierActivity.ActivityTypeID = Activity.ActivityTypeID 
                                            INNER JOIN EmployeeToActivity ON Activity.ActivityID = EmployeeToActivity.ActivityID
                                            INNER JOIN Employee ON Employee.PassportID = EmployeeToActivity.PassportID
                                            INNER JOIN Event ON Event.EventID = Activity.EventID)
                                            GROUP BY Employee.Surname, Employee.Name, Employee.Patronymic, Employee.PassportID, Activity.ActivityID, 
                                            CodifierActivity.ActivityName, Activity.Date, Event.EventName, CodifierActivity.Score
                                            ORDER BY Activity.Date DESC;", conn);

            SqlDataReader reader = cmd.ExecuteReader();

            for (int j = 0; j < 7; j++) { ws.Cells[1, j + 1].Value = reader.GetName(j); }
            int i = 2;
            while (reader.Read())
            {
                for (int h = 0; h < 7; h++) { ws.Cells[i, h + 1].Value = reader[h]; }
                i++;
            }

            reader.Close();
            conn.Close();
            return RedirectToAction("Index");
        }
        public ActionResult EmpLevelExcel()
        {
            Microsoft.Office.Interop.Excel.Application app =
        new Microsoft.Office.Interop.Excel.Application();

            app.Visible = true;

            Workbook wb = app.Workbooks.Add();
            Worksheet ws = wb.Worksheets[1];

            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT Employee.PassportID as [Паспорт], CONCAT(Employee.Surname,' ' ,Employee.Name,' ' ,Employee.Patronymic) as ФИО, 
                                            Sum(CodifierActivity.Score) AS [Сумма баллов],
			                               (Sum(CodifierActivity.Score) / 10) AS Уровень,
			                               (Sum(CodifierActivity.Score) / 50 * 0.5) AS Поощрение
                                           FROM (CodifierActivity INNER JOIN (Activity  INNER JOIN (Employee INNER JOIN EmployeeToActivity 
                                           ON Employee.PassportID = EmployeeToActivity.PassportID) 
                                           ON Activity.ActivityID = EmployeeToActivity.ActivityID) 
                                           ON CodifierActivity.ActivityTypeID = Activity.ActivityTypeID) 
                                           GROUP BY Employee.Surname, Employee.Name, Employee.Patronymic, Employee.PassportID, Employee.Phone
                                           ORDER BY Sum(CodifierActivity.Score) DESC;", conn);

            SqlDataReader reader = cmd.ExecuteReader();

            for (int j = 0; j < 5; j++) { ws.Cells[1, j + 1].Value = reader.GetName(j); }
            int i = 2;
            while (reader.Read())
            {
                for (int h = 0; h < 5; h++) { ws.Cells[i, h + 1].Value = reader[h]; }
                i++;
            }

            reader.Close();
            conn.Close();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HRLog(string passHR)
        {
            if (passHR == "HRLOGIN555")
            {
                return View("Index");
            }
            else
            {
                TempData["msg"] = "<script>alert('Пароль для входа в пространство HR-сотрудника неверен.');</script>";
                 return RedirectToAction("HRLog", "HR");
            }
        }
    }
}