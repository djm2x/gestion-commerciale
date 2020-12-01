import { Client } from './../../../myModels/models';
import { UowService } from 'src/app/services/uow.service';
import { Pannier } from './../global.service';
import { Component, OnInit } from '@angular/core';
import { GlobalService } from '../global.service';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { Observable, merge } from 'rxjs';
import { switchMap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { MatAutocompleteSelectedEvent } from '@angular/material';

@Component({
  selector: 'app-pannier',
  templateUrl: './pannier.component.html',
  styleUrls: ['./pannier.component.scss']
})
export class PannierComponent implements OnInit {

  myAuto = new FormControl('');
  filteredOptions: Observable<any>;
  invalid = false;
  showClientDetails = false;
  o = new Client();
  myForm: FormGroup;
  constructor(public service: GlobalService, private uow: UowService
    , private fb: FormBuilder) { }

  ngOnInit() {
    // this.myAuto.valueChanges.subscribe(r => {
    //   this.service.client = r;
    // });

    this.autoComplete();
    this.createForm();

    merge(...[ this.myAuto.valueChanges, this.tel.valueChanges, this.ice.valueChanges]).subscribe((r) => {
      // console.log(this.myAuto.value)
      this.service.client.nom = this.myAuto.value;
      this.service.client.tel = this.tel.value;
      this.service.client.ice = this.ice.value;

      // console.log(this.service.client);

    });
  }

  autoComplete() {
    this.filteredOptions = this.myAuto.valueChanges.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap((value: string) => value.length > 1 ? this.uow.clients.autoCompleteClient(value) : []),
    );
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.o = event.option.value as any;
    this.myAuto.setValue(this.o.nom);
    // this.service.client = this.o;
    // console.log(this.service.client);
    this.createForm();
    this.showClientDetails = true;
    // this.idOrganismeEmetteur.setValue(o.id);
  }

  createForm() {
    this.myForm = this.fb.group({
      id: this.o.id,
      nom: [this.o.nom, Validators.required],
      tel: [this.o.tel],
      ice: [this.o.ice],
    });
  }

  // get nom() { return this.myForm.get('nom'); }
  get tel() { return this.myForm.get('tel'); }
  get ice() { return this.myForm.get('ice'); }

  delete(o: Pannier) {
    const i = this.service.panniers.findIndex(e => e.id === o.id);
    this.service.panniers.splice(i, 1);
    this.service.globalTotal -= o.prixTotal;
  }

  edit(i: number, o: Pannier, prixVente: number, qtePrise: number, remise: number) {
    const pu = this.service.panniers[i].prixUnitaire;
    if (prixVente < (pu * 0.9)) {
      this.invalid = true;
      return;
    }

    this.invalid = false;
    this.service.panniers[i].prixVente = prixVente;
    this.service.panniers[i].qtePrise = qtePrise;
    this.service.panniers[i].remise = remise;


    this.service.globalTotal -= this.service.panniers[i].prixTotal;
    // const s = (+prixVente * +qtePrise);
    this.service.panniers[i].prixTotal = (+prixVente * +qtePrise) * (1 - remise / 100);

    this.service.globalTotal += this.service.panniers[i].prixTotal;
  }



}
