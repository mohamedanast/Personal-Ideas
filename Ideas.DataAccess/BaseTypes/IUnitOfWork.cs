using Ideas.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.DataAccess.BaseTypes
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Idea> IdeaRepo { get; }
        IRepository<IdeaAttachment> IdeaAttachmentRepo { get; }
        IRepository<Tag> TagRepo { get; }
        IRepository<IdeaTag> IdeaTagRepo { get; }

        void Commit();
    }
}
