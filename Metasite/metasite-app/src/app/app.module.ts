import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MAT_DATE_LOCALE } from '@angular/material/core';
import {
  MatDatepickerModule, MatFormFieldModule, MatNativeDateModule,
  MatInputModule, MatButtonModule, MatSelectModule, MatCheckboxModule,
  MatTableModule
} from '@angular/material';

import { MAT_MOMENT_DATE_ADAPTER_OPTIONS, MatMomentDateModule }
  from '@angular/material-moment-adapter';

import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatCheckboxModule,
    MatTableModule,
    MatMomentDateModule
  ],
  providers: [
    { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } },
    { provide: MAT_DATE_LOCALE, useValue: 'lt' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
