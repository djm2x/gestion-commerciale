import { Injectable } from '@angular/core';
import { SuperService } from './super.service';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FileUploadService extends SuperService<any> {

  constructor() {
    super('files');
  }

  postFile(file) {
    return this.http.post(`${this.urlApi}/files/postFile`, file, { reportProgress: true });
  }

  deleteFile(filename, folder) {
    return this.http.post(`${this.urlApi}/files/deleteFile/`, { filename, folder }, { reportProgress: true });
  }

  download(filename) {
    return this.http.get(`${this.url}/Visite/${filename}`);
  }

  deleteFiles(filenames: string[], folder) {
    if (filenames.length === 0) {
      return of(null);
    }
    return this.http.post(`${this.urlApi}/${this.controller}/deleteFiles/`, { filenames, folder }, { reportProgress: true });
  }

  uploadFiles(files: FormData, folder) {
    if (!files) {
      return of(null);
    }
    return this.http.post(`${this.urlApi}/${this.controller}/uploadFiles/${folder}`, files, {
      // headers: {'Content-Type': 'multipart/form-data'},
      reportProgress: true,
    });
  }

}
