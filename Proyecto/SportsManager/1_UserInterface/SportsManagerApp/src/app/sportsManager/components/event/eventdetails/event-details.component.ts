import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { Component, Output, EventEmitter, Input, SimpleChanges } from "@angular/core";
import { SessionService } from "src/app/sportsManager/services/session.service";
import { Event } from '../../../interfaces/event'
import { AddCommentRequest } from "src/app/sportsManager/interfaces/add-comment-request";
import { CommentService } from "src/app/sportsManager/services/comment.service";
import { EventService } from "src/app/sportsManager/services/event.service";

@Component({
    selector: 'app-event-details',
    templateUrl: './event-details.component.html',
    styleUrls: ['./event-details.component.css']
})
export class EventDetailsComponent extends BaseComponent {

    commentModel: AddCommentRequest;
    errorMessage: string;
    activateSubmit: boolean = true;
    _event: Event;

    @Input()
    set event(value: Event) {
        this._event = value;
        this.setupModel();
    }
    get event(): Event {
        return this._event;
    }

    @Output() closeRequested = new EventEmitter<any>();

    constructor(
        private sessionService: SessionService,
        private commentService: CommentService,
        private eventService: EventService) {
        super();
    };

    setupModel() {
        this.commentModel = { creatorName: this.sessionService.getCurrentUserName(), description: '', id: this._event.id }
    }

    closeDetails() {
        this.commentModel = { creatorName: this.sessionService.getCurrentUserName(), description: '', id: 0 }
        this._event = undefined;
        this.closeRequested.emit(true);
    }

    componentOnInit() {
        this.errorMessage = undefined;
    }

    onSubmit() {
        this.activateSubmit = false;
        this.errorMessage = undefined;
        this.commentModel.id = this._event.id;
        this.commentService.addComment(this.commentModel).subscribe(
            response => this.handleResponse(response),
            error => this.handleError(error));
    }

    componentOnChanges(changes: SimpleChanges) {
        this.activateSubmit = true;
    }

    get formIsValid(): boolean {
        return this.commentModel.creatorName !== undefined &&
            this.commentModel.description !== undefined &&
            this.commentModel.description !== '';
    }

    private handleResponse(response: any) {
        this.eventService.getEventById(this._event.id).subscribe(
            response => { this._event = response },
            error => this.handleError(error));
        this.resetForm();
    }

    private handleError(error: any) {
        console.log(error);
        this.errorMessage = <any>error;
    }

    resetForm() {
        this.commentModel.description = '';
    }

}