import { Component, OnInit, Input } from '@angular/core';
import { DistributorUser } from 'src/app/shared/models/distributor-managment/distributor-user.model';
import { Distributor } from 'src/app/shared/models/distributor-managment/distributor.model';
import { DistributorsManagmentService } from '../../distributors-managment.service';
import { CoreService } from 'src/app/shared/services/core.service';
import { City } from 'src/app/shared/models/distributor-managment/city.model';
import { Area } from 'src/app/shared/models/distributor-managment/area.model';

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
  areaToDisplay: Area[] = [];
  constructor(
    private distributorManagmentService: DistributorsManagmentService,
    private core: CoreService) { }

  ngOnInit() {
    this.getCities();
    this.distributorManagmentService.distributorEditor.subscribe(res => {
      if(res.distributorRequestSuccess) return;
      if (res.distributor) {
        this.isEditing = true;
        this.getDistributorById(res.distributor.id);
      } else {
        this.isEditing = false;
        this.distributor = new Distributor();
      }
    })
  }

  getDistributorById(distributorId: string) {
    this.distributorManagmentService.getDistributorById(distributorId).subscribe(res => {
      this.distributor = res;
      console.log(this.distributor);
      this.distributor.id = distributorId;
    });
  }

  changeCurrentDisCity(cityName: string) {
    console.log(cityName)
    this.areaToDisplay = this.cities.find(x => x.name == cityName).areas;
  }

  getCities() {
    this.distributorManagmentService.getCities().subscribe(res => {
      this.cities = res.data;
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
      this.core.showSuccessOperation();
    });
  }

  resendInvitationEmail(email: string) {
    this.distributorManagmentService.resendInvitationEmail(email).subscribe(res => {
      console.log('invitaiton sent');
    })
  }
  saveData() {
    if (this.isEditing) {
      this.updateDistributor();
    } else {
      this.createDistributor();
    }
  }
  //#endregion

}
