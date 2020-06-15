
import { OrderStatus } from './order-status.enum';
import { OrderItem } from './order-item.model';

export class Order {
    public constructor(
        public id?: string,
        public customerId?: string,
        public customerName?: string,
        public address?: string,
        public totalPrice?: number,
        public orderStatus?: OrderStatus,
        public orderPlacedDate?: Date,
        public orderConfirmedDate?: Date,
        public orderShippedDate?: Date,
        public orderDeliveredDate?: Date,
        public orderCanceledDate?: Date,
        public orderItems?: OrderItem[]
    ) { }
}