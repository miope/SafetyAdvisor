using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SafetyAdvisor.Models
{
    public class EvaluationItem
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Caption is a required field")]
        public string Caption { get; set; }
        [Required(ErrorMessage="Content is a required field")]
        public string Content { get; set; }
        [Display(Name="Reactive?")]
        public bool IsReactive { get; set; }

        public int? ParentId { get; set; }
        public EvaluationItem Parent { get; set; }

        public virtual ICollection<EvaluationItem> Children { get; set; }

        public IEnumerable<EvaluationItem> GetFamillyTree()
        {
            var _current = this;
            while (_current != null)
            {
                yield return _current;
                _current = _current.Parent;
            }
        }
    }
}