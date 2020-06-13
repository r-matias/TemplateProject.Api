using System.ComponentModel.DataAnnotations;

namespace TemplateProject.Infra.CrossCutting.Enums
{
    public enum ELocation
    {
        Undefined = 0,
        Europe = 1,
        [Display(Name = "South America")]
        SouthAmerica = 2,
        [Display(Name = "North America")]
        NorthAmerica = 3
    }
}
