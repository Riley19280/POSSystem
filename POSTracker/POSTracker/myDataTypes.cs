
using SQLite;

namespace myDataTypes
{
	//[Table("customer")]
	public class customer
	{
		public customer() { }
		public customer(string name, double balance)
		{
			this.name = name;
			this.balance = balance;
		}
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		public string name { get; set; }
		public double balance { get; set; } = 0;
	}

	//[Table("defaultAmount")]
	public class defaultAmount
	{
		public defaultAmount() { }
		public defaultAmount(double amount)
		{
			this.amount = amount;
		}

		public defaultAmount(int id, double amount)
		{
			this.id = id;
			this.amount = amount;
		}

		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		public double amount { get; set; } = 0;
	}


}