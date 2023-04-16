import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StorageService } from 'src/app/core/services/storage.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  constructor(
    private http: HttpClient,
    private storageService: StorageService
  ) { }

  private getHttpOptions() {
    const authToken = this.storageService.isLoggedIn() ? this.storageService.getUserData().accessToken : '';
    return {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${authToken}`,
        })
    };
  }

  create(customerName: string, invoiceNumber: string, value: number,
         description: string, month: number, paymentDate: string): Observable<any> {

    const httpOptions = this.getHttpOptions();
    const url = `${environment.apiUrl}/invoice`;

    var body = {
      customerName: customerName,
      invoiceNumber: invoiceNumber,
      value: value,
      description: description,
      month: month,
      paymentDate: paymentDate
    };

    return this.http.post(url, body, httpOptions);
  }

  getSummarizedInvoicesValue(year: number) {
    const httpOptions = this.getHttpOptions();
    const url = `${environment.apiUrl}/invoice/summarized?year=${year}`;

    return this.http.get(url, httpOptions);
  }
}
