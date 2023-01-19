﻿using Backend.Models;
using Backend.Services;
using Backend.Services.AccountService;
using Backend.Services.AccountService.Interfaces;
using Backend.Services.Repository;
using Backend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System.Reflection;

namespace Backend.Controllers
{
    [Route("Certificate")]
    public class CertificateController : Controller
    {
        
        private readonly IAccountService accountService;
        public CertificateController(IAccountService _accountService)
        {
            accountService = _accountService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(CertificateViewModel certificateViewModel)
        {
            certificateViewModel.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            var responce = await new CertificateService().GetCertificates(new CertificateViewModel() { User = certificateViewModel.User });
            if (responce.StatusCode == Models.Enum.StatusCode.OK)
            {
                certificateViewModel.Certificates = responce.Data;
                return View(certificateViewModel);
            }
            return View(certificateViewModel);
        }
        [HttpGet("/DownloadCertificate/{id}")]
        public async Task<IActionResult> DownloadCertificate(CertificateViewModel certificateViewModel,long id)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "Files\\Certificates\\"+id;
            return File(System.IO.File.OpenRead(path), "application/octet-stream", Path.GetFileName(path));
        }

        [HttpGet("/ChangeLang/{id}")]
        public async Task<IActionResult> ChangeLang(int id)
        {
            User user = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            user.Lang = id;
            UserRepository ur = new UserRepository();
            await ur.Update(user);
            return RedirectToAction("Index");
        }
    }
}