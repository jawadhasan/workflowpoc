import { Component, OnInit } from '@angular/core';
import { ICompany, CompanyService } from '../company.service';
import { Envelop } from '../../../app/shared/envelop';

@Component({
  selector: 'app-company-grid',
  templateUrl: './company-grid.component.html',
  styleUrls: ['./company-grid.component.css']
})
export class CompanyGridComponent implements OnInit {

  pageTitle = 'Company Grid Page';
  companies: ICompany[];
  
  constructor(private companyService: CompanyService) { }

  ngOnInit() {
    this.companyService.getCompanies()
    .subscribe(
    (data: Envelop<ICompany[]>) => {
      this.companies = data.result;
    },
    (err: any) => console.error(err),
    () => console.info('loading of companies data done.')
    );
  }
}
