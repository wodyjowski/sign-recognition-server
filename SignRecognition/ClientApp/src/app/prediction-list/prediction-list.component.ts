import { Component, OnInit } from '@angular/core';
import { PredictionService } from '../_services/prediction.service';
import { IPrediction } from '../_models/prediction';

import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-prediction-list',
  templateUrl: './prediction-list.component.html',
  providers: [ PredictionService ],
  styleUrls: ['./prediction-list.component.css']
})
export class PredictionListComponent implements OnInit {

  predictions: IPrediction[];
  loading = true;
  page = 1;

  user = true;

  predCount: Number = null;
  userPredCount: Number = null;

  constructor(private predictionService: PredictionService,
    private router: Router,
    private http: HttpClient,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.getLocations();
    this.predictionService.getPredictionCount().subscribe(p => this.predCount = p);
    this.predictionService.getUserPredictionCount().subscribe(p => this.userPredCount = p);
  }

  getLocations() {
    this.predictions = [];
    this.loading = true;
    if (this.user) {
      this.predictionService.getUserPredictions()
      .subscribe(predictions => this.omg(predictions));
    } else {
      this.predictionService.getPredictions()
      .subscribe(predictions => this.omg(predictions));
    }
  }

  omg (predictions) {
    this.predictions = predictions;
    // window.console.log(predictions);
    this.loading = false;
  }

  navigate(location: IPrediction) {
      this.router.navigate(['/'], { queryParams: { lat: location.latitude, lng: location.longitude }});
  }

  remove (location: IPrediction) {
      this.http.post('/Location/Delete', location)
      .pipe(
        catchError(err => this.error(err)))
      .subscribe(() => this.handleResponse(location) );
      event.stopPropagation();
  }

  handleResponse(location) {
      this.predictions.splice(this.predictions.indexOf(location), 1);
      this.toastr.success('Location removed');
  }

  error(err) {
      this.toastr.error('Error');
      return err;
  }

  onScroll() {
    // this.toastr.warning('Scroll');
    this.loading = true;
    this.predictionService.getPredictions(this.page)
    .subscribe(predictions => { this.predictions =  this.predictions.concat(predictions); ++this.page; this.loading = false;} );
  }

  setUserPred(type: boolean) {
    this.user = type;
    this.getLocations();
  }

}

