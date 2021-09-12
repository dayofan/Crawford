using Crawford.ApplicationServices;
using Crawford.Domain;
using Crawford.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Crawford.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMembershipService _membershipService;
        private readonly IClaimsService _claimsService;

        public HomeController(IMembershipService membershipService, IClaimsService claimsService)
        {
            _membershipService = membershipService ?? throw new ArgumentNullException(nameof(membershipService));
            _claimsService = claimsService ?? throw new ArgumentNullException(nameof(claimsService));
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel loginViewModel)
        {
            var userProfile = new UserProfile
            {
                UserName = loginViewModel.UserName,
                Password = loginViewModel.Password
            };

            if (ModelState.IsValid)
            {
                var isUserValid = _membershipService.IsUserValid(userProfile);
                if (isUserValid)
                {
                    HttpContext.Session.Set("UserName", Encoding.UTF8.GetBytes(userProfile.UserName));
                    return RedirectToAction(nameof(LossData));
                }
            }

            return View(loginViewModel);
        }

        public ActionResult LossData()
        {
            if (HttpContext.Session.TryGetValue("UserName", out var userName) && userName != null)
            {
                var model = _claimsService.GetLossTypeData().Select(d => new LossDataViewModel 
                {
                    LossTypeId = d.LossTypeId,
                    LossTypeCode = d.LossTypeCode,
                    LossTypeDescription = d.LossTypeDescription
                });

                return View(model.ToList());
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
