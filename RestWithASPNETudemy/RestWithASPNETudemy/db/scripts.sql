CREATE DATABASE `rest_with_asp_net_udemy` /*!40100 DEFAULT CHARACTER SET utf8 */;

use rest_with_asp_net_udemy;

CREATE TABLE `persons` (
  `Id` int(10) unsigned DEFAULT NULL,
  `Firstname` varchar(50) DEFAULT NULL,
  `Lastname` varchar(50) DEFAULT NULL,
  `Address` varchar(50) DEFAULT NULL,
  `Genre` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;