using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutLogic
{
	public interface IPriceCalculationLogic
	{
		decimal GetPrice(CheckoutItem checkoutItem);
	}
}
