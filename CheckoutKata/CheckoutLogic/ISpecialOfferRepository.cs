using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutLogic
{
	/// <summary>
	/// Interface for repository to provide special offers
	/// </summary>
	public interface ISpecialOfferRepository
	{
		/// <summary>
		/// Get a special offer by sku.
		/// </summary>
		/// <param name="sku"></param>
		/// <returns></returns>
		SpecialOffer GetBySku(string sku);
	}
}
