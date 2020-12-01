import { SessionService } from './../../../shared/session.service';
import { Article, Fourniture } from '../../../myModels/models';
import { Component, OnInit, Input, EventEmitter, ViewChild } from '@angular/core';
import { Validators, FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UowService } from 'src/app/services/uow.service';
import { SnackBarService } from 'src/app/loader/snack-bar.service';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class UpdateComponent implements OnInit {
  cat = new FormControl(0);
  //
  fournisseurs = this.uow.fournisseurs.get();
  categories = this.uow.categories.get();
  sousCategories = [{ id: 1, libelle: 'Generale' }];

  myForm: FormGroup;
  myCommandeForm: FormGroup;
  //
  file: File;
  filename = 'Choisie un rapport';
  iconFile = '';
  pieceJointe = '';
  pieceJointeToShow: string[] = [];
  pieceJointeToDelete: string[] = [];
  formData = new FormData();
  oneFile = true;
  //
  qteStock = new FormControl(0);
  o = new Article();
  commande = new Fourniture();
  id = 0;
  qteSelected = 0;
  unites = this.uow.unites.getJson();
  constructor(private route: ActivatedRoute, private snack: SnackBarService
    , private uow: UowService, private fb: FormBuilder, private session: SessionService) { }

  ngOnInit() {

    this.createForm();
    // this.createCommandeForm();
    this.id = +this.route.snapshot.paramMap.get('id');
    if (this.id !== 0) {
      this.uow.articles.getOne(this.id).subscribe(r => {
        this.o = r as any;
        this.qteStock.setValue(r.qteStock);
        console.log(this.o);
        this.cat.patchValue(this.o.sousCategorie.idCategorie);
        this.pieceJointe = this.o.image;
        this.createForm();
        this.myForm.get('idSousCategorie').patchValue(this.o.idSousCategorie)
      });
    }

    // this.myCommandeForm.get('qte').valueChanges.subscribe(r => {
    //   this.qteStock.setValue(r - this.qteSelected);
    // });

    this.myForm.get('titreFr').valueChanges.subscribe(v => {
      this.myForm.get('titreAr').setValue(v);
    })
  }

  createForm() {
    this.myForm = this.fb.group({
      id: this.o.id,
      code: [this.o.code, Validators.required],
      titreFr: [this.o.titreFr, Validators.required],
      titreAr: [this.o.titreAr, Validators.required],
      emplacementMagasin: [this.o.emplacementMagasin, Validators.required],
      emplacementDepot: [this.o.emplacementDepot, Validators.required],
      dateDernierAchat: [this.o.dateDernierAchat, Validators.required],
      prixUnitaire: [this.o.prixUnitaire, Validators.required],
      qteStock: [this.o.qteStock],
      stockMin: [this.o.stockMin],
      image: [this.o.image],
      unite: [this.o.unite],
      constructeur: [this.o.constructeur, Validators.required],
      idSousCategorie: [this.o.idSousCategorie, Validators.required],
    });
  }

  // createCommandeForm() {
  //   this.myCommandeForm = this.fb.group({
  //     id: this.commande.id,
  //     idArcticle: [this.commande.idArcticle, Validators.required],
  //     idFournisseur: [this.commande.idFournisseur, Validators.required],
  //     qte: [this.commande.qte, Validators.required],
  //     prixAchat: [this.commande.prixAchat, Validators.required],
  //     dateAchat: [this.commande.dateAchat, Validators.required],
  //   });
  // }

  submit(o: Article) {
    o.dateDernierAchat = this.valideDate(o.dateDernierAchat);
    // o.qteStock = this.qteStock.value;
    o.image = this.pieceJointe;
    console.log(o);
    // return;
    if (this.id === 0) {
      this.uow.articles.post(o).subscribe((r: any) => {
        if (r.code === 1 ) {
          this.o = r.model;
          if (this.file) {
            const nameFile = `${this.file.lastModified}_${this.file.name}` as string;
            const formData = new FormData();
            formData.append(nameFile, this.file, nameFile);
  
            this.uow.files.uploadFiles(formData, 'Articles').subscribe(rs => {
              this.snack.notifyOk('Article est bien enregitrer');
            });
          } else {
            this.snack.notifyOk('Article est bien enregitrer');
          }
        } else {
          this.snack.notifyAlert('code existe deja');
        }
       
      });
    } else {
      this.uow.articles.put(o.id, o).subscribe(r => {
        // if (c.id !== 0) {
        //   c.idArcticle = o.id;
        //   this.uow.fournitures.post(c).subscribe(s => {
        //     this.router.navigate(['/admin/article/list']);
        //   });
        // } else {
        //   this.router.navigate(['/admin/article/list']);
        // }
        if (this.file) {
          const nameFile = `${this.file.lastModified}_${this.file.name}` as string;
          const formData = new FormData();
          formData.append(nameFile, this.file, nameFile);
          this.uow.files.uploadFiles(formData, 'Articles').subscribe(rs => {
            this.uow.files.deleteFiles(this.pieceJointeToDelete, 'Articles').subscribe(() => {
              this.snack.notifyOk('Article est bien enregitrer');
            });
          });
        } else {
          this.snack.notifyOk('Article est bien enregitrer');
        }
      });
    }
  }

  openInput(o/*: HTMLInputElement*/) {
    // console.log('>>>>>>>>>>>>>');
    o.click();
  }

  selectChange(idCat) {
    this.uow.sousCategories.getByCat(idCat).subscribe(r => {
      console.log(r);
      this.sousCategories = r as any[];
    });
    // if (this.id !== 0) {
    //   this.uow.fournitures.getByIds(this.id, idF).subscribe((r: Fourniture) => {
    //     console.log(r);
    //     if (r !== null) {
    //       this.myCommandeForm.get('qte').setValue(r.qte);
    //       this.myCommandeForm.get('prixAchat').setValue(r.prixAchat);
    //       this.qteSelected = r.qte;
    //     } else {
    //       this.myCommandeForm.get('qte').setValue(0);
    //       this.myCommandeForm.get('prixAchat').setValue(0);
    //       this.qteSelected = 0;
    //     }
    //   });
    // }
  }

  valideDate(date: Date): Date {
    date = new Date(date);
  
    const hoursDiff = date.getHours() - date.getTimezoneOffset() / 60;
    const minutesDiff = (date.getHours() - date.getTimezoneOffset()) % 60;
    date.setHours(hoursDiff);
    date.setMinutes(minutesDiff);
  
    return date;
  }

  upload(files: FileList) {
    // console.log('>>>>>>>>>>>>>');
    // console.log(this.pieceJointe)
    if (this.oneFile) {
      this.file = files.item(0);
      this.pieceJointe = `${this.file.lastModified}_${this.file.name}`;
      this.o.image = this.pieceJointe;
      // console.log(this.pieceJointe)
      // this.formData = new FormData();
      // this.formData.append(nameFile, file, nameFile);
      // this.pieceJointeToShow[0] = `${file.lastModified}_${file.name};`;
      // console.log('>>>>>>>>>>>>>');
      // console.log(file);
    } else {


      Array.from(files).forEach((file: File) => {
        const nameFile = `${file.lastModified}_${file.name}` as string;
        const exist = this.formData.has(`${file.lastModified}_${file.name}`) || this.pieceJointe.includes(nameFile) as boolean;

        if (!exist) {
          this.formData.append(nameFile, file, nameFile);
          this.pieceJointe += `${nameFile};`;
          this.pieceJointeToShow.push(`${file.lastModified}_${file.name};`);
          // console.log('>>>>>>>>>>>>>');
          // console.log(file);
        }


      });
    }
    // console.log(this.pieceJointe);

    // to delete
  }


  remove(nameFile: string) {
    if (this.oneFile) {
      this.pieceJointe = '';
      this.pieceJointeToDelete[0] = nameFile;
    } else {


      // this.pieceJointeToDelete.push(nameFile);
      // this.pieceJointeToShow = this.pieceJointeToShow.filter(e => e !== nameFile);
      // this.formData.delete(nameFile);
      // // this.files = this.files.splice(i, 1);
      // console.log(this.pieceJointe);
      // this.pieceJointe = this.pieceJointe.replace(`${nameFile};`, '');
      // console.log(this.pieceJointe);
      // // this.files = [];
      // // this.filename = 'Choisie un rapport';
      // // this.myForm.get('pieceJointe').setValue('');
      // // this.iconFile = '';
    }
  }
}
