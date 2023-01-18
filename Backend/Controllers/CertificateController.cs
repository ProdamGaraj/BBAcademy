using Backend.Models;
using Backend.Services;
using Backend.Services.AccountService;
using Backend.Services.Repository;
using Backend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class CertificateController : Controller
    {
        //[HttpGet]
        //public async Task<IActionResult> Index(CertificateViewModel accountViewModel)
        //{
        //    if (TempData["filter"] is not null)
        //    {
        //        accountViewModel.filter = TempData["filter"].ToString();
        //    }
        //    accountViewModel.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
        //    var responce = (await new CourseService().GetCourses(new CourseViewModel() { User = accountViewModel.User }));
        //    if (responce.StatusCode == Models.Enum.StatusCode.OK)
        //    {
        //        accountViewModel.AllCourses = responce.Data.AllCourses;
        //        return View(accountViewModel);
        //    }
        //    accountViewModel.AllCourses.Add(new Models.Course());
        //    return View(accountViewModel);
        //}

    }
}