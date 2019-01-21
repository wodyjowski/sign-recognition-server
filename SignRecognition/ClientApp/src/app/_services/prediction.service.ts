import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { HttpResponse } from '@angular/common/http';

import { Observable } from 'rxjs';

import { IPrediction } from '../_models/prediction';


  @Injectable()
  export class PredictionService {

    predictionUrl = 'api/Prediction';

    constructor(
        private http: HttpClient) {
      }


    saveLocation(location: IPrediction): Observable<HttpResponse<IPrediction>> {
      return this.http.post<IPrediction>(
        this.predictionUrl,
        location,
        {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
            'Authorization': 'my-auth-token'
          }),
          observe: 'response'
      });
    }

    getPredictions(): Observable<any[]> {
      return this.http.get<IPrediction[]>(this.predictionUrl);
    }

  }