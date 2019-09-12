using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutLogic
{
	/// <summary>
	/// Represents a special offer.
	/// </summary>
	public class SpecialOffer
	{
		/// <summary>
		/// Sku the offer is for
		/// </summary>
		public string Sku { get; set; }

		/// <summary>
		/// Quantity that the offer relates to
		/// </summary>
		public int Quantity { get; set; }

		/// <summary>
		/// The special price to use.
		/// </summary>
		public decimal OfferPrice { get; set; }

	}
}
