using HotelReservation.Models;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;
using System.Data.Entity;

namespace HotelReservation.Controllers
{
    public class FeedbackController : Controller
    {
         SignUp context;

        public FeedbackController()
        {
            context = new SignUp();
            }

  
        // GET: Feedback
        public ActionResult Index()
        {
            return View(context.Feedbacks.ToList());
        }

        public ActionResult Create()
        {
            FeedbackViewModel model = new FeedbackViewModel();
            model.Answers = Common.GetAnswers();
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Create(FeedbackViewModel model)
        {
            if(null == model.Email || null == model.Comment || null == model.Select){
                ViewBag.Message = "Please Enter the details before submitting!";
                model.Answers = Common.GetAnswers();
                return View(model);
            }
            else
            {
                ViewBag.Message = "";
            }
            if (ModelState.IsValid)
            {

                context.Feedbacks.Add(new Feedback { Answer = model.Select, Comment = model.Comment, Email = model.Email, FullName = model.FullName });
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            model.Answers = Common.GetAnswers();
            return View(model);
        }

      
    }
}