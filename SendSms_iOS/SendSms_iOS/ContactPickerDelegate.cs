using System;
using ContactsUI;
using Contacts;

namespace SendSms_iOS
{
    public class ContactPickerDelegate : CNContactPickerDelegate
    {
        public ContactPickerDelegate()
        {
        }

        public ContactPickerDelegate(IntPtr handle) : base(handle)
        {
        }
        public override void ContactPickerDidCancel(CNContactPickerViewController picker)
        {
            // Raise the selection canceled event
            RaiseSelectionCanceled();
        }

        public override void DidSelectContact(CNContactPickerViewController picker, 
            CNContact contact)
        {
            // Raise the contact selected event
            RaiseContactSelected(contact);
        }

        public override void DidSelectContactProperty(CNContactPickerViewController picker, 
            CNContactProperty contactProperty)
        {
            // Raise the contact property selected event
            RaiseContactPropertySelected(contactProperty);
        }

        public delegate void SelectionCanceledDelegate();
        public event SelectionCanceledDelegate SelectionCanceled;

        internal void RaiseSelectionCanceled()
        {
            this.SelectionCanceled?.Invoke();
        }

        public delegate void ContactSelectedDelegate(CNContact contact);
        public event ContactSelectedDelegate ContactSelected;

        internal void RaiseContactSelected(CNContact contact)
        {
            this.ContactSelected?.Invoke(contact);
        }

        public delegate void ContactPropertySelectedDelegate(CNContactProperty property);
        public event ContactPropertySelectedDelegate ContactPropertySelected;

        internal void RaiseContactPropertySelected(CNContactProperty property)
        {
            this.ContactPropertySelected?.Invoke(property);
        }
    }
}