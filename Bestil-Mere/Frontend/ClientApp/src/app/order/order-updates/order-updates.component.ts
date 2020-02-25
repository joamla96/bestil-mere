import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {OrderService} from '../shared/order.service';
import {first, switchMap} from 'rxjs/operators';
import {OrderStatus} from '../shared/orderStatus';
import {Subscription} from 'rxjs';
import {isNullOrUndefined} from 'util';

@Component({
	selector: 'app-order-updates',
	templateUrl: './order-updates.component.html',
	styleUrls: ['./order-updates.component.css']
})
export class OrderUpdatesComponent implements OnInit, OnDestroy {
	statusResps: OrderStatus[] = [];
	OrderStatus = OrderStatus;
	sub: Subscription;
	accepted = false;
	status = 'P';

	constructor(private route: ActivatedRoute, private service: OrderService, private router: Router) {
	}

	ngOnInit() {
		this.route.queryParams.pipe(first(), switchMap(qp => {
			return this.service.openConnection(qp.id);
		})).subscribe(() => {
			this.sub = this.service.orderUpdates()
				.subscribe((currentStatus: OrderStatus) => {
					this.statusResps.push(currentStatus);
					if (currentStatus === OrderStatus.Accepted) {
						this.status = 'A';
						setTimeout(() => {
								this.router.navigateByUrl('/order/order-list');
						}, 5000);
					}
					if (currentStatus === OrderStatus.Rejected) {
						this.status = 'R';
						setTimeout(() => {
								this.router.navigateByUrl('/order/order-list');
						}, 5000);
					}
				});

		});
	}

	ngOnDestroy(): void {
		this.sub.unsubscribe();
		if (!isNullOrUndefined(this.service.hubConnection)) {
			this.service.hubConnection.stop();
		}
	}

}
