import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
	private url = environment.gateway;


  constructor(private http: HttpClient) { }

	getCustomerProfile(): Observable<any> {
  	let email = 'jesper@gmail.com';
		return this.http.get(this.url + 'customers/email/' + email);
	}

	saveCustomerProfile(customer): Observable<any> {
		return this.http.put(this.url + 'customers/' + customer.id, customer);
	}


}
