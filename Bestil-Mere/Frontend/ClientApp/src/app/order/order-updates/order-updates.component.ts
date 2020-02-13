import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {OrderService} from '../shared/order.service';
import {first, switchMap} from 'rxjs/operators';

@Component({
	selector: 'app-order-updates',
	templateUrl: './order-updates.component.html',
	styleUrls: ['./order-updates.component.css']
})
export class OrderUpdatesComponent implements OnInit {

	constructor(private route: ActivatedRoute, private service: OrderService) {
	}

	ngOnInit() {
		this.service.openConnection().then(() => {
			this.service.orderUpdates('')
				.subscribe(() => {
					console.log('connected.');
				});
		});

	}

}
