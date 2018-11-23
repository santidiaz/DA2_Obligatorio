import {
    OnChanges,
    SimpleChange,
    OnInit,    
    AfterContentInit,
    AfterContentChecked,
    AfterViewInit,
    AfterViewChecked,
    OnDestroy
} from "@angular/core";


export class BaseComponent implements OnChanges, OnInit, AfterContentInit, AfterContentChecked,
    AfterViewInit, AfterViewChecked, OnDestroy {

    isAdmin: boolean;

    constructor() {
    }

     //#region ng life cycle
     ngOnInit() {
        this.componentOnInit();
    }

    ngOnDestroy() {
        this.componentOnDestroy();
    }

    ngDoCheck() {
        this.componentDoCheck();
    }

    ngOnChanges(changes: {[propertyName: string]: SimpleChange}) {
        this.componentOnChanges(changes);
    }

    ngAfterContentInit() {
        this.componentAfterContentInit();
    }

    ngAfterContentChecked() {
        this.componentAfterContentChecked();
    }

    ngAfterViewInit() {
        this.componentAfterViewInit();
    }

    ngAfterViewChecked() {
        this.componentAfterViewChecked();
    }
    //#endregion

    //#region Protected Methods
    protected componentOnInit() {
    }

    protected componentOnDestroy() {
        // unsubscribe all the active subscriptions
    }

    protected componentDoCheck() {
    }

    protected componentOnChanges(changes: {[propertyName: string]: SimpleChange}) {
    }

    protected componentAfterContentInit() {
    }

    protected componentAfterContentChecked() {
    }

    protected componentAfterViewInit() {
    }

    protected componentAfterViewChecked() {
    }
    //#endregion

    // protected userIsAuthorized(): boolean {
    //     return this._contextInfo.existsUserPermission(this._userFeature, this._userProductId);
    // }
}
