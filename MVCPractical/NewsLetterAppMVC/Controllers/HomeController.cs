using NewsLetterAppMVC.Models;
using NewsLetterAppMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsLetterAppMVC.Controllers
{
    public class HomeController : Controller
    {
        SignUp signups = new SignUp();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SignUp su)
        {

                using (NEWSLETTERNEWEREntities3 db = new NEWSLETTERNEWEREntities3())
                {
                    var signup = new SignUp();
                    // signup.Id = db.SignUps.Select Id;
                    signup.FirstName = su.FirstName;
                    signup.LastName = su.LastName;
                    signup.EmailAddress = su.EmailAddress;
                    signup.DateOfBirth = su.DateOfBirth;
                    signup.CarYear = su.CarYear;
                    signup.CarMake = su.CarMake;
                    signup.CarModel = su.CarModel;
                    signup.DUI = su.DUI;
                    signup.SpeedingTickets = su.SpeedingTickets;
                    signup.CoverageType = su.CoverageType;


                    DateTime todaysDate = DateTime.UtcNow;
                    DateTime ApplicantBirthdate = Convert.ToDateTime(su.DateOfBirth);
                    DateTime pastYearDate;
                    int years = 0;
                    int days;
                    double quoteAmt = 50;

                    if (DateTime.UtcNow > ApplicantBirthdate)
                    {
                        years = new DateTime(DateTime.UtcNow.Subtract(ApplicantBirthdate).Ticks).Year - 1;
                    }

                    pastYearDate = ApplicantBirthdate.AddYears(years);
                    days = todaysDate.Subtract(pastYearDate).Days;

                    if (years < 25)
                    {
                        quoteAmt = quoteAmt + 25;
                    }

                    if (years < 18)
                    {
                        quoteAmt = quoteAmt + 100;
                    }

                    if (years > 100)
                    {
                        quoteAmt = quoteAmt + 25;
                    }

                    if (su.CarYear < 2000)
                    {
                        quoteAmt = quoteAmt + 25;
                    }

                    if (su.CarYear > 2015)
                    {
                        quoteAmt = quoteAmt + 25;
                    }

                    if (su.CarMake.Equals("Porsche") || su.CarMake.Equals("porsche"))
                    {
                        quoteAmt = quoteAmt + 25;
                    }

                    if (su.CarModel.Equals("911 Carrera") || su.CarModel.Equals("911 carrera"))
                    {
                        quoteAmt = quoteAmt + 25;
                    }

                    quoteAmt = quoteAmt + Convert.ToDouble(su.SpeedingTickets * 10);


                    if (su.DUI.Equals("True") || su.DUI.Equals("Yes") || su.DUI.Equals("true") || su.DUI.Equals("yes"))
                    {
                        quoteAmt = quoteAmt*1.25;
                    }

                    if (su.CoverageType.Equals("Full") || su.CoverageType.Equals("full"))
                    {
                        quoteAmt = quoteAmt *1.5;
                    }

                    su.QuoteAmt = quoteAmt;

                    signup.QuoteAmt = su.QuoteAmt;

                    db.SignUps.Add(signup);
                    db.SaveChanges();
            }

                return View(su);

        }

    }
}