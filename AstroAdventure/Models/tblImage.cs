namespace AstroAdventure.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblImage")]
    public partial class tblImage
    {
        [Key]
        public int ImageID { get; set; }

        
        [StringLength(500)]
        public string Url { get; set; }

        [StringLength(200)]
        public string AltText { get; set; }

        public int? ArticleID { get; set; }

        public int? PlanetID { get; set; }

        public int? StarID { get; set; } 

        public virtual tblArticle tblArticle { get; set; }

        public virtual tblPlanet tblPlanet { get; set; }

        public virtual tblStar tblStar { get; set; }
    }
}
