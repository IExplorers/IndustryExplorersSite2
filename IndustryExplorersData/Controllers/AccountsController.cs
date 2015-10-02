using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using IndustryExplorersData.Models;
using System.Web.Http.Cors;
using System.Web.Security;


namespace IndustryExplorersData.Controllers
{
    [EnableCors(origins: "http://localhost:62216/", headers: "*", methods: "*")]

    public class AccountsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Accounts
        public IQueryable<Account> GetAccounts()
        {
            return db.Accounts;
        }

        // GET:api/Accounts/alias/{myalias}
        [Route("api/accounts/alias/{myalias}")]
        public async Task<Account> GetAccountByAlias(string myalias)
        {
            List<Account> accountList = await db.Accounts.Where(i => i.alias == myalias).ToListAsync<Account>();
            return accountList.First<Account>();

        }

        // GET: api/Accounts/5
        public async Task<IHttpActionResult> GetAccount(Guid id)
        {
            Account account = await db.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // POST: api/Accounts
        [ResponseType(typeof(Account))]
        public async Task<IHttpActionResult> PostAccount(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            account.Id = Guid.NewGuid();
            account.active = false;
            account.validation_token = Guid.NewGuid();
            account.date_created = DateTime.Today;
            account.date_updated = DateTime.Today;
            db.Accounts.Add(account);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountAliasExists(account.alias))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = account.Id }, account);
        }

        // PUT: api/Accounts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAccount(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != account.Id)
            //{
            //    return BadRequest();
            //}

            db.Entry(account).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountAliasExists(account.alias))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Accounts/5
        [ResponseType(typeof(Account))]
        public async Task<IHttpActionResult> DeleteAccount(Guid id)
        {
            Account account = await db.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            db.Accounts.Remove(account);
            await db.SaveChangesAsync();

            return Ok(account);
        }
        private bool AccountAliasExists(string alias)
        {
            return db.Accounts.Count(e => e.alias == alias) > 0;
        }
    }
}

