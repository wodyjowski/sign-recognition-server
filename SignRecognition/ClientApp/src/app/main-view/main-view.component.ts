import { ElementRef, NgZone, OnInit, ViewChild, Component  } from '@angular/core';
import { MapsAPILoader } from '@agm/core';


import * as SunCalc from 'suncalc';
import { ToastrService } from 'ngx-toastr';

import { LocationService } from '../location-service/location.service';
import { ILocation } from '../location-service/location';

import { ActivatedRoute } from '@angular/router';
import { Input } from '@angular/compiler/src/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-main-view',
  templateUrl: './main-view.component.html',
  providers: [ LocationService ],
  styleUrls: ['./main-view.component.css']
})
export class MainViewComponent implements OnInit {

  protected map;

  palce: google.maps.places.PlaceResult;
  lat = 0;
  lng = 0;
  zoom = 2;
  times;
  saveButton = false;
  fromParams = false;
  foundLocation = false;

  scalc: SunCalc;

  @ViewChild('search')
  public searchElementRef: ElementRef;

  constructor(private mapsAPILoader: MapsAPILoader,
    private ngZone: NgZone, private toastr: ToastrService,
    private locationService: LocationService,
    private route: ActivatedRoute) {
      this.times = SunCalc.getTimes(Date.now(), this.lat, this.lng);
     }

  ngOnInit() {
    this.route.queryParams
    .subscribe(params => {
      console.log(params);
      const lat = Number(params.lat);
      const lng = Number(params.lng);
      if (!Number.isNaN(lat) && !Number.isNaN(lng)) {
        this.lat = lat;
        this.lng = lng;
        this.fromParams = true;
        this.showPosition(this.lat, this.lng);
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
      this.getLocation();
    }
  }

  mapReady(map) {
    this.map = map;
  }

  showPosition(lat, lon) {

    this.foundLocation = true;

    this.lat = lat;
    this.lng = lon;
    this.zoom = 12;

    window.console.log(this.times);

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

  saveLocation() {
    const location = new Location(this.palce.name, this.palce.id, this.lat, this.lng);

    this.locationService.saveLocation(location).subscribe( response => {
      if (response != null) {
        console.log(response);
        if (response.status === 200) {
          this.toastr.success('Location saved');
        } else if (response.status === 202) {
          this.toastr.info('Location already saved');
        }

      }
    });

  }

}

class Location implements ILocation {


  constructor(name: string, id: string, lat: number, lng: number) {
    this.name = name;
    this.locationId = id;
    this.latitude = lat;
    this.longitude = lng;
  }

  id: string;
  name: string;
  locationId: string;
  latitude: number;
  longitude: number;
  date: Date;
}
