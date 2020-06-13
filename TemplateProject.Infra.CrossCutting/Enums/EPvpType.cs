using System.ComponentModel.DataAnnotations;

namespace TemplateProject.Infra.CrossCutting.Enums
{
    public enum EPvpType
    {
        Undefined = 0,
        [Display(Name = "Open PvP")]
        OpenPvp = 1,
        [Display(Name = "Retro Hardcore PvP")]
        RetroHardcorePvp = 2,
        [Display(Name = "Optional PvP")]
        OptionalPvp = 3,
        [Display(Name = "Retro Open PvP")]
        RetroOpenPvp = 4,
        [Display(Name = "Hardcore PvP")]
        HardcorePvp = 5
    }
}
