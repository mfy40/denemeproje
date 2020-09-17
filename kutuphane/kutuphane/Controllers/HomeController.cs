using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using kutuphane.database.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
 

namespace kutuphane.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly KutuphaneDbContext _kutuphaneDbContext;

        public HomeController(ILogger<HomeController> logger, KutuphaneDbContext kutuphaneDbContext)
        {
            _logger = logger;
            _kutuphaneDbContext = kutuphaneDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }


        // master branchı için değişiklik 13:30  
       
            



        //public IActionResult Operations()
        //{
        //    return View();
        //}

        //public IActionResult BookOperation()
        //{
        //    return View();
        //}


        //public IActionResult StudentOperation()
        //{
        //    return View();
        //}

    }
}
