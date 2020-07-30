export class Config {
  public static ProductCatalog = 'ProductCatalog';
  public static Products = `${Config.ProductCatalog}/Products`;
  public static Brands = `${Config.ProductCatalog}/Brands`;
  public static ProductCategories = `${Config.ProductCatalog}/ProductCategories`;

  public static Orders = 'Orders';

  public static CustomerManagment = 'CustomerManagment';
  public static Customers = `${Config.CustomerManagment}/Customers`;

  public static DistributorManagment = 'DistributorManagment';
  public static Distributors = `${Config.DistributorManagment}/Distributors`;

  public static OfferManagment = 'OfferManagment';
  public static Offers = `${Config.OfferManagment}/Offers`;

  public static PhotoUploader = `PhotoUploader`;
  public static BasePhotoUrl = `https://brimodevcdnendpoint.azureedge.net/images/`

}
