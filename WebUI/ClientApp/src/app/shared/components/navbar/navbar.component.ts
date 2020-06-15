import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  isAdmin = false;
  constructor(private router: Router) { }

  ngOnInit() {
    const roles = JSON.parse(localStorage.getItem('id_token_claims_obj')).role as string[];
    if (roles) this.isAdmin = roles.includes('Admin');
  }
  logout() {
    localStorage.clear();
    this.router.navigate(['/login']);
  }
}
