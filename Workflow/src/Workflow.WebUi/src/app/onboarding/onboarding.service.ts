import { Injectable, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { FormData, Personal, Address, Work } from './onboarding.model'
import { STEPS, IWorkflow } from '../workflow/workflow.model';
import { Invite } from '../data/formData.model';


@Injectable({
    providedIn: 'root'
})
export class OnboardingService {
    // private baseUrl: string = "http://localhost:31540/api/";
    baseUrl: string = "http://localhost:31537/api/";
    workflow: IWorkflow;
    private formData: FormData = new FormData();

    @Output() stepCompleted = new EventEmitter<string>();
    @Output() stepAnimationDone = new EventEmitter<any>();

    constructor(private http: HttpClient) { }


    supportedWorkflows = [
        { id: 1, value: 'OnboardingGiver' },
        { id: 2, value: 'OnboardingTaker' },
        { id: 3, value: 'Basic' }
    ];


    isStepValid(step: string): boolean {
        return this.workflow.getStep(step).valid;
    }

    updateStepData(step: string, data: any) {
        let wfStep = this.workflow.getStep(step);
        if (wfStep) {
            wfStep.data = data;
            wfStep.valid = true; //validating in same function, coz its simple for now
        }
    }

    //following getStepData/setStepData methods are called from init()/save() of respective components
    getStepData(step: string): any {
        console.log('getting data ' + step);
        switch (step) {
            case STEPS.personal: return new Personal(this.formData.firstName, this.formData.lastName, this.formData.email);
            case STEPS.work: return new Work(this.formData.work);
            case STEPS.address: return new Address(this.formData.street, this.formData.city, this.formData.state, this.formData.zip);
            case STEPS.result: return this.formData;
            default: throw new Error("not implemented");
        }
    }

    setStepData(step: string, data: any) {
        switch (step) {
            case STEPS.personal:
                this.setPersonal(data);
                break;

            case STEPS.work:
                this.setWork(data);
                break;

            case STEPS.address:
                this.setAddress(data);
                break;

            case STEPS.result:
                this.setResult();
                break;

            default: throw new Error("not implemented");
        }
    }


    private setPersonal(data: Personal) {
        this.formData.firstName = data.firstName;
        this.formData.lastName = data.lastName;
        this.formData.email = data.email;

        this.workflow.workflowData.firstName = data.firstName;
        this.workflow.workflowData.lastName = data.lastName;
        this.workflow.workflowData.email = data.email;

        this.updateStepData(STEPS.personal, data);
        this.stepCompleted.emit(STEPS.personal);
    }
    private setWork(data: string) {
        this.formData.work = data;

        this.workflow.workflowData.work = data;

        this.updateStepData(STEPS.work, data);
        this.stepCompleted.emit(STEPS.work);

    }
    private setAddress(data: Address) {
        this.formData.street = data.street;
        this.formData.city = data.city;
        this.formData.state = data.state;
        this.formData.zip = data.zip;


        this.workflow.workflowData.street = data.street;
        this.workflow.workflowData.city = data.city;
        this.workflow.workflowData.state = data.state;
        this.workflow.workflowData.zip = data.zip;

        this.updateStepData(STEPS.address, data);
        this.stepCompleted.emit(STEPS.address);
    }
    private setResult() {
        this.updateStepData(STEPS.result, true);
        this.workflow.workflowData.isValid = true;

        this.stepCompleted.emit(STEPS.result);
    }


    setFormData(data: any): void {
        this.formData.firstName = data.firstName;
        this.formData.lastName = data.lastName;
        this.formData.email = data.email;
        this.formData.work = data.work;
        this.formData.street = data.street;
        this.formData.city = data.city;
        this.formData.state = data.state;
        this.formData.zip = data.zip;
    }
    getFormData(): FormData {
        // Return the entire Form-Data
        return this.formData;
    }

    resetFormData(): FormData {
        // Reset the workflow
        this.workflow.steps.forEach(element => {
            element.valid = false;
        })

        // Return the form data after all this. * members had been reset
        this.formData.clear();
        return this.formData;
    }

    isFormValid(): boolean {
        return this.workflow.isValid();
    }



    createWorkflow(inviteForm: Invite): Observable<any> {
        return this.http.post<any>(`${this.baseUrl}workflow/`, inviteForm);
    }

    //Load workflow from server
    loadWorkflow(requestId: string): Observable<IWorkflow> {
        return this.http.get<IWorkflow>(`${this.baseUrl}workflow/` + requestId);
    }

    updateWorkflow(step:string, requestId: string, workflow: IWorkflow): Observable<any> {
        console.log('updateWorkflow called for step: ', step);
        return this.http.put<any>(`${this.baseUrl}workflow/${step}/` + requestId, workflow);
    }
  
}