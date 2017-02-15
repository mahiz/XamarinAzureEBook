using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Authentication;
using TaskManager.iOS;
using Xamarin.Forms;

[assembly:Dependency(typeof(Authentication))]
namespace TaskManager.iOS
{
    public class Authentication : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider)
        {
            try
            {
                //attempt to find root view controller to present authentication page
                var window = UIKit.UIApplication.SharedApplication.KeyWindow;
                var root = window.RootViewController;
                if (root != null)
                {
                    var current = root;
                    while (current.PresentedViewController != null)
                    {
                        current = current.PresentedViewController;
                    }

                    //login and save user status
                    var user = await client.LoginAsync(current, provider);
                    return user;
                }
            }
            catch (Exception e)
            {
                e.Data["method"] = "LoginAsync";
            }

            return null;
        }
    }

}
