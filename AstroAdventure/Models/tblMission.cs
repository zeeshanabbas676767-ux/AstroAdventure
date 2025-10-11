namespace AstroAdventure.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblMission")]
    public partial class tblMission
    {
        [Key]
        public int MissionID { get; set; }

        [Required]
        [StringLength(200)]
        public string MissionName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LaunchDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public string Objective { get; set; }

        public int? RelatedPlanetID { get; set; }

        public virtual tblPlanet tblPlanet { get; set; }
    }
}
