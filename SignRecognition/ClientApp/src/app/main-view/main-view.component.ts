import { ElementRef, NgZone, OnInit, ViewChild, Component  } from '@angular/core';
import { MapsAPILoader } from '@agm/core';


import * as SunCalc from 'suncalc';
import { ToastrService } from 'ngx-toastr';

import { PredictionService } from '../_services/prediction.service';
import { IPrediction } from '../_models/prediction';

import { ActivatedRoute } from '@angular/router';
import { Input } from '@angular/compiler/src/core';
import { FormControl } from '@angular/forms';
import { LocationService, FinalLocation } from '../_services/location.service';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-main-view',
  templateUrl: './main-view.component.html',
  providers: [ PredictionService, LocationService ],
  styleUrls: ['./main-view.component.css']
})
export class MainViewComponent implements OnInit {

  protected map;

  palce: google.maps.places.PlaceResult;
  lat = 0;
  lng = 0;
  zoom = 2;
  saveButton = false;
  fromParams = false;
  foundLocation = false;
  id = null;

  scalc: SunCalc;

  predictions = [];
  locations = [];

  isAdmin = true;

  @ViewChild('search')
  public searchElementRef: ElementRef;

  constructor(private mapsAPILoader: MapsAPILoader,
    private ngZone: NgZone, private toastr: ToastrService,
    private predictionService: PredictionService,
    private route: ActivatedRoute,
    private locationService: LocationService) {}

  ngOnInit() {

    this.isAdmin = JSON.parse(localStorage.getItem('isAdmin'));

    this.route.queryParams
    .subscribe(params => {
      // console.log(params);
      const id = params.id;
      if (id) {
        this.id = id;
        this.fromParams = true;
        this.loadPrediction(id);
      }
    });


    // load Places Autocomplete
    this.mapsAPILoader.load().then(() => {
      const autocomplete = new google.maps.places.Autocomplete(this.searchElementRef.nativeElement);
      autocomplete.addListener('place_changed', () => {
        this.ngZone.run(() => {
          // get the place result
          const place: google.maps.places.PlaceResult = autocomplete.getPlace();

          // verify result
          if (place.geometry === undefined || place.geometry === null) {
            return;
          }

          // set latitude, longitude and zoom
          this.showPosition(place.geometry.location.lat(), place.geometry.location.lng());

          const user = JSON.parse(localStorage.getItem('currentUser'));
          if (user) {
            this.saveButton = true;
            this.palce = place;
          }
        });
      });
    });
    if (!this.fromParams) {
      // get predictions
      this.locaAllLocations();
    }
  }

  // loadAllPredictions() {
  //    this.predictionService.getPredictions().subscribe(pred => this.predictions = pred);
  // }

  locaAllLocations() {
    this.locations = [];
    this.locationService.getLocations().subscribe(l => this.locations = l);
  }

  loadPrediction(id: string) {
    this.predictionService.getPrediction(id).subscribe(p => {
      if (p) {
        this.lat = p.latitude;
        this.lng = p.longitude;
        this.showPosition(this.lat, this.lng);
        this.locations = [p];
      }
    });
  }

  mapReady(map) {
    this.map = map;
    if (!this.fromParams) {
      this.getLocation();
    }
  }

  showPosition(lat, lon) {

    this.foundLocation = true;

    this.lat = lat;
    this.lng = lon;
    this.zoom = 12;
  }

  getLocation() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.showPosition(position.coords.latitude, position.coords.longitude);
        this.map.setCenter({ lat: this.lat, lng: this.lng });
        this.searchElementRef.nativeElement.value = '';
      });
    }
  }


  refresh() {
    this.predictions = [];
    this.locaAllLocations();
  }


  removeLocation(location: FinalLocation) {
    this.locationService.removeLocation(location.id).pipe(first())
    .subscribe(
      data => {
          this.toastr.success('Location Removed');
          this.locaAllLocations();
            },
      error => {
              this.toastr.error('Location removal error');
            });
  }

}

class Prediction implements IPrediction {

  constructor(name: string, id: string, lat: number, lng: number) {
    this.latitude = lat;
    this.longitude = lng;
  }

  id: string;
  creationDate: Date;
  user: string;
  class: string;
  latitude: number;
  longitude: number;
}
