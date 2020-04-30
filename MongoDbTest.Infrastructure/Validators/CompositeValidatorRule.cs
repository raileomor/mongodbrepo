using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;

namespace MongoDbTest.Infrastructure.Validators
{
    public class CompositeValidatorRule : IValidationRule
    {
        private readonly IValidator[] _validators;

        public CompositeValidatorRule(params IValidator[] validators)
        {
            _validators = validators;
        }

        public IEnumerable<IPropertyValidator> Validators
        {
            get { yield break; }
        }

        public string[] RuleSets
        {
            get; set;
        }

        public void ApplyAsyncCondition(Func<PropertyValidatorContext, CancellationToken, Task<bool>> predicate, ApplyConditionTo applyConditionTo = ApplyConditionTo.AllValidators)
        {
            throw new NotImplementedException();
        }

        public void ApplyCondition(Func<PropertyValidatorContext, bool> predicate, ApplyConditionTo applyConditionTo = ApplyConditionTo.AllValidators)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ValidationFailure> Validate(ValidationContext context)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ValidationFailure>> ValidateAsync(ValidationContext context, CancellationToken cancellation)
        {
            var result = new List<ValidationFailure>();
            //List<Task<ValidationResult>> tasks = new List<Task<ValidationResult>>();

            IEnumerable<Task<ValidationResult>> tasks = _validators.Select(val => val.ValidateAsync(context));

            ValidationResult[]  validationResults = await Task.WhenAll(tasks);

            foreach (var vr in validationResults)
            {
                result.AddRange(vr.Errors);
            }

            return result;
        }
    }
}