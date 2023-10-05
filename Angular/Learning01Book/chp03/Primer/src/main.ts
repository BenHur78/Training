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

  let val1: string | undefined;
  let val2: string | undefined = "London";
  let coalesced1 = val1 || "fallback value";
  let coalesced2 = val2 || "fallback value";
  console.log(`Result 1: ${coalesced1}`);
  console.log(`Result 2: ${coalesced2}`);