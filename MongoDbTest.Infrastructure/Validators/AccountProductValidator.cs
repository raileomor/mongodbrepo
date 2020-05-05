using FluentValidation;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Validators
{
    public class AccountProductValidator: AbstractValidator<Account>, IValidatorResult
    {
        public string Rule => "ContainsProducts";
        public object ServiceResult => null;
        public AccountProductValidator()
        {
            RuleFor(x => x.Products).Must(x => x.Contains("InvestmentStock")).WithMessage("InvestmentStock no encontrado");
            RuleFor(x => x.Products).Must(x => x.Contains("Commodity")).WithMessage("Commodity no encontrado");
        }
    }
}