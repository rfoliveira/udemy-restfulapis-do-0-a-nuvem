create table if not exists books (
	Id int not null AUTO_INCREMENT,
	Author longtext not null,
	LaunchDate datetime(6) not null,
	Price decimal(10,2) not null,
	Title longtext not null,
	primary key (Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;