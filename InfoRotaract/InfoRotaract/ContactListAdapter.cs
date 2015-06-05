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

//Add new things






sfsdfsfdsnamespace InfoRotaract
{
	public class ContactListAdapter: BaseAdapter<Contact>
	{
		private Contact[] _feeds;
		private Activity _context;

		public ContactListAdapter(Contact[] feeds, Activity context)
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
			if(view == null)
			{
				view = _context.LayoutInflater.Inflate(Resource.Layout.ContactItem, null);
			}
			view.FindViewById<TextView>(Resource.Id.tvName).Text = String.Format("{0} {1} [{2}]", _feeds[position].FirstName, _feeds[position].LastName,_feeds[position].BloodGroup);
			view.FindViewById<TextView>(Resource.Id.tvPhone).Text = String.Format("{0} / Phone: {1}", _feeds[position].Address, _feeds[position].Phone);
			return view;
		}

		public override int Count
		{
			get { return _feeds.Count();  }
		}

		public override Contact this[int position]
		{
			get { return _feeds[position];  }
		}
	}
}