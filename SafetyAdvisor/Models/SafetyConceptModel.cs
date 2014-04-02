using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

using SafetyAdvisor.Helpers;

namespace SafetyAdvisor.Models
{
    public class SafetyConceptModel
    {
        private EvaluationItem _evaluationItem;

        public IEnumerable<string> Files { get; private set; }

        public EvaluationItem EvaluationItem
        {
            get
            {
                return _evaluationItem;
            }
            set
            {
                _evaluationItem = value;
                this.Files = BackloadFileManager.GetFiles(_evaluationItem.Id);
            }
        }

        public SafetyConceptModel()
        {
            this.Files = new List<string>();
        }

        public SafetyConceptModel(EvaluationItem item)
        {
            this.EvaluationItem = item;
        }
    }
}