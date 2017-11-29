﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TalentosEasy;
using TalentosEasy.Models;

namespace TalentosEasy.Controllers
{
    public class TalentoController : ApiController
    {
        private ModelDados db = new ModelDados();

        // GET: api/Talento
        public IQueryable<Talento> GetTalento()
        {
            return db.Talento;
        }

        // GET: api/Talento/5
        [ResponseType(typeof(Talento))]
        public IHttpActionResult GetTalento(int id)
        {
            Talento talento = db.Talento.Find(id);
            if (talento == null)
            {
                return NotFound();
            }

            return Ok(talento);
        }

        // PUT: api/Talento/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTalento(int id, Talento talento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != talento.IdTalento)
            {
                return BadRequest();
            }

            db.Entry(talento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TalentoExists(id))
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

        // POST: api/Talento
        [ResponseType(typeof(Talento))]
        public IHttpActionResult PostTalento(Talento talento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Talento.Add(talento);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = talento.IdTalento }, talento);
        }

        // DELETE: api/Talento/5
        [ResponseType(typeof(Talento))]
        public IHttpActionResult DeleteTalento(int id)
        {
            Talento talento = db.Talento.Find(id);
            if (talento == null)
            {
                return NotFound();
            }

            db.Talento.Remove(talento);
            db.SaveChanges();

            return Ok(talento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TalentoExists(int id)
        {
            return db.Talento.Count(e => e.IdTalento == id) > 0;
        }
    }
}