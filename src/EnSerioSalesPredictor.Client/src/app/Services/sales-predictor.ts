import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { appsettings } from '../Settings/appsettings';
import { SalesPredictionDto } from '../Models/SalesPredictionDto';
import { OrderDto } from '../Models/OrderDto';
import { ShipperDto } from '../Models/ShipperDto';
import { ProductDto } from '../Models/ProductDto';
import { EmployeeDto } from '../Models/EmployeeDto';
import { CreateOrderDto } from '../Models/CreateOrderDto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SalesPredictor {
  private http=inject(HttpClient);
  private apiUrl: string = appsettings.apiUrl;
  private apiUrlSalesPredictions: string = `${this.apiUrl}/salespredictions`;
  private apiUrlShipper: string = `${this.apiUrl}/shippers`;
  private apiUrlProduct: string = `${this.apiUrl}/products`;
  private apiUrlEmployee: string = `${this.apiUrl}/employees`;

  constructor() {};

  salesList(
    pageNumber: number = 1,
    pageSize: number = 10,
    orderBy: string = 'customerName',
    sort: string = 'asc'
  ) {
    const params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString())
      .set('orderBy', orderBy)
      .set('sort', sort);

    return this.http.get<SalesPredictionDto[]>(this.apiUrlSalesPredictions, { params, observe: 'response' });
  }

  getOrdersByEmployeeId(id:number) {
    return this.http.get<OrderDto[]>(`${this.apiUrl}/${id}`);
  }

  getShippers() {
    return this.http.get<ShipperDto[]>(this.apiUrlShipper);
  }

  getProducts() {
    return this.http.get<ProductDto[]>(this.apiUrlProduct);
  }

  getEmployees() {
    return this.http.get<EmployeeDto[]>(this.apiUrlEmployee);
  }

  createOrder(customerId: number, order: CreateOrderDto): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiUrl}/customers/${customerId}/orders`, order);
  }
}
