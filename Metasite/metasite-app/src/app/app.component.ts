import { Component, OnInit } from '@angular/core';
import { EventType } from './models/eventType.model';
import { AppService } from './app.service';
import { Filter } from './models/filter.model';
import { EventLog } from './models/eventLog.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'metasite-app';
  eventTypes: EventType[];
  eventLog: EventLog[];
  filter: Filter;

  displayedColumns: string[] = ['msIsdnNumber', 'eventType', 'duration', 'timestamp'];

  constructor(private appService: AppService) { }

  ngOnInit(): void {
    this.getEventTypes();
    this.filter = new Filter();
  }

  getEventTypes() {
    this.appService.getEventTypes().subscribe(et => this.eventTypes = et);
  }

  filterForm() {
    this.appService.getEventLog(this.filter).subscribe(el => this.eventLog = el);
  }
}
