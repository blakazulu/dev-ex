import {Component} from '@angular/core';
import {MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'app-report-dialog',
  templateUrl: './report-dialog.component.html',
  styleUrls: ['./report-dialog.component.scss']
})
export class ReportDialogComponent {
  textValue = '';

  constructor(private dialogRef: MatDialogRef<ReportDialogComponent>) {
  }

  saveReport() {
    this.dialogRef.close(this.textValue);
  }
}
