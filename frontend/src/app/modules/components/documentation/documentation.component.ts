import { Validators } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { DocumentationServiceService } from 'app/modules/services/documentation-service.service';

@Component({
  selector: 'app-documentation',
  templateUrl: './documentation.component.html',
  styleUrls: ['./documentation.component.scss']
})
export class DocumentationComponent implements OnInit {

  constructor(private documentationService: DocumentationServiceService, private formBuilder: FormBuilder) { }

  documents: any[];
  create:boolean = false;
  file: any;

  selectedFiles: FileList;
  currentFileUpload: File
  
  uploadForm: FormGroup;

  documentationSearchName: string;

  ngOnInit() {
    this.documentationService.getAllDocs()
      .subscribe((data: any[]) => {
        this.documents = data;
      });

      this.uploadForm = this.formBuilder.group({
        name: ["", [Validators.required]],
        file: ["", [Validators.required]]
      });
  }

  like(id){
    this.documentationService.likeDoc(id);
  }

  createDocument(){
    this.create = !this.create;
  }

  upload(event: any){
      this.currentFileUpload = this.selectedFiles.item(0);
      const name = this.uploadForm.get('name').value;
      this.documentationService.uploadDoc(this.currentFileUpload, name);
  }

  onFileSelect(event) {
    this.selectedFiles = event.target.files;
  }
}
