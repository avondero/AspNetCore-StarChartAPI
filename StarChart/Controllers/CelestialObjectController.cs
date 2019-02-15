namespace StarChart.Controllers
{
    #region Usings

    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using StarChart.Data;
    using StarChart.Models;

    #endregion

    [ApiController]
    [Route("")]
    public class CelestialObjectController : ControllerBase
    {
        #region Champs

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructeurs et destructeurs

        public CelestialObjectController(ApplicationDbContext context)
        {
            this._context = context;
        }

        #endregion

        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var celestialObject = this._context.CelestialObjects.Find(id);
            if (celestialObject == null)
            {
                return this.NotFound();
            }

            celestialObject.Satellites = this._context.CelestialObjects.Where(o => o.OrbitedObjectId == id).ToList();
            return this.Ok(celestialObject);

        }

        [HttpGet("{name}", Name = "GetByName")]
        public IActionResult GetByName(string name)
        {
            var celestialObjects = this._context.CelestialObjects.Where(o => o.Name == name);
            if (!celestialObjects.Any())
            {
                return this.NotFound();
            }

            Parallel.ForEach(celestialObjects, co => co.Satellites = this._context.CelestialObjects.Where(o => o.OrbitedObjectId == co.Id).ToList());

            return this.Ok(celestialObjects);

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var celestialObjects = this._context.CelestialObjects.ToList();
            Parallel.ForEach(celestialObjects, co => co.Satellites = this._context.CelestialObjects.Where(o => o.OrbitedObjectId == co.Id).ToList());
            return this.Ok(celestialObjects);

        }

        [HttpPost]
        public IActionResult Create([FromBody] CelestialObject celestialObject)
        {
            this._context.CelestialObjects.Add(celestialObject);
            this._context.SaveChanges();

            return this.CreatedAtRoute("GetById", new { Id = celestialObject.Id }, celestialObject);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CelestialObject celestial)
        {
            var celestialObject = this._context.CelestialObjects.Find(id);
            if (celestialObject == null)
            {
                return this.NotFound();
            }

            celestialObject.Name = celestial.Name;
            celestialObject.OrbitalPeriod = celestial.OrbitalPeriod;
            celestialObject.OrbitedObjectId = celestial.OrbitedObjectId;

            this._context.Update(celestialObject);
            this._context.SaveChanges();
            return this.NoContent();
        }


        [HttpPatch("{id}/{name}")]
        public IActionResult RenameObject(int id, string celestialName)
        {
            var celestialObject = this._context.CelestialObjects.Find(id);
            if (celestialObject == null)
            {
                return this.NotFound();
            }

            celestialObject.Name = celestialName;

            this._context.Update(celestialObject);
            this._context.SaveChanges();
            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var celestialObject = this._context.CelestialObjects.Where(o => o.Id == id || o.OrbitedObjectId == id);
            if (!celestialObject.Any())
            {
                return this.NotFound();
            }
            
            this._context.RemoveRange(celestialObject);
            this._context.SaveChanges();
            return this.NoContent();
        }
    }
}
