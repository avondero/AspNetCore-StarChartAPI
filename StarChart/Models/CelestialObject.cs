namespace StarChart.Models
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public class CelestialObject
    {
        #region Propriétés et indexeurs

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public TimeSpan OrbitalPeriod { get; set; }

        public int? OrbitedObjectId { get; set; }

        [NotMapped]
        public List<CelestialObject> Satellites { get; set; }

        #endregion
    }
}
