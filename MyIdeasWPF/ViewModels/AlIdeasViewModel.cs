using Ideas.DataAccess.BaseTypes;
using Ideas.DataAccess.UtilityTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.ViewModels
{
    public class AllIdeasViewModel : IdeasViewModel
    {
        protected override void GetIdeas()
        {
            using (IUnitOfWork transaction = DbFactory.GetUnitOfWork())
            {
                ideas = transaction.IdeaRepo.GetByQuery(i => i.Status != (int)IdeaStatus.Archived, ideas => ideas.OrderByDescending(i => i.Created)).ToList();
            }
        }

    }
}
 