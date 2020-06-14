using TemplateProject.Service.Validators.UserValidators;

namespace TemplateProject.Service.Validators.UserValidators
{
    public class UserUpdateValiadtor : UserCommonValidator
    {
        public UserUpdateValiadtor()
        {
            RequiredFields();
        }
    }
}
