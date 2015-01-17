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
	[Activity(Label = "Event Detail", Icon = "@drawable/ic_launcher")]
	public class EventListItemActivity : Activity
	{
		private Event _event;
		private TextView lblEventTitle,
			lblEventHostClub,
			lblEventVenueNTime,
			lblDescription,
			lblContactPerson,
			lblContactEmail,
			lblEventMoreInfo;

		private ImageButton imgBtnEventFav, imgBtnCall, imgBtnEmail, imgBtnLink;
		private ImageView imgEventCoverPic;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.EventListItemView);

			lblEventTitle = FindViewById<TextView>(Resource.Id.lblEventTitle);
			lblDescription = FindViewById<TextView>(Resource.Id.lblDescription);
			lblEventHostClub = FindViewById<TextView>(Resource.Id.lblEventHost);
			lblEventVenueNTime = FindViewById<TextView>(Resource.Id.lblEventVenueNTime);
			lblContactPerson = FindViewById<TextView>(Resource.Id.lblContactPerson);
			lblContactEmail = FindViewById<TextView>(Resource.Id.lblContactEmail);
			lblEventMoreInfo = FindViewById<TextView>(Resource.Id.lblEventMoreInfo);

			imgBtnEventFav = FindViewById<ImageButton>(Resource.Id.imgEventFav);
			imgBtnEventFav.Click += OnFavButtonClicked;

			imgBtnCall = FindViewById<ImageButton>(Resource.Id.imgCall);
			imgBtnCall.Click += OnCallButtonClicked;

			imgBtnEmail = FindViewById<ImageButton>(Resource.Id.imgEmail);
			imgBtnEmail.Click += OnEmailButtonClicked;

			imgBtnLink = FindViewById<ImageButton>(Resource.Id.imgLink);
			imgBtnLink.Click += OnLinkButtonClicked;

			imgEventCoverPic = FindViewById<ImageView>(Resource.Id.imgEventCoverPic);


			_event = new Event
			{
				EventName = Intent.GetStringExtra("eventname"),
				Description = Intent.GetStringExtra("description"),
				Club = Intent.GetStringExtra("club"),
				Venue = Intent.GetStringExtra("venue"),
				EventDate = Convert.ToDateTime(Intent.GetStringExtra("eventdate")),
				Updates = Intent.GetStringExtra("updates"),
				Contacts = Intent.GetStringExtra("contacts"),
				Favorite = Intent.GetStringExtra("favorite")
			};


			lblEventTitle.Text = _event.EventName;
			lblEventHostClub.Text = _event.Club;
			lblEventVenueNTime.Text = String.Format("Venue: {0} | {1}", _event.Venue, _event.EventDate);
			lblDescription.Text = _event.Description;
			lblContactPerson.Text = _event.Contacts;
			lblContactEmail.Text = _event.Updates;
			lblEventMoreInfo.Text = _event.Favorite;

		}

		private void OnFavButtonClicked(object sender, EventArgs e)
		{
			//imgBtnEventFav.SetImageDrawable(Resource.Drawable.eventIcon);
		}

		private void OnLinkButtonClicked(object sender, EventArgs e)
		{
			//var item = _event.Updates;
			//var uri = Android.Net.Uri.Parse("url:" + item);
			//var intent = new Intent(Intent.ActionView, uri);
			//StartActivity(intent);
		}

		private void OnEmailButtonClicked(object sender, EventArgs e)
		{
			var item = _event.Contacts;
			var uri = Android.Net.Uri.Parse("sms:" + item);
			var intent = new Intent(Intent.ActionView, uri);
			StartActivity(intent);
		}

		private void OnCallButtonClicked(object sender, EventArgs e)
		{
			var item = _event.Contacts;
			var uri = Android.Net.Uri.Parse("tel:" + item);
			var intent = new Intent(Intent.ActionView, uri);
			StartActivity(intent);
		}
	}
}