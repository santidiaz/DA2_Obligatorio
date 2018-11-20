import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import {
  MatButtonModule,
  MatCardModule,
  MatDialogModule,
  MatInputModule,
  MatTableModule,
  MatToolbarModule,
  MatMenuModule,
  MatIconModule,
  MatProgressSpinnerModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatCommonModule,
  MatExpansionModule,
  MatCheckboxModule,
  MatRadioModule,
  MatSelectModule,
  MatListModule,
  MatOptionModule,
  MatGridListModule
} from "@angular/material";

@NgModule({
  imports: [
    CommonModule,
    MatToolbarModule,
    MatButtonModule,
    MatCardModule,
    MatInputModule,
    MatDialogModule,
    MatTableModule,
    MatMenuModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCommonModule,
    MatDialogModule,
    MatCheckboxModule,
    MatRadioModule,
    MatSelectModule,
    MatListModule,
    MatOptionModule,
    MatGridListModule,
    MatExpansionModule
  ],
  exports: [
    CommonModule,
    MatToolbarModule,
    MatButtonModule,
    MatCardModule,
    MatInputModule,
    MatDialogModule,
    MatTableModule,
    MatMenuModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCommonModule,
    MatCheckboxModule,
    MatRadioModule,
    MatSelectModule,
    MatListModule,
    MatOptionModule,
    MatGridListModule,
    MatExpansionModule
  ]
})
export class CustomMaterialModule {}