namespace NetCore.Application.Customer.ChangeEmail
{
    public sealed record ChangeEmailCommand(string OldEmail, string NewEmail);
}
