import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserData } from '.';
import { Token } from '../account/account.component';


@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient) { }

    getAll(pageNum: number, searchUsername: string) {
        return this.http.get<UserListData[]>(`api/User/AllUsers`,
        { params: {
            page: pageNum.toString(),
            username: searchUsername
        }});
    }


    getUserById(userId: string) {
        return this.http.get<UserData>(`api/User/` + userId);
    }

    deleteUser(userId: string) {
        return this.http.delete('api/User/' + userId);
    }

    grantAdmin(userId: string) {
        return this.http.post('api/User/GrantAdmin/' + userId, null);
    }


    getTokens(userId: string) {
        return  this.http.get<Token[]>(`api/Token/` + userId);
    }

    removeToken(tokenId: string) {
        return  this.http.delete<Token[]>(`api/Token/` + tokenId);
    }
}

export interface UserListData {
    id: string;
    userName: string;
    email: string;
    creationDate: Date;
    adminRights: boolean;
}
