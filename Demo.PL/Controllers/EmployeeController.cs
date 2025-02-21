using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //IEmployeeRepository _employeeRepository;
        //IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchInput)
        {
            var employees = Enumerable.Empty<Employee>();



            //if (string.IsNullOrEmpty(SearchInput))
            //{
              employees = await _unitOfWork.EmployeeRepository.GetAll();

            //}
            //else
            //{
            //    employees = await _unitOfWork.EmployeeRepository.GetName(SearchInput);
            //}

            var result = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {

            ViewData["Departments"] = await _unitOfWork.DepartmentRepository.GetAll();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel model)
        {
            if(model.ImageName is not null)
                model.ImageName = DocumentSettings.UploadFile(model.Image, "Images");

            var result = _mapper.Map<Employee>(model);

            if (ModelState.IsValid)
            {
                _unitOfWork.EmployeeRepository.Add(result);
                //var count = _unitOfWork.Complete();
                //if (count > 0)
                //{
                //    return RedirectToAction("Index");
                //}
            }


            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> DetailsAsync(string id, string viewName = "Details")
        {
            if (id == null)
                return BadRequest();

            var employee = await _unitOfWork.EmployeeRepository.Get(id);
            if (employee == null)
                return NotFound();
            var result = _mapper.Map<EmployeeViewModel>(employee);

            return View(viewName, result);
        }
        [HttpGet]
        public async Task<IActionResult> EditAsync(string id)
        {

            ViewData["Departments"] = await _unitOfWork.DepartmentRepository.GetAll();


            return await DetailsAsync(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]string id , EmployeeViewModel model)
        {
            if(id != model.Id)
                return BadRequest();

            if(model.ImageName != null)
            {
                DocumentSettings.DeleteFile(model.ImageName, "Images");
            }
            model.ImageName = DocumentSettings.UploadFile(model.Image, "Images");


            var employee = _mapper.Map<Employee>(model);

            if(ModelState.IsValid)
                {
                _unitOfWork.EmployeeRepository.Update(employee,id);
                //var count = _unitOfWork.Complete();
                //if (count > 0)
                //{
                //    return RedirectToAction("Index");
                //}
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAsync(string id)
        {

            return await DetailsAsync(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete([FromRoute] string id ,Employee model)
        {
            if(id != model.Id)
                return BadRequest();

            var employee = _mapper.Map<Employee>(model);


            if (ModelState.IsValid)
            {
                _unitOfWork.EmployeeRepository.Delete(id);
                //var count = _unitOfWork.Complete();
                //if (count > 0)
                //    if(model.ImageName is not null) 
                //    {
                //    DocumentSettings.DeleteFile(model.ImageName, "Images");
                //    }
                //    return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
