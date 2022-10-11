﻿namespace AuthMicr;
using AuthMicr.Helpers;
using AuthMicr.Services;
using Microsoft.Identity.Client;
using System.Diagnostics;
using AuthenticationToken = Microsoft.Datasync.Client.AuthenticationToken;

public partial class MainPage : ContentPage
{
    public IPublicClientApplication IdentityClient { get; set; }

    public MainPage()
	{
		InitializeComponent();
    }

    public async Task<AuthenticationToken> GetAuthenticationToken()
    {
        if (IdentityClient == null)
        {
            object parentWindow = null;
#if ANDROID
            parentWindow = Platform.CurrentActivity;
#endif
            IdentityClient = PlatformService.GetIdentityClient(parentWindow);
        }

        var accounts = await IdentityClient.GetAccountsAsync();
        AuthenticationResult result = null;
        bool tryInteractiveLogin = false;

        try
        {
            result = await IdentityClient
                .AcquireTokenSilent(Constants.Scopes, accounts.FirstOrDefault())
                .ExecuteAsync();
        }
        catch (MsalUiRequiredException)
        {
            tryInteractiveLogin = true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"MSAL Silent Error: {ex.Message}");
        }

        if (tryInteractiveLogin)
        {
            try
            {
                result = await IdentityClient
                    .AcquireTokenInteractive(Constants.Scopes)
                    .ExecuteAsync()
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"MSAL Interactive Error: {ex.Message}");
            }
        }

        return new AuthenticationToken
        {
            DisplayName = result?.Account?.Username ?? "",
            ExpiresOn = result?.ExpiresOn ?? DateTimeOffset.MinValue,
            Token = result?.AccessToken ?? "",
            UserId = result?.Account?.Username ?? ""
        };
    }
}
