import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../customer.service';

@Component({
  selector: 'app-add-account',
  templateUrl: './add-account.component.html',
  styleUrls: ['./add-account.component.css']
})
export class AddAccountComponent implements OnInit {
  userId: number = 0;
  initialCredit: number = 0;
  responseMessage: string = '';

  constructor(private customerService: CustomerService) { }

  ngOnInit(): void {
  }
  addAccount() {
    if (this.userId <= 0 || this.initialCredit < 0) {
      this.responseMessage = 'Please enter a valid User ID and Initial Credit.';
      return;
    }

    this.customerService.addAccount(this.userId, this.initialCredit).subscribe(
      (data) => {
        this.responseMessage = data;
      },
      (error) => {
        this.responseMessage = 'An error occurred while adding the account.';
        console.error(error);
      }
    );
  }

}
