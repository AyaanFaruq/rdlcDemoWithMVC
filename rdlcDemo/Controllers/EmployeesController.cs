using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using rdlcDemo.Models;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;

namespace rdlcDemo.Controllers
{
    public class EmployeesController : Controller
    {
        private AppDbContext _context;

        public EmployeesController()
        {
            _context = new AppDbContext();
        }


        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        // GET: Employees
        public ActionResult Index()
        {
            return View(_context.Employees.ToList());
        }


        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Address")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(employee).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        
        //Report Controller 
        public ActionResult ShowAll()
        {
            ReportViewer rptViewer = new ReportViewer();

            var list = _context.Employees.ToList();


            //rptViewer.LocalReport.DataSources.Add(new ReportDataSource("empDataSet", list));
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("salaryDataSet", list));

            //rptViewer.LocalReport.DataSources.Add(new ReportDataSource("salaryDataSet", employee));          

            rptViewer.ProcessingMode = ProcessingMode.Local;
            rptViewer.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            //rptViewer.LocalReport.ReportPath = Server.MapPath("~/Report/rptEmployeeList.rdlc");
            rptViewer.LocalReport.ReportPath = Server.MapPath("~/Report/rptSalary.rdlc");
            rptViewer.LocalReport.EnableExternalImages = true;

            rptViewer.LocalReport.Refresh();
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            byte[] bytes = rptViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            return File(bytes, "application/pdf");

        }

        public ActionResult Show(int id)
        {
            ReportViewer rptViewer = new ReportViewer();

            var employee = _context.Employees.Where(e => e.Id == id).ToList();

            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("empIdDataSet", employee));

            rptViewer.ProcessingMode = ProcessingMode.Local;
            rptViewer.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            rptViewer.LocalReport.ReportPath = Server.MapPath("~/Report/rptEmployeeById.rdlc");
            rptViewer.LocalReport.EnableExternalImages = true;

            rptViewer.LocalReport.Refresh();
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
          
            byte[] bytes = rptViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            return File(bytes, "application/pdf");

        }
        

    }
}
