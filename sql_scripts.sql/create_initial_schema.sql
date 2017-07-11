-- noinspection SqlDialectInspectionForFile

use ControlPartsTest

create table templates
(
	name varchar(30) NOT NULL UNIQUE,
	field_separator varchar(25),
	table_separator varchar(25),
	template_text text not null,
	Id int not null
		primary key identity(1,1)
)


create table tables
(
	templatenameandfields varchar(255),
	Id int not null
		primary key identity(1,1),
	template_id int
		references templates
)


create table col
(
	columnnamedatatype varchar(255),
	Id int not null
		primary key identity(1,1),
	tablee_id int
		references tables
)


create table fields
(
	columnnamedatatype varchar(255),
	Id int not null
		primary key IDENTITY(1,1),
	tablee_id int
		references templates
)

