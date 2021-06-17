using AppCore.Entities;
using AppCore.Interfaces;
using AutoMapper;
using Infrastructure.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Models.Employees;

namespace Web.Controllers
{
    public class EmployeesController : Controller
    {
        private AppDbContext db = new AppDbContext();
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeImportService _employeeImportService;
        private readonly IEmployeeValidator _employeeValidator;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepository,
            IEmployeeImportService employeeImportService,
            IEmployeeValidator employeeValidator,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _employeeImportService = employeeImportService;
            _employeeValidator = employeeValidator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var employees = _employeeRepository.GetAll().ToList();
            var model = _mapper.Map<List<EmployeeItemViewModel>>(employees);
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null)
                return HttpNotFound();

            var model = _mapper.Map<EmployeeDetailsViewModel>(employee);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new EmployeeManageViewModel 
            { 
                IsEditMode = false,
                BirthDate = DateTime.Now
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeManageViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var employee = _mapper.Map<Employee>(model);
            employee.Number = model.Number;
            employee.IsExternal = model.IsExternal;
            var validationErrors = _employeeValidator.GetValidationErrors(employee);
            if (validationErrors.Any())
            {
                SetModelState(validationErrors);
                return View(model);
            }

            _employeeRepository.Add(employee);
            await _unitOfWork.CommitAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null)
                return HttpNotFound();

            var model = _mapper.Map<EmployeeManageViewModel>(employee);
            model.IsEditMode = true;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EmployeeManageViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var employee = _employeeRepository.GetById(model.Id);
            _mapper.Map(model, employee);
            _employeeRepository.Update(employee);
            await _unitOfWork.CommitAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null)
                return HttpNotFound();

            var model = _mapper.Map<EmployeeItemViewModel>(employee);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(EmployeeItemViewModel model)
        {
            var employee = _employeeRepository.GetById(model.Id);
            if (employee == null)
                return HttpNotFound();

            _employeeRepository.Remove(employee);
            await _unitOfWork.CommitAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Import()
        {
            var model = new ImportViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Import(ImportViewModel model)
        {
            var importResult = await _employeeImportService.Import(model.Data.InputStream);
            using (var memoryStream = new MemoryStream())
            using (TextWriter textWriter = new StreamWriter(memoryStream))
            {
                foreach (var item in importResult)
                {
                    textWriter.WriteLine(item);
                }

                textWriter.Flush();
                return File(memoryStream.GetBuffer(), "text/plain", "log.txt");
            }
        }
      
        private void SetModelState(List<string> errors)
        {
            foreach (var error in errors)
                ModelState.AddModelError("", error);
        }
    }
}
