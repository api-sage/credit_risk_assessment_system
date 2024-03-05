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
    return this.http.post(`${this.apiUrl}/AssessCreditHistory`, data);
  }
  

  // Rename the method to match the endpoint name
  getAssessedCreditHistory(): Observable<any> {
    return this.http.get(`${this.apiUrl}/GetAssessedCreditHistory`);
  }
}
