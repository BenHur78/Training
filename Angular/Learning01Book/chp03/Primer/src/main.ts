/*import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
  */

  //Typescript requires we to explicitly declare param type to any
  function myFunction(param: number) {
    //if (typeof(param) == "number") {
      let result = param + 100;
      console.log("My result: " + result);
    //} else {
    //  throw ("Expected a number: " + param)
    //}
   }

   myFunction(1);
   //myFunction("London");