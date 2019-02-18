import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { CompaniesComponent } from './companies.component';
import { CompanyGridComponent } from './company-grid/company-grid.component';
import { CompanyEditComponent } from './company-edit/company-edit.component';

@NgModule({
  imports: [
   SharedModule,
   RouterModule.forChild([
    {
      path: 'companies', component: CompaniesComponent,    
      children: [         
        { path: 'company/:id', component: CompanyEditComponent },
        { path: '', component: CompanyGridComponent }               
      ]
    }
  ])
  ],
  declarations: [CompaniesComponent, CompanyGridComponent, CompanyEditComponent]
})
export class CompaniesModule { }
