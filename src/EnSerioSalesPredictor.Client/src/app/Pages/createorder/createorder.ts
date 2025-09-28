import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormsModule, NgForm } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { CreateOrderDto } from '../../Models/CreateOrderDto';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { SalesPredictor } from '../../Services/sales-predictor';
import { ShipperDto } from '../../Models/ShipperDto';
import { ProductDto } from '../../Models/ProductDto';
import { EmployeeDto } from '../../Models/EmployeeDto';

@Component({
  selector: 'app-createorder',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDialogModule,
    MatSelectModule
  ],
  templateUrl: './createorder.html',
  styleUrls: ['./createorder.css']
})
export class Createorder implements OnInit {
  order: CreateOrderDto = {
    customerId: 0,
    empId: 0,
    shipperId: 0,
    shipName: '',
    shipAddress: '',
    shipCity: '',
    orderDate: new Date(),
    requiredDate: '',
    shippedDate: '',
    freight: 0,
    shipCountry: '',
    createOrderDetails: {
      productId: 0,
      unitPrice: 0,
      qty: 0,
      discount: 0
    }
  };

  shippers: ShipperDto[] = [];
  products: ProductDto[] = [];
  employees: EmployeeDto[] = [];

  constructor(
    public dialogRef: MatDialogRef<Createorder>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private shipperService: SalesPredictor
  ) {}

  ngOnInit(): void {
    this.loadShippers();
    this.loadProducts();
    this.loadEmployees();
  }

   loadShippers(): void {
    this.shipperService.getShippers().subscribe({
      next: (result: ShipperDto[]) => this.shippers = result,
      error: (err) => console.error('Error loading shippers', err)
    });
  }

  loadProducts(): void {
    this.shipperService.getProducts().subscribe({
      next: (result: ProductDto[]) => this.products = result, 
      error: (err) => console.error('Error loading products', err)
    });
  }

  loadEmployees(): void {
    this.shipperService.getEmployees().subscribe({
      next: (result: EmployeeDto[]) => this.employees = result, 
      error: (err) => console.error('Error loading employees', err)
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onCreate(form: NgForm): void {
    if (form.invalid) {
      Object.values(form.controls).forEach(c => c.markAsTouched());
      return;
    }
    this.dialogRef.close(this.order);
  }
}
