using Ideas.DataAccess.BaseTypes;
using Ideas.DataAccess.Entities;
using Ideas.DataAccess.Model;
using Ideas.DataAccess.UtilityTypes;
using Ideas.Utilities;
using Ideas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ideas.UI.Utilities;
using System.Globalization;

namespace Ideas.ViewModels
{
    public class PopularTagsViewModel : ViewModel
    {
        protected IList<WeightedTag> tags;

        public virtual IEnumerable<WeightedTag> PopularTags
        {
            get
            {
                if (tags == null) GetTags();

                return tags;
            }
        }

        protected virtual void GetTags()
        {
            using (IUnitOfWork transaction = DbFactory.GetUnitOfWork())
            {
                // Include Tag for joining
                IQueryable<IdeaTag> ideaTags = transaction.IdeaTagRepo.GetByQuery(includeProperties: "Tag") as IQueryable<IdeaTag>;

                tags = (from it in ideaTags
                        group it by it.Tag into grouped
                        orderby grouped.Count() descending
                        select new WeightedTag { Tag = grouped.Key, Count = grouped.Count() })
                        .Take(Constants.PopularTagCount).OrderBy(t=>t.Tag.TagName).ToList();

                // Calculate font size based on count
                int TotalCount = tags.Sum(t => t.Count);
                int NumTags = tags.Count;
                foreach(WeightedTag tag in tags)
                {
                    tag.Weight = Constants.TagBaseFontSize + (NumTags * tag.Count) / NumTags;
                }
            }
        }
    }
}
