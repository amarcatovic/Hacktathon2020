import { CompanytasksService } from './../../services/companytasks.service';
import { Component, OnInit } from '@angular/core';
import { DocumentationServiceService } from 'app/modules/services/documentation-service.service';

@Component({
  selector: 'app-companyMain',
  templateUrl: './companyMain.component.html',
  styleUrls: ['./companyMain.component.scss']
})
export class CompanyMainComponent implements OnInit {

  tasks: any[];
  users: any[];
  currentDate: Date = new Date();

  constructor(private taskService: CompanytasksService) { }


  ngOnInit() {
    this.taskService.getCompanyPublicTask()
      .subscribe((data: any[]) => {
        this.tasks = data;
        console.log(this.tasks)
      });

    this.taskService.getCompanyUsers()
      .subscribe((data: any[]) => {
        this.users = data;
        console.log(this.users)
      });
  }

}
