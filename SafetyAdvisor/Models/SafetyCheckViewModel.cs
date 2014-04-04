using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafetyAdvisor.Models
{

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