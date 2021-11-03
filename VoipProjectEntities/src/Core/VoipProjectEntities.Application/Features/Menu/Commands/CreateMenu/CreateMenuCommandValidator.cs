using FluentValidation;

namespace VoipProjectEntities.Application.Features.Menu.Commands.CreateMenu
{
    public class CreateMenuCommandValidator : AbstractValidator<CreateMenuCommand>
    {
        public CreateMenuCommandValidator()
        {
            RuleFor(p => p.MenuLink)
                .NotNull();
        }
    }
}
