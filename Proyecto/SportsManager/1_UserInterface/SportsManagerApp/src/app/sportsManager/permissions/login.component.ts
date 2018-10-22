import { Component, OnInit } from '@angular/core';
import { User } from '../entities/user';
import { USERS } from '../../mock-users';
 
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  users = USERS;
  selectedUser: User;

  constructor() { }
 
  ngOnInit() {
  }

  onSelect(user: User): void {
    this.selectedUser = user;
  }
 
}