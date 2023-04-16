import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StorageService } from 'src/app/core/services/storage.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {

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

  create(category: string, value: number, description: string,
    paymentDate: string, competenceDate: string, customerName: string): Observable<any> {

    const httpOptions = this.getHttpOptions();
    const url = `${environment.apiUrl}/expense`;

    var body = {
      category: category,
      value: value,
      description: description,
      paymentDate: paymentDate,
      competenceDate: competenceDate,
      customerName: customerName
    };

    return this.http.post(url, body, httpOptions);
  }
}
