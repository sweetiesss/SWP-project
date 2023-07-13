using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;
using System.Collections.Generic;

namespace SWP_Management.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult GetAccounts()
        {
            var accounts = _accountRepository.GetList();
            return Ok(accounts);
        }

        [HttpGet("{accountId}")]
        public IActionResult GetAccount(int accountId)
        {
            var account = _accountRepository.GetById(accountId);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost]
        public IActionResult CreateAccount(Account account)
        {
            _accountRepository.Add(account);
            return CreatedAtAction(nameof(GetAccount), new { accountId = account.Id }, account);
        }

        [HttpPut("{accountId}")]
        public IActionResult UpdateAccount(int accountId, Account account)
        {
            if (accountId != account.Id)
            {
                return BadRequest();
            }

            var existingAccount = _accountRepository.GetById(accountId);
            if (existingAccount == null)
            {
                return NotFound();
            }

            _accountRepository.Update(account);
            return NoContent();
        }

        [HttpDelete("{accountId}")]
        public IActionResult DeleteAccount(int accountId)
        {
            var account = _accountRepository.GetById(accountId);
            if (account == null)
            {
                return NotFound();
            }

            _accountRepository.Delete(accountId);
            return NoContent();
        }
    }
}
