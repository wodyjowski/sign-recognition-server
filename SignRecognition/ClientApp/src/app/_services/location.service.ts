import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { HttpResponse } from '@angular/common/http';

import { Observable } from 'rxjs';

import { IPrediction } from '../_models/prediction';


  @Injectable()
  export class LocationService {

    locationUrl = 'api/Location';

    constructor(
        private http: HttpClient) {
      }

    getLocations() {
      return this.http.get<FinalLocation[]>(this.locationUrl);
    }

    removeLocation(locId: string) {
      return this.http.delete(this.locationUrl + '/' + locId);
    }

  }

  export interface FinalLocation {
    id: string;
    creationDate: Date;
    modifDate: Date;
    class: string;
    latitude: number;
    longitude: number;
}
