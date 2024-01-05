create table vegetables_and_fruits
(
	_name varchar(20),
	_type varchar(20) check (_type = 'v' or _type = 'f'),
	_color varchar(20),
	_calorific_value integer
);
drop table vegetables_and_fruits;
 insert into vegetables_and_fruits
 values	('apple', 'f', 'red', 46 ),
 		('tomato', 'v', 'red', 18),
 		('potato', 'v', 'brown', 83),
 		('carrot', 'v', 'red', 33),
 		('cucumber', 'v', 'green',15),
 		('orange', 'f', 'orange', 38),
 		('watermelon','f','green',38),
 		('bananas', 'f','yellow', 91),
 		('cowberry','f','red', 40),
 		('grape','f','green',69),
 		('cherry','f','violet',49);
 		





