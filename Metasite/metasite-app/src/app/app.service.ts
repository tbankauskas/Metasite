import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '..//environments/environment';
import { Observable } from 'rxjs';
import { EventType } from './models/eventType.model';

@Injectable({ providedIn: 'root' })
export class AppService {

    baseUrl = environment.API_URL;

    constructor(private http: HttpClient) { }

    getEventTypes(): Observable<EventType[]> {
        return this.http.get<EventType[]>(this.baseUrl + 'api/data/eventtypes');
    }
}
