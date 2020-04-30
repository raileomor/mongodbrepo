using FluentValidation;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Validators
{
    public class AccountProductValidator: AbstractValidator<Account>, IAccountProductValidator
    {
        public AccountProductValidator()
        {
            RuleFor(x => x.Products).Must(x => x.Contains("InvestmentStock")).WithMessage("InvestmentStock no encontrado");
            RuleFor(x => x.Products).Must(x => x.Contains("Commodity")).WithMessage("Commodity no encontrado");
        }
    }
}