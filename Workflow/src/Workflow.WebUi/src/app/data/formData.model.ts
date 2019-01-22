
export interface Invite {
  sourceEmailAddress: string;
  workflowName: string;
}
export interface WorkflowInvitationResponse{
  requestId: string,
  createdAt: string,
  sourceEmailAddress: string;
  workflowName: string;
}


export class FormData {
  firstName: string = '';
  lastName: string = '';
  email: string = '';
  work: string = '';
  street: string = '';
  city: string = '';
  state: string = '';
  zip: string = '';

  clear() {
    this.firstName = '';
    this.lastName = '';
    this.email = '';
    this.work = '';
    this.street = '';
    this.city = '';
    this.state = '';
    this.zip = '';
  }
}

export class Personal {
  firstName: string = '';
  lastName: string = '';
  email: string = '';

  constructor(firstName: string, lastName: string, email: string) {
    this.firstName = firstName;
    this.lastName = lastName;
    this.email = email;
  }

  isValid(): boolean {
    return !this.isEmpty(this.firstName)
    && !this.isEmpty(this.lastName)
    && !this.isEmpty(this.email);
  }
  private isEmpty(str) {
    return (!str || 0 === str.length);
  }
}

export class Address {
  street: string = '';
  city: string = '';
  state: string = ''
  zip: string = ''

  constructor(street: string, city: string, state: string, zip: string) {
    this.street = street;
    this.city = city;
    this.state = state;
    this.zip = zip;
  }

  isValid(): boolean {   
    return !this.isEmpty(this.street)
     && !this.isEmpty(this.city)
     && !this.isEmpty(this.state)
     && !this.isEmpty(this.zip);
  }

  private isEmpty(str) {
    return (!str || 0 === str.length);
  }
}
