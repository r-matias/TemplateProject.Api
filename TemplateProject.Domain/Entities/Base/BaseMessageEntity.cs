using System;
using TemplateProject.Domain.Entities.Model.Base;

namespace TemplateProject.Domain.Entities.Model
{
    public abstract class BaseMessageEntity : CreatedEntity<Guid>
    {
        public string Text { get; set; }
    }
}
