import { Component, OnInit } from '@angular/core';
import { Personal } from 'src/app/data/formData.model';
import { OnboardingService } from '../onboarding.service';
import { STEPS } from './../../workflow/workflow.model';

@Component({
  selector: 'app-personal-step',
  templateUrl: './personal-step.component.html',
  styleUrls: ['./personal-step.component.css']
})
export class PersonalStepComponent implements OnInit {
  personal: Personal;

  constructor(private onboardingService: OnboardingService) {   
  }
  ngOnInit() {   
    this.personal = this.onboardingService.getStepData(STEPS.personal);   
  }
  goToNext(form: any) {
    this.onboardingService.setStepData(STEPS.personal, this.personal);
  }
}
