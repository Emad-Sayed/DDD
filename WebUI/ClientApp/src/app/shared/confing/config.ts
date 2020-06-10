export class Config {
  public static apiUrl = 'http://localhost:2020/api/';
  // public static apiUrl = 'https://brimo-dev-identity-brimoapi.azurewebsites.net/api/';

  public static IdentityServerUrl = 'http://localhost:5000/';
  // public static IdentityServerUrl = 'http://brimo-dev-identity-sts.azurewebsites.net/';


  public static ProductCatalog = 'ProductCatalog';
  public static Products = `${Config.ProductCatalog}/Products`;
  public static Brands = `${Config.ProductCatalog}/Brands`;
  public static ProductCategories = `${Config.ProductCatalog}/ProductCategories`;

  public static Orders = 'Orders';

  public static CustomerManagment = 'CustomerManagment';
  public static Customers = `${Config.CustomerManagment}/Customers`;

  public static DistributorManagment = 'DistributorManagment';
  public static Distributors = `${Config.DistributorManagment}/Distributors`;

  public static PhotoUploader = `PhotoUploader`;
  public static BasePhotoUrl = `https://brimodevcdnendpoint.azureedge.net/images/`

}
