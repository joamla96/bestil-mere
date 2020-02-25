import { Component, OnInit } from '@angular/core';
import {FormBuilder, Validators} from "@angular/forms";
import {CustomerService} from "../shared/customer.service";
import {first} from "rxjs/operators";
import {Customer} from "../shared/customer";
import {AuthService} from "../../auth.service";

@Component({
  selector: 'app-customer-profile',
  templateUrl: './customer-profile.component.html',
  styleUrls: ['./customer-profile.component.css']
})
export class CustomerProfileComponent implements OnInit {
  customerProfile: Customer;
	profileForm = this.fb.group({
		id: [''],
		firstName: ['', Validators.required],
		lastName: ['', Validators.required],
		email: ['', Validators.required],
		address: [''],
		city: [''],
		country: [''],
		postalCode: ['']
	});

  constructor(private service: CustomerService,
							private fb: FormBuilder,
							private authService: AuthService) {	}

  ngOnInit() {
  	let userEmail = this.authService.userEmail;
		this.service.getCustomerProfile(userEmail)
			.pipe(first())
			.subscribe(data => {
				this.customerProfile = data;
				this.profileForm.setValue(this.customerProfile);
			});
	}

	onSubmit() {
		this.customerProfile = this.profileForm.value;
		console.warn(this.customerProfile);
		this.service.saveCustomerProfile(this.customerProfile)
			.pipe(first())
			.subscribe();
	}

}
