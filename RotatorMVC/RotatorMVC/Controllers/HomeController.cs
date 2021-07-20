using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RotatorMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RotatorMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Rotator()
        {
            Palindrome model = new();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Rotator(Palindrome palindrome)
        {
            if(String.IsNullOrWhiteSpace(palindrome.InputWord))
            {
                palindrome.Message = $"You must enter a word in order to continue...";
                
            } 
            else
            {
                var inputWord = palindrome.InputWord;
                var revWord = "";

                for ( var i = inputWord.Length - 1; i >= 0; i-- )
                {
                    revWord += inputWord[i];
                }

                palindrome.RevWord = revWord;

                revWord = Regex.Replace(revWord.ToLower(), "[^a-zA-Z0-9]+", "");
                inputWord = Regex.Replace(inputWord.ToLower(), "[^a-zA-Z0-9]+", "");

                palindrome.IsPalindrome = revWord == inputWord ? true : false;

                if ( palindrome.IsPalindrome )
                {
                    palindrome.Message = $"Success! {palindrome.InputWord} is a Palindrome";
                }
                else
                {
                    palindrome.Message = $"Sorry, but {palindrome.InputWord} is not a Palindrome";
                }
            }

            return View(palindrome);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
