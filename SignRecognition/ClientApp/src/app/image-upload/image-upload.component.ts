import {  ElementRef, NgZone, OnInit, ViewChild, Component } from '@angular/core';


import { MapsAPILoader } from '@agm/core';
import { ToastrService } from 'ngx-toastr';

import * as EXIF from 'exif-js/exif';
import { DatePipe } from '@angular/common';

import * as moment from 'moment';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';


@Component({
  selector: 'app-image-upload',
  templateUrl: './image-upload.component.html',
  providers: [DatePipe],
  styleUrls: ['./image-upload.component.css']
})


export class ImageUploadComponent implements OnInit {

  @ViewChild('search')
  public searchElementRef: ElementRef;

  selectedFile = null;
  url = '';
  lat = null;
  lng = null;
  zoom = 2;
  loading = false;
  foundLocation = false;
  dateTime = null;
  imageLoaded = false;

  imageFormGroup: FormGroup;
  submitted = false;


  // convenience getter for easy access to form fields
  get f() { return this.imageFormGroup.controls; }

  constructor(private mapsAPILoader: MapsAPILoader,
    private ngZone: NgZone, private toastr: ToastrService,
    private formBuilder: FormBuilder,
    private http: HttpClient) { }

  ngOnInit() {
    this.imageFormGroup = this.formBuilder.group({
      name: ['', Validators.compose([Validators.required, Validators.minLength(3)])],
      date: ''
  });
  }

  onFileSelected(event) {
    window.console.log(event.target.files[0]);
    this.selectedFile = event.target.files[0];

    if (event.target.files && event.target.files[0]) {
      const reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]); // read file as data url
      reader.onload = (evv) => { // called once readAsDataURL is completed
        this.url = (<any>evv.target).result;
      };

      const exifReader = new FileReader();
      exifReader.readAsArrayBuffer(event.target.files[0]);
      exifReader.onload = (evv) => {
        this.getExif(exifReader.result);
      };

    }
  }

  getExif( image ) {
    const data = EXIF.readFromBinaryFile(image);
    window.console.log(data);


    if (data) {
    this.dateTime = moment(data.DateTimeOriginal, 'YYYY-MM-DDTHH:mm').format('YYYY-MM-DDTHH:mm');
    if (data.GPSLatitude && data.GPSLongitude) {
      this.lat = this.toDecimal(data.GPSLatitude);
      this.lng = this.toDecimal(data.GPSLongitude);
      window.console.log(this.lat);
      window.console.log(this.lng);
      this.foundLocation = true;
      this.zoom = 12;
      }

    } else {
      window.console.log('No EXIF data');
    }
    this.imageFormGroup.patchValue({
        date: this.dateTime
    });
    this.imageLoaded = true;
    this.initMap();
  }

  toDecimal(number) {
    return number[0].numerator + number[1].numerator /
        (60 * number[1].denominator) + number[2].numerator / (3600 * number[2].denominator);
  }

  showPosition(lat, lon) {
    this.foundLocation = true;

    this.lat = lat;
    this.lng = lon;
    this.zoom = 12;
  }

  initMap() {
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
        });
      });
    });
  }


  onSubmit() {
    this.submitted = true;

    if (this.imageFormGroup.invalid) {
      return;
  }

  this.loading = true;

  const name = this.f.name.value;
  const date = this.f.date.value;
  const image = this.selectedFile;

  let lat = null;
  let lng = null;

  if (this.lat && this.lng) {
    lat = this.lat.toString();
    lng = this.lng.toString();
  }
    const fd = new FormData();
    fd.append('name', this.f.name.value);
    fd.append('date', this.f.date.value);
    fd.append('image', this.selectedFile);

    if (this.lat && this.lng) {
      fd.append('lat', lat);
      fd.append('lng', lng);
    }

    this.http.post<any>(`/Photo/Upload`, fd).pipe(
      catchError(err => this.handleError(err)))
      .subscribe(
    Response => this.validateResponse(Response));

  }

  handleError(err) {
    this.toastr.error('Error');
    return err;
  }

  validateResponse(Response: any) {
    if (Response) {
      this.toastr.success('Image uploaded');
    } else {
      this.toastr.error('Error');
    }

    this.foundLocation = false;
    this.loading = false;
    this.submitted = false;
    this.imageLoaded = false;
    this.lat = null;
    this.lng = null;
    this.imageFormGroup.patchValue({
      date: '',
      name: '',
      image: ''
  });
  this.searchElementRef.nativeElement.value = '';
  }

}
