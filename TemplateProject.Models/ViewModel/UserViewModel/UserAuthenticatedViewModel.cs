namespace TemplateProject.Models.ViewModel.UserViewModel
{
    public class UserAuthenticatedViewModel : UserBaseViewModel
    {
        new private string Password { get; set; }
        new private bool Active { get; set; }
    }
}
