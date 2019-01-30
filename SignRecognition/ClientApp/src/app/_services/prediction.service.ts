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

    getPredictions(pageNum: number = 0, allUsers: boolean = false, searchLocation: string = null): Observable<IPrediction[]> {

      let params = new HttpParams();
      params = params.append('page', pageNum.toString());
      let url = this.predictionUrl;

      if (allUsers) {
        url += '/UAll';
      }

      if (searchLocation && searchLocation !== '') {
        params = params.append('locationName', searchLocation);
      }

      return this.http.get<IPrediction[]>(url,
        {params: params} );
    }


    getPredictionCount(location: string = null) {

      let url = this.predictionUrl + '/PredCount?locationName=';

      if (location) {
        url += location;
      }

      return this.http.get<number>(url);
    }

    getUserPredictionCount(location: string = null) {

      let url = this.predictionUrl + '/UPredCount?locationName=';

      if (location) {
        url += location;
      }

      return this.http.get<number>(url);
    }

    deletePrediction(predId: string) {
      return this.http.delete(this.predictionUrl + '/' + predId);
    }

    getPrediction(predId: string) {
      return this.http.get<IPrediction>(this.predictionUrl + '/' + predId);
    }

  }
