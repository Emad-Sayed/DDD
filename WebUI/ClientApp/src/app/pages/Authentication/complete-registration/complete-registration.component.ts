import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/shared/services/auth.service';
import { CoreService } from 'src/app/shared/services/core.service';

@Component({
  selector: 'app-complete-registration',
  templateUrl: './complete-registration.component.html',
  styleUrls: ['./complete-registration.component.scss']
})
export class CompleteRegistrationComponent implements OnInit {

  isEmailConfirmed = false;
  userEmail = '';
  passwordVM: any = {};

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private core: CoreService,
    private authService: AuthService) { }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(res => {
      this.userEmail = res.email;
      this.checkMailConfirmtion(res.email, res.token);
    });
  }

  checkMailConfirmtion(email: string, token: string): void {
    this.authService.confirmEmail(email, token).subscribe(res => {
      this.isEmailConfirmed = res.confirmEmail;
    })
  }

  resetPassword(): void {
    this.authService.resetPassword(this.userEmail, this.passwordVM.password).subscribe(res => {
      this.core.showSuccessOperation()
      this.router.navigate(['/login']);
    })
  }

}
