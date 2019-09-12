using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckoutLogic
{
	/// <summary>
	/// A Checkout that is in progress
	/// </summary>
	public class Checkout
	{

		private List<CheckoutItem> checkoutItems = new List<CheckoutItem>();
		private readonly IPriceCalculationLogic priceCalculationLogic;

		public Checkout(IPriceCalculationLogic priceCalculationLogic)
		{
			this.priceCalculationLogic = priceCalculationLogic ?? throw new ArgumentNullException(nameof(priceCalculationLogic));
		}

		/// <summary>
		/// The current total of all items that have been scanned
		/// </summary>
		/// <returns></returns>
		public decimal Total()
		{
			return this.checkoutItems.Sum(p => this.priceCalculationLogic.GetPrice(p));
		}

		/// <summary>
		/// Provide access to the items that have been scanned so far.
		/// </summary>
		/// <returns></returns>
		public IList<CheckoutItem> Items()
		{
			//return as an array to ensure users of method can't change internal list
			return this.checkoutItems.ToArray();
		}

		/// <summary>
		/// Scan an item into the Checkout
		/// </summary>
		/// <param name="item"></param>
		public void Scan(Item item)
		{
			//validation
			if (item == null)
			{
				throw new ArgumentNullException(nameof(item));
			}

			//lookup the item in the list
			CheckoutItem checkoutItem = this.checkoutItems.FirstOrDefault(p => p.Sku == item.Sku);
			if (checkoutItem == null)
			{
				//no item with sku was found so create one and add to list (without quantity initially)
				checkoutItem = new CheckoutItem { Sku = item.Sku, Price = item.ItemPrice }; 
				this.checkoutItems.Add(checkoutItem);
			}
			//increment quantity
			checkoutItem.Quantity++;
		}
	}
}
