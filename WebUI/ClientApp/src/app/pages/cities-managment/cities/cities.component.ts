import { Component, OnInit } from '@angular/core';
import { Area } from 'src/app/shared/models/distributor-managment/area.model';
import { City } from 'src/app/shared/models/distributor-managment/city.model';

@Component({
  selector: 'app-cities',
  templateUrl: './cities.component.html',
  styleUrls: ['./cities.component.scss']
})
export class CitiesComponent implements OnInit {
  cities: City[] = [
    new City('', 'الشيخ زايد', [
      new Area('', 'القنطرة شرق'),
      new Area('', 'القنطرة غرب'),
      new Area('', 'القنطرة شمال'),
      new Area('', 'القنطرة جنوب'),
    ]),
    // new City('', 'المنصورة', [
    //   new Area('', 'المشاية'),
    //   new Area('', 'شارع البحر'),
    //   new Area('', 'عمر افندي'),
    //   new Area('', 'جزيره الورد'),
    // ])

  ];

  constructor() { }

  ngOnInit() {
  }

}
