using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateProject.Domain.Interfaces;
using TemplateProject.Entities.Model.Base;
using TemplateProject.Models;
using TemplateProject.Models.ViewModel;

namespace TemplateProject.Service.Services
{
    public class BaseService<TEntity, TId> : IService<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        private readonly IUnitOfWork<TEntity, TId> _unitOfWork;
        private readonly IMapper _mapper;

        public BaseService(IUnitOfWork<TEntity, TId> unitOfWork,
                           IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseValueModel<TViewModel>> Post<TValidator, TViewModel>(TViewModel obj) 
            where TValidator : AbstractValidator<TViewModel>
            where TViewModel : BaseViewModel
        {
            var responseValue = await Validate(obj, (TValidator)Activator.CreateInstance(typeof(TValidator), _unitOfWork));

            if(responseValue.IsValid)
            {
                var entity = _mapper.Map<TViewModel, TEntity>(obj);
                await _unitOfWork.Repository.Insert(entity);
            }

            return responseValue;
        }

        public async Task<ResponseValueModel<TViewModel>> Put<TValidator, TViewModel>(TId id, TViewModel obj) 
            where TValidator : AbstractValidator<TViewModel>
            where TViewModel : BaseViewModel
        {
            var responseValue = await Validate(obj, Activator.CreateInstance<TValidator>());

            if(responseValue.IsValid)
            {
                var entityDb = await _unitOfWork.Repository.Select(id);
                var entityDbMappedFromViewModel = _mapper.Map(obj, entityDb);
                await _unitOfWork.Repository.Update(entityDbMappedFromViewModel);
            }

            return responseValue;
        }

        public async Task<ResponseValueModel<bool>> Delete(TId id)
        {
            var responseValue = new ResponseValueModel<bool>();

            if (EqualityComparer<TId>.Default.Equals(id, default(TId)))
                responseValue.AddError($"Id {id} inválido.");

            if (responseValue.IsValid)
                await _unitOfWork.Repository.Delete(id);

            responseValue.SetData(responseValue.IsValid);

            return responseValue;
        }

        public async Task<ResponseValueModel<IList<TViewModel>>> Get<TViewModel>()
            where TViewModel : BaseViewModel
        {
            var responseValue = new ResponseValueModel<IList<TViewModel>>();

            var listEntity = await _unitOfWork.Repository.Select();
            var listViewModel = _mapper.Map<IList<TEntity>, IList<TViewModel>>(listEntity);

            responseValue.SetData(listViewModel);

            return responseValue;
        }

        public async Task<ResponseValueModel<TViewModel>> Get<TViewModel>(TId id)
            where TViewModel : BaseViewModel
        {
            var responseValue = new ResponseValueModel<TViewModel>();

            if (EqualityComparer<TId>.Default.Equals(id, default(TId)))
                responseValue.AddError($"Id {id} inválido.");

            if(responseValue.IsValid)
            {
                var entity = await _unitOfWork.Repository.Select(id);
                var viewModel = _mapper.Map<TEntity, TViewModel>(entity);

                responseValue.SetData(viewModel);
            }

            return responseValue;
        }

        private async Task<ResponseValueModel<TViewModel>> Validate<TViewModel>(TViewModel obj, AbstractValidator<TViewModel> validator)
            where TViewModel : BaseViewModel
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            var resultsValidate = await validator.ValidateAsync(obj);

            var responseValue = new ResponseValueModel<TViewModel>();

            responseValue.AddError(resultsValidate);

            if (responseValue.IsValid)
                responseValue.SetData(obj);

            return responseValue;
        }
    }
}
