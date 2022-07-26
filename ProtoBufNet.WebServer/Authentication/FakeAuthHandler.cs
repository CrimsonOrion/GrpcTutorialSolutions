using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

using System.Security.Claims;
using System.Text.Encodings.Web;

namespace ProtoBufNet.WebServer;

internal class FakeAuthHandler : AuthenticationHandler<FakeAuthOptions>
{
    public const string SchemeName = "Fake";

    public FakeAuthHandler(IOptionsMonitor<FakeAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Options.AlwaysAuthenticate)
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        ClaimsIdentity claimsIdentity = new(SchemeName);
        AuthenticationTicket ticket = new(new(claimsIdentity), Scheme.Name);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
