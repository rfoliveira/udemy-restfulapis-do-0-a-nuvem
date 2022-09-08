CREATE DATABASE IF NOT EXISTS rest_with_asp_net_udemy /*!40100 DEFAULT CHARACTER SET utf8 */;

use rest_with_asp_net_udemy;

CREATE TABLE if not exists persons (
  Id int NOT NULL AUTO_INCREMENT primary key,
  Firstname varchar(50) not NULL,
  Lastname varchar(50) not NULL,
  Address varchar(50) not NULL,
  Genre varchar(50) not NULL,
  Enabled bit not null default 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8;