using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly INullMailService _mailService;

        //Inject Mail Service and DB Context
        public AppController(INullMailService mailService, IDutchRepository repository)
        {
            _repository = repository;
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

        public IActionResult Shop()
        {
            //Get products from the DB and return them
            var results = _repository.GetAllProducts();
                
            return View(results);
        }
    }
}

