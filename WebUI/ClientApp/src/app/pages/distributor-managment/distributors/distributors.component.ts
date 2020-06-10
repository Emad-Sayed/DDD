import { Component, OnInit } from '@angular/core';
import { CoreService } from 'src/app/shared/services/core.service';
import { DistributorsManagmentService } from '../distributors-managment.service';
import { Distributor } from 'src/app/shared/models/distributor-managment/distributor.model';

@Component({
  selector: 'app-distributors',
  templateUrl: './distributors.component.html',
  styleUrls: ['./distributors.component.scss']
})
export class DistributorsComponent implements OnInit {

  distributors: Distributor[] = [];
  distributorsTotalCount: number = 0;

  openEditor = true;
  state: {
    refresh: Function;
  };
  constructor(
    private distributorManagmentService: DistributorsManagmentService,
    private core: CoreService) {
  }

  ngOnInit() {
    this.getDistributors();
    this.distributorManagmentService.distributorEditor.subscribe(res => {
      this.openEditor = res.openEditor;
      if (res.distributorRequestSuccess)
        this.getDistributors();
    })
  }
  getDistributors() {
    this.distributorManagmentService.getDistributors().subscribe(res => {
      this.distributors = res.data;
      this.distributorsTotalCount = res.totalCount;
    })
  }

  openEditorToUpdateDistributor(distributor: Distributor) {
    this.distributorManagmentService.distributorEditor.next({ openEditor: true, distributor: distributor });
  }

  openEditorToAddDistributor() {
    this.distributorManagmentService.distributorEditor.next({ openEditor: true });
  }

  deleteDistributor(distributorId: string) {
    this.distributorManagmentService.deleteDistributor(distributorId).subscribe(res => {
      this.distributorManagmentService.distributorEditor.next({ distributorRequestSuccess: true });
      this.core.showSuccessOperation();
    })
  }
}
