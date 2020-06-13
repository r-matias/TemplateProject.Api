using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateProject.Entities.Model.Base;
using TemplateProject.Models;
using TemplateProject.Models.ViewModel;

namespace TemplateProject.Domain.Interfaces
{
    public interface IService<TEntity, TId> 
        where TEntity : BaseEntity<TId>
    {
        Task<ResponseValueModel<TViewModel>> Post<TValidator, TViewModel>(TViewModel obj) 
            where TValidator : AbstractValidator<TViewModel>
            where TViewModel : BaseViewModel;
        Task<ResponseValueModel<TViewModel>> Put<TValidator, TViewModel>(TId id, TViewModel obj)
            where TValidator : AbstractValidator<TViewModel>
            where TViewModel : BaseViewModel;
        Task<ResponseValueModel<bool>> Delete(TId id);
        Task<ResponseValueModel<TViewModel>> Get<TViewModel>(TId id)
            where TViewModel : BaseViewModel;
        Task<ResponseValueModel<IList<TViewModel>>> Get<TViewModel>()
            where TViewModel : BaseViewModel;
    }
}
