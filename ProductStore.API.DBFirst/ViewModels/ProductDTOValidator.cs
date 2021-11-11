using FluentValidation;
using ProductStore.API.DBFirst.ViewModels.Product;

namespace ProductStore.API.DBFirst.ViewModels
{
    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public enum ErrorLevel
        {
            [StringValue("sms")]
            SMS = 1,

            [StringValue("email")]
            EMAIL = 2
        }

        public ProductDTOValidator()
        {
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.Price).InclusiveBetween(1, 100);
            RuleFor(model => model.Description).NotEmpty();
            RuleFor(x => x.Name).Must(IsValidName).WithMessage("Please specify correct name");
            RuleFor(x => x.Name).IsEnumName(typeof(ErrorLevel));
        }

        private bool IsValidName(string name)
        {
            if (name.Equals("hoa"))
            {
                return true;
            }
            return false;
        }
    }
}