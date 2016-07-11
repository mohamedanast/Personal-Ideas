namespace Ideas.DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IdeaTag
    {
        public int IdeaTagId { get; set; }

        public int IdeaId { get; set; }

        public int TagId { get; set; }

        public virtual Idea Idea { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
