using CheckoutLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
			Checkout checkout = new Checkout();
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
			Checkout checkout = new Checkout();
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
		/// Proves that the checkout can scan multiple items, but where some items are the same sku.
		/// </summary>
		[TestMethod]
		public void Checkout_RequestTotal_MultipleItems()
		{
			//Arrange
			Item appleItem = new Item { Sku = "A99", ItemPrice = 0.5M };
			Item biscuitItem = new Item { Sku = "B15", ItemPrice = 0.3M };
			Item crispsItem = new Item { Sku = "C40", ItemPrice= 0.6M };

			//Act
			Checkout checkout = new Checkout();
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
			Assert.AreEqual(0.5M, checkoutItems[0].Price);
			Assert.AreEqual("C40", checkoutItems[1].Sku);
			Assert.AreEqual(2, checkoutItems[1].Quantity);
			Assert.AreEqual(0.6M, checkoutItems[1].Price);
			Assert.AreEqual("B15", checkoutItems[2].Sku);
			Assert.AreEqual(2, checkoutItems[2].Quantity);
			Assert.AreEqual(0.3M, checkoutItems[2].Price);

			Assert.AreEqual(3.3M, checkout.Total());
		}
	}
}
