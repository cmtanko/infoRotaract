using System;
using SQLite;

namespace InfoRotaract.DataModel
{
	public class Event
	{
		public Event()
		{

		}

		public Event(string eventname, string description, string club, string venue, DateTime eventDate, string updates, string contacts, string favorite)
		{
			EventName = eventname;
			Description = description;
			Club = club;
			Venue = venue;
			EventDate = eventDate;
			Updates = updates;
			Contacts = contacts;
			Favorite = favorite;
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		[MaxLength(100)]
		public string EventName { get; set; }
		[MaxLength(100)]
		public string Description { get; set; }
		[MaxLength(1000)]
		public string Club { get; set; }
		[MaxLength(100)]
		public string Venue { get; set; }
		[MaxLength(100)]
		public DateTime EventDate { get; set; }
		[MaxLength(100)]
		public string Updates { get; set; }
		[MaxLength(100)]
		public string Contacts { get; set; }
		[MaxLength(100)]
		public string Favorite { get; set; }
	}
}