using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using InfoRotaract.DataModel;
using Java.Util;
using SQLite;

namespace InfoRotaract
{
	class EventManager
	{
		private string _databasePath;

		public EventManager()
		{
			string databaseName = "Events.db3";
			string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			_databasePath = Path.Combine(docFolder, databaseName);

			CreateDatabase();
			InsertData();
		}
		private void CreateDatabase()
		{
			if (File.Exists(_databasePath))
				File.Delete(_databasePath);
			CreateTable();
		}

		private void CreateTable()
		{
			using (var database = new SQLiteConnection(_databasePath))
			{
				database.CreateTable<Event>();
			}
		}
		private void InsertData()
		{
			List<Event> events = new List<Event>();
			events.Add(new Event("Rotaract Dance Party", "Lorem ipsum dolor sit amet consectetur adipisicing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua Ut enim ad minim veniamquis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat","Rotact Club Of Kathmandu North" , "Patan Durbar Square", new DateTime().ToLocalTime(), "updates", "9803086679", "www.facebook.com/events/newyear"));
			events.Add(new Event("Blood Donation Program", "Rotaract dance party with lots of fun and many more", "Rotaract Club of Patan", "Patan Durbar Square", new DateTime().ToLocalTime(), "updates", "9803086679", "1"));
			events.Add(new Event("Oratory Competition", "This is with lots of fun and many more", "Rotaract Club of Patan", "Patan Durbar Square", new DateTime().ToLocalTime(), "updates", "9803086679", "1"));
			events.Add(new Event("Fellowship Event", "Rotaract Club of Kathmandu would like to invite you to our first annual dance party with lots of fun and many more", "Rotaract Club of Patan", "Patan Durbar Square", new DateTime().ToLocalTime(), "updates", "9803086679", "1"));
			events.Add(new Event("Rotaract Dance Party", "Rotaract Club of Patan would like to invite you to our first annual dance party with lots of fun and many more", "Rotaract Club of Patan", "Patan Durbar Square", new DateTime().ToLocalTime(), "updates", "9803086679", "1"));
			events.Add(new Event("Blood Donation Program", "Rotaract dance party with lots of fun and many more", "Rotaract Club of Patan", "Patan Durbar Square", new DateTime().ToLocalTime(), "updates", "9803086679", "1"));
			events.Add(new Event("Oratory Competition", "This is with lots of fun and many more", "Rotaract Club of Patan", "Patan Durbar Square", new DateTime().ToLocalTime(), "updates", "9803086679", "1"));
			events.Add(new Event("Fellowship Event", "Rotaract Club of Kathmandu would like to invite you to our first annual dance party with lots of fun and many more", "Rotaract Club of Patan", "Patan Durbar Square", new DateTime().ToLocalTime(), "updates", "9803086679", "1"));
			using (var database = new SQLiteConnection(_databasePath))
			{
				database.InsertAll(events);
			}
		}
		public List<Event> GetDisplayEvents()
		{
			using (var database = new SQLiteConnection(_databasePath))
			{
				var sql = "SELECT * FROM Event";
				List<Event> events = database.Query<Event>(sql);
				return events;
			}
		}
	}
}