using System;
using TemplateProject.Entities.Model.Base;

namespace TemplateProject.Entities.Model
{
    public abstract class BaseMessageEntity : CreatedEntity<Guid>
    {
        public string Text { get; set; }
    }
}
