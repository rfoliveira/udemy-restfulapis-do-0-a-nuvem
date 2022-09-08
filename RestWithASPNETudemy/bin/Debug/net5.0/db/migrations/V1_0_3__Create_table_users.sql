CREATE TABLE IF NOT EXISTS users (
	id int not null auto_increment primary key,
	user_name VARCHAR (50) NOT NULL, 
	password VARCHAR (130) NOT NULL, 
	full_name VARCHAR (120) NOT NULL, 
	refresh_token VARCHAR (500) NULL, 
	refresh_token_expiry_time DATETIME NULL,
	UNIQUE user_name (user_name)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;