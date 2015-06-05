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































	public class EventListAdapter: BaseAdapter<Event>
	{
//comment added
		private Event[] _feeds;
		private Activity _context;

		public EventListAdapter(Event[] feeds, Activity context)
			: base()
		{
			_feeds = feeds;
			_context = context;
		}
		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView;
			if (view == null)
			{
				view = _context.LayoutInflater.Inflate(Resource.Layout.EventItemView, null);
			}
			view.FindViewById<TextView>(Resource.Id.lblEventTitle).Text = String.Format("{0}", _feeds[position].EventName);
			view.FindViewById<TextView>(Resource.Id.lblEventHost).Text = String.Format("{0}", _feeds[position].Club);
			view.FindViewById<TextView>(Resource.Id.lblEventVenueNTime).Text = String.Format("Venue: {0} | {1}", _feeds[position].Venue , _feeds[position].EventDate);

			return view;
		}

		public override int Count
		{
			get { return _feeds.Count(); }
		}

		public override Event this[int position]
		{
			get { return _feeds[position]; }
		}
	}
}