using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace TemplateProject.Models
{
    public class ResponseValueModel<TViewModel>
    {
        private List<string> _errors;

        public ResponseValueModel()
        {
            _errors = new List<string>();
        }

        public bool IsValid => !_errors.Any();

        public IReadOnlyList<string> Errors => _errors;

        public TViewModel Data { get; private set; }

        public void SetData(TViewModel data)
        {
            Data = data;
        }

        public void AddError(string message)
        {
            _errors.Add(message);
        }

        public void AddError(ValidationResult validationResult)
        {
            var validationErrorsAdd = validationResult.Errors.Where(x => !_errors.Contains(x.ErrorMessage)).ToList();

            foreach (ValidationFailure error in validationErrorsAdd)
            {
                _errors.Add(error.ErrorMessage);
            }
        }
    }
}
