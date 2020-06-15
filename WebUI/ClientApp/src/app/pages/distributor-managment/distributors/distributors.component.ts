import { Component, OnInit, OnDestroy } from '@angular/core';
import { CoreService } from 'src/app/shared/services/core.service';
import { DistributorsManagmentService } from '../distributors-managment.service';
import { Distributor } from 'src/app/shared/models/distributor-managment/distributor.model';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Page } from 'src/app/shared/models/shared/page.model';

@Component({
  selector: 'app-distributors',
  templateUrl: './distributors.component.html',
  styleUrls: ['./distributors.component.scss']
})
export class DistributorsComponent implements OnInit, OnDestroy {

  private subject: Subject<string> = new Subject();

  distributors: Distributor[] = [];
  distributorsTotalCount: number = 0;

  page: Page = new Page();

  openEditor = true;
  query: any = {};

  constructor(
    private distributorManagmentService: DistributorsManagmentService,
    private core: CoreService) {
  }

  ngOnDestroy(): void {
    this.distributorManagmentService.distributorEditor.next({ openEditor: false });
  }

  ngOnInit() {
    this.getDistributors();
    this.distributorManagmentService.distributorEditor.subscribe(res => {
      this.openEditor = res.openEditor;
      if (res.distributorRequestSuccess)
        this.getDistributors();
    });

    this.subject.pipe(
      debounceTime(500),
      distinctUntilChanged()
    ).subscribe(res => {
      this.searchInDistributors(res);
    });
  }

  getDistributors() {
    this.query.pageNumber = this.page.pageNumber;
    this.query.pageSize = this.page.pageSize;
    this.distributorManagmentService.getDistributors(this.query).subscribe(res => {
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

  searchInDistributors(value: any) {
    this.distributors = [];
    this.query.keyWord = value;
    this.page.pageNumber = 1;
    this.getDistributors();
  }

  onKeyUp(searchTextValue: string) {
    this.subject.next(searchTextValue);
  }

  onScroll() {
    this.page.pageNumber++;
    if ((this.page.pageNumber * this.page.pageSize) >= this.distributorsTotalCount) return;
    this.getDistributors();
  }
}
