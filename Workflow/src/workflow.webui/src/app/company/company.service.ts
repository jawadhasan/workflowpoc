import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpResponse,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable } from 'rxjs';

import { map, catchError } from 'rxjs/operators';
import { Envelop } from '../shared/envelop';

export interface ICompany {
  id: number;
  name: string;
  street: string;
  postCode: string;
  city: string;
  isActive: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  apiUrl: string = "http://localhost:31540/api/";

  constructor(private httpClient: HttpClient) {  }

  getCompanies(): Observable<Envelop<ICompany[]>> {   
    // return this.http.post<any>(`${this.apiUrl}workflow/`, inviteForm);
    return this.httpClient.get<Envelop<ICompany[]>>(`${this.apiUrl}companies`);
  }

  getCompany(id: string): Observable<Envelop<ICompany>> {
    return this.httpClient.get<Envelop<ICompany>>(this.apiUrl + 'companies/' + id);
  }

  insertCompany(company: ICompany): Observable<ICompany> {
    return this.httpClient
      .post<Envelop<ICompany>>(this.apiUrl + 'companies', company)
      .pipe(
        map((data: Envelop<ICompany>) => {
          if (data) {
            return data.result;
          }
          return null;
        }),
        catchError(this.handleError)
      );
  }

  updateCompany(company: ICompany): Observable<ICompany> {
    return this.httpClient
      .put<Envelop<ICompany>>(this.apiUrl + 'companies/' + company.id, company)
      .pipe(
        map((data: Envelop<ICompany>) => {
          if (data) {
            return data.result;
          }
          return null;
        }),
        catchError(this.handleError)
      );
  }

  deleteCompany(id: number): Observable<Envelop<any>> {
    return this.httpClient.delete<Envelop<any>>(this.apiUrl + 'companies/' + id);
  }

  private handleError(error: HttpErrorResponse) {
    console.error('server error:', error);

    if (error.error instanceof Error) {
      const errMessage = error.error.message;
      return Observable.throw(errMessage);
    }

    return Observable.throw(error || 'ASP.NET Core server error');
  }
}
