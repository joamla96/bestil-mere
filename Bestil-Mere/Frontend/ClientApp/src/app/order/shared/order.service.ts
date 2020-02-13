import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../../environments/environment';
import {CreateOrderModel} from './createOrderModel';
import * as signalR from '@aspnet/signalr';
import {isNullOrUndefined} from 'util';

@Injectable({
	providedIn: 'root'
})
export class OrderService {
	private url = environment.gateway;
	private hubConnection: signalR.HubConnection;
	private orderUpdatesUrl = environment.gateway + 'order/order-updates';

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

	openConnection(): Promise<void> {
		if (isNullOrUndefined(this.hubConnection)) {
			console.log('here');
			this.hubConnection = new signalR.HubConnectionBuilder()
				.withUrl(this.orderUpdatesUrl)
				.build();
		}
		return this.hubConnection.start();
	}

	orderUpdates(orderId: string): Observable<any> { // TODO: Impl of orderstatus
		return new Observable((obs) => {
			this.hubConnection.on('orderUpdates',
				(data) => {
					obs.next(data);
				});
		});
	}
}
