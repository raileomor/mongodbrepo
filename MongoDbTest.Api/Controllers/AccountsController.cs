using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Models;
using MongoDbTest.Infrastructure.Validators;

namespace MongoDbTest.Api.Controllers
{
    /// <summary>
    /// Accounts Administrator
    /// </summary>
    /// <summary xml:lang="es">
    /// Administrador de cuentas
    /// </summary>
    [Route("accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountServices _accountService;
        private readonly IAccountValidator _accountValidator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accountService"></param>
        /// <param name="accountValidator"></param>
        public AccountsController(IAccountServices accountService, IAccountValidator accountValidator)
        {
            _accountService = accountService;
            _accountValidator = accountValidator;
        }

        /// <summary>
        /// Get Account
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener Cuenta
        /// </summary>
        /// <returns>Return Account</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<Account>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var result = await _accountService.GetAllAsync();

            return Ok(result);
        }

        /// <summary>
        /// Get Account by Id
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener Cuenta por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return Account</returns>
        [HttpGet("{id:length(24)}", Name = "GetAccount")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Account), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(string id)
        {
            var account = await _accountService.GetByIdAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        /// <summary>
        /// Validate Account by Id
        /// </summary>
        /// <summary xml:lang="es">
        /// Validar Cuenta por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return Validation Results</returns>
        [HttpGet("{id:length(24)}/validate")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<ValidationResult>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ValidateAccount(string id)
        {
            var account = await _accountService.GetByIdAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            //account.Id = ObjectId.GenerateNewId().ToString();
            await _accountValidator.ValidateAsync(account);
            var a = _accountValidator.Results;
            return Ok(a);
        }

        /// <summary>
        /// Get Accounts by filter
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener Cuentas por filtro
        /// </summary>
        /// <param name="accountFilter"></param>
        /// <returns>Return Accounts</returns>
        [HttpPost]
        [Route("FindAccounts")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<Account>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> FindAccounts(Account accountFilter)
        {
            var result = await _accountService.GetOneAsync(ac => ac.AccountId == accountFilter.AccountId);

            return Ok(result);
        }

        /// <summary>
        /// Create Account
        /// </summary>
        /// <summary xml:lang="es">
        /// Crear Cuenta
        /// </summary>
        /// <param name="account"></param>
        /// <returns>Return Account</returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Account), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create(Account account)
        {
            await _accountService.AddAsync(account);

            return CreatedAtRoute("GetAccount", new { id = account.Id }, account);
        }

        /// <summary>
        /// Update Account
        /// </summary>
        /// <summary xml:lang="es">
        /// Actualizar Cuenta
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accountIn"></param>
        /// <returns>Return No Content</returns>
        [HttpPut("{id:length(24)}")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(string id, Account accountIn)
        {
            var account = await _accountService.GetByIdAsync(id);

            if (id != accountIn.Id || account == null || accountIn.Id != account.Id)
            {
                return NotFound();
            }

            await _accountService.ReplaceAsync(id, accountIn);

            return NoContent();
        }

        /// <summary>
        /// Update Account
        /// </summary>
        /// <summary xml:lang="es">
        /// Actualizar Cuenta
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accountIn"></param>
        /// <returns>Return No Content</returns>
        [HttpPut("{id:length(24)}/products")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateProducts(string id, Account accountIn)
        {
            var account = await _accountService.GetByIdAsync(id);

            if (id != accountIn.Id || account == null || accountIn.Id != account.Id)
            {
                return NotFound();
            }

            await _accountService.UpdateProductsAsync(accountIn);

            return NoContent();
        }

        /// <summary>
        /// Delete Account
        /// </summary>
        /// <summary xml:lang="es">
        /// Eliminar Cuenta
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return No Content</returns>
        [HttpDelete("{id:length(24)}")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            var account = await _accountService.GetByIdAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            await _accountService.DeleteAsync(account.Id);

            return NoContent();
        }
    }
}