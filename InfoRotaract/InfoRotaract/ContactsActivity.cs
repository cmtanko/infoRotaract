using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using InfoRotaract.DataModel;
using Java.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.Json;
using Exception = System.Exception;
using SearchView = Android.Support.V7.Widget.SearchView;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace InfoRotaract
{
    [Activity(Label = "Contacts")]

    public class ContactsActivity : ActionBarActivity
    {
        private List<Contact> _contacts;
        private ListView _lvListContact;
        private Spinner _spBloodGroup;
        private Spinner _spClubGroup;
        private Spinner _spSexGroup;
        private SearchView _searchView;
        private LinearLayout _layoutAdvSearch;
        private string _bloodgroup = "", _clubgroup = "", _nameSearch="", _sexgroup="";
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

            FindViewById<TextView>(Resource.Id.textView1);

            _layoutAdvSearch = FindViewById<LinearLayout>(Resource.Id.layout_AdvSearch);
            _layoutAdvSearch.Visibility = ViewStates.Gone;

            _lvListContact = FindViewById<ListView>(Resource.Id.listContacts);
            _lvListContact.ItemClick += OnContactItemClicked;

            _spBloodGroup = FindViewById<Spinner>(Resource.Id.spBloodGroup);
            _spBloodGroup.ItemSelected += OnBloodGroupSelected;
            
            _spClubGroup = FindViewById<Spinner>(Resource.Id.spClubGroup);
            _spClubGroup.ItemSelected += OnClubGroupSelected;

            _spSexGroup = FindViewById<Spinner>(Resource.Id.spSexGroup);
            _spSexGroup.ItemSelected += OnSexGroupSelected;

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



        private async void  CallFromUrl(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
					List<Contact> newContacts = new List<Contact>();
                    var fetchedContacts = await client.GetStringAsync(url);
                    JArray jArray = JArray.Parse(fetchedContacts);
					foreach (JObject fetchedContact in jArray)
                    {
						newContacts.Add(JsonConvert.DeserializeObject<Contact>(fetchedContact.ToString()));
                    }
					ContactManager manager = new ContactManager();
					manager.InsertData(newContacts);
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
            bloogdGroupAdapter.Add("AB +ve");
			bloogdGroupAdapter.Add("AB -ve");
			bloogdGroupAdapter.Add("A +ve");
			bloogdGroupAdapter.Add("A -ve");
			bloogdGroupAdapter.Add("B +ve");
			bloogdGroupAdapter.Add("B -ve");
			bloogdGroupAdapter.Add("O +ve");
			bloogdGroupAdapter.Add("O -ve");
            _spBloodGroup.Adapter = bloogdGroupAdapter;

            ArrayAdapter<string> clubGroupAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem);
            clubGroupAdapter.Add("Club");
            clubGroupAdapter.Add("Rotaract Club Of Kathmandu North East");
            clubGroupAdapter.Add("Rotaract Club Of Patan");
            clubGroupAdapter.Add("Rotaract Club Of Balaju");
            clubGroupAdapter.Add("Rotaract Club Of Swoyambhu");
            clubGroupAdapter.Add("Rotaract Club Of Baneshwor");
            clubGroupAdapter.Add("Kantipur Dental College");
            _spClubGroup.Adapter = clubGroupAdapter;

            ArrayAdapter<string> sexGroupAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem);
            sexGroupAdapter.Add("Gender");
            sexGroupAdapter.Add("Male");
            sexGroupAdapter.Add("Female");
            _spSexGroup.Adapter = sexGroupAdapter;
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
            _lvListContact.Adapter = new ContactListAdapter(_contacts.ToArray(), this);
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.advanceSearch, menu);
            var item = menu.FindItem(Resource.Id.action_searchAdv);
            //var itemSearchMore = menu.FindItem(Resource.Id.action_searchMore);

            var searchItem = MenuItemCompat.GetActionView(item);
            _searchView = searchItem.JavaCast<SearchView>();


            _searchView.QueryTextChange += (s, e) =>
            {
                _nameSearch = e.NewText.ToString(CultureInfo.InvariantCulture);
                PopulateContactList(_bloodgroup, _clubgroup, _sexgroup, _nameSearch);
            };

            _searchView.QueryTextSubmit += (s, e) =>
            {
                _nameSearch = e.Query.ToString(CultureInfo.InvariantCulture);
                PopulateContactList(_bloodgroup, _clubgroup, _sexgroup, _nameSearch);
                e.Handled = true;
            };
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();
            if (item.TitleFormatted != null)
            {
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
                if (item.TitleFormatted.ToString() == "Update")
                {
                    OnSyncButtonClicked();
                }
            }
            return base.OnOptionsItemSelected(item);
        }


        private void OnSyncButtonClicked()
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
    }
}