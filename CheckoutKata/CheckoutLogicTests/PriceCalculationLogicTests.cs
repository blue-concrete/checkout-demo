using CheckoutLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutLogicTests
{
	[TestClass]
	public class PriceCalculationLogicTests
	{
		[TestMethod]
		public void PriceCalculationLogic_SingleQuantity_NoOffers()
		{
			//Arrange
			ISpecialOfferRepository specialOfferRepository = Substitute.For<ISpecialOfferRepository>();

			CheckoutItem checkoutItem = new CheckoutItem { Price = 0.5M, Quantity = 1 };

			PriceCalculationLogic priceCalculationLogic = new PriceCalculationLogic(specialOfferRepository);

			//Act
			var price = priceCalculationLogic.GetPrice(checkoutItem);

			//Assert
			Assert.AreEqual(0.5m, price);
		}

		[TestMethod]
		public void PriceCalculationLogic_MultipleQuantity_NoOffers()
		{
			//Arrange
			ISpecialOfferRepository specialOfferRepository = Substitute.For<ISpecialOfferRepository>();

			CheckoutItem checkoutItem = new CheckoutItem { Price = 0.5M, Quantity = 4 };

			PriceCalculationLogic priceCalculationLogic = new PriceCalculationLogic(specialOfferRepository);

			//Act
			var price = priceCalculationLogic.GetPrice(checkoutItem);

			//Assert
			Assert.AreEqual(2m, price);
		}

		[TestMethod]
		public void PriceCalculationLogic_MultipleQuantity_WithOffer_NotQualifying()
		{
			//Arrange
			ISpecialOfferRepository specialOfferRepository = Substitute.For<ISpecialOfferRepository>();
			specialOfferRepository.GetBySku("A99").Returns(new SpecialOffer { Quantity = 3, OfferPrice = 1.3M });

			CheckoutItem checkoutItem = new CheckoutItem { Sku = "A99", Price = 0.5M, Quantity = 2 };

			PriceCalculationLogic priceCalculationLogic = new PriceCalculationLogic(specialOfferRepository);

			//Act
			var price = priceCalculationLogic.GetPrice(checkoutItem);

			//Assert
			Assert.AreEqual(1m, price);
			specialOfferRepository.Received(1).GetBySku("A99");
		}

		[TestMethod]
		public void PriceCalculationLogic_MultipleQuantity_WithOffer_Qualifying()
		{
			//Arrange
			ISpecialOfferRepository specialOfferRepository = Substitute.For<ISpecialOfferRepository>();
			specialOfferRepository.GetBySku("A99").Returns(new SpecialOffer { Quantity = 3, OfferPrice = 1.3M });

			CheckoutItem checkoutItem = new CheckoutItem { Sku = "A99", Price = 0.5M, Quantity = 3 };

			PriceCalculationLogic priceCalculationLogic = new PriceCalculationLogic(specialOfferRepository);

			//Act
			var price = priceCalculationLogic.GetPrice(checkoutItem);

			//Assert
			Assert.AreEqual(1.3m, price);
			specialOfferRepository.Received(1).GetBySku("A99");
		}

		[TestMethod]
		public void PriceCalculationLogic_MultipleQuantity_WithOffer_MultiQualifying()
		{
			//Arrange
			ISpecialOfferRepository specialOfferRepository = Substitute.For<ISpecialOfferRepository>();
			specialOfferRepository.GetBySku("A99").Returns(new SpecialOffer { Quantity = 3, OfferPrice = 1.3M });

			CheckoutItem checkoutItem = new CheckoutItem { Sku = "A99", Price = 0.5M, Quantity = 6 };

			PriceCalculationLogic priceCalculationLogic = new PriceCalculationLogic(specialOfferRepository);

			//Act
			var price = priceCalculationLogic.GetPrice(checkoutItem);

			//Assert
			Assert.AreEqual(2.6m, price);
			specialOfferRepository.Received(1).GetBySku("A99");
		}

		[TestMethod]
		public void PriceCalculationLogic_MultipleQuantity_WithOffer_Qualifying_Remainder()
		{
			//Arrange
			ISpecialOfferRepository specialOfferRepository = Substitute.For<ISpecialOfferRepository>();
			specialOfferRepository.GetBySku("A99").Returns(new SpecialOffer { Quantity = 3, OfferPrice = 1.3M });

			CheckoutItem checkoutItem = new CheckoutItem { Sku = "A99", Price = 0.5M, Quantity = 4 };

			PriceCalculationLogic priceCalculationLogic = new PriceCalculationLogic(specialOfferRepository);

			//Act
			var price = priceCalculationLogic.GetPrice(checkoutItem);

			//Assert
			Assert.AreEqual(1.8m, price);
			specialOfferRepository.Received(1).GetBySku("A99");
		}

		[TestMethod]
		public void PriceCalculationLogic_MultipleQuantity_WithOffer_MultiQualifying_Remainder()
		{
			//Arrange
			ISpecialOfferRepository specialOfferRepository = Substitute.For<ISpecialOfferRepository>();
			specialOfferRepository.GetBySku("A99").Returns(new SpecialOffer { Quantity = 3, OfferPrice = 1.3M });

			CheckoutItem checkoutItem = new CheckoutItem { Sku = "A99", Price = 0.5M, Quantity = 7 };

			PriceCalculationLogic priceCalculationLogic = new PriceCalculationLogic(specialOfferRepository);

			//Act
			var price = priceCalculationLogic.GetPrice(checkoutItem);

			//Assert
			Assert.AreEqual(3.1m, price);
			specialOfferRepository.Received(1).GetBySku("A99");
		}

	}
}
