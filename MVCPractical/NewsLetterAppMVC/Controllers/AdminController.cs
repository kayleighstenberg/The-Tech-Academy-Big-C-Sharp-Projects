using NewsLetterAppMVC.Models;
using NewsLetterAppMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace NewsLetterAppMVC.Controllers
{
    public class AdminController : Controller
    {
        //GET: Admin
        public ActionResult Index()

        {
            using (NEWSLETTERNEWEREntities3 db = new NEWSLETTERNEWEREntities3())
            {
                var signups = (from c in db.SignUps
                               select c).ToList();
                var signupVms = new List<SignUpVm>();
                foreach (var signup in signups)
                {
                    var signupVm = new SignUpVm();
                    signupVm.Id = signup.Id;
                    signupVm.FirstName = signup.FirstName;
                    signupVm.LastName = signup.LastName;
                    signupVm.EmailAddress = signup.EmailAddress;
                    signupVm.QuoteAmt = signup.QuoteAmt;
                    signupVms.Add(signupVm);
                }

                return View(signupVms);

            }
        }

    }
}