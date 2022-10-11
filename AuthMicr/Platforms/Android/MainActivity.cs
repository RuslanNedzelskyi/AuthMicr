using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Microsoft.Identity.Client;

namespace AuthMicr;

[Activity(Exported = true)]
[IntentFilter(new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
    DataHost = "auth",
    DataScheme = "msal{client-id}")]
public class MsalActivity : BrowserTabActivity
{
    protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
    {
        base.OnActivityResult(requestCode, resultCode, data);
        // Return control to MSAL
        AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
    }
}
