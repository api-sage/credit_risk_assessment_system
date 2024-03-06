import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private apiUrl = 'https://localhost:7108';
  this: any;

  constructor(private http: HttpClient) { }

  // Rename the method to match the endpoint name
  assessCreditHistory(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/CheckCreditScore`, data);
  }
  

  // Rename the method to match the endpoint name
  getAssessedCreditHistory(bvn: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/CheckCreditHistory`, bvn);
  }
}
