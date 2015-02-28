
use orders;

create table Products(
	Id int auto_increment not null,
	name nvarchar(250) ,
	price decimal(18, 2),
	constraint PK_Products PRIMARY KEY (Id)
);

create table Customers(
	Id int auto_increment not null,
	name nvarchar(250) ,
	constraint PK_Products PRIMARY KEY (Id)
);

create table orders(
	Id int auto_increment not null,
	orderDate Datetime,
	constraint PK_Orders PRIMARY key (Id)
);

create table order_items(
	Id int auto_increment not null,
	orderId int,
	productId int,
	quantity decimal(18,2),
	constraint PK_order_items primary key (Id),
	constraint FK_order_items_orders foreign key(orderId) references orders(id),
	constraint FK_order_items_products foreign key(productId) references products(id)
);

INSERT INTO `products` VALUES (1,'beer',1.20), (2,'cheese',9.50), (3,'rakiya',12.40), (4,'salami',6.33), (5,'tomatos',2.50), (6,'cucumbers',1.35), (7,'water',0.85), (8,'apples',0.75);
INSERT INTO `customers` VALUES (1,'Peter'), (2,'Maria'), (3,'Nakov'), (4,'Vlado');
INSERT INTO `orders` VALUES (1,'2015-02-13 13:47:04'), (2,'2015-02-14 22:03:44'), (3,'2015-02-18 09:22:01'), (4,'2015-02-11 20:17:18');
INSERT INTO `order_items` VALUES (12,4,6,2.00), (13,3,2,4.00), (14,3,5,1.50), (15,2,1,6.00), (16,2,3,1.20), (17,1,2,1.00), (18,1,3,1.00), (19,1,4,2.00), (20,1,5,1.00), (21,3,1,4.00), (22,1,1,3.00);

select 
	p.name as `product_name`, 
	count(oi.Id) as `num_orders`, 
	case when SUM(oi.quantity) is null then 0.00
	else SUM(oi.quantity)  end as `quantity`,
	p.price,
	case when SUM(oi.quantity)*p.price is null then 0.0000
	else SUM(oi.quantity)*p.price end as `total_price`
from products p
	left outer join order_items oi ON oi.productId = p.Id
group by p.name
order by p.name