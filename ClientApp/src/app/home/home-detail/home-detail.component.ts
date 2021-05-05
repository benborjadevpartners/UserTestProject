import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../../_services/index';

@Component({
  selector: 'app-home-detail',
  templateUrl: './home-detail.component.html',
  styleUrls: ['./home-detail.component.css']
})
export class HomeDetailComponent implements OnInit {

  @Input() userId: number;
  private sub: any;
  model: any = {};

  constructor(private router: Router,
    private userService: UserService,
    private route: ActivatedRoute) {
  }

  ngOnInit() {

    this.sub = this.route.params.subscribe(params => {
      this.userId = +params['id']; // (+) converts string 'id' to a number

      if (this.userId) {
        this.loadUser();
      }

    });
  }

  loadUser() {
    
    this.userService.getById(this.userId)
      .subscribe(
        data => {
          this.model = data;
        });
  }
}
