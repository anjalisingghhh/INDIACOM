﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INDIACom.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

       public ActionResult News()
            {
                return View();
            }
        
        public ActionResult SpecialSessionStatic()
        {
            return View();
        }
        public ActionResult SpecialSession()
        {
            return View();
        }
        public ActionResult SpecialSession2()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Venue()
        {
            return View();
        }

        public ActionResult ConferenceSecretariat()
        {
            return View();
        }
        public ActionResult cpp()
        {
            return View();
        }
        public ActionResult RegistrationFee()
        {
            return View();
        }
        public ActionResult Accommodation()
        {
            return View();
        }
        public ActionResult GuidelinestoSubmitPaper()
        {
            return View();
        }
        public ActionResult PlagiarismPolicy()
        {
            return View();
        }
        public ActionResult ls()
        {
            return View();
        }
        public ActionResult ReviewProcessPublicationIndexing()
        {
            return View();
        }
        public ActionResult Downloads()
        {
            return View();
        }
        public ActionResult TechnicalCommittee()
        {
            return View();
        }
        public ActionResult SubmitPaper()
        {
            return View();
        }
        public ActionResult OurSupporters()
        {
            return View();
        }
        public ActionResult LocalOrganisingCommittee()
        {
            return View();
        }
        public ActionResult ListSpeakers()
        {
            return View();
        }
        public ActionResult InvitedSpeakers()
        {
            return View();
        }
        public ActionResult ChairsAndCoChairs()
        {
            return View();
        }
        public ActionResult EditorialBoard()
        {
            return View();
        }
        public ActionResult Committees()
        {
            return View();
        }
        public ActionResult IndiacomProceedings()
        {
            return View();
        }

        public ActionResult IEEEOversightCommittee()
        {
            return View();
        }

        public ActionResult delcon()
        {
            return View();
        }

        public ActionResult CallForPaper()
        {
            return View();
        }
    
        public ActionResult AdvisoryCommittee()
        {
            return View();
        }
        public ActionResult FAQ()
        {
            return View();
        }
        public ActionResult DocumentVerify()
        {
            return View();
        }
        public ActionResult AdminPanel()
        {
            return View();
        }
    }
}