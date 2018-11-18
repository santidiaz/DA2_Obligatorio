import { Component, SimpleChanges } from '@angular/core';
import { BaseComponent } from '../../shared/base.component';
import { SessionService } from '../../services/session.service';
import { Router } from '@angular/router';
import { Log } from '../../interfaces/log';
import { LogService } from '../../services/log.service';
import { LogRequest } from '../../interfaces/log-request';

@Component({
  selector: 'app-log-report',
  templateUrl: './log-report.component.html',
  styleUrls: ['./log-report.component.css']
})
export class LogReportComponent extends BaseComponent {

  auditLogs: Array<Log> = [];
  logRequest: LogRequest;
  errorMessage: string;

  constructor(
    private sessionService: SessionService,
    private logService: LogService,
    private router: Router) {
    super();
    this.logRequest = { fromDate: undefined, toDate: undefined };
  }

  componentOnInit() {
    this.errorMessage = undefined;
  }


  filterLogByDates() {
    this.errorMessage = undefined;
    this.logService.getLogs(this.logRequest).subscribe(
      response => this.handleResponse(response),
      error => this.handleError(error));
  }

  componentOnChanges(changes: SimpleChanges) {
    this.errorMessage = undefined;
  }

  get datesAreValid() : boolean {    
    if(this.logRequest.fromDate != undefined && 
      this.logRequest.toDate != undefined &&
      this.logRequest.fromDate <= this.logRequest.toDate){
        return true;
      }
    return false;
  }

  private handleResponse(response: Array<Log>) {
    this.auditLogs = response;
  }

  private handleError(response: any) {
    this.errorMessage = response.error;
    ;
  }
}