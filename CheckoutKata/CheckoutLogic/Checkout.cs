using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutLogic
{
	/// <summary>
	/// A Checkout that is in progress
	/// </summary>
	public class Checkout
	{
		/// <summary>
		/// The current total of all items that have been scanned
		/// </summary>
		/// <returns></returns>
		public decimal Total()
		{
			return 0m;
		}

		/// <summary>
		/// Scan an item into the Checkout
		/// </summary>
		/// <param name="item"></param>
		public void Scan(Item item)
		{

		}
	}
}
