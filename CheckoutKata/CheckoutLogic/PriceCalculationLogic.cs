using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutLogic
{
	/// <summary>
	/// Logic used for Price Calculation.
	/// </summary>
	public class PriceCalculationLogic : IPriceCalculationLogic
	{
		private readonly ISpecialOfferRepository specialOfferRepository;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="specialOfferRepository"></param>
		public PriceCalculationLogic(ISpecialOfferRepository specialOfferRepository)
		{
			this.specialOfferRepository = specialOfferRepository ?? throw new ArgumentNullException(nameof(specialOfferRepository));
		}

		public decimal GetPrice(CheckoutItem checkoutItem)
		{
			return 0m;
		}
	}
}
