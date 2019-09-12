using CheckoutLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace CheckoutLogicTests
{
	[TestClass]
	public class CheckoutTests
	{

		/// <summary>
		/// Proves that the checkout can scan multiple different items
		/// </summary>
		[TestMethod]
		public void Checkout_ScanItem_SingleItems()
		{
			//Arrange
			Item appleItem = new Item { Sku = "A99" };
			Item biscuitItem = new Item { Sku = "B15" };
			Item crispsItem = new Item { Sku = "C40" };

			//Act
			Checkout checkout = new Checkout(Substitute.For<IPriceCalculationLogic>());
			checkout.Scan(appleItem);
			checkout.Scan(biscuitItem);
			checkout.Scan(crispsItem);

			//Assert
			var checkoutItems = checkout.Items();
			Assert.AreEqual(3, checkoutItems.Count);

			Assert.AreEqual("A99", checkoutItems[0].Sku);
			Assert.AreEqual(1, checkoutItems[0].Quantity);
			Assert.AreEqual("B15", checkoutItems[1].Sku);
			Assert.AreEqual(1, checkoutItems[1].Quantity);
			Assert.AreEqual("C40", checkoutItems[2].Sku);
			Assert.AreEqual(1, checkoutItems[2].Quantity);
		}

		/// <summary>
		/// Proves that the checkout can scan multiple items, but where some items are the same sku.
		/// </summary>
		[TestMethod]
		public void Checkout_ScanItem_MultipleItems()
		{
			//Arrange
			Item appleItem = new Item { Sku = "A99"};
			Item biscuitItem = new Item { Sku = "B15"};
			Item crispsItem = new Item { Sku = "C40"};

			//Act
			Checkout checkout = new Checkout(Substitute.For<IPriceCalculationLogic>());
			checkout.Scan(appleItem);
			checkout.Scan(crispsItem);
			checkout.Scan(biscuitItem);
			checkout.Scan(biscuitItem);
			checkout.Scan(appleItem);
			checkout.Scan(crispsItem);
			checkout.Scan(appleItem);

			//Assert
			var checkoutItems = checkout.Items();
			Assert.AreEqual(3, checkoutItems.Count);

			Assert.AreEqual("A99", checkoutItems[0].Sku);
			Assert.AreEqual(3, checkoutItems[0].Quantity);
			Assert.AreEqual("C40", checkoutItems[1].Sku);
			Assert.AreEqual(2, checkoutItems[1].Quantity);
			Assert.AreEqual("B15", checkoutItems[2].Sku);
			Assert.AreEqual(2, checkoutItems[2].Quantity);
		}

		/// <summary>
		/// Proves that the Checkout correctly passes it's items to the price calculation logic..
		/// </summary>
		[TestMethod]
		public void Checkout_ScanItem_PassItemsToPriceCalculation()
		{
			//Arrange
			Item appleItem = new Item { Sku = "A99" };
			Item biscuitItem = new Item { Sku = "B15" };
			Item crispsItem = new Item { Sku = "C40" };
			var priceCalculationLogic = Substitute.For<IPriceCalculationLogic>();
			//return some mock prices when requested
			priceCalculationLogic.GetPrice(Arg.Is<CheckoutItem>(x => x.Sku == "A99")).Returns(0.5M);
			priceCalculationLogic.GetPrice(Arg.Is<CheckoutItem>(x => x.Sku == "B15")).Returns(0.3M);
			priceCalculationLogic.GetPrice(Arg.Is<CheckoutItem>(x => x.Sku == "C40")).Returns(0.6M);
			//Act
			Checkout checkout = new Checkout(priceCalculationLogic);
			checkout.Scan(appleItem);
			checkout.Scan(biscuitItem);
			checkout.Scan(crispsItem);

			//Assert
			decimal total = checkout.Total();
			Assert.AreEqual(1.4M, total);

			//Ensure the methods were each called.
			priceCalculationLogic.Received(1).GetPrice(Arg.Is<CheckoutItem>(x => x.Sku == "A99"));
			priceCalculationLogic.Received(1).GetPrice(Arg.Is<CheckoutItem>(x => x.Sku == "B15"));
			priceCalculationLogic.Received(1).GetPrice(Arg.Is<CheckoutItem>(x => x.Sku == "C40"));

		}

	}
}
