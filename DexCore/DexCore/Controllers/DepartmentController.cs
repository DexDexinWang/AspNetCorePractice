using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DexCore.Models;
using DexCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DexCore.Controllers
{
    public class DepartmentController: Controller
    {

        private readonly IDepartmentService _departmentService;
        private readonly IOptions<DexCoreOptions> _dexCoreOptions;
        public DepartmentController(IDepartmentService departmentService, IOptions<DexCoreOptions> dexCoreOptions)
        {
            _departmentService = departmentService;
            _dexCoreOptions = dexCoreOptions;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Department Index";
            var departments = await _departmentService.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Title = "Add Department";
            return View(new Department());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Department model)
        {
            if (ModelState.IsValid)
            {
                await _departmentService.Add(model);
            }
            return RedirectToAction(nameof(Index));
        } 
    }
}
