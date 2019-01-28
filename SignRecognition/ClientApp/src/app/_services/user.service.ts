import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserData } from '.';


@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient) { }

    getAll(pageNum: number) {
        return this.http.get<UserListData[]>(`api/User/AllUsers`,
        { params: {
            page: pageNum.toString()
        }});
    }


    getUserById(userId: string) {
        return this.http.get<UserData>(`api/User/` + userId);
    }
}

export interface UserListData {
    id: string;
    userName: string;
    email: string;
    creationDate: Date;
    adminRights: boolean;
}
