import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Gateway } from '../models/gateway';
import { GatewayResponse } from '../models/gateway-response';
import { PeripheralDevice } from '../models/peripheral-device';

@Injectable({
  providedIn: 'root'
})
export class GatewaysService {


  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getGateways() {
    return this.httpClient.post<GatewayResponse>(this.baseUrl + 'api/gateway/list', {"pageSize" : 20,
    "page" : 1,
    "orderBy": "",
    "order" : ""});
  }

  getGateway(id: number) {
    return this.httpClient.get<Gateway>(this.baseUrl + 'api/gateway/' + id);
  }

  addGateway(data: Gateway) {
    return this.httpClient.post<Gateway>(this.baseUrl + 'api/gateway/add', data);
  }
  updateGateway(data: Gateway) {
    return this.httpClient.put<Gateway>(this.baseUrl + 'api/gateway/update', data);
  }
  removeGateway(id: number) {
    this.httpClient.delete(this.baseUrl + 'api/gateway/delete/' + id).toPromise();
  }

  getDevices(gatewayId: number){
    return this.httpClient.get<PeripheralDevice[]>(this.baseUrl + 'api/peripheralDevice/list?gatewayId=' + gatewayId)
  }

  addDevice(gatewayId: number, data: PeripheralDevice) {
    return this.httpClient.post<PeripheralDevice>(this.baseUrl + 'api/peripheralDevice/add?gatewayId=' + gatewayId, data);
  }

  updateDevice(data: PeripheralDevice) {
    return this.httpClient.put<PeripheralDevice>(this.baseUrl + 'api/peripheralDevice/update', data);
  }

  removeDevice(id: number) {
    this.httpClient.delete(this.baseUrl + 'api/peripheralDevice/delete/' + id).toPromise();
  }
}
