namespace Ideas.DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IdeaAttachment
    {
        [Key]
        public int AttachmentId { get; set; }

        public int IdeaId { get; set; }

        [Required]
        public byte[] Contents { get; set; }

        public virtual Idea Idea { get; set; }
    }
}
