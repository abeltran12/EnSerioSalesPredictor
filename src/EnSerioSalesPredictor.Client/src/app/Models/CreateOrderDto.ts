import { CreateOrderDetailsDto } from "./CreateOrderDetailsDto";

export interface CreateOrderDto {
  customerId: number;
  empId: number;
  shipperId: number;
  shipName: string;
  shipAddress: string;
  shipCity: string;
  orderDate: Date;
  requiredDate: string;
  shippedDate: string;
  freight: number;
  shipCountry: string;
  createOrderDetails: CreateOrderDetailsDto;
}