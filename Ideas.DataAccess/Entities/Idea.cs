namespace Ideas.DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using UtilityTypes;
    public partial class Idea
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Idea()
        {
            IdeaAttachments = new HashSet<IdeaAttachment>();
            IdeaTags = new HashSet<IdeaTag>();
        }

        public int IdeaId { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Modified { get; set; }

        [Column("Status", TypeName = "tinyint")]
        public byte Status { get; set; }
        //public IdeaStatus Status { get; set; }    // TODO: Why not able to use the status as enum? (Querying is not working) Retry?

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IdeaAttachment> IdeaAttachments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IdeaTag> IdeaTags { get; set; }
    }
}
