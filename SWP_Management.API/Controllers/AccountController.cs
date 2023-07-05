using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.API.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly ILecturerRepository _lecturerRepository;

        public AccountController(IAccountRepository accountRepository,
                                 ISubjectRepository subjectRepository,
                                 ISemesterRepository semesterRepository,
                                 ILecturerRepository lecturerRepository)
        {
            _accountRepository = accountRepository;
            _subjectRepository = subjectRepository;
            _semesterRepository = semesterRepository;
            _lecturerRepository = lecturerRepository;
        }
        public IActionResult Index()
        {
            var accountList = _accountRepository.GetList().ToList();
            return View(accountList);
        }


    }
}
