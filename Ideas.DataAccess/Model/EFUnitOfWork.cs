using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ideas.DataAccess.Entities;
using System.Data.Entity;
using Ideas.DataAccess.BaseTypes;

namespace Ideas.DataAccess.Model
{
    public class EFUnitOfWork : IUnitOfWork
    {
        protected DbContext dbContext;

        IRepository<Idea> ideaRepo;
        IRepository<IdeaAttachment> ideaAttachmentRepo;
        IRepository<Tag> tagRepo;
        IRepository<IdeaTag> ideaTagRepo;

        public EFUnitOfWork(DbContext context)
        {
            dbContext = context;
        }

        public IRepository<IdeaAttachment> IdeaAttachmentRepo
        {
            get
            {
                if (ideaAttachmentRepo == null)
                    ideaAttachmentRepo = new Repository<IdeaAttachment>(dbContext);

                return ideaAttachmentRepo;
            }
        }

        public IRepository<Idea> IdeaRepo
        {
            get
            {
                if (ideaRepo== null)
                    ideaRepo = new Repository<Idea>(dbContext);

                return ideaRepo;
            }
        }

        public IRepository<IdeaTag> IdeaTagRepo
        {
            get
            {
                if (ideaTagRepo == null)
                    ideaTagRepo = new Repository<IdeaTag>(dbContext);

                return ideaTagRepo;
            }
        }

        public IRepository<Tag> TagRepo
        {
            get
            {
                if (tagRepo == null)
                    tagRepo = new Repository<Tag>(dbContext); 

                return tagRepo;
            }
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }

        #region IDisposable
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                    dbContext.Dispose();
                }
                
                disposed = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
