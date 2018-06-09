using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.columns = ListController.columnChoices; // the ListController has a static controllor of default hardcoded columns #01 to iterate through using columnChoices. each value gets rendered to the user through columns.
            ViewBag.title = "Search";
            return View();
        }

        // TODO #1 - Create a Results action method to process 
        // search request and display results
        [HttpPost]
        public IActionResult Results(string searchType, string searchTerm) //method to display search result 
        {
            if (searchType.Equals("all")) //tests to see if keywords should be compared to all Values for all Keys. 
            {
                List<Dictionary<string, string>> searchResults = JobData.FindByValue(searchTerm); // builds a list to store the data of the successful results
                ViewBag.jobs = searchResults; // sends list to Views to be rendered to the user

            }
            else  // assumes keyword to match columnChoices then compares specific keyword to specific Value from requested Key.
            {
                List<Dictionary<string, string>> searchResults = JobData.FindByColumnAndValue(searchType, searchTerm); //builds a list to store the data of the successful results
                ViewBag.jobs = searchResults; // renders results to user by sending it to Views 
            }
            ViewBag.columns = ListController.columnChoices; // the ListController has a static controllor of default hardcoded columns #01 to iterate through using columnChoices. each value gets passed to the View through columns.
            ViewBag.title = "Search by " + ListController.columnChoices[searchType] + ": " + searchTerm;
            return View("Index"); // this passes our result to Views/Search/Index.cshtml
        }
    }
}
