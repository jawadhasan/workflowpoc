import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatStepper } from '@angular/material';
import { OnboardingService } from './onboarding.service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { STEPS, OnboardingWorkflow } from '../workflow/workflow.model';

@Component({
  selector: 'app-onboarding',
  templateUrl: './onboarding.component.html',
  styleUrls: ['./onboarding.component.css']
})
export class OnboardingComponent implements OnInit {
  @ViewChild('stepper') stepper: MatStepper;
  workflow: OnboardingWorkflow; //do we need this or only service level object is ok?

  rawData: any; //for debugging only
  showDebugInfo:boolean = false; //for debugging only
  isLinear: boolean = true; //can be configurable

  currentSelectedIndex = 0;
  


  constructor(private router: Router, private route: ActivatedRoute, private onboardingService: OnboardingService) {

    //this callback is responsible for routing to next step after save is called.
    this.onboardingService.stepCompleted.subscribe(step => {
      this.onStepCompletedHandler(step);
    });

  }

  onStepCompletedHandler(step: string){   
    var stepData = this.onboardingService.getStepData(step);
   
    this.onboardingService.updateWorkflow(step,this.workflow.requestId, stepData)
    .subscribe(data=>{
      console.log(' in subscribe ', data);
    });

    this.stepper.selected.completed = true; //could be done systamtically
    this.stepper.selected.editable = true; ////could be done systamtically
    this.stepper.next();

    //if last step (result) then navigate to other part of the app.
    if (step == STEPS.result) {
      this.stepper.reset();
      this.router.navigate(["/"]);
    }
  }

  ngOnInit() {   
     
    //load from server
    let requestId: string = this.route.snapshot.params['id'];
    this.onboardingService.loadWorkflow(requestId)
      .subscribe(data => {
        if(data){
          this.workflow = new OnboardingWorkflow(data);  
          this.rawData = data;
          this.onboardingService.workflow = this.workflow;
          console.log(this.onboardingService.workflow);
          this.onboardingService.setFormData(this.onboardingService.workflow.workflowData);
          this.gotoRoute();
        }else{
          alert('no such request exist!');
        }

      }, (err: any) => console.log(err),
        () => console.log('loaded from server finished!') );

    this.stepper.selectionChange.subscribe(e => {
      console.log('selectionChange: e.selectedIndex: ', e.selectedIndex);
      this.currentSelectedIndex = e.selectedIndex;
      this.gotoRoute();
    });
  }


  gotoRoute() {   
    let currentStep = this.onboardingService.workflow.steps[this.currentSelectedIndex];  
    if (currentStep) {
      let url = `onboarding/${this.workflow.requestId}/${currentStep.step.toLowerCase()}`;
      console.log('url', url);
      this.router.navigate([url]);
    }
  }

}
