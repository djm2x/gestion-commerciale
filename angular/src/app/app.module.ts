import { DeleteComponent } from './layouts/delete/delete.component';
import { LoaderModule } from './loader/loader.module';
import { MatModule } from './mat.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Injector } from '@angular/core';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { InjectService } from './inject.service';
import { AppRoutingModule } from './app-routing.module';
import { MessageComponent } from './shared/snakebar.service';
import { LoaderInterceptor } from './loader/loader-interceptor';
import { MatDialogRef, MAT_DIALOG_DATA, MAT_DATE_LOCALE } from '@angular/material';
import { ToastrModule } from 'ngx-toastr';
import { NgxMatDatetimePickerModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';

@NgModule({
  declarations: [
    AppComponent,
    MessageComponent,
    DeleteComponent,
  ],
  entryComponents: [
    DeleteComponent
  ],
  imports: [
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatModule,
    FormsModule,
    BrowserAnimationsModule,
    LoaderModule,
    ToastrModule.forRoot(),
    // MatSnackBarModule,
    // MatProgressSpinnerModule,

    // NgxMatDatetimePickerModule,
    // NgxMatTimepickerModule,
  ],
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: 'fr' },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoaderInterceptor,
      multi: true
    },
    { provide: MatDialogRef, useValue: {} },
    { provide: MAT_DIALOG_DATA, useValue: {} }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(private injector: Injector) {    // Create global Service Injector.
    InjectService.injector = this.injector;
  }
}
