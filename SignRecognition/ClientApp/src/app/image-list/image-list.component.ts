import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-image-list',
  templateUrl: './image-list.component.html',
  styleUrls: ['./image-list.component.css']
})
export class ImageListComponent implements OnInit {

  constructor(private router: Router,
    private http: HttpClient,
    private toastr: ToastrService) { }


  imageList: Image[] = null;
  allImages = false;

  loading = true;

  ngOnInit() {
    this.getMyImages();
  }

  getMyImages() {
    this.http.get(`/Photo/MyPhotos`).subscribe(
      images => { this.setImages(images);
      this.loading = false;
    });
  }

  getAllImages() {
    this.http.get(`/Photo/AllPhotos`).subscribe(
      images => { this.setImages(images);
        this.loading = false;
      });
  }

  setImages(images) {
    this.imageList = images;
  }

  my() {
    this.loading = true;
    this.imageList = [];
    this.allImages = false;
    this.getMyImages();
  }

  all() {
    this.loading = true;
    this.imageList = [];
    this.allImages = true;
    this.getAllImages();
  }

  reload() {
    this.loading = true;
    if (this.allImages) {
      this.getAllImages();
    } else {
      this.getMyImages();
    }
  }

  remove(image: Image) {
    const id = image.id;
    this.http.post('/Photo/RemoveImage', { id }).subscribe(result => this. success());
    event.stopPropagation();
  }

  success() {
    this.toastr.success('Photo removed');
    this.reload();
  }

  openLocation(image: Image) {
    if (image.latitude && image.longitude) {
      this.router.navigate(['/'], { queryParams: { lat: image.latitude, lng: image.longitude }});
    }
  }


}

class Image {
  id: string;
  name: string;
  latitude: string;
  longitude: string;
  dateTime: Date;
  url: string;
}



