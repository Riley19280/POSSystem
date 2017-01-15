using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using myDataTypes;
using SQLite;
using Xamarin.Forms;

namespace POSTracker
{
	public class manager
	{
		SQLiteConnection conn;
		public bool EstablishDatabase()
		{
			conn = DependencyService.Get<ISQLite>().GetConnection();
			//conn.DropTable<defaultAmount>();
			//conn.DropTable<customer>();

			defaultAmount tableExist = new defaultAmount(10, 0);

			try
			{
				conn.Insert(tableExist);
				conn.Delete(tableExist);
			}
			catch (Exception)
			{


				conn.CreateTable<defaultAmount>();
				defaultAmount da = new defaultAmount(0, 1);
				conn.Insert(da);
				da = new defaultAmount(1, 5);
				conn.Insert(da);
				da = new defaultAmount(2, 10);
				conn.Insert(da);
				//da = new defaultAmount(3, 20);//TODO:NUMPRESETS
				//conn.Insert(da);
				//da = new defaultAmount(4, 100);
				//conn.Insert(da);
			}

			conn.CreateTable<customer>();

			return conn != null ? true : false;
		}

		public List<myDataTypes.customer> getAllCustomers()
		{
			return conn.Query<customer>("SELECT * FROM customer ORDER BY name");
		}

		public void addCustomer(customer customer)
		{
			conn.Insert(customer);
		}

		public List<myDataTypes.customer> searchForCustomer(string text)
		{
			return conn.Query<customer>("SELECT * FROM customer WHERE name LIKE '%" + text + "%'");
			//		return conn.Query<customer>("SELECT * FROM customer WHERE name LIKE '%" + text + "' OR name LIKE '" + text + "%'");
		}

		public List<myDataTypes.customer> positiveBalances()
		{
			return conn.Query<customer>("SELECT * FROM customer WHERE balance > 0 ORDER BY balance DESC");
		}

		public List<myDataTypes.customer> negativeBalances()
		{
			return conn.Query<customer>("SELECT * FROM customer WHERE balance < 0 ORDER BY balance ASC");
		}

		public List<double> getDefaultAmounts()
		{
			List<double> doubles = new List<double>();
			foreach (defaultAmount da in conn.Table<defaultAmount>())
			{
				doubles.Add(da.amount);
			}
			return doubles.OrderBy(i => i).ToList();
		}

		public double updateCustomerBalance(double amount, int id)
		{
			conn.Query<customer>("UPDATE customer SET balance = ? WHERE id=?", amount, id);
			return conn.Query<customer>("SELECT * FROM customer WHERE id=?", id)[0].balance;
		}

		public double getValueOfPreset(int id)
		{
			List<defaultAmount> g = conn.Table<defaultAmount>().ToList();


			foreach (var item in g)
			{
				if (item.id == id)
					return item.amount;
			}
			return 0;
			//	return conn.Query<defaultAmount>("SELECT * FROM defaultAmount WHERE id=?", id)[0].amount;
		}

		public void setPreset(int id, double value)
		{
			conn.InsertOrReplace(new defaultAmount(id, value));
		}

		public void deleteCustomer(customer c)
		{
			conn.Delete(c);
		}

		public customer checkCustomerExists(string name)
		{
			var people = conn.Query<customer>("SELECT * FROM customer where name=?", name).ToList();
			if (people.Count == 1)
			{
				return people[0];
			}
			else if(people.Count > 1){
				var c = new customer();
				c.name = "$#@)&$()many";
				return c;
			}
			else
			{
				var c = new customer();
				c.name = "$#@)&$()none";
				return c;
			}


		}

	}
}
