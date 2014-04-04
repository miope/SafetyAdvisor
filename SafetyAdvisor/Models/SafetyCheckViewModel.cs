using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafetyAdvisor.Models
{

    public class SafetyCheckViewModel
    {
        public IEnumerable<SelectSafetyConceptEditorViewModel> PreviousItems { get; set; }
        public IEnumerable<SelectSafetyConceptEditorViewModel> CurrentItems { get; set; }

        public IEnumerable<SelectSafetyConceptEditorViewModel> GetPreviouslySelected()
        {
            return this.PreviousItems == null ? new List<SelectSafetyConceptEditorViewModel>() : this.PreviousItems.Where(i => i.Selected);
        }

        public IEnumerable<SelectSafetyConceptEditorViewModel> GetCurrentlySelected()
        {
            return this.CurrentItems == null ? new List<SelectSafetyConceptEditorViewModel>() : this.CurrentItems.Where(i => i.Selected);
        }

        public IEnumerable<int> GetPreviousItemsIds()
        {
            return this.PreviousItems == null ? new List<int>() : this.PreviousItems.Where(pi => pi != null).Select(pi => pi.Id).ToList();
        }

        public IEnumerable<int> GetCurrentItemsIds()
        {
            return this.CurrentItems == null ? new List<int>() : this.CurrentItems.Select(ci => ci.Id).ToList();
        }

        public bool CanGoNext()
        {
            return this.CurrentItems != null && this.CurrentItems.SelectMany(i => i.EvaluationItem.Children).Any();
        }

        public bool CanGoPrev()
        {
            return this.CurrentItems != null && GetPreviousItemsIds().Any();
        }
    }

    public class SelectSafetyConceptEditorViewModel : SafetyConceptModel
    {
        public int Id { get; set; }
        public bool Selected { get; set; }

        public SelectSafetyConceptEditorViewModel()
            : base()
        { 
        }

        public SelectSafetyConceptEditorViewModel(EvaluationItem item)
            : base(item)
        {
        }
    }
}