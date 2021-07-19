import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Gateway } from '../models/gateway';
import CustomStore from 'devextreme/data/custom_store';
import { GatewaysService } from '../services/gateways.service';
import { tap } from 'rxjs/operators'
import { Status } from '../models/peripheral-device';
import notify from "devextreme/ui/notify";



@Component({
  selector: 'app-gateways-list',
  templateUrl: './gateways-list.component.html',
  styleUrls: ['./gateways-list.component.css']
})
export class GatewaysListComponent implements OnInit {

  gatewaysDataSource: CustomStore;
  devicesDataSource: CustomStore[] = [];
  master: any;
  detail: any;

  statuses = [
    { id: Status.Online, value: 'Online' },
    { id: Status.Offline, value: 'Offline' }
  ];

  constructor(private gatewaysService: GatewaysService) {
    this.gatewaysDataSource = new CustomStore({
      load: () => this.gatewaysService.getGateways().pipe(tap(item => { this.fillDevices(item.gateways) })).toPromise().then(response => {
        return {
          data: response.gateways,
          totalCount: response.totalCount
        };
      }),
      remove: async (id) => this.gatewaysService.removeGateway(id),
      update: (key, data) =>
      this.gatewaysService.updateGateway(this.master).toPromise(),
      insert: (data) => this.gatewaysService.addGateway(data).toPromise().catch(error => this.handlerError(error)),
      key: 'id',
    });
  }

  fillDevices(gateways: Gateway[]) {
    gateways.forEach(gateway => {
      this.devicesDataSource[gateway.id] = new CustomStore({
        load: () => this.gatewaysService.getDevices(gateway.id).toPromise(),
        remove: async (id) => this.gatewaysService.removeDevice(id),
        update: (key, data) => { data.id = key; return this.gatewaysService.updateDevice(this.detail).toPromise().catch(error => this.handlerError(error)) },
        insert: (data) => this.gatewaysService.addDevice(gateway.id, data).toPromise().catch(error => this.handlerError(error)),
        key: 'id',
      });
    });

  }

  ngOnInit(): void {
  }

  masterRowUpdating(event){
    Object.keys(event.newData).forEach(item => {
      event.oldData[item] = event.newData[item]
    });
    this.master = event.oldData;
    // console.log(event.oldData);
  }

  detailRowUpdating(event){
    Object.keys(event.newData).forEach(item => {
      event.oldData[item] = event.newData[item]
    });
    this.detail = event.oldData;
  }

  handlerError(error){
    console.log(error.error.errors[Object.keys(error.error.errors)[0]]);
    notify(error.error.errors[Object.keys(error.error.errors)[0]], 'error');
  }

}
