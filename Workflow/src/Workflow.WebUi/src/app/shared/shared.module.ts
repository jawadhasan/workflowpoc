import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CapitalizePipe } from './capitalize.pipe';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from '../material/material.module';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    CapitalizePipe
  ],
  exports: [
    CapitalizePipe,
    CommonModule,
    FormsModule,
    MaterialModule
  ]
})
export class SharedModule { }
