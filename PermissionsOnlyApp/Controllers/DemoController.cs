using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeatureAuthorize.PolicyCode;
using Microsoft.AspNetCore.Mvc;
using PermissionParts;

namespace PermissionsOnlyApp.Controllers
{
    public class DemoController : Controller
    {
        [HasPermission(Permissions.DemoPermission)]
        public IActionResult Index()
        {
            return View();
        }
    }
}