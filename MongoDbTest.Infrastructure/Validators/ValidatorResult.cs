using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using MongoDbTest.Infrastructure.Interfaces;

namespace MongoDbTest.Infrastructure.Validators
{
    public class ValidatorResult
    {
        public string Rule { get; }
        private readonly List<ValidatorError> _errors  = new List<ValidatorError>();
        public bool IsValid => !Errors.Any();
        public IEnumerable<ValidatorError> Errors => _errors;
        public object ServiceResult { get; }

        public ValidatorResult()
        {
        }

        public ValidatorResult(ValidationResult validationResult, IValidator validator)
        {
            var validatorResult = validator as IValidatorResult;
            Rule = validatorResult.Rule;
            ServiceResult = validatorResult.ServiceResult;
            AddErrors(validationResult.Errors);
        }

        public void AddError(string propertyName, string errorMessage)
        {
            _errors.Add(new ValidatorError(propertyName, errorMessage));
        }

        public void AddErrors(IList<ValidationFailure> failures)
        {
            foreach (var vf in failures)
            {
                AddError(vf.PropertyName, vf.ErrorMessage);
            }
        }
    }

    public class ValidatorError
    {
        public string PropertyName { get; }
        public string ErrorMessage { get; }
        public ValidatorError(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }
    }
}