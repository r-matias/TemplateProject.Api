using System;

namespace TemplateProject.Entities.Model.Base
{
    public abstract class CreatedEntity<TId> : BaseEntity<TId>
    {
        public virtual DateTime CreatedDate { get; set; }
    }
}
