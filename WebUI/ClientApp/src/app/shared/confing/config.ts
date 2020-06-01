export class Config {
  // public static apiUrl = 'http://localhost:2020/api/';
  public static apiUrl = 'https://brimo-dev-identity-brimoapi.azurewebsites.net/api/';

  // public static IdentityServerUrl = 'http://localhost:5000/';
  public static IdentityServerUrl = 'http://brimo-dev-identity-sts.azurewebsites.net/';


  public static ProductCatalog = 'ProductCatalog';
  public static Products = `${Config.ProductCatalog}/Products`;
  public static Brands = `${Config.ProductCatalog}/Brands`;
  public static ProductCategories = `${Config.ProductCatalog}/ProductCategories`;

}
