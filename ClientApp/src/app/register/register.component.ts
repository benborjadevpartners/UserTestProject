import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute  } from '@angular/router';
import { User } from '../_models';
import { UserService } from '../_services/index';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Input() userId?: number;
  private sub: any;
  model: any = {};
  loading = false;
  invalidUsername = false;

  constructor(private router: Router,
    private userService: UserService,
    private route: ActivatedRoute  ) {

  }

  ngOnInit() {

    this.sub = this.route.params.subscribe(params => {
      this.userId = +params['id']; // (+) converts string 'id' to a number
      
      if (this.userId) {
        this.loadUser();
        this.loading = false;
      }
    });

    
  }

  register() {
    this.loading = true;

    if (this.userId > 0) {
      this.editUser();
    } else {
      this.createUser();
    }

    this.loading = false;
  }

  loadUser() {

    this.loading = true;
    this.userService.getById(this.userId)
      .subscribe(
        data => {
          this.model = data;
        },
        error => {
          this.loading = false;
        });
  }

  createUser() {
    this.userService.create(this.model)
      .subscribe(
        data => {
          this.model = data;
          if (this.model.id === 0) {
            this.invalidUsername = true;
          } else {
            this.router.navigate(['/login']);
          }
          
        },
        error => {
          this.invalidUsername = true;
        });
  }

  editUser() {
    this.userService.update(this.model)
      .subscribe(
        data => {
          this.router.navigate(['/']);
        },
        error => {
          this.loading = false;
        });
  }
}


