import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-complete-registration',
  templateUrl: './complete-registration.component.html',
  styleUrls: ['./complete-registration.component.scss']
})
export class CompleteRegistrationComponent implements OnInit {

  isEmailConfirmed = false;

  constructor(private activatedRoute: ActivatedRoute, private authService: AuthService) { }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(res => {
      this.checkMailConfirmtion(res.email, res.token);
    });
  }

  checkMailConfirmtion(email: string, token: string): void {
    this.authService.confirmEmail(email, token).subscribe(res => {
      this.isEmailConfirmed = res.confirmEmail;
    })
  }

}
