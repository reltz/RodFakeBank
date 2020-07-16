CREATE TABLE CLIENT (
    ClientID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    LastName varchar(255),
    FirstName varchar(255),
    City varchar(255)
);

CREATE TABLE ACCOUNT (
	AccountNumber int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	AccountType varchar(35),
	Balance float,
	AccountClientID int FOREIGN KEY REFERENCES CLIENT(ClientID)
	);
	

DROP TABLE IF EXISTS ACCOUNT;
DROP TABLE IF EXISTS CLIENT;





INSERT INTO CLIENT (LastName, FirstName, City) VALUES ('Eltz', 'Rodrigo', 'Ottawa');
INSERT INTO ACCOUNT (AccountType, Balance, AccountClientID) VALUES ('Savings', 0, 1);