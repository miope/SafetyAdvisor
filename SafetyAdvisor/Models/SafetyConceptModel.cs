using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SafetyAdvisor.Models
{
    public class SafetyConceptModel
    {
        public EvaluationItem EvaluationItem { get; set; }
        public IEnumerable<string> Files { get; set; }
    }
}