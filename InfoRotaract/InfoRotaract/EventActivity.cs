
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using InfoRotaract.DataModel;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace InfoRotaract
{]
//dsf
asdf
asdfasdf'asd
f
asdfasdf
asd
f'
	[Activity(Label = "Events")]
	class EventActivity:ActionBarActivity
	{
		private List<Event> _events;
		private ListView _lvListEvents;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.EventActivityView);

			_lvListEvents = FindViewById<ListView>(Resource.Id.lvEvents);
			_lvListEvents.ItemClick += OnContactListItemClicked;

			var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
			SetSupportActionBar(toolbar);
			SupportActionBar.Title = "Info Rotaract";

			PopulateEvents();
		}

		private void OnContactListItemClicked(object sender, AdapterView.ItemClickEventArgs e)
		{
			var intent = new Intent(this, typeof(EventListItemActivity));
			var selectedFeed = _events[e.Position];
			intent.PutExtra("eventname", selectedFeed.EventName);
			intent.PutExtra("description", selectedFeed.Description);
			intent.PutExtra("club", selectedFeed.Club);
			intent.PutExtra("venue", selectedFeed.Venue);
			intent.PutExtra("eventdate", selectedFeed.EventDate.ToShortDateString());
			intent.PutExtra("updates", selectedFeed.Updates);
			intent.PutExtra("contacts", selectedFeed.Contacts);
			intent.PutExtra("favorite", selectedFeed.Favorite);
			StartActivity(intent);
		}

		private void PopulateEvents()
		{
			EventManager manager = new EventManager();
			_events = null;
			_events = manager.GetDisplayEvents();
			UpdateList();
		}
		private void UpdateList()
		{
			_lvListEvents.Adapter = new EventListAdapter(_events.ToArray(), this);
		}
		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.advanceSearch, menu);
			return base.OnCreateOptionsMenu(menu);
		}
	}
}