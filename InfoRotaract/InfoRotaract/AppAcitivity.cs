using System;
using System.ComponentModel;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Toolbar = Android.Support.V7.Widget.Toolbar;
namespace InfoRotaract
{
	[Activity(Label = "InfoRtr", MainLauncher = true, Icon = "@drawable/ic_launcher")]
	[Android.Runtime.Register("android/support/sample/v7/ActionBarUsage")]
	public class AppAcitivity : ActionBarActivity
	{
		//TextView mSearchText;
		//int mSortMode = -1;
		//protected override void OnCreate(Bundle savedInstanceState)
		//{
		//	base.OnCreate(savedInstanceState);
		//	mSearchText = new TextView(this);
		//	SetContentView(Resource.Layout.Contacts);
		//}

		//public override bool OnCreateOptionsMenu(IMenu menu)
		//{
		//	var inflater = MenuInflater;
		//	inflater.Inflate(Resource.Menu.actions,menu);
		//	var arg1 = menu.FindItem(Resource.Id.action_search);

		//	var test = new Android.Support.V7.Widget.SearchView(this);
		//	arg1.SetActionView(test);

		//	var searchView = MenuItemCompat.GetActionView(arg1);
		//	var searchView2 = searchView as Android.Support.V7.Widget.SearchView;

		//	searchView2.QueryTextChange += OnQueryTextChange;
		//	return true;
		//}

		//private void OnQueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
		//{
		//	string newText = e.NewText;
		//	newText = string.IsNullOrEmpty(newText) ? "" : "Query = " + newText;
		//}

		//protected override void OnCreate(Bundle savedInstanceState)
		//{
		//	base.OnCreate(savedInstanceState);
		//	SetContentView(Resource.Layout.App);
		//	//ActionBar.Hide(); 

		//	TabHost.TabSpec spec;
		//	Intent intent;

		//	intent = new Intent(this, typeof(ContactsActivity));
		//	intent.AddFlags(ActivityFlags.NewTask);
		//	spec = TabHost.NewTabSpec("Blood Group");
		//	spec.SetIndicator("Blood Group");
		//	spec.SetContent(intent);
		//	TabHost.AddTab(spec);

		//	intent = new Intent(this, typeof(EventActivity));
		//	intent.AddFlags(ActivityFlags.NewTask);
		//	spec = TabHost.NewTabSpec("Events");
		//	spec.SetIndicator("Events");
		//	spec.SetContent(intent);
		//	TabHost.AddTab(spec);

		//	intent = new Intent(this, typeof(ContactsActivity));
		//	intent.AddFlags(ActivityFlags.NewTask);
		//	spec = TabHost.NewTabSpec("News");
		//	spec.SetIndicator("News");
		//	spec.SetContent(intent);
		//	TabHost.AddTab(spec);



		//}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Home);

			var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
			SetSupportActionBar(toolbar);
			SupportActionBar.Title = "Info Rotaract";
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.home, menu);
			return base.OnCreateOptionsMenu(menu);
		}
		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			
			switch (item.TitleFormatted.ToString().ToLower())
			{
				case "events":
				{
					Toast.MakeText(this, "Loading " + item.TitleFormatted + " page", ToastLength.Short).Show();
					StartActivity(typeof(EventActivity));
					break;
				}
				case "news":
				{
					Toast.MakeText(this, "Loading " + item.TitleFormatted + " page", ToastLength.Short).Show();
					StartActivity(typeof(EventActivity));
					break;
				}
				case "members":
				{
					Toast.MakeText(this, "Loading " + item.TitleFormatted + " page", ToastLength.Short).Show();
					StartActivity(typeof(ContactsActivity));
					break;
				}
					case "default":
				{
					break;
				}
			}
			
			
			
			return base.OnOptionsItemSelected(item);
		}
	}
}