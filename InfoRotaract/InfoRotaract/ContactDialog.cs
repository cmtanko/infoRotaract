using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using InfoRotaract.DataModel;

namespace InfoRotaract
{
//co
mme


asdfasdfasdf
 

as
df
ASD
f












    public class ContactDialog : DialogFragment
    {
        private Contact _contact;
        private ImageButton btnCall, btnSMS;

        public ContactDialog()
        {
            
        }

        public ContactDialog(string firstName, string lastName, string club, string age, string sex, string email, string phone, string bloodGroup,
            string available,string address)
        {
            _contact = new Contact();
            _contact.FirstName = firstName;
            _contact.LastName = lastName;
            _contact.Club = club;
            _contact.Age = age;
            _contact.Sex = sex;
            _contact.Email = email;
            _contact.Phone = phone;
            _contact.BloodGroup = bloodGroup;
            _contact.Available = available;
            _contact.Address = address;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.ContactItemList, container,false);

            var tvName = view.FindViewById<TextView>(Resource.Id.tvContactName);
            var tvEmail = view.FindViewById<TextView>(Resource.Id.tvContactEmail);
            var tvPhone = view.FindViewById<TextView>(Resource.Id.tvContactPhone);
            var tvAddress = view.FindViewById<TextView>(Resource.Id.tvContactAddress);
            var tvClub = view.FindViewById<TextView>(Resource.Id.tvContactClub);
            var tvBloodGroup = view.FindViewById<TextView>(Resource.Id.tvContactBloodGroup);
            var tvAvailable = view.FindViewById<TextView>(Resource.Id.tvContactAvailability);

            btnCall = view.FindViewById<ImageButton>(Resource.Id.btnCall);
            btnCall.Click += OnCallButtonClicked;

            btnSMS = view.FindViewById<ImageButton>(Resource.Id.btnSMS);
            btnSMS.Click += OnSmsButtonClicked;


            tvName.Text = String.Format("{0} {1}",_contact.FirstName, _contact.LastName);
            tvEmail.Text = _contact.Email;
            tvPhone.Text = _contact.Phone;
            tvAddress.Text = _contact.Address;
            tvClub.Text = _contact.Club;
            tvBloodGroup.Text = _contact.BloodGroup;
            tvAvailable.Text = _contact.Available=="1" ? "is Available" : "not Available";
            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;

        }
        private void OnCallButtonClicked(object sender, EventArgs e)
        {
            var item = _contact.Phone;
            var uri = Android.Net.Uri.Parse("tel:" + item);
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }

        private void OnSmsButtonClicked(object sender, EventArgs e)
        {
            var item = _contact.Phone;
            var uri = Android.Net.Uri.Parse("sms:" + item);
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }
    }
}