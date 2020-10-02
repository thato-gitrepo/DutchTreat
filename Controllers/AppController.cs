using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly INullMailService _mailService;

        //Inject Services
        public AppController(INullMailService mailService)
        {
            _mailService = mailService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        //Get data from user
        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            //Validate user input
            if (ModelState.IsValid)
            {
                //Send the email
                _mailService.SendMessage("thatomokhemisa@gmail.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: { model.Message }");
                ViewBag.UserMessage = "Email Sent.";
                ModelState.Clear();
            }
            else
            {
                //Show the errors
            }
            return View();
        }
    }
}

