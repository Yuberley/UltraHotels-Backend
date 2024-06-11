using FluentValidation;

namespace HotelHub.Application.Hotels.Create;

internal sealed class AddHotelCommandValidator : AbstractValidator<AddHotelCommand>
{
    public AddHotelCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(200);

        RuleFor(c => c.Description).NotEmpty().MaximumLength(2000);

        RuleFor(c => c.Country).NotEmpty().MaximumLength(100);

        RuleFor(c => c.State).NotEmpty().MaximumLength(100);

        RuleFor(c => c.City).NotEmpty().MaximumLength(100);

        RuleFor(c => c.ZipCode).NotEmpty().MaximumLength(10);
        
        RuleFor(c => c.Street).NotEmpty().MaximumLength(200);
        
        // RuleFor(c => c.IsActive).NotNull();
    }
}