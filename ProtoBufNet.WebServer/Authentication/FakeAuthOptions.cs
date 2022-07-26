using Microsoft.AspNetCore.Authentication;

namespace ProtoBufNet.WebServer;

public class FakeAuthOptions : AuthenticationSchemeOptions
{
    public bool AlwaysAuthenticate { get; set; } = false;
}
