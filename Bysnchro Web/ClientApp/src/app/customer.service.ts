import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private apiUrl = 'https://localhost:7168/api';

  constructor(private http: HttpClient) { }

  getCustomerInfo(customerId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/GetUser/${customerId}`).pipe(
      catchError((error) => {
        if (error.status === 404) {
          return throwError('Customer not found');
        }
        else {
          console.error('An error occured:', error);
          return throwError('An Error occured while fetching customer data');
        }
      })
    )
  }
  addAccount(userId: number, initialCredit: number): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/AddAccount`, {
      CustomerId: userId,
      InitalCredit: initialCredit
    }, { responseType: 'text' as 'json' }).pipe(
      catchError((error) => {
        return throwError('An error occurred while adding the account.');
      })
    )
  }

}
