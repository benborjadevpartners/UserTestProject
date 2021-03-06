import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../_services/index';

@Component({
  
  templateUrl: 'login.component.html'
})

export class LoginComponent implements OnInit {
  model: any = {};
  loading = false;
  returnUrl: string;
  message: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService
    ) { }

  ngOnInit() {
    // reset login status
    this.authenticationService.logout();

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  tryLogin() {
    this.loading = true;
    
    this.authenticationService.login(this.model.username, this.model.password)
      .subscribe(
        data => {
          // login successful so redirect to return url
          this.router.navigateByUrl(this.returnUrl);
        },
        error => {                    
          this.loading = false;
          this.message = "Invalid login.";
        });
  }
}
