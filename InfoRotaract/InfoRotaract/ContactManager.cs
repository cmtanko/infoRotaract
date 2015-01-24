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
using SQLite;

namespace InfoRotaract
{
	public class ContactManager
	{
		private string _databasePath;

		public ContactManager()
		{
			string databaseName = "Contacts.db3";
			string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			_databasePath = Path.Combine(docFolder, databaseName);

			//CreateDatabase();
			//InsertData();
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
				database.CreateTable<Contact>();
			}
		}
		private void InsertData()
		{
			List<Contact> contacts = new List<Contact>();
			contacts.Add(new Contact("Rtr. Suchan", "Badyakar", "Rotaract Club Of Kathmandu North East", "27", "Male", "suchan211@gmail.com", "9803086679", "AB+", "1","Patan"));
			contacts.Add(new Contact("Rtr.Kapil", "Pandey", "Rotaract Club Of Patan", "22", "Male", "kpl@gmail.com", "9808768965", "B+", "1", "Patan"));
			contacts.Add(new Contact("Rtr. Sanjay", "Shrestha", "Rotaract Club Of Kathmandu East", "27", "Male", "suchan211@gmail.com", "9803086679", "AB+", "1", "Patan"));
			contacts.Add(new Contact("Rtr.Bijay", "Pandey", "Rotaract Club Of Patan", "22", "Male", "kpl@gmail.com", "9808768965", "B+", "1", "Patan"));
			contacts.Add(new Contact("Rtr. Roshan", "Badyakar", "Rotaract Club Of Kathmandu North East", "27", "Male", "suchan211@gmail.com", "9803086679", "B-", "1", "Patan"));
			contacts.Add(new Contact("Rtr.Smith", "Pandey", "Rotaract Club Of Patan", "22", "Male", "kpl@gmail.com", "9808768965", "B+", "1", "Patan"));
			contacts.Add(new Contact("Rtr. Kiop", "Badyakar", "Rotaract Club Of Kathmandu North East", "27", "Male", "suchan211@gmail.com", "9803086679", "A+", "1", "Patan"));
			contacts.Add(new Contact("Rtr.Rip", "Pandey", "Rotaract Club Of Patan", "22", "Male", "kpl@gmail.com", "9808768965", "B+", "1", "Patan"));
			contacts.Add(new Contact("Rtr. werwer", "Badyakar", "Rotaract Club Of Kathmandu North East", "27", "Male", "suchan211@gmail.com", "9803086679", "B+", "1", "Patan"));
			contacts.Add(new Contact("Rtr.Rfdgd", "Pandey", "Rotaract Club Of Patan", "22", "Male", "kpl@gmail.com", "9808768965", "B+", "1", "Patan"));
			contacts.Add(new Contact("Rtr. Reiu", "Badyakar", "Rotaract Club Of Kathmandu North East", "27", "Male", "suchan211@gmail.com", "9803086679", "AB+", "1", "Patan"));
			contacts.Add(new Contact("Rtr.Kapil", "Pandey", "Rotaract Club Of Patan", "22", "Male", "kpl@gmail.com", "9808768965", "B+", "1", "Patan"));
			contacts.Add(new Contact("Rtr. Suchan", "Badyakar", "Rotaract Club Of Kathmandu North East", "27", "Male", "suchan211@gmail.com", "9803086679", "A+", "1", "Patan"));
			contacts.Add(new Contact("Rtr.Kapil", "Pandey", "Rotaract Club Of Patan", "22", "Male", "kpl@gmail.com", "9808768965", "B+", "1", "Patan"));
			contacts.Add(new Contact("Rtr. Suchan", "Badyakar", "Rotaract Club Of Kathmandu North East", "27", "Male", "suchan211@gmail.com", "9803086679", "A-", "1", "Patan"));
			contacts.Add(new Contact("Rtr.Kapil", "Pandey", "Rotaract Club Of Patan", "22", "Male", "kpl@gmail.com", "9808768965", "B+", "1", "Patan"));
			contacts.Add(new Contact("Rtr. Suchan", "Badyakar", "Rotaract Club Of Kathmandu North East", "27", "Male", "suchan211@gmail.com", "9803086679", "B+", "1", "Patan"));
			contacts.Add(new Contact("Rtr.Kapil", "Pandey", "Rotaract Club Of Patan", "22", "Male", "kpl@gmail.com", "9808768965", "B+", "1", "Patan"));
			contacts.Add(new Contact("Rtr. Suchan", "Badyakar", "Rotaract Club Of Kathmandu North East", "27", "Male", "suchan211@gmail.com", "9803086679", "O-", "1", "Patan"));
			contacts.Add(new Contact("Rtr.Kapil", "Pandey", "Rotaract Club Of Patan", "22", "Male", "kpl@gmail.com", "9808768965", "B+", "1", "Patan"));
			contacts.Add(new Contact("Rtr. Suchan", "Badyakar", "Rotaract Club Of Kathmandu North East", "27", "Male", "suchan211@gmail.com", "9803086679", "O+", "1", "Patan"));
			contacts.Add(new Contact("Rtr.Kapil", "Pandey", "Rotaract Club Of Patan", "22", "Male", "kpl@gmail.com", "9808768965", "B+", "1", "Patan"));

			using (var database = new SQLiteConnection(_databasePath))
			{
				database.InsertAll(contacts);
			}
		}

		public void InsertData(List<Contact> contacts )
		{
			var database = new SQLiteConnection(_databasePath);
			var sql = "DELETE from contact";
			database.Execute(sql);
			using (database)
			{
				database.InsertAll(contacts);
			}
		}
		public List<Contact> GetDisplayContacts(string bloodgroup, string clubgroup, string sexgroup, string nameSearch)
		{
			string bloodGroupName,clubGroupName,sexGroupName, nameSearhParam;
			if (bloodgroup == "" || bloodgroup == "Group")
			{
				bloodGroupName = "%";
			}
			else
			{
				bloodGroupName = bloodgroup;
			}

			if (clubgroup == "" || clubgroup == "Club")
			{
				clubGroupName = "%";
			}
			else
			{
				clubGroupName = clubgroup;
			}

			if (sexgroup == "" || sexgroup == "Gender")
			{
				sexGroupName = "%";
			}
			else
			{
				sexGroupName = sexgroup;
			}
			using (var database = new SQLiteConnection(_databasePath))
			{
				var sql = "SELECT * FROM Contact WHERE BloodGroup LIKE '" + bloodGroupName + "' AND Club LIKE '" + clubGroupName + "' AND Sex LIKE '" + sexGroupName +"' AND FirstName LIKE '%" + nameSearch + "%'";
				List<Contact> contacts = database.Query<Contact>(sql);
				return contacts;
			}
		}
	}

}