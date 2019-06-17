create table if not exists books (
	id varchar(127) not null,
	Author longtext,
	LaunchDate datetime(6) not null,
	Price decimal(10,2) not null,
	Title longtext,
	primary key(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;