using Sipay.Models.Request.Objects;
using System.Collections.Generic;

namespace Sipay
{
	public interface ISipayBuilder
	{
		/// <summary>
		/// İşlemler Test sunucusuna yönlendirilir.
		/// </summary>
		/// <returns></returns>
		ISipayBuilder Test(bool isTest = true);

		/// <summary>
		/// Ödemenin alınacağı kart bilgileri
		/// </summary>
		/// <param name="cardholderName">Kart üzerindeki isim</param>
		/// <param name="creditCardNumber">Kart numarası</param>
		/// <param name="expiryMonth">Kart SKT Ay bilgisi</param>
		/// <param name="expiryYear">Kart SKT Yıl bilgisi</param>
		/// <param name="cvv">Kart Güvenlik Kodu</param>
		/// <returns></returns>
		ISipayBuilder CreditCard
			(string cardholderName,
			string creditCardNumber,
			string expiryMonth,
			string expiryYear,
			string cvv);

		/// <summary>
		/// Ödemeyi gerçekleştiren müşteri bilgisi
		/// </summary>
		/// <param name="firstName">Müşteri isim</param>
		/// <param name="lastName">Müşteri soyisim</param>
		/// <param name="ipAddress">Müşteri ip bilgisi</param>
		/// <returns></returns>
		ISipayBuilder Customer(string firstName, string lastName, string ipAddress);

		/// <summary>
		/// Ödeme alınan müşterinin fatura bilgileri
		/// </summary>
		/// <param name="address1">Adres satırı 1</param>
		/// <param name="address2">Adres satırı 2</param>
		/// <param name="state">Adres şehir bilgisi</param>
		/// <param name="city">Adres ilçe bilgisi</param>
		/// <param name="country">Adres ülke bilgisi</param>
		/// <param name="zipCode">Adres posta kodu</param>
		/// <param name="email">Email bilgisi</param>
		/// <param name="phone">Telefon bilgisi</param>
		/// <returns></returns>
		ISipayBuilder BillingAddress(
			string address1,
			string address2,
			string state,
			string city,
			string country,
			string zipCode,
			string email,
			string phone
			);

		/// <summary>
		/// Ödemesi gerçekleştirilen sipariş bilgileri
		/// </summary>
		/// <param name="CurrencyCode">Sipariş para birimi</param>
		/// <param name="Installments">Toplam taksit sayısı</param>
		/// <param name="OrderGuid">Benzersiz sipariş numarası</param>
		/// <param name="OrderTotal">Toplam sipariş tutarı</param>
		/// <returns></returns>
		ISipayBuilder Order
			(string currencyCode,
			int installments,
			string orderGuid,
			double orderTotal
			);

		/// <summary>
		/// Sipariş içeriği (ürün bilgileri)
		/// </summary>
		/// <param name="basketItems">Ürün bilgileri</param>
		/// <returns></returns>
		ISipayBuilder BasketItems(List<SipayBasketItem> basketItems);

		/// <summary>
		/// API cevap döneceği url bilgileri
		/// </summary>
		/// <param name="ReturlUrl">Başarılı bilgisi dönüş adresi</param>
		/// <param name="CancelUrl">Başarısız bilgisi dönüş adresi</param>
		/// <returns></returns>
		ISipayBuilder Callbacks(string returlUrl, string cancelUrl);

		ISipayBuilder Execute(string token = "");
		
		/// <summary>
		/// Servise erişim için gerekli olan bilgiler
		/// </summary>
		/// <param name="appId">Public Key</param>
		/// <param name="appSecret">Secret Key</param>
		/// <param name="merchantKey">Merchant Key</param>
		/// <returns></returns>
		ISipayBuilder Settings(string appId, string appSecret, string merchantKey);
	}
}
