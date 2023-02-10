﻿using System.Reflection;
using BLL.CertificateService;
using BLL.Models.CertificateOut;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Controller]
    [Route("[controller]/[action]")]
    public class CertificateController : Controller
    {
        private readonly ICertificateService _certificateService;

        public CertificateController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        public async Task<ActionResult<ICollection<CertificateOutDto>>> GetAll()
        {
            var userId = HttpContext.User.GetId();
            var result = await _certificateService.GetCertificatesByUser(userId);

            return Ok(result);
        }
    }
}