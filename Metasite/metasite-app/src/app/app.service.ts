import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';
import { EventType } from './models/eventType.model';
import { EventLog } from './models/eventLog.model';
import { Filter } from './models/filter.model';
import { EventTops } from './models/eventTops.model';

@Injectable({ providedIn: 'root' })
export class AppService {

    baseUrl = environment.API_URL;

    constructor(private http: HttpClient) { }

    getEventTypes(): Observable<EventType[]> {
        return this.http.get<EventType[]>(this.baseUrl + 'api/data/eventtypes');
    }

    getEventLog(filter: Filter): Observable<EventLog[]> {
        return this.http.post<EventLog[]>(this.baseUrl + 'api/data/eventlog', filter);
    }

    getEventLogTops(filter: Filter): Observable<EventTops[]> {
        return this.http.post<EventTops[]>(this.baseUrl + 'api/data/eventtops', filter);
    }
}
