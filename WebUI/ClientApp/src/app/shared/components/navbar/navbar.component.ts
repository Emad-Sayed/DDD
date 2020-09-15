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
    const roles = localStorage.getItem('roles').split(',');
    if (roles) this.isAdmin = roles.includes('Admin');
  }
  logout() {
    localStorage.clear();
    this.router.navigate(['/login']);
  }
}
