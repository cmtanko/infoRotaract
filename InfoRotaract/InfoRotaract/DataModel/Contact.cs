using Android.Locations;
using SQLite;

namespace InfoRotaract.DataModel
{
	public class Contact
	{
		public Contact()
		{
			
		}

		public Contact(string firstName,string lastName, string club, string age, string sex, string email, string phone, string bloodGroup,
			string available,string address)
		{
			FirstName = firstName;
			LastName = lastName;
			Club = club;
			Age = age;
			Sex = sex;
			Email = email;
			Phone = phone;
			BloodGroup = bloodGroup;
			Available = available;
			Address = address;

		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		[MaxLength(100)]
		public string FirstName { get; set; }
		[MaxLength(100)]
		public string LastName { get; set; }
		[MaxLength(100)]
		public string Club { get; set; }
		[MaxLength(100)]
		public string Age { get; set; }
		[MaxLength(100)]
		public string Sex { get; set; }
		[MaxLength(100)]
		public string Email { get; set; }
		[MaxLength(100)]
		public string Phone { get; set; }
		[MaxLength(100)]
		public string BloodGroup { get; set; }
		[MaxLength(100)]
		public string Available { get; set; }
		[MaxLength(100)]

		public string Address { get; set; }

	}
}