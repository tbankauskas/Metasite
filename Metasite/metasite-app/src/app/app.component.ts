import { Component, OnInit } from '@angular/core';
import { EventType } from './models/eventType.model';
import { AppService } from './app.service';
import { Filter } from './models/filter.model';
import { EventLog } from './models/eventLog.model';
import { EventTops } from './models/eventTops.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'metasite-app';
  eventTypes: EventType[];
  eventLog: EventLog[];
  topsDataSource: EventTops[];
  filter: Filter;
  recordsCount: number;

  isTops: boolean;
  topViewColumnTitle: string;

  displayedColumns: string[] = ['msIsdnNumber', 'eventType', 'duration', 'timestamp'];
  displayedTopsColumns: string[] = ['msIsdnNumber', 'sum'];

  constructor(private appService: AppService) { }

  ngOnInit(): void {
    this.getEventTypes();
    this.filter = new Filter();
  }

  getEventTypes() {
    this.appService.getEventTypes().subscribe(et => this.eventTypes = et);
  }

  filterForm() {
    if (this.filter.filterTop5) {
      this.appService.getEventLogTops(this.filter).subscribe(el => {
        this.topsDataSource = el;
        this.recordsCount = this.topsDataSource.length;
        this.isTops = true;
        this.topViewColumnTitle = this.filter.type === 'sms' ? 'SMS Count' : 'Duration Sum';
      });
      return;
    }
    this.appService.getEventLog(this.filter).subscribe(el => {
      this.eventLog = el;
      this.recordsCount = this.eventLog.length;
      this.isTops = false;
    });
  }
}
