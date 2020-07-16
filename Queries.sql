select ac.AccountNumber, ac.AccountClientID, ac.AccountType, ac.Balance, cl.ClientID, CONCAT(cl.FirstName,' ',cl.LastName) AS 'Name', cl.City from dbo.ACCOUNT as ac right join dbo.CLIENT as cl on ac.AccountClientID = cl.ClientID 





