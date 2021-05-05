import { Injectable} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../_models/index';

@Injectable()
export class UserService {
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<User[]>('/users/get-all');    
  }

  getById(id: number) {
    return this.http.get('/users/get-user/' + id);
  }

  create(user: User) {
    return this.http.post('/users/add-user', user);
  }

  update(user: User) {
    return this.http.put('/users/edit-user/' + user.id, user);
  }

  delete(id: number) {
    return this.http.delete('/users/delete-user/' + id);
  }
}
