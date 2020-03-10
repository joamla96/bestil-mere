import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {RestaurantService} from '../shared/restaurant.service';
import {first, switchMap} from 'rxjs/operators';
import {Order} from '../shared/order';
import {Subscription} from 'rxjs';
import {isNullOrUndefined} from 'util';

@Component({
	selector: 'app-restaurant-updates',
	templateUrl: './restaurant-updates.component.html',
	styleUrls: ['./restaurant-updates.component.css']
})
export class RestaurantUpdatesComponent implements OnInit, OnDestroy {
	orders: Order[] = [];
	sub: Subscription;
	accepted = false;

	constructor(private route: ActivatedRoute, private service: RestaurantService, private router: Router) {
	}

	ngOnInit() {
		this.route.queryParams.pipe(first(), switchMap(qp => {
			return this.service.openConnection(qp.id);
		})).subscribe(() => {
			this.sub = this.service.orderUpdates()
				.subscribe((currentOrder: Order) => {
					this.orders.push(currentOrder);
				});

		});
	}

	ngOnDestroy(): void {
		this.sub.unsubscribe();
		if (!isNullOrUndefined(this.service.hubConnection)) {
			this.service.hubConnection.stop();
		}
	}

	acceptOrder(id) {
		this.service.acceptOrder(id).subscribe();
	}

	rejectOrder(id) {
		this.service.rejectOrder(id).subscribe();
	}

}
