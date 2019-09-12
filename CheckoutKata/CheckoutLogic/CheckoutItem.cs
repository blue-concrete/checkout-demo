using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutLogic
{
	/// <summary>
	/// An item stored in the Checkout
	/// </summary>
	public class CheckoutItem
	{
		/// <summary>
		/// The Sku of an item that has been scanned
		/// </summary>
		public string Sku { get; set; }

		/// <summary>
		/// The quantity of an item that has been scanned.
		/// </summary>
		public int Quantity { get; set; }

		/// <summary>
		/// The price of the item that has been scanned
		/// </summary>
		public decimal Price { get; set; }
	}
}
