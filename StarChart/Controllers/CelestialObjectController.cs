namespace StarChart.Controllers
{
    #region Usings

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
    }
}
