namespace Ideas.DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tag()
        {
            IdeaTags = new HashSet<IdeaTag>();
        }

        public int TagId { get; set; }

        [Column("Tag")]
        [Required]
        [StringLength(50)]
        public string TagName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        public bool IsArchived { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IdeaTag> IdeaTags { get; set; }
    }
}
