import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
    imports: [
        FormsModule,
        HttpClientModule
    ],
    declarations: [
    ],
    exports: [
        FormsModule,
        HttpClientModule
    ],
    entryComponents: [
    ]
})
export class SharedModuleModule { }
