using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using MongoDbTest.Infrastructure.Interfaces;

namespace MongoDbTest.Infrastructure.Validators
{
    public class ValidatorResult: IValidatorResult
    {
        private readonly List<ValidatorError> _errors  = new List<ValidatorError>();
        public bool IsValid => !Errors.Any();
        public IEnumerable<ValidatorError> Errors => _errors;
        public object ServiceResult => null;

        public ValidatorResult()
        {
        }

        public ValidatorResult(ValidationResult validationResult)
        {
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