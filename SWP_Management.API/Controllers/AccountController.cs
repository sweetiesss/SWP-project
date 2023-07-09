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
            var lecturerList = _lecturerRepository.GetList();
            ViewData["LecturerList"] = lecturerList;


            var studentList = _studentRepository.GetList();
            ViewData["StudentList"] = studentList;

            return View();
        }

        // Add
        [HttpPost]
        public async Task<IActionResult> AddAccount(string Username, string Password, string? lecturerId, string? studentId)
        {

            var student = _studentRepository.GetById(studentId);
            var lecturer = _lecturerRepository.GetById(lecturerId);
            AddAccount();


            //Validation
            bool returnSwitch = false;

            if (!new Validator().validate(Username, @"^(\d|\w)+$"))
            {
                ViewData["Username"] = "Username";
                returnSwitch = true;
            }

            if (!new Validator().validate(Password, @"^(\d|\w)+$"))
            {
                ViewData["Password"] = "Password";
                returnSwitch = true;
            }

            if (returnSwitch) return View();



            if (lecturerId != null && studentId != null)
            {
                ViewBag.Result = "Error Role";
                return View();
            }

            Account? account = new Account();

            var accountList = _accountRepository.GetList().ToList();
            //Check for student
            for (int i = 0; i < accountList.Count; i++)
            {
                if (accountList[i].StudentId != null)
                {
                    if (accountList[i].StudentId.Equals(studentId))
                    {
                        ViewBag.Result = "Duplicate Student";
                        return View();
                    }
                }
            }

            //Check for lecturer
            for (int i = 0; i < accountList.Count; i++)
            {
                if (accountList[i].TeacherId != null)
                {
                    if (accountList[i].TeacherId.Equals(lecturerId))
                    {
                        ViewBag.Result = "Duplicate Lecturer";
                        return View();
                    }
                }
            }

            //Check existing Username
            var usernameDupe = accountList.Where(p => p.Username.Equals(Username)).FirstOrDefault();
            if (usernameDupe != null)
            {
                ViewBag.Result = "Duplicate Username";
                return View();
            }
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

            UpdateAccount(id);

            Account? account = new Account();
            account = _accountRepository.GetById(id);



            //Validation
            bool returnSwitch = false;

            if (!new Validator().validate(Username, @"^(\d|\w)+$"))
            {
                ViewData["Username"] = "Username";
                returnSwitch = true;
            }

            if (!new Validator().validate(Password, @"^(\d|\w)+$"))
            {
                ViewData["Password"] = "Password";
                returnSwitch = true;
            }

            if (returnSwitch) return View(account);



            var student = _studentRepository.GetById(studentId);
            var lecturer = _lecturerRepository.GetById(lecturerId);

            //If duplicate exist, call back method to get two list on Viewdata and the chosen account
            //UpdateAccount(id);

            //Account? account = new Account();
            //account = _accountRepository.GetById(id);
            var accountList = _accountRepository.GetList().ToList();
            if (account != null)
            {
                //Check for student
                if(account.StudentId != null) 
                {
                    //Check existing Id
                    
                    for (int i = 0; i< accountList.Count; i++)
                    {
                        //Don't combine this if to the other one
                        if (accountList[i].StudentId != null)
                        {
                            if (accountList[i].StudentId.Equals(studentId) &&
                                accountList[i].Id != account.Id)
                            {
                                ViewBag.Result = "Duplicate Student";
                                return View(account);
                            }
                        }
                    }
                }

                //Check for lecturer
                if(account.TeacherId != null)
                {
                    for (int i = 0; i < accountList.Count; i++)
                    {
                        if (accountList[i].TeacherId != null)
                        {
                            if (accountList[i].TeacherId.Equals(lecturerId) &&
                                accountList[i].Id != account.Id)
                            {
                                ViewBag.Result = "Duplicate Lecturer";
                                return View(account);
                            }
                        }
                    }
                }

                //Check existing Username
                var usernameDupe = accountList.Where(p => p.Username.Equals(Username)).FirstOrDefault();
                if (usernameDupe != null && usernameDupe.Id != account.Id)
                {
                    ViewBag.Result = "Duplicate Username";
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
