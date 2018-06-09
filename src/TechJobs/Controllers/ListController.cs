using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class ListController : Controller
    {
        internal static Dictionary<string, string> columnChoices = new Dictionary<string, string>();

        // This is a "static constructor" which can be used
        // to initialize static members of a class

        static ListController()   // this is building the contructor that we call later #01
        {
            
            columnChoices.Add("core competency", "Skill");
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");
        }

        public IActionResult Index() // this method presents they user with the different column values that the user can select from to make thier List.
        {
            ViewBag.columns = columnChoices;
            return View();
        }

        public IActionResult Values(string column) // takes column value from query parameter
        {
            if (column.Equals("all"))
            {
                List<Dictionary<string, string>> jobs = JobData.FindAll(); // builds a list to hold all of the jobs to be given to the user
                ViewBag.title =  "All Jobs";
                ViewBag.jobs = jobs;
                return View("Jobs"); // renders View Jobs template instead of the default view
            }
            else
            {
                List<string> items = JobData.FindAll(column); // builds a list to hold only the values of the column.
                ViewBag.title =  "All " + columnChoices[column] + " Values";
                ViewBag.column = column;
                ViewBag.items = items;
                return View();
            }
        }

        public IActionResult Jobs(string column, string value)
        {
            List<Dictionary<String, String>> jobs = JobData.FindByColumnAndValue(column, value);
            ViewBag.title = "Jobs with " + columnChoices[column] + ": " + value;
            ViewBag.jobs = jobs;

            return View();
        }
    }
}
