import { Component, inject } from '@angular/core';
import { MatCardModule} from '@angular/material/card';
import { MatTableDataSource, MatTableModule} from '@angular/material/table';
import { MatIconModule} from '@angular/material/icon';
import { MatButtonModule} from '@angular/material/button';
import { SalesPredictor } from '../../Services/sales-predictor';
import { SalesPredictionDto } from '../../Models/SalesPredictionDto';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { PageEvent } from '@angular/material/paginator';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule, Sort } from '@angular/material/sort';
import { Pagination } from '../../Models/Pagination';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { Createorder } from '../createorder/createorder';

@Component({
  selector: 'app-index',
  imports: [
    MatCardModule,
    MatTableModule,
    MatIconModule,
    MatButtonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatPaginatorModule,
    MatSortModule,
    MatDialogModule
  ],
  templateUrl: './index.html',
  styleUrl: './index.css'
})
export class Index {
  private salesService = inject(SalesPredictor);
  private dialog = inject(MatDialog);

  searchTerm: string = '';
  public pagination!: Pagination;

  public salesList: SalesPredictionDto[] = [];
  public dataSource = new MatTableDataSource<SalesPredictionDto>([]);
  public displayedColumns: string[] = [
    'customerName',
    'lastOrderDate',
    'nextPredictedOrder',
    'actions'
  ];

  totalItems = 0;
  pageSize = 10;
  currentPage = 1;
  sortColumn = '';
  sortDirection = '';

  ngOnInit() {
    this.getSalesPrediction();
  }

  onSearchPage() {
    this.currentPage = 1;
    this.getSalesPrediction();
  }

  onPageChange(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex + 1;
    this.getSalesPrediction();
  }

  onSortChange(sort: Sort) {
    this.sortColumn = sort.active;
    this.sortDirection = sort.direction;
    this.currentPage = 1;
    this.getSalesPrediction();
  }

  getSalesPrediction() {
    this.salesService.salesList(
      this.currentPage,
      this.pageSize,
      this.sortColumn,
      this.sortDirection
    ).subscribe({
      next: (response) => {
        this.salesList = response.body ?? [];
        this.dataSource.data = this.salesList;

        const paginationHeader = response.headers.get('X-Pagination');
        if (paginationHeader) {
          const meta = JSON.parse(paginationHeader);
          this.totalItems = meta.totalCount;
          this.pageSize = meta.pageSize;
          this.currentPage = meta.currentPage;
        }
      },
      error: (err) => {
        console.error('Error cargando predicciones:', err);
      }
    });
  }

  onSearch() {
    if (this.searchTerm.trim()) {
      console.log('Searching for:', this.searchTerm);
      // Aquí va tu lógica de búsqueda
    }
  }

  viewOrders(row: SalesPredictionDto) {
    console.log('View orders for', row);
  }

  newOrder(row: SalesPredictionDto) {
    this.dialog.open(Createorder, {
      width: '700px',
      data: { customerId: row.customerName } 
    });
  }
}
