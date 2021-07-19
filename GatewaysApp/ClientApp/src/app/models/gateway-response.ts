import { Gateway } from "./gateway";

export interface GatewayResponse {
  totalCount: number;
  gateways: Gateway[];
}
