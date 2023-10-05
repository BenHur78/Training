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

  let firstVal: any = 5;
  let secondVal: any = "5";
  if (firstVal == secondVal) {
   console.log("They are the same");
  } else {
   console.log("They are NOT the same");
  }