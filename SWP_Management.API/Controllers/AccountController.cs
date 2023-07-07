using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.API.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ILecturerRepository _lecturerRepository;

        public AccountController(IAccountRepository accountRepository,
                                 IStudentRepository studentRepository,
                                 ILecturerRepository lecturerRepository)
        {
            _accountRepository = accountRepository;
            _studentRepository = studentRepository;
            _lecturerRepository = lecturerRepository;
        }
        public IActionResult Index()
        {
            var accountList = _accountRepository.GetList().ToList();
            return View(accountList);
        }

        public async Task<IActionResult> AddAccount()
        {
            var assignmentList = _lecturerRepository.GetList();
            ViewData["LecturerList"] = assignmentList;


            var studentList = _studentRepository.GetList();
            ViewData["StudentList"] = studentList;

            return View();
        }

        public ViewResult SelectAssignmentStudent() => View();


        // Add
        [HttpPost]
        public async Task<IActionResult> SelectAccount(string Username, string Password, string lecturerId, string studentId)
        {
            Account account = new Account();

            var student = _studentRepository.GetById(studentId);
            var lecturer = _lecturerRepository.GetById(lecturerId);
            // Insert data to an entity for add
            account.Username = Username;
            account.Password = Password;
            account.TeacherId = lecturerId;
            account.StudentId = studentId;
            account.Teacher = lecturer;
            account.Student = student;

            // add
            _accountRepository.Add(account);


            return RedirectToAction("Index");
        }


        //Update
        public async Task<IActionResult> UpdateAccount(int Id)
        {

            var lecturerList = _lecturerRepository.GetList();
            ViewData["LecturerList"] = lecturerList;


            var studentList = _studentRepository.GetList();
            ViewData["StudentList"] = studentList;

            var account = _accountRepository.GetById(Id);
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAccount(int id, string Username, string Password, string? lecturerId, string? studentId)
        {

            var student = _studentRepository.GetById(studentId);
            var lecturer = _lecturerRepository.GetById(lecturerId);

            //If duplicate exists, these list are neccessary so UpdateAccount page won't return null in AssignmentList/StudentList Viewdata
            var lecturerList = _lecturerRepository.GetList();
            ViewData["AssignList"] = lecturerList;


            var studentList = _studentRepository.GetList();
            ViewData["StudentList"] = studentList;

            var account = _accountRepository.GetById(id);
            if (account != null)
            {
                var existing = _accountRepository.GetList().Where(p => p.TeacherId.Equals(lecturerId)
                                                                    && p.StudentId.Equals(studentId)).FirstOrDefault();
                if (existing != null && existing.Id != account.Id)
                {
                    ViewBag.Result = "Duplicate";
                    return View(account);
                }
                account.Username = Username;
                account.Password = Password;
                account.TeacherId = lecturerId;
                account.StudentId = studentId;
                account.Teacher = lecturer;
                account.Student = student;
            }


            _accountRepository.Update(account);
            return RedirectToAction("Index");
        }

        // Delete
        [HttpPost]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            _accountRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
