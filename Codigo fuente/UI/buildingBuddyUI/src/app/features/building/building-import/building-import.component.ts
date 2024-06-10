import { Component } from '@angular/core';

interface FileData {
  name: string;
  url: string;
  title: string;
}

@Component({
  selector: 'app-building-import',
  templateUrl: './building-import.component.html',
  styleUrls: ['./building-import.component.css']
})
export class BuildingImportComponent {
  fileName: string = "";
  title: string = "";

  constructor() {}

  onFileUploadChange(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      this.fileName = file.name;
      // You can add more logic here to handle the file upload
    }
  }

  uploadFile() {
    if (this.fileName && this.title) {
      // Here you can handle the upload logic for the single file
      console.log('File uploaded:', this.fileName);
      console.log('Title:', this.title);
      // Reset form fields
      this.fileName = '';
      this.title = '';
    }
  }
}
