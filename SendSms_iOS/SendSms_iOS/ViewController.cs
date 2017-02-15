using Contacts;
using ContactsUI;
using Foundation;
using System;
using System.Linq;
using UIKit;

namespace SendSms_iOS
{
    public partial class ViewController : UIViewController
    {
        public void SendSms(string phoneNumber)
        {
            var smsTo = NSUrl.FromString($"sms:{phoneNumber}");
            if (UIApplication.SharedApplication.CanOpenUrl(smsTo))
            {
                UIApplication.SharedApplication.OpenUrl(smsTo);
            }
            else
            {
                UIAlertView alert = new UIAlertView()
                {
                    Title = "Error",
                    Message = "SMS is unavailable for the selected number."
                };
                alert.AddButton("OK");
                alert.Show();
            }
        }

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            this.SelectContactButton.TouchUpInside += SelectContactButton_TouchUpInside;
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void SelectContactButton_TouchUpInside(object sender, EventArgs e)
        {
            // Create a new picker
            var picker = new CNContactPickerViewController();

            // A contact has many properties (email, phone numbers, birthday)
            // and you must specify which properties you want to fetch,
            // in this case PhoneNumbers
            picker.DisplayedPropertyKeys = new NSString[] { CNContactKey.PhoneNumbers };
            picker.PredicateForEnablingContact = NSPredicate.FromValue(true);
            picker.PredicateForSelectionOfContact = NSPredicate.FromValue(true);

            // Respond to selection
            var pickerDelegate = new ContactPickerDelegate();
            picker.Delegate = pickerDelegate;

            // If the user cancels the contact selection,
            // show an empty string
            pickerDelegate.SelectionCanceled += () => {
                this.SelectedContactField.Text = "";
            };

            // If the user selects a contact,
            // show the full name
            pickerDelegate.ContactSelected += (contact) => {
                this.SelectedContactField.Text = $"{contact.GivenName} {contact.FamilyName}";

                // If the contact has phone numbers
                    if (contact.PhoneNumbers != null)
                    {
                    // Query for mobile only
                    // Each PhoneNumber has a label with a description
                    // and a Value with the actual phone number 
                    // exposed by the StringValue property
                        var mobilePhone = contact.PhoneNumbers.
                                          Where(p => p.Label.Contains("Mobile")).
                                          Select(p => p.Value.StringValue); 

                    // If at least one mobile phone number
                        if (mobilePhone != null)
                        {
                            // Generate a new data source
                            var tblSource = new TableSource(mobilePhone.ToArray(), this);
                            // and perform data-binding
                            this.TableView.Source = tblSource;

                        } 
                    }
                };

                pickerDelegate.ContactPropertySelected += (property) => {
                this.SelectedContactField.Text = property.Value.ToString();
            };

            // Display picker
            PresentViewController(picker, true, null);
        }

    }
}