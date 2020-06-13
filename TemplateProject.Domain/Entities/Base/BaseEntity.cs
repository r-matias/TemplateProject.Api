namespace TemplateProject.Entities.Model.Base
{
    public abstract class BaseEntity<TId>
    {
        public BaseEntity()
        {
            Active = true;
        }

        public TId Id { get; private set; }
        public bool Active { get; private set; }
    }
}
