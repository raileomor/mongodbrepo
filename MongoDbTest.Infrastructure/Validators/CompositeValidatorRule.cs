using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using MongoDbTest.Infrastructure.Interfaces;

namespace MongoDbTest.Infrastructure.Validators
{
    public class CompositeValidatorRule : IAccountValidationRule
    {
        private readonly List<IValidator> _validators;

        private ValidationResult[] _results;

        public CompositeValidatorRule()
        {
            _validators = new List<IValidator>();
        }

        public IEnumerable<IPropertyValidator> Validators
        {
            get { yield break; }
        }

        public void Add(IValidator validator)
        {
            if (validator != null)
                _validators.Add(validator);
        }

        public IEnumerable<ValidationResult> Results => _results.ToList();

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
            var _valArray = _validators.ToArray();
            IEnumerable<Task<ValidationResult>> tasks = _valArray.Select(val => val.ValidateAsync(context));

            ValidationResult[]  validationResults = await Task.WhenAll(tasks);
            _results = validationResults;

            foreach (var vr in validationResults)
            {
                result.AddRange(vr.Errors);
            }

            return result;
        }
    }
}