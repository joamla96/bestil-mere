import { Component, OnInit } from '@angular/core';
import {LogisticsService} from "../../services/logistics.service";

@Component({
  selector: 'app-logistics',
  templateUrl: './logistics.component.html',
  styleUrls: ['./logistics.component.css']
})
export class LogisticsComponent implements OnInit {

  constructor(
    private logisticsService : LogisticsService
  ) { }

  ngOnInit() {
    this.logisticsService.getAll().subscribe(data => console.log(data));
  }

}
