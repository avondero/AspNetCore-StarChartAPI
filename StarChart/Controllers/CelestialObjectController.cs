namespace StarChart.Controllers
{
    #region Usings

    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using StarChart.Data;

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
    }
}
