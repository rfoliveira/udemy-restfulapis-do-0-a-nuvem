drop table if exists books;

create table if not exists books (
	id int auto_increment primary key,
	Author longtext,
	LaunchDate datetime(6) not null,
	Price decimal(10,2) not null,
	Title longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

INSERT INTO books (Author, LaunchDate, Price, Title) values ('Autor do livro 1', NOW(), 100.25, 'Título do livro 1');
INSERT INTO books (Author, LaunchDate, Price, Title) values ('Autor do livro 2', NOW() + interval 1 day, 110.25, 'Título do livro 2');
INSERT INTO books (Author, LaunchDate, Price, Title) values ('Autor do livro 3', NOW() + interval 2 day, 120.25, 'Título do livro 3');
INSERT INTO books (Author, LaunchDate, Price, Title) values ('Autor do livro 4', NOW() + interval 3 day, 130.25, 'Título do livro 4');
INSERT INTO books (Author, LaunchDate, Price, Title) values ('Autor do livro 5', NOW() + interval 4 day, 140.25, 'Título do livro 5');