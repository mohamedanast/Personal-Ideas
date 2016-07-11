using Ideas.DataAccess.BaseTypes;
using Ideas.DataAccess.UtilityTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.ViewModels
{
    public class FruitfuldeasViewModel : IdeasViewModel
    {
        protected override void GetIdeas()
        {
            using (IUnitOfWork transaction = DbFactory.GetUnitOfWork())
            {
                ideas = transaction.IdeaRepo.GetByQuery(i => i.Status == IdeaStatus.Implemented || i.Status == IdeaStatus.OnImplementation,
                    elts => elts.OrderByDescending(elt => elt.Created)).Items.ToList();
            }
        }
    }
}
 