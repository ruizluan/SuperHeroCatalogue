using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using SuperHeroCatalogue.Application.Interfaces;
using SuperHeroCatalogue.Application.Models;
using SuperHeroCatalogue.Infra.CrossCutting.IoC;

namespace SuperHeroCatalogue.WebService.Providers
{
    internal class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {

        private readonly string _publicClientId;
        private static readonly IKernel Kernel = new StandardKernel(new NinjectModulo());
        private readonly IAuthService _authService = Kernel.Get<IAuthService>();

        public static UserModel User;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException(nameof(publicClientId));
            }

            _publicClientId = publicClientId;
        }

        public override Task GrantResourceOwnerCredentials
        (OAuthGrantResourceOwnerCredentialsContext context)
        {
            

            if (!_authService.AuthenticateUser(context.UserName, context.Password, out User))
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
            }

            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            var cookiesIdentity = new ClaimsIdentity(context.Options.AuthenticationType);

            var properties = CreateProperties(context.UserName);
            var ticket = new AuthenticationTicket(oAuthIdentity, properties);

            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication
        (OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri
        (OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId != _publicClientId) return Task.FromResult<object>(null);

            var expectedRootUri = new Uri(context.Request.Uri, "/");

            if (expectedRootUri.AbsoluteUri == context.RedirectUri)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
                {
                    { "userName", userName }
                };
            return new AuthenticationProperties(data);
        }
    }
}