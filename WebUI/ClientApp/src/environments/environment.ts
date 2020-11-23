// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  instrumentationKey: '5d54bb2b-dbe6-4be0-b638-99d1156bded7',
  apiUrl: 'https://brimo-dev-identity-brimoapi.azurewebsites.net/api/',
  identityServerUrl: 'http://brimo-dev-identity-sts.azurewebsites.net/'
  
  // apiUrl: 'http://localhost:2020/api/',
  // identityServerUrl: 'http://localhost:5000/'
};

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
