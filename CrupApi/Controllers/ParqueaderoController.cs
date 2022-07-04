using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
//using System.Net.Http;
using System.Web.Http;
using Datos;

namespace CrupApi.Controllers
{
    public class ParqueaderoController : ApiController
    {
        private ParqueaderoEntities context = new ParqueaderoEntities();

        [HttpGet]
        public async Task<IEnumerable<Parqueadero>> Get()
        {
            using (ParqueaderoEntities db = new ParqueaderoEntities())
            {
                return await db.Parqueadero.AsNoTracking().ToListAsync();
            }
        }

        [HttpGet]
        public Parqueadero Get(int id)
        {
            using (ParqueaderoEntities db = new ParqueaderoEntities())
            {
                return db.Parqueadero.FirstOrDefault(x => x.Id==id);
            }
        }

        [HttpPost]
        public IHttpActionResult Agregar([FromBody]Parqueadero parqueadero)
        {
            if (ModelState.IsValid)
            {
                context.Parqueadero.Add(parqueadero);
                context.SaveChanges();
                return Ok(parqueadero);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IHttpActionResult Actualizar(int id, [FromBody] Parqueadero parqueadero)
        {
            if (ModelState.IsValid)
            {
                var parqueaderoExiste = context.Parqueadero.Count(e => e.Id == id) > 0;
                if (parqueaderoExiste)
                {
                    context.Parqueadero.Add(parqueadero);
                    context.SaveChanges();
                    return Ok(parqueadero);
                }
                else
                {
                    return NotFound();
                }
                
            }
            else
            {
                return BadRequest();
            }
        }



        [HttpDelete]
        public IHttpActionResult Eliminar(int id)
        {
            var parqueaderoEncontrado = context.Parqueadero.Find(id);
            if (parqueaderoEncontrado != null)
            {
                context.Parqueadero.Remove(parqueaderoEncontrado);
                context.SaveChanges();
                return Ok(parqueaderoEncontrado);
            }
            else
            {
                return NotFound();
            }

        }


    }
}
