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

  let firstBool = true;
let secondBool = false;

let firstString = "This is a string";
let secondString = 'And so is this';

let place: string | undefined | null;
console.log(`Place value: ${place} Type: ${typeof(place)}`);