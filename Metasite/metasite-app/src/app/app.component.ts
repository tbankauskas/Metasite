import { Component, OnInit } from '@angular/core';
import { EventType } from './models/eventType.model';
import { AppService } from '../app/app.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'metasite-app';
  eventTypes: EventType[];

  constructor(private appService: AppService) { }

  ngOnInit(): void {
    this.getEventTypes();
  }

  getEventTypes() {
    this.appService.getEventTypes().subscribe(et => this.eventTypes = et);
  }
}
