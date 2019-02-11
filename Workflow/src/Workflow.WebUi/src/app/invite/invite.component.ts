import { Component, OnInit } from '@angular/core';
import { OnboardingService } from '../onboarding/onboarding.service';
import { Invite, WorkflowInvitationResponse } from '../data/formData.model';

@Component({
  selector: 'app-invite',
  templateUrl: './invite.component.html',
  styleUrls: ['./invite.component.scss']
})
export class InviteComponent implements OnInit {

  workflowUrl: string;
  wfinviteForm: Invite = {
    sourceEmailAddress: "",
    workflowName: ""
  };
  workflowInvitationResponse: WorkflowInvitationResponse;


  constructor(private onboardingService: OnboardingService) { }

  ngOnInit() {
    
    this.wfinviteForm.sourceEmailAddress = "jawadhasan80@gmai.com"
    this.wfinviteForm.workflowName = "OnboardingGiver";
  
  }
  invite(form: any) { 
    this.onboardingService.createWorkflow(this.wfinviteForm)
      .subscribe((data:WorkflowInvitationResponse)=>{
        this.workflowInvitationResponse = data;        
        // this.workflowUrl =  "http://localhost:4200/onboarding/" + data.requestId;
        this.workflowUrl = `${window.location.href}onboarding/${data.requestId}`;
      }, (error: any)=> console.log(error));   
  }

}


