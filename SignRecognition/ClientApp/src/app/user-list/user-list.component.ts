import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../_services/authentication.service';
import { UserService, UserListData } from '../_services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  constructor(private authenticationService: AuthenticationService,
    private userService: UserService,
    private router: Router) { }

  users: UserListData[] = null;
  loading = false;
  page = 1;

  searchMode = false;
  searchUsername: string;

  ngOnInit() {
      this.getUsers(0);
  }

  refresh() {
    this.searchUsername = null;
    this.users = null;
    this.getUsers(0);
  }

  onScroll() {
    if (!this.users) {
      return;
    }

    if (this.searchMode) {
      this.getUsers(++this.page, this.searchUsername);
    } else {
      this.getUsers(++this.page);
    }
  }


  getUsers(page: number, username: string = '') {
      this.loading = true;
      this.userService.getAll(page, username).subscribe(u => {
        if (this.users) {
          this.users.concat(u);
        } else  if (u) {
          this.users = u;
        }

        this.loading = false;
      });
  }

  navigate(user: UserListData) {
    this.router.navigate(['account'], { queryParams: { id: user.id }});
  }

  search() {
    this.searchMode = true;
    this.users = null;
    this.getUsers(0, this.searchUsername);
  }



}
