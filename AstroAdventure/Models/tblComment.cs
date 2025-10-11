namespace AstroAdventure.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblComment")]
    public partial class tblComment
    {
        [Key]
        public int CommentID { get; set; }

        public int UserID { get; set; }

        public int? ArticleID { get; set; }

        public int? PlanetID { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual tblArticle tblArticle { get; set; }

        public virtual tblPlanet tblPlanet { get; set; }

        public virtual tblUser tblUser { get; set; }
    }
}
