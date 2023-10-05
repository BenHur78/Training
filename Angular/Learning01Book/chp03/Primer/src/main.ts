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

  function myFunction(param: number | string) {
    if (typeof(param) == "number" || typeof(param) == "string") {
      let fixed = param.toFixed(2);
      console.log("My result: " + fixed);
    } else {
      throw ("Expected a number or a string: " + param)
    }
   }

   myFunction(1);
   myFunction("London");