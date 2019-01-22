import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';

import { OnboardingComponent } from './onbording.component';
import { PersonalStepComponent } from './personal-step/personal-step.component';
import { WorkStepComponent } from './work-step/work-step.component';
import { AddressStepComponent } from './address-step/address-step.component';
import { ResultStepComponent } from './result-step/result-step.component';

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild([
      {
        path: 'onboarding/:id', component: OnboardingComponent,
        children: [         
          { path: 'personal', component: PersonalStepComponent },
          { path: 'work', component: WorkStepComponent },
          { path: 'address', component: AddressStepComponent },
          { path: 'result', component: ResultStepComponent }            
        ]
      }
    ])
  ],
  declarations: [
    OnboardingComponent,
    PersonalStepComponent,
    WorkStepComponent,
    AddressStepComponent,
    ResultStepComponent
  ]
})
export class OnboardingModule { }
