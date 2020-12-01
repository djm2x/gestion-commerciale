import { Component, OnInit } from '@angular/core';
import { TabService } from './tab.service';
import { EcommerceService } from './ecommerce.service';
import { FormControl } from '@angular/forms';
import { UowService } from 'src/app/services/uow.service';

@Component({
  selector: 'app-ecommerce',
  templateUrl: './ecommerce.component.html',
  styleUrls: ['./ecommerce.component.scss']
})
export class EcommerceComponent implements OnInit {

  constructor(public tabService: TabService, public service: EcommerceService
    , private uow: UowService) { }

  ngOnInit() {
  }

}
