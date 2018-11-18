import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { AddCommentRequest } from '../interfaces/add-comment-request';

@Injectable()
export class CommentService {

  constructor(private baseService: BaseService) { }

  addComment(request: AddCommentRequest): Observable<any> {
    return this.baseService.post<AddCommentRequest, any>('comment', request, true);
  }
}