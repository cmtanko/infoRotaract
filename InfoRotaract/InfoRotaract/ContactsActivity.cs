using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Net.Http;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using InfoRotaract.DataModel;
using Java.Lang;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.Apache.Http.Client.Methods;
using Exception = System.Exception;
using SearchView = Android.Support.V7.Widget.SearchView;
using Toolbar = Android.Support.V7.Widget.Toolbar;


namespace InfoRotaract
{
	[Activity(Label = "Contacts")]

	public class ContactsActivity : ActionBarActivity
	{
		private List<Contact> _contacts;
		private ListView lvListContact;
		private TextView _textView;
		private Spinner spBloodGroup;
		private Spinner spClubGroup;
		private Spinner spSexGroup;
		private Button btnSync;
		private SearchView _searchView;
		private LinearLayout _layoutAdvSearch;
		private string _bloodgroup = "", _clubgroup = "", _nameSearch="", _sexgroup="";
		//private SwipeRefreshLayout _swipeRefreshLayout;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Contacts);

			//Toolbar Related Activities
				var toolbarSearchBar = FindViewById<Toolbar>(Resource.Id.toolbar_advSearch);
				SetSupportActionBar(toolbarSearchBar);
				SupportActionBar.Title = "Blood Group";
				SupportActionBar.SetDisplayHomeAsUpEnabled(true);
				SupportActionBar.SetHomeButtonEnabled(true);
				toolbarSearchBar.InflateMenu(Resource.Menu.advanceSearch);


			//_swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeLayout);
			//_swipeRefreshLayout.SetColorScheme(Android.Resource.Color.HoloBlueBright, Android.Resource.Color.HoloBlueDark, Android.Resource.Color.HoloGreenDark);
			//_swipeRefreshLayout.Refresh += OnRefreshClick;

			_textView = FindViewById<TextView>(Resource.Id.textView1);

			_layoutAdvSearch = FindViewById<LinearLayout>(Resource.Id.layout_AdvSearch);

			lvListContact = FindViewById<ListView>(Resource.Id.listContacts);
			lvListContact.ItemClick += OnContactItemClicked;

			spBloodGroup = FindViewById<Spinner>(Resource.Id.spBloodGroup);
			spBloodGroup.ItemSelected += OnBloodGroupSelected;
			
			spClubGroup = FindViewById<Spinner>(Resource.Id.spClubGroup);
			spClubGroup.ItemSelected += OnClubGroupSelected;

			spSexGroup = FindViewById<Spinner>(Resource.Id.spSexGroup);
			spSexGroup.ItemSelected += OnSexGroupSelected;

			btnSync = FindViewById<Button>(Resource.Id.btnSync);
			btnSync.Click += OnSyncButtonClicked;

			PopulateSpinners();
			PopulateContactList(_bloodgroup, _clubgroup, _sexgroup, _nameSearch);
		}


		private void OnContactItemClicked(object sender, AdapterView.ItemClickEventArgs e)
		{
			var selectedFeed = _contacts[e.Position];
			FragmentTransaction transaction = FragmentManager.BeginTransaction();
			ContactDialog contactDialog = new ContactDialog(selectedFeed.FirstName,
															selectedFeed.LastName,
															selectedFeed.Club,
															selectedFeed.Age,
															selectedFeed.Sex,
															selectedFeed.Email,
															selectedFeed.Phone,
															selectedFeed.BloodGroup,
															selectedFeed.Available,
															selectedFeed.Address);
			contactDialog.Show(transaction, "Dialog fragment");
		}

		private void OnSyncButtonClicked(object sender, EventArgs e)
		{
			string url = "https://api.myjson.com/bins/465l7";
			CallFromUrl(url);
			////if (!Reachability.IsHostReachable("http://google.com"))
			////{
			////	// Put alternative content/message here
			////}
			////else
			////{
			////	// Put Internet Required Code here
			////}
		}

		private async void  CallFromUrl(string url)
		{
			try
			{
				using (var client = new HttpClient())
				{
					var fetchedContacts = await client.GetStringAsync(url);
					JArray jArray = JArray.Parse(fetchedContacts);
					var feed = JsonConvert.DeserializeObject(jArray.ToString());
					//var fetchedContact = JsonConvert.DeserializeObject<Contact>(jObject.ToString());
					Toast.MakeText(this, "Contacts upto date" + jArray[0].Children().ToString() , ToastLength.Long).Show();
				}
			}
			catch (Exception e)
			{

				Toast.MakeText(this, "No Internet Connection : " + e.Message, ToastLength.Long).Show();
			}

		}



		private void OnClubGroupSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;
			_clubgroup = spinner.SelectedItem.ToString();
			PopulateContactList(_bloodgroup, _clubgroup, _sexgroup, _nameSearch);
		}

		private void OnBloodGroupSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;
			_bloodgroup = spinner.SelectedItem.ToString();
			PopulateContactList(_bloodgroup, _clubgroup, _sexgroup, _nameSearch);
		}

		private void OnSexGroupSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;
			_sexgroup = spinner.SelectedItem.ToString();
			PopulateContactList(_bloodgroup, _clubgroup, _sexgroup, _nameSearch);
		}
		private void PopulateSpinners()
		{
			ArrayAdapter<string> bloogdGroupAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem);
			bloogdGroupAdapter.Add("Group");
			bloogdGroupAdapter.Add("AB+");
			bloogdGroupAdapter.Add("AB-");
			bloogdGroupAdapter.Add("A+");
			bloogdGroupAdapter.Add("A-");
			bloogdGroupAdapter.Add("B+");
			bloogdGroupAdapter.Add("B-");
			bloogdGroupAdapter.Add("O+");
			bloogdGroupAdapter.Add("O-");
			spBloodGroup.Adapter = bloogdGroupAdapter;

			ArrayAdapter<string> clubGroupAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem);
			clubGroupAdapter.Add("Club");
			clubGroupAdapter.Add("Rotaract Club Of Kathmandu North East");
			clubGroupAdapter.Add("Rotaract Club Of Patan");
			clubGroupAdapter.Add("Rotaract Club Of Balaju");
			clubGroupAdapter.Add("Rotaract Club Of Swoyambhu");
			clubGroupAdapter.Add("Rotaract Club Of Baneshwor");
			clubGroupAdapter.Add("Kantipur Dental College");
			spClubGroup.Adapter = clubGroupAdapter;

			ArrayAdapter<string> sexGroupAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem);
			sexGroupAdapter.Add("Gender");
			sexGroupAdapter.Add("Male");
			sexGroupAdapter.Add("Female");
			spSexGroup.Adapter = sexGroupAdapter;
		}

		private void PopulateContactList(string bloodgroup, string clubgroup, string sexgroup, string nameSearch)
		{
			ContactManager manager = new ContactManager();
			_contacts = null;
			_contacts = manager.GetDisplayContacts(bloodgroup.Trim(), clubgroup.Trim(), sexgroup.Trim() ,nameSearch.Trim());
			UpdateList();

		}

		private void UpdateList()
		{
			lvListContact.Adapter = new ContactListAdapter(_contacts.ToArray(), this);
		}
		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.advanceSearch, menu);
			var item = menu.FindItem(Resource.Id.action_searchAdv);
			var itemSearchMore = menu.FindItem(Resource.Id.action_searchMore);

			var searchItem = MenuItemCompat.GetActionView(item);
			var _searchView = searchItem.JavaCast<SearchView>();


			_searchView.QueryTextChange += (s, e) =>
			{
				_nameSearch = e.NewText.ToString();
				PopulateContactList(_bloodgroup, _clubgroup, _sexgroup, _nameSearch);
			};

			_searchView.QueryTextSubmit += (s, e) =>
			{
				_nameSearch = e.Query.ToString();
				PopulateContactList(_bloodgroup, _clubgroup, _sexgroup, _nameSearch);
				e.Handled = true;
			};
			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if (item.ItemId == Android.Resource.Id.Home)
				Finish();
			if (item.TitleFormatted.ToString() == "Advance Search")
			{
				if (_layoutAdvSearch.IsShown)
				{
					_layoutAdvSearch.Visibility = ViewStates.Gone;
				}
				else
				{
					_layoutAdvSearch.Visibility = ViewStates.Visible;
				}
			}
			return base.OnOptionsItemSelected(item);
		}
		//private void OnRefreshClick(object sender, EventArgs e)
		//{
		//	BackgroundWorker worker = new BackgroundWorker();
		//	worker.DoWork += worker_DoWork;
		//	worker.RunWorkerCompleted += worker_RunWorkerCompleted;
		//	worker.RunWorkerAsync();
		//}

		//private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		//{
		//	RunOnUiThread(() => { _swipeRefreshLayout.Refreshing = false; });
		//}

		//private void worker_DoWork(object sender, DoWorkEventArgs e)
		//{
		//	Thread.Sleep(500);
		//}
	}
}