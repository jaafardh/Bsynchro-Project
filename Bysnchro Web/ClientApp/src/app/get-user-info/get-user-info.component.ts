import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../customer.service';

@Component({
  selector: 'app-get-user-info',
  templateUrl: './get-user-info.component.html',
  styleUrls: ['./get-user-info.component.css']
})
export class GetUserInfoComponent implements OnInit {
  customerData: any;
  errorOccurred: boolean = false;
  customerId: number = 0;
  errormessage: string = '';
  constructor(private customerService: CustomerService) { }

  ngOnInit(): void {
  }
  GetUser() {
    if (this.customerId <= 0) {
      this.errormessage = 'Customer id is not acceptable';
      this.errorOccurred = true;
      return; // Exit the function if userId is invalid
    }
    this.errormessage = '';
    this.errorOccurred = false;
    this.customerService.getCustomerInfo(this.customerId).subscribe(
      (data) => {
        if (data !== null) { 
          this.customerData = data;
        } else {
          this.errorOccurred = true;
          this.customerData = null;
        }
      },
      (error) => {
        // Handle the "Not Found" response (HTTP status code 404)
        if (error.status === 404) {
          this.errorOccurred = true;
          this.errormessage = 'Not found!';
        } else {
          // Handle other errors as needed
          console.error('An error occurred:', error);
          this.errorOccurred = true;
          this.errormessage = 'Error: ' + error;
        }
      }
    );
  }
}
