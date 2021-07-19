import { PeripheralDevice } from "./peripheral-device";

export interface Gateway {
  id: number;
  serialNumber: string;
  name: string;
  ipAddress: string;
  peripheralDevices: PeripheralDevice[];
}
