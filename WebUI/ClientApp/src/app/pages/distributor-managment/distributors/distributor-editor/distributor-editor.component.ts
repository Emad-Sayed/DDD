import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { Component, OnInit, Input, ViewChild, ElementRef } from '@angular/core';
import { DistributorUser } from 'src/app/shared/models/distributor-managment/distributor-user.model';
import { Distributor } from 'src/app/shared/models/distributor-managment/distributor.model';
import { DistributorsManagmentService } from '../../distributors-managment.service';
import { CoreService } from 'src/app/shared/services/core.service';
import { City } from 'src/app/shared/models/distributor-managment/city.model';
import { Area } from 'src/app/shared/models/distributor-managment/area.model';
import { FormControl } from '@angular/forms';
import { MatAutocompleteSelectedEvent, MatAutocomplete } from '@angular/material/autocomplete';
import { MatChipInputEvent } from '@angular/material/chips';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

@Component({
  selector: 'app-distributor-editor',
  templateUrl: './distributor-editor.component.html',
  styleUrls: ['./distributor-editor.component.scss']
})
export class DistributorEditorComponent implements OnInit {

  @Input('isEditorOpen') isEditorOpen: boolean;

  isEditing = false;
  distributor: Distributor = new Distributor();
  cities: City[] = [];
  allAreas: Area[] = [];
  locationsRows = [];

  visible = true;
  selectable = true;
  removable = true;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  fruitCtrl = new FormControl();
  filteredAreas: Observable<Area[]>;
  fruits: Area[] = [];
  // allFruits: string[] = ['Apple', 'Lemon', 'Lime', 'Orange', 'Strawberry'];

  @ViewChild('fruitInput', { static: false }) fruitInput: ElementRef<HTMLInputElement>;
  @ViewChild('auto', { static: false }) matAutocomplete: MatAutocomplete;

  constructor(
    private distributorManagmentService: DistributorsManagmentService,
    private core: CoreService) {
    // this.filteredAreas = this.fruitCtrl.valueChanges.pipe(
    //   startWith(null),
    //   map((fruit: string | null) => fruit ? this._filter(fruit) : this.allAreas.slice()));
  }

  ngOnInit() {
    this.getCities();
    this.distributorManagmentService.distributorEditor.subscribe(res => {
      if (res.distributorRequestSuccess) return;
      if (res.distributor) {
        this.isEditing = true;
        this.getDistributorById(res.distributor.id);
      } else {
        this.isEditing = false;
        this.distributor = new Distributor();
      }
    })
  }

  addNewCityRow() {
    this.locationsRows.push({ cityId: '', areas: [] });
  }
  add(event: MatChipInputEvent, row: any): void {
    const input = event.input;
    const value = event.value as Area;

    // Add our fruit
    if ((value.name || '').trim()) {
      this.fruits.push(value);
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }

    this.fruitCtrl.setValue(null);
  }

  remove(row: any, area: Area): void {
    const index = row.areas.indexOf(area);

    if (index >= 0) {
      row.areas.splice(index, 1);
    }
  }

  selected(event: MatAutocompleteSelectedEvent, row: any): void {
    console.log('select', event.option.value)
    // this.fruits.push(event.option.value);
    if (row.areas.find(x => x.id == event.option.value.id)) return;
    row.areas.push(event.option.value);
    this.fruitInput.nativeElement.value = '';
    this.fruitCtrl.setValue(null);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.allAreas.filter(fruit => fruit.name.toLowerCase().indexOf(filterValue) === 0).map(x => x.name);
  }

  cityChanged(row: any) {
    row.areas = [];
  }

  getDistributorById(distributorId: string) {
    this.distributorManagmentService.getDistributorById(distributorId).subscribe(res => {
      this.distributor = res;
      this.distributor.id = distributorId;
    });
  }


  fillterAreas(cityId: string) {
    const city = this.cities.find(x => x.id == cityId);
    return city == null ? [] : city.areas;
  }

  getCities() {
    this.distributorManagmentService.getCities().subscribe(res => {
      this.cities = res.data;
      this.allAreas = [];
      this.cities.map(x => x.areas.forEach(area => {
        this.allAreas.push(area);
      }));
    })
  }

  fillteredCities(cityName: string) {
    const city = this.cities.find(x => x.name == cityName);
    if (city) return city.areas;
  }

  openEditor() {
    this.distributor = new Distributor();
    this.distributorManagmentService.distributorEditor.next({ openEditor: true });
  }

  openClose() {
    this.distributorManagmentService.distributorEditor.next({ openEditor: false });
  }

  //#region DistributorUsers
  addNewDistributorUser() {
    this.distributor.distributorUsers.push(new DistributorUser(true));
  }

  saveDistributorUser(distributorUser: DistributorUser) {
    if (distributorUser.newAdded) {
      this.createDistributorUser(distributorUser);
    } else {
      this.updateDistributorUser(distributorUser);
    }
  }

  removeDistributorUser(distributorUser: DistributorUser, distributorUserIndex: number) {
    if (distributorUser.newAdded)
      this.distributor.distributorUsers.splice(distributorUserIndex, 1);
    else
      this.deleteDistributorUser(distributorUser);
  }

  deleteDistributorUser(distributorUser: DistributorUser) {
    this.distributorManagmentService.deleteDistributorUser(distributorUser, this.distributor.id).subscribe(res => {
      this.distributor.distributorUsers.splice(this.distributor.distributorUsers.indexOf(distributorUser), 1);
    });
  }

  createDistributorUser(distributorUser: DistributorUser) {
    distributorUser.distributorId = this.distributor.id;
    this.distributorManagmentService.createDistributorUser(distributorUser, this.distributor.id).subscribe(res => {
      this.core.showSuccessOperation();
    });
  }

  updateDistributorUser(distributorUser: DistributorUser) {
    this.distributorManagmentService.updateDistributorUser(distributorUser, this.distributor.id).subscribe(res => {
      this.core.showSuccessOperation();
    });
  }

  //#endregion

  //#region Distributor
  createDistributor() {
    this.distributorManagmentService.createDistributor(this.distributor).subscribe(res => {
      this.distributorManagmentService.distributorEditor.next({ distributorRequestSuccess: true, openEditor: true });
      this.core.showSuccessOperation();
    });
  }

  updateDistributor() {
    this.distributorManagmentService.updateDistributor(this.distributor).subscribe(res => {
      this.distributorManagmentService.distributorEditor.next({ distributorRequestSuccess: true, openEditor: true });
      this.locationsRows = []
      this.getDistributorById(this.distributor.id);
      this.core.showSuccessOperation();
    });
  }

  resendInvitationEmail(email: string) {
    this.distributorManagmentService.resendInvitationEmail(email).subscribe(res => {
      this.core.showSuccessOperation('تم إرسال الدعوة');
    })
  }

  removeDistributorAreas(distAreas: Area[]) {
    this.distributorManagmentService.removeDistributorAreas(this.distributor.id, distAreas.map(x => x.id)).subscribe(res => {
      this.getDistributorById(this.distributor.id);
      this.core.showSuccessOperation();
    })
  }
  saveData() {
    const allAreas = this.locationsRows.map(x => x.areas.map(x => x.id));
    this.distributor.areasIds = [].concat.apply([], allAreas);
    if (this.isEditing) {
      this.updateDistributor();
    } else {
      this.createDistributor();
    }
  }
  //#endregion

}
