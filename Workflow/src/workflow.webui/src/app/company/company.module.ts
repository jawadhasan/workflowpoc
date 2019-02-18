import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { CompanyComponent } from './company.component';
import { CompanyGridComponent } from './company-grid/company-grid.component';
import { CompanyEditComponent } from './company-edit/company-edit.component';

@NgModule({
  imports: [
   SharedModule,
   RouterModule.forChild([
    {
      path: 'companies', component: CompanyComponent,    
      children: [         
        { path: 'company/:id', component: CompanyEditComponent },
        { path: '', component: CompanyGridComponent }               
      ]
    }
  ])
  ],
  declarations: [CompanyComponent, CompanyGridComponent, CompanyEditComponent]
})
export class CompanyModule { }
