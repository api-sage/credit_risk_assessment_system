import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = '';

  constructor(private http: HttpClient) {}

  getCreditScore(bvn: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/credit-score/${bvn}`);
  }

  getCreditHistory(bvn: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/credit-history/${bvn}`);
  }
}
