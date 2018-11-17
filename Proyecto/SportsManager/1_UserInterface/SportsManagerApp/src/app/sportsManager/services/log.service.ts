import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { Log } from '../interfaces/log';
import { HttpParams } from '@angular/common/http';
import { LogRequest } from '../interfaces/log-request';

@Injectable()
export class LogService {

  constructor(private baseService: BaseService) { }

  getLogs(request: LogRequest): Observable<Array<Log>> {
    return this.baseService.get<Array<Log>>('log', true, request);
  }
}