namespace TemplateProject.Models.ViewModel.UserViewModel
{
    public abstract class UserBaseViewModel : BaseViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
    }
}
