
export interface PeripheralDevice {
  id: number;
  uid: number;
  vendor: string
  dateCreated: Date;
  status: Status;
}

export enum Status {
  Online,
  Offline
}
