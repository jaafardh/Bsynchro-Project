import { Component, OnInit } from '@angular/core';
import { Data } from '@angular/router';

@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent implements OnInit {

  searchResult: FlightRm[] =[
    {
      airline: "American Airlines",
      remainingNumberOfSeats: 500,
      deparature: { time: Date.now().toString(), Place: "LosAnglees" },
      arrival: { time: Date.now().toString(), Place: "Istanbul" },
      price: "350",
    },
    {
      airline: "British Airways",
      remainingNumberOfSeats: 60,
      deparature: { time: Date.now().toString(), Place: "London, England" },
      arrival: { time: Date.now().toString(), Place: "Vizzola-Ticino" },
      price: "600",
    }
  ]

  constructor() { }

  ngOnInit(): void {
  }
  addFlight() {
    const newFlight: FlightRm = {
      airline: "Emirates",
      remainingNumberOfSeats: 200,
      deparature: { time: Date.now().toString(), Place: "Dubai" },
      arrival: { time: Date.now().toString(), Place: "New York" },
      price: "800",
    };
    this.searchResult.push(newFlight);
  }

}

export interface FlightRm {
  airline: string;
  arrival: TimePlaceRm;
  deparature: TimePlaceRm;
  price: string;
  remainingNumberOfSeats: number;
}
export interface TimePlaceRm {
  Place: string;
  time: string;
}
