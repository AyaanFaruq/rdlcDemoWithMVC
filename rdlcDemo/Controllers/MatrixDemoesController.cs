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

namespace rdlcDemo.Controllers
{
    public class MatrixDemoesController : Controller
    {
        private AppDbContext _context;

        public MatrixDemoesController()
        {
            _context = new AppDbContext();
        }


        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: MatrixDemoes
        public ActionResult Index()
        {
            return View(_context.MatrixDemo.ToList());
        }

      
        // GET: MatrixDemoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MatrixDemoes/Create
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SalesPerson,Product,SalesPrice")] MatrixDemo matrixDemo)
        {
            if (ModelState.IsValid)
            {
                _context.MatrixDemo.Add(matrixDemo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(matrixDemo);
        }

        // GET: MatrixDemoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatrixDemo matrixDemo = _context.MatrixDemo.Find(id);
            if (matrixDemo == null)
            {
                return HttpNotFound();
            }
            return View(matrixDemo);
        }

        // POST: MatrixDemoes/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SalesPerson,Product,SalesPrice")] MatrixDemo matrixDemo)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(matrixDemo).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(matrixDemo);
        }

        //Matrix Report Controller Code

        //Report Controller 
        public ActionResult ShowAll()
        {
            ReportViewer rptViewer = new ReportViewer();

            var list = _context.MatrixDemo.ToList();
            
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("matrixDataSet", list));
            
            rptViewer.ProcessingMode = ProcessingMode.Local;
            rptViewer.Width = System.Web.UI.WebControls.Unit.Percentage(100);
      
            rptViewer.LocalReport.ReportPath = Server.MapPath("~/Report/rptMatrix.rdlc");
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
