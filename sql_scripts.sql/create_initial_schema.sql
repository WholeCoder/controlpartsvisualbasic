-- noinspection SqlDialectInspectionForFile

use ControlParts

create table templates
(
	name varchar(30),
	template_text text,
	Id int not null
		primary key
)


create table tables
(
	templatenameandfields varchar(255),
	Id int not null
		primary key,
	template_id int
		references templates
)


create table col
(
	columnnamedatatype varchar(255),
	Id int not null
		primary key,
	tablee_id int
		references tables
)


create table fields
(
	columnnamedatatype varchar(255),
	Id int not null
		primary key,
	tablee_id int
		references templates
)


