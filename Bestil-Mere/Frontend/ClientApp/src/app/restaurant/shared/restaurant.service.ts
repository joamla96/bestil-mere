import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../../environments/environment';
import * as signalR from '@aspnet/signalr';
import {Order} from './order';

@Injectable({
	providedIn: 'root'
})
export class RestaurantService {
	private url = environment.gateway;
	hubConnection: signalR.HubConnection;
	private restaurantUpdatesUrl = environment.gateway + 'restaurant/updates';

	constructor(private http: HttpClient) {
	}

	getRestaurants(): Observable<any> {
		return this.http.get(this.url + 'restaurants');
	}

	acceptOrder(id): Observable<any> {
		console.log("AcceptOrder called: " + id);
		return this.http.patch(this.url + 'restaurants/accept-order/' + id, {});
	}

	rejectOrder(id): Observable<any> {
		console.log("RejectOrder called: " + id);
		return this.http.patch(this.url + 'restaurants/reject-order/' + id, {});
	}

	openConnection(restaurantId: string): Promise<void> {
			this.hubConnection = new signalR.HubConnectionBuilder()
				.withUrl(this.restaurantUpdatesUrl + '?restaurant=' + restaurantId, {
					skipNegotiation: true,
					transport: 1
				})
				.build();
		return this.hubConnection.start();
	}

	orderUpdates(): Observable<Order> {
		return new Observable((obs) => {
			this.hubConnection.on('orderUpdates',
				(data) => {
					obs.next(data);
				});

		});
	}
}
