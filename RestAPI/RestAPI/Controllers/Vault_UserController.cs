using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RestAPI;

namespace RestAPI.Controllers
{
    [Authorize]
    public class Vault_UserController : ApiController
    {
        private VaultEntities db = new VaultEntities();

        // GET: api/Vault_User
        public IQueryable<Vault_User> GetVault_User()
        {
            return db.Vault_User;
        }

        // GET: api/Vault_User/5
        [ResponseType(typeof(Vault_User))]
        public IHttpActionResult GetVault_User(string id)
        {
            Vault_User vault_User = db.Vault_User.Find(id);
            if (vault_User == null)
            {
                return NotFound();
            }

            return Ok(vault_User);
        }

        // PUT: api/Vault_User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVault_User(string id, Vault_User vault_User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vault_User.username)
            {
                return BadRequest();
            }

            db.Entry(vault_User).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Vault_UserExists(id))
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

        // POST: api/Vault_User
        [ResponseType(typeof(Vault_User))]
        public IHttpActionResult PostVault_User(Vault_User vault_User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vault_User.Add(vault_User);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Vault_UserExists(vault_User.username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = vault_User.username }, vault_User);
        }

        // DELETE: api/Vault_User/5
        [ResponseType(typeof(Vault_User))]
        public IHttpActionResult DeleteVault_User(string id)
        {
            Vault_User vault_User = db.Vault_User.Find(id);
            if (vault_User == null)
            {
                return NotFound();
            }

            db.Vault_User.Remove(vault_User);
            db.SaveChanges();

            return Ok(vault_User);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Vault_UserExists(string id)
        {
            return db.Vault_User.Count(e => e.username == id) > 0;
        }
    }
}