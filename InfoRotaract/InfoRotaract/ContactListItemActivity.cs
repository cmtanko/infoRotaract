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
	[Activity(Label = "Contact Detail", Icon = "@drawable/ic_launcher")]
	public class ContactListItemActivity: Activity
	{
		private Contact _contact;
		private ImageButton btnCall, btnSMS;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			//ActionBar.Hide(); 
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.ContactItemList);

			btnCall = FindViewById<ImageButton>(Resource.Id.btnCall);
			btnCall.Click += OnCallButtonClicked;

			btnSMS = FindViewById<ImageButton>(Resource.Id.btnSMS);
			btnSMS.Click += OnSmsButtonClicked;

			_contact = new Contact();
			_contact.FirstName = Intent.GetStringExtra("firstname");
			_contact.LastName = Intent.GetStringExtra("lastname");
			_contact.Email = Intent.GetStringExtra("email");
			_contact.Phone = Intent.GetStringExtra("phone");
			_contact.Address = Intent.GetStringExtra("address");
			_contact.Club = Intent.GetStringExtra("club");
			_contact.Age = Intent.GetStringExtra("age");
			_contact.Sex = Intent.GetStringExtra("sex");
			_contact.BloodGroup = Intent.GetStringExtra("bloodgroup");
			_contact.Available = Convert.ToBoolean(Intent.GetStringExtra("available"));

			var tvName = FindViewById<TextView>(Resource.Id.tvContactName);
			var tvEmail = FindViewById<TextView>(Resource.Id.tvContactEmail);
			var tvPhone = FindViewById<TextView>(Resource.Id.tvContactPhone);
			var tvAddress = FindViewById<TextView>(Resource.Id.tvContactAddress);
			var tvClub = FindViewById<TextView>(Resource.Id.tvContactClub);
			var tvBloodGroup = FindViewById<TextView>(Resource.Id.tvContactBloodGroup);
			var tvAvailable = FindViewById<TextView>(Resource.Id.tvContactAvailability);


			tvName.Text = _contact.FirstName + " " + _contact.LastName + "[" + _contact.Sex + "/" + _contact.Age + "]";
			tvEmail.Text = _contact.Email;
			tvPhone.Text = _contact.Phone;
			tvAddress.Text = "Address: " + _contact.Address;
			tvClub.Text =  _contact.Club;
			tvBloodGroup.Text = _contact.BloodGroup;
			tvAvailable.Text = _contact.Available?"is Available":"not Available";
		}

		private void OnCallButtonClicked(object sender, EventArgs e)
		{
			var item = FindViewById<TextView>(Resource.Id.tvContactPhone);
			var uri = Android.Net.Uri.Parse("tel:" + item.Text);
			var intent = new Intent(Intent.ActionView, uri);
			StartActivity(intent);
		}

		private void OnSmsButtonClicked(object sender, EventArgs e)
		{
			var item = FindViewById<TextView>(Resource.Id.tvContactPhone);
			var uri = Android.Net.Uri.Parse("sms:" + item.Text);
			var intent = new Intent(Intent.ActionView, uri);
			StartActivity(intent);
		}
	}
}