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

  userPredictions = true;

  predCount: Number = null;
  userPredCount: Number = null;

  private admin = false;

  searchLocation: string;

  constructor(private predictionService: PredictionService,
    private router: Router,
    private http: HttpClient,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.getLocations();
    this.refreshCount();
    this.admin = JSON.parse(localStorage.getItem('isAdmin'));
  }

  refreshCount(location: string = null) {
    this.predictionService.getPredictionCount(location).subscribe(p => this.predCount = p);
    this.predictionService.getUserPredictionCount(location).subscribe(p => this.userPredCount = p);
  }

  refresh() {
    this.searchLocation = null;
    this.refreshCount();
    this.getLocations();
  }

  getLocations(location: string = null) {
    this.predictions = [];
    this.loading = true;

    this.predictionService.getPredictions(0, this.userPredictions, location)
    .subscribe(predictions => this.stopLoading(predictions));

  }

  stopLoading (predictions) {
    this.predictions = predictions;
    // window.console.log(predictions);
    this.loading = false;
  }

  navigate(location: IPrediction) {
      this.router.navigate(['/'], { queryParams: { id: location.id }});
  }

  remove (prediction: IPrediction) {
      this.predictionService.deletePrediction(prediction.id)
      .subscribe(r => {
        this.predictions.splice(this.predictions.indexOf(prediction), 1);
        this.toastr.success('Prediction removed');
      });
      event.stopPropagation();
  }

  error(err) {
      this.toastr.error('Error');
      return err;
  }

  onScroll() {
    // this.toastr.warning('Scroll');
    if (!this.predictions) {
      return;
    }

    this.loading = true;

    this.predictionService.getPredictions(this.page, this.userPredictions, this.searchLocation)
      .subscribe(predictions => { this.predictions =  this.predictions.concat(predictions); ++this.page; this.loading = false; } );

  }

  setUserPred(type: boolean) {
    this.userPredictions = type;
    this.searchLocation = null;
    this.refreshCount(this.searchLocation);
    this.getLocations();
  }

  search() {
    this.refreshCount(this.searchLocation);
    this.getLocations(this.searchLocation);
  }

}

