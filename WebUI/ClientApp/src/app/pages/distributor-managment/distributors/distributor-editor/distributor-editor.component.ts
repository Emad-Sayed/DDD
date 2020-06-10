import { Component, OnInit, Input } from '@angular/core';
import { DistributorUser } from 'src/app/shared/models/distributor-managment/distributor-user.model';
import { Distributor } from 'src/app/shared/models/distributor-managment/distributor.model';
import { DistributorsManagmentService } from '../../distributors-managment.service';
import { CoreService } from 'src/app/shared/services/core.service';

@Component({
  selector: 'app-distributor-editor',
  templateUrl: './distributor-editor.component.html',
  styleUrls: ['./distributor-editor.component.scss']
})
export class DistributorEditorComponent implements OnInit {

  @Input('isEditorOpen') isEditorOpen: boolean;

  isEditing = false;
  distributor: Distributor = new Distributor();

  constructor(
    private distributorCatalogService: DistributorsManagmentService,
    private core: CoreService) { }

  ngOnInit() {
    this.distributorCatalogService.distributorEditor.subscribe(res => {
      if (res.distributor) {
        this.isEditing = true;
        this.getDistributorById(res.distributor.id);
      } else {
        this.distributor = new Distributor();
      }
    })
  }

  getDistributorById(distributorId: string) {
    this.distributorCatalogService.getDistributorById(distributorId).subscribe(res => {
      this.distributor = res;
      this.distributor.id = distributorId;
    });
  }

  openEditor() {
    this.distributor = new Distributor();
    this.distributorCatalogService.distributorEditor.next({ openEditor: true });
  }

  openClose() {
    this.distributorCatalogService.distributorEditor.next({ openEditor: false });
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
    this.distributorCatalogService.deleteDistributorUser(distributorUser, this.distributor.id).subscribe(res => {
      this.distributor.distributorUsers.splice(this.distributor.distributorUsers.indexOf(distributorUser), 1);
    });
  }

  createDistributorUser(distributorUser: DistributorUser) {
    distributorUser.distributorId = this.distributor.id;
    this.distributorCatalogService.createDistributorUser(distributorUser, this.distributor.id).subscribe(res => {
      this.core.showSuccessOperation();
    });
  }

  updateDistributorUser(distributorUser: DistributorUser) {
    this.distributorCatalogService.updateDistributorUser(distributorUser, this.distributor.id).subscribe(res => {
      this.core.showSuccessOperation();
    });
  }

  //#endregion

  //#region Distributor
  createDistributor() {
    this.distributorCatalogService.createDistributor(this.distributor).subscribe(res => {
      this.distributorCatalogService.distributorEditor.next({ distributorRequestSuccess: true });
      this.core.showSuccessOperation();
    });
  }

  updateDistributor() {
    this.distributorCatalogService.updateDistributor(this.distributor).subscribe(res => {
      this.distributorCatalogService.distributorEditor.next({ distributorRequestSuccess: true });
      this.core.showSuccessOperation();
    });
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
