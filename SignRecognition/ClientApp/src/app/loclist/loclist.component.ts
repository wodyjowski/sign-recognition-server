import { Component, OnInit } from '@angular/core';
import { LocationService } from '../location-service/location.service';
import { ILocation } from '../location-service/location';

import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-loclist',
  templateUrl: './loclist.component.html',
  providers: [ LocationService ],
  styleUrls: ['./loclist.component.css']
})
export class LoclistComponent implements OnInit {

  locations: ILocation[];
  loading = true;

  constructor(private locationService: LocationService,
    private router: Router,
    private http: HttpClient,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.getLocations();
  }

  getLocations() {
    this.locations = [];
    this.loading = true;
    this.locationService.geLocations()
    .subscribe(locations => this.omg(locations));
  }

  omg (locations) {
    this.locations = locations;
    // window.console.log(locations);
    this.loading = false;
  }

  navigate(location: ILocation) {
      this.router.navigate(['/'], { queryParams: { lat: location.latitude, lng: location.longitude }});
  }

  remove (location: ILocation) {
      this.http.post('/Location/Delete', location)
      .pipe(
        catchError(err => this.error(err)))
      .subscribe(() => this.handleResponse(location) );
      event.stopPropagation();
  }

  handleResponse(location) {
      this.locations.splice(this.locations.indexOf(location), 1);
      this.toastr.success('Location removed');
  }

  error(err) {
      this.toastr.error('Error');
      return err;
  }


}

