
export const WORKFLOWS = {
    basic: 'basic',
    simple: 'simple',
    registeruser: 'registeruser'
}

export const STEPS = {
  personal: 'personal',
  work: 'work',
  address: 'address',
  result: 'result'
}

export interface IWorkflow{ 
  name: string;
  steps: Array<IWorkflowStep>;
  workflowData: any; 
  isValid():boolean;
  getStep(step: string): IWorkflowStep;
  updateStepData(step: string, data:any):void;
  getFirstStep():string; 
}

export interface IWorkflowStep{
  title: string;
  data:any;  
  step: string;
  valid: boolean;
  icon: string;
  editable:boolean;
}
export class WorkflowStep implements IWorkflowStep {
    title: string;
    data: any;
    step: string;    
    valid: boolean;
    icon: string;
    editable: boolean;


    constructor(title:string, step: string, icon:string, editable:boolean = true){
        this.title = title;
        this.step = step;
        this.icon = icon; 
        this.valid = false;
        this.editable = editable;
    }
}

const personalStep: IWorkflowStep = new WorkflowStep("Please tell us about yourself.",STEPS.personal,"glyphicon glyphicon-user", false);
const workStep: IWorkflowStep = new WorkflowStep("What do you do?.",STEPS.work,"glyphicon glyphicon-tasks");
const addressStep: IWorkflowStep = new WorkflowStep("Where do you live?",STEPS.address,"glyphicon glyphicon-home");
const resultStep: IWorkflowStep = new WorkflowStep("Thanks for staying tuned!",STEPS.result,"glyphicon glyphicon-ok");


abstract class BaseWorkflow implements IWorkflow{  
    workflowData: any;   
    name:string;
    steps: IWorkflowStep[];    

    constructor(name: string, steps:IWorkflowStep[], data: any){
        this.name = name;
        this.steps = steps;   
        this.workflowData = data;     
    }
    updateStepData(step: string, data: any): void {
        let wfStep = this.getStep(step);
        if(wfStep){            
            wfStep.data = data;
            //validating in same function, coz its simple for now
            wfStep.valid = true;
        }
    }
    getStep(step: string): IWorkflowStep {
        return this.steps.find(x=> x.step.toLowerCase() == step);
    }

    isValid():boolean{
        return this.steps.every(s=> s.valid);
    }
    getFirstStep():string{
        return this.steps[0].step; //being used for routing
    }
}

export class BasicWorkFlow extends BaseWorkflow{  
    constructor(){
         super(WORKFLOWS.basic, [personalStep,resultStep], new FormData());
    }
}
export class SimpleWorkFlow extends BaseWorkflow {
    constructor(){
        super(WORKFLOWS.simple, [personalStep, addressStep,  resultStep], new FormData());     
    }    
}
export class RegisterUserWorkFlow extends BaseWorkflow {
    constructor(){
        super(WORKFLOWS.registeruser, [personalStep, workStep, addressStep, resultStep], new FormData());     
    }
}






export class OnboardingWorkflow extends BaseWorkflow{
    requestId:  string;
    sourceEmailAddress:string;
    expiresIn:number;
    createdOn: Date;
    status: any;

    constructor(data: any){
        super(data.name.toLowerCase(), data.steps, data.workflowData);
         this.requestId = data.requestId;
         this.sourceEmailAddress = data.sourceEmailAddress;   
         this.expiresIn = data.expiresIn;
         this.createdOn = data.createdOn;
         this.status = data.status;    
    }
}

