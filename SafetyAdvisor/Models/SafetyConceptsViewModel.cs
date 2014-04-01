using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafetyAdvisor.Models
{
    public class SafetyConceptsViewModel
    {
        public IEnumerable<SafetyConceptModel> PreventiveSafetyConcepts { get; set; }
        public IEnumerable<SafetyConceptModel> ReactiveSafetyConcepts { get; set; }
    }
}