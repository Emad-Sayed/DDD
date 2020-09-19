import { Component, OnInit } from '@angular/core';
import { Area } from 'src/app/shared/models/distributor-managment/area.model';
import { City } from 'src/app/shared/models/distributor-managment/city.model';
import { Page } from 'src/app/shared/models/shared/page.model';
import { CoreService } from 'src/app/shared/services/core.service';
import { CitiesManagmentService } from '../cities-managment.service';
import { PopupServiceService } from 'src/app/shared/services/popup-service.service';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-cities',
  templateUrl: './cities.component.html',
  styleUrls: ['./cities.component.scss']
})
export class CitiesComponent implements OnInit {

  private subject: Subject<string> = new Subject();
  cities: City[] = [];
  query: any = {};
  page: Page = new Page();
  citiesTotalCount = 0;

  constructor(
    private citiesManagment: CitiesManagmentService,
    private core: CoreService,
    private popupService: PopupServiceService) { }

  ngOnInit() {
    this.getAllCities();

    this.subject.pipe(
      debounceTime(500),
      distinctUntilChanged()
    ).subscribe(res => {
      this.searchInCities(res);
    });
  }

  //#region City
  getAllCities() {
    this.query.pageNumber = ++this.page.pageNumber;
    this.query.pageSize = this.page.pageSize;
    this.citiesManagment.getCitys(this.query).subscribe(res => {
      this.cities.push(...res.data);
      this.citiesTotalCount = res.totalCount;
    })
  }

  createCity(city: City) {
    this.citiesManagment.createCity(city).subscribe(res => {
      city.isNewAdded = false;
      city.isEditing = false;
      city.id = res.result;
      this.core.showSuccessOperation();
    });
  }

  updateCity(city: City) {
    this.citiesManagment.updateCity(city).subscribe(res => {
      city.isEditing = false;
      this.core.showSuccessOperation();
    });
  }

  deleteCity(cityId: string) {
    this.citiesManagment.deleteCity(cityId).subscribe(res => {
      const city = this.cities.find(x => x.id == cityId);
      this.cities.splice(this.cities.indexOf(city), 1);
      this.core.showSuccessOperation();
    });
  }

  saveCity(city: City) {
    city.cityId = city.id;
    if (city.isNewAdded)
      this.createCity(city);
    else
      this.updateCity(city);
  }
  //#endregion


  //#region Area
  createArea(cityId: string, area: Area) {
    this.citiesManagment.createArea(cityId, area).subscribe(res => {
      area.isNewAdded = false;
      area.isEditing = false;
      area.id = res.result;
      this.core.showSuccessOperation();
    });
  }

  updateArea(cityId: string, area: Area) {
    this.citiesManagment.updateArea(cityId, area).subscribe(res => {
      area.isEditing = false;
      this.core.showSuccessOperation();
    });
  }

  deleteArea(cityId: string, areaId: string) {
    this.citiesManagment.deleteArea(cityId, areaId).subscribe(res => {
      const city = this.cities.find(x => x.id == cityId);
      const area = city.areas.find(x => x.id == areaId);
      city.areas.splice(city.areas.indexOf(area), 1);

      this.core.showSuccessOperation();
    });
  }

  saveArea(cityId: string, area: Area) {
    area.areaId = area.id;
    area.cityId = cityId;
    if (area.isNewAdded)
      this.createArea(cityId, area);
    else
      this.updateArea(cityId, area);
  }
  //#endregion

  addNewAreaRow(city: City, panal: any) {
    city.areas.push(new Area('', '', null, true, true));
    panal.expanded = true;
  }

  addNewCityRow() {
    this.cities.unshift(new City('', '', [], true, true));
  }

  showDeleteCityPopup(city: City): void {
    const dialogRef = this.popupService.deleteElement('حذف المدينة', ' هل انت متاكد؟ سيتم حذف المدينة بالمناطق التي تحتويها', {
      category: '',
      name: city.name
    });
    dialogRef.afterClosed().subscribe(result => {
      if (!result) return;
      this.deleteCity(city.id);
    });
  }

  showDeleteAreaPopup(cityId: string, area: Area): void {
    const dialogRef = this.popupService.deleteElement('حذف المنطقة', ' هل انت متاكد؟ سيتم حذف المنطقة', {
      category: '',
      name: area.name
    });
    dialogRef.afterClosed().subscribe(result => {
      if (!result) return;
      this.deleteArea(cityId, area.id);
    });
  }

  onKeyUp(searchTextValue: string) {
    this.subject.next(searchTextValue);
  }

  onScroll() {
    if ((this.page.pageNumber * this.page.pageSize) >= this.citiesTotalCount) return;
    this.getAllCities();
  }


  searchInCities(value: any) {
    this.cities = [];
    this.query.keyWord = value;
    this.page.pageNumber = 0;
    this.getAllCities();
  }

}
