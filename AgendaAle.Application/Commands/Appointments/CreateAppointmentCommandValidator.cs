using FluentValidation;

namespace AgendaAle.Application.Commands.Appointments;

public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("O título do agendamento não pode estar vazio.")
            .MaximumLength(100).WithMessage("O título deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("a descrição deve ter no máximo 500 caracteres.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("A data é obrigatória.")
            .Must(BeAFutureDate).WithMessage("Você não pode marcar um compromisso no passado.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("O ID do usuário é obrigatório.");
    }

    private bool BeAFutureDate(DateTime date)
    {
        return date > DateTime.UtcNow;
    }
}