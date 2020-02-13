export enum OrderStatus {
	Created, // When an order has been created
	Pending, // When payment is current waiting on response from restaurant
	Accepted, // When the order has been accepted by the restaurant
	Rejected, // Rejected
	Done // When the order has been proceeded, and is no longer in the hands of the restaurant

}
