import {OrderLine} from './orderLine';

export class CreateOrderModel {
	customerId: string;
	restaurantId: string;
	country: string;
	orderLines: OrderLine[];
}
