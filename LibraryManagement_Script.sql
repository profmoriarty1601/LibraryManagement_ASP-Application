
create table SnehalUserType
(
	UserTypeId int  NOT NULL primary key,
	UserTypeName varchar(15) NOT NULL
);


create table SnehalUser
(
	UserId int  NOT NULL primary key Identity(1,1),
	UserName varchar(15) NOT NULL,
	UserPassword varchar(15) NOT NULL,
	IsActive bit Not Null,
	UserTypeId int foreign key references SnehalUserType(UserTypeId)
);

create table SnehalBooksList
(
	BookId int  NOT NULL primary key identity,
	BookName varchar(25) NOT NULL,
	BookAuthor varchar(25) NOT NULL,
	BookDate date NOT NULL,
	BookPublicationName varchar(25) NOT NULL,
	BookYOP int NOT NULL,
	BookQty int NOT NULL
);

create table SnehalBookAssigned
(
	AssignedId int  NOT NULL primary key identity,
	IssueDate datetime,
	BookId int foreign key references SnehalBooksList(bookId),
	UserId int foreign key references SnehalUser(userId)
);

