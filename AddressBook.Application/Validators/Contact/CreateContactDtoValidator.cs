using AddressBook.Application.DTOs.Contact;
using FluentValidation;

namespace AddressBook.Application.Validators.Contact
{
    /// <summary>
    /// FluentValidation validator for <see cref="CreateContactDto"/>.
    /// </summary>
    /// <remarks>
    /// This validator defines all business validation rules required
    /// when creating a new contact.
    ///
    /// Validation is automatically executed by ASP.NET Core during
    /// model binding when <c>AddFluentValidationAutoValidation()</c>
    /// is configured in the API layer.
    /// </remarks>
    public class CreateContactDtoValidator : AbstractValidator<CreateContactDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateContactDtoValidator"/> class
        /// and configures validation rules for contact creation.
        /// </summary>
        public CreateContactDtoValidator()
        {
            // Name must be provided and cannot be empty
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            // Email is optional, but must be valid if provided
            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("Invalid email address");

            // Phone is optional, but must match international number format if provided
            RuleFor(x => x.Phone)
                .Matches(@"^\+?[0-9]{7,15}$")
                .When(x => !string.IsNullOrWhiteSpace(x.Phone))
                .WithMessage("Invalid phone number");
        }
    }
}