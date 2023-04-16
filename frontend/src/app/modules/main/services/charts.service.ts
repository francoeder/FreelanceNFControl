import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({
    providedIn: 'root'
  })
  export class ChartsService {
  
    yearFilter: BehaviorSubject<number>;

    constructor() {
        this.yearFilter = new BehaviorSubject<number>(new Date().getFullYear());
    }
  }