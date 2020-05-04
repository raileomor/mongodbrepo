using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Validators
{
    public class AccountValidatorRules : IAccountValidationRule
    {
        private readonly List<IValidator> _validators;
        private readonly List<ValidationResult> _results;
        public IEnumerable<ValidationResult> Results => _results.ToList();
        public IEnumerable<IPropertyValidator> Validators
        {
            get { yield break; }
        }

        public AccountValidatorRules()
        {
            _validators = new List<IValidator>();
            _results = new List<ValidationResult>();
        }

        public void Add(IValidator<Account> validator)
        {
            if (validator != null)
                _validators.Add(validator);
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
            var failures = new List<ValidationFailure>();
            var validationResults = _validators.Select(val => val.Validate(context));

            _results.AddRange(validationResults.ToList());

            foreach (var vr in validationResults)
            {
                failures.AddRange(vr.Errors);
            }

            return failures;
        }

        public async Task<IEnumerable<ValidationFailure>> ValidateAsync(ValidationContext context, CancellationToken cancellation)
        {
            var failures = new List<ValidationFailure>();
            IEnumerable<Task<ValidationResult>> tasks = _validators.Select(val => val.ValidateAsync(context));

            var validationResults = await Task.WhenAll(tasks);
            _results.AddRange(validationResults.ToList());

            foreach (var vr in validationResults)
            {
                failures.AddRange(vr.Errors);
            }

            return failures;
        }
    }
}