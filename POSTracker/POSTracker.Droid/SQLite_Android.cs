using POSTracker.Droid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Android;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace POSTracker.Droid
{
	public class SQLite_Android : ISQLite
	{
		public SQLite_Android() { }
		public SQLite.SQLiteConnection GetConnection()
		{
			var sqliteFilename = "POSTracker.db3";
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);
			// Create the connection
			var conn = new SQLite.SQLiteConnection(path);
			// Return the database connection
			return conn;
		}
	}
}