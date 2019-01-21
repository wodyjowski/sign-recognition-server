import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { HttpResponse } from '@angular/common/http';

import { Observable } from 'rxjs';

import { ILocation } from './location';


  @Injectable()
  export class LocationService {

    locationUrl = '/Location/Create';  // URL to web api
    getLocationUrl = '/Location/Get';

    constructor(
        private http: HttpClient) {
      }


    saveLocation(location: ILocation): Observable<HttpResponse<ILocation>> {
      return this.http.post<ILocation>(
        this.locationUrl,
        location,
        {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
            'Authorization': 'my-auth-token'
          }),
          observe: 'response'
      });
    }

    geLocations(): Observable<any[]> {
      return this.http.get<any[]>(this.getLocationUrl);
    }

  }
