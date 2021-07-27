using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5_11.Models;

namespace WebApplication5_11.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Form()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Form(student st)
        {
            if(string.IsNullOrEmpty(st.Name) || string.IsNullOrEmpty(st.Name) ||
                string.IsNullOrEmpty(st.Job) || st.Age == 0 || string.IsNullOrEmpty(st.Phone) || string.IsNullOrEmpty(st.Email) ||
                string.IsNullOrEmpty(st.Languages))
            {
                ViewBag.message = "შეავსე ყველა ველი";
                return View();
            }
            else
            {
                MyDatabaseDataContext db = new MyDatabaseDataContext();

                db.students.InsertOnSubmit(st);
                db.SubmitChanges();

                return RedirectToAction("List");
            }
        }
        public ActionResult List(string search="")
        {
            using (MyDatabaseDataContext db = new MyDatabaseDataContext())
            {
                return View(db.students.Where(x =>search.Length==0 || x.Name.Contains(search)).ToList());
            }
        }
        public ActionResult Edit(int id)
        {
            MyDatabaseDataContext db = new MyDatabaseDataContext();
            return View(db.students.Where(x=>x.id==id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(student st)
        {
            MyDatabaseDataContext db = new MyDatabaseDataContext();
            student s = db.students.Where(x => x.id == st.id).FirstOrDefault();
            s.Name = st.Name;
            s.Surname = st.Surname;
            s.Age = st.Age;
            s.Job = st.Job; 
            s.Phone = st.Phone;
            s.Languages = st.Languages;
            s.Email = st.Email;

            db.SubmitChanges();

            return RedirectToAction("List");
        }
        public ActionResult Details(int id)
        {
            MyDatabaseDataContext db = new MyDatabaseDataContext();
            return View(db.students.Where(x => x.id == id).FirstOrDefault());
        }
        public ActionResult Delete(int id)
        {
            MyDatabaseDataContext db = new MyDatabaseDataContext();
            db.students.DeleteOnSubmit(db.students.Where(x => x.id == id).FirstOrDefault());
            db.SubmitChanges();
            return RedirectToAction("List");
        }
    }
}