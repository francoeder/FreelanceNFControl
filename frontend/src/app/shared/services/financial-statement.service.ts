import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { StorageService } from 'src/app/core/services/storage.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FinancialStatementService {

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

  getFinancialStatement(year: number) {
    const httpOptions = this.getHttpOptions();
    const url = `${environment.apiUrl}/financialstatement?year=${year}`;

    return this.http.get(url, httpOptions);
  }
}
