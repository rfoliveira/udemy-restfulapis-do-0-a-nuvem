CREATE TABLE IF NOT EXISTS users (
	id int not null auto_increment primary key,
	user_name VARCHAR (50) NOT NULL DEFAULT '0', 
	password VARCHAR (130) NOT NULL DEFAULT '0', 
	full_name VARCHAR (120) NOT NULL, 
	refresh_token VARCHAR (500) NOT NULL DEFAULT '0', 
	refresh_token_expiry_time DATETIME NULL DEFAULT NULL,
	UNIQUE user_name (user_name)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;