create table if not exists users (
	ID int not null auto_increment primary key,
	Login varchar(50) unique not null,
	AccessKey varchar(50) not null
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
