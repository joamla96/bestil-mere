import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../../environments/environment';
import {CreateOrderModel} from './createOrderModel';

@Injectable({
	providedIn: 'root'
})
export class OrderService {
	private url = environment.gateway;

	constructor(private http: HttpClient) {
	}

	createTestOrder(): Observable<any> {
		const model: CreateOrderModel = {
			customerId: '123123123',
			restaurantId: '66666666',
			orderLines: [
				{
					meal: {
						name: 'Esbjerg pizza',
						extraMealItems: [
							{
								name: 'Kylling',
								quantity: 2
							}
						],
						mealItems: [
							{
								name: 'Ost',
							},
							{
								name: 'Oksekød',
							},
							{
								name: 'Pølser',
							}
						],
					},
					quantity: 1
				}
			]
		};
		return this.http.post(this.url + 'order/create-order', model);
	}
}
