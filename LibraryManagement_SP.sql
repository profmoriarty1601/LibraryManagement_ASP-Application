
/* Read all books */
CREATE PROCEDURE READ_BOOKS_SP
AS
BEGIN
	SELECT * FROM SnehalBooksList
END;

EXEC READ_BOOKS_SP;


/* Read all users */
CREATE PROCEDURE READ_ALL_USERS_SP
AS
BEGIN
	SELECT * FROM SnehalUser
END;

EXEC READ_ALL_USERS_SP;


/* Read all admin/librarian users */
CREATE PROCEDURE READ_ADMIN_USERS_SP
AS
BEGIN
	SELECT * FROM SnehalUser WHERE UserTypeId=1
END;

EXEC READ_ADMIN_USERS_SP;


/* Read admin by id */
CREATE PROCEDURE READ_ADMIN_ById_SP
@AdminId int
AS
BEGIN
	SELECT * FROM SnehalUser WHERE UserTypeId=1 AND UserId=@AdminId
END;

EXEC READ_ADMIN_ById_SP
@AdminId=1;


/* Read admin login by username password */
CREATE PROCEDURE LOGIN_ADMIN_SP
@AdminUsername varchar(15),
@AdminPassword varchar(15)
AS
BEGIN
	SELECT * FROM SnehalUser 
	WHERE UserTypeId=1
	AND UserName=@AdminUsername 
	AND UserPassword=@AdminPassword
END;

EXEC LOGIN_ADMIN_SP
@AdminUsername='admin',
@AdminPassword='adminpass23';


/* Read all student users */
CREATE PROCEDURE READ_STUDENT_USERS_SP
AS
BEGIN
	SELECT * FROM SnehalUser WHERE UserTypeId=2 
END;

EXEC READ_STUDENT_USERS_SP;


/* Read student by id */
CREATE PROCEDURE READ_BOOKS_STUDENT_Id_SP
@StudentId int
AS
BEGIN
	SELECT * FROM SnehalBooksList
	WHERE BookId IN 
	(
		SELECT BookId FROM SnehalBookAssigned
		WHERE UserId=@StudentId
	)
END;

EXEC READ_BOOKS_STUDENT_Id_SP
@StudentId=2;


/* Read Books assigned by bookId to StudentId */
CREATE PROCEDURE READ_STUDENT_ById_BOOKS_SP
@StudentId int
AS
BEGIN
	SELECT * FROM SnehalBookAssigned 
	WHERE UserId=@StudentId
END;

EXEC READ_STUDENT_ById_BOOKS_SP
@StudentId=2;


/* Read student login by username password */
CREATE PROCEDURE LOGIN_STUDENT_SP
@StudentUsername varchar(15),
@StudentPassword varchar(15)
AS
BEGIN
	SELECT * FROM SnehalUser 
	WHERE UserTypeId=2 
	AND UserName=@StudentUsername 
	AND UserPassword=@StudentPassword
END;

EXEC LOGIN_STUDENT_SP
@StudentUsername='jacob',
@StudentPassword='pass2';



/******************************************/
/* CRUD Operations for User Type Student */
/****************************************/

/*
	'A' : add new student
	'B' : update student password
	'C' : toggle isActive for student
	'D' : assign books to student
*/

CREATE PROCEDURE STUDENT_CRUD_SP
@action varchar(10)=NULL,
@username varchar(15)=NULL,
@password varchar(15)=NULL
AS
BEGIN
	IF @action='A'
		INSERT INTO SnehalUser VALUES (@username, @password, TRUE, 2) 
END
BEGIN
	IF @action='B'
		UPDATE SnehalUser SET UserPassword=@password
		WHERE UserName=@username
END
BEGIN
	IF @action='C'
		UPDATE SnehalUser SET IsActive = ~IsActive
		WHERE UserName=@username AND UserPassword=@password
END



/******************************************/
/* CRUD Operations for Books */
/****************************************/

/*
	'A' : add new book
	'B' : update book details
	'C' : toggle isActive for student
	'D' : assign books to student


CREATE PROCEDURE BOOKS_CRUD_SP
@action varchar(10)=NULL,
@bookname 
AS
BEGIN
	*/

/* Update books count */
CREATE PROCEDURE UPDATE_BOOK_COUNT_ByOne
@bookId
AS
BEGIN
	UPDATE SnehalBooksList
	SET BookQty=(BookQty+1)
	WHERE BookId=@bookId 
END;

EXEC UPDATE_BOOK_COUNT_ByOne @bookId=1;

