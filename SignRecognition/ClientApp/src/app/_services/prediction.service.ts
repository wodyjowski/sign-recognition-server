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

    getPredictions(pageNum: number = 0): Observable<any[]> {
      return this.http.get<IPrediction[]>(this.predictionUrl, {
        params: {
          page: pageNum.toString()
        }});
    }

    getUserPredictions(pageNum: number = 0): Observable<any[]> {
      return this.http.get<IPrediction[]>(this.predictionUrl + '/UAll', {
        params: {
          page: pageNum.toString()
        }});
    }

    getPredictionCount() {
      return this.http.get<number>(this.predictionUrl + '/PredCount');
    }

    getUserPredictionCount() {
      return this.http.get<number>(this.predictionUrl + '/UPredCount');
    }

    deletePrediction(predId: string) {
      return this.http.delete(this.predictionUrl + '/' + predId);
    }

  }
