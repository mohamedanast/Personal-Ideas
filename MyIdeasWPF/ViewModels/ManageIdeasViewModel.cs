using Ideas.DataAccess.BaseTypes;
using Ideas.DataAccess.UtilityTypes;
using Ideas.UI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.ViewModels
{
    public class ManageIdeasViewModel : IdeasViewModel
    {
        protected override void GetIdeas()
        {
            using (IUnitOfWork transaction = DbFactory.GetUnitOfWork())
            {
                ideas = transaction.IdeaRepo.GetByQuery(i => i.Status != (int)IdeaStatus.Archived, ideas => ideas.OrderByDescending(i => i.Created)).ToList();
            }
        }

        public override IEnumerable<IdeaView> IdeasViewData
        {
            get
            {
                if (ideas == null) GetIdeas();
                Dictionary<string, string> statusCollection = IdeaCommon.GetStatusCollection(IdeaStatus.Archived, false);
                IList<IdeaView> ideasViewData = new List<IdeaView>();
                return ideas.Select(idea => new IdeaView
                {
                    Idea = idea,
                    SecondDisplayColumn = statusCollection[((IdeaStatus)idea.Status).ToString()]
                });
            }
        }
    }
}
 