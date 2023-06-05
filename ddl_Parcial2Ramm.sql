-- DDL
CREATE DATABASE Parcial2Ramm
USE [master]
GO
CREATE LOGIN [usrparcial2] WITH PASSWORD = N'12345678',
	DEFAULT_DATABASE = [Parcial2Ramm],
	CHECK_EXPIRATION = OFF,
	CHECK_POLICY = ON
GO
USE [Parcial2Ramm]
GO
CREATE USER [usrparcial2] FOR LOGIN [usrparcial2]
GO
ALTER ROLE [db_owner] ADD MEMBER [usrparcial2]
GO

drop table Serie;

CREATE TABLE Serie(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	titulo VARCHAR(250) NOT NULL,
	sinopsis VARCHAR(5000) NOT NULL,
	director VARCHAR(100) NOT NULL,
	duracion INT NOT NULL,
	fechaEstreno DATE NOT NULL,
	registroActivo BIT NULL DEFAULT 1,
);

-- DML
INSERT INTO Serie(titulo, sinopsis, director, duracion, fechaEstreno)
VALUES('The IT Crowd', 'The IT Crowd es una serie de televisión de comedia', 'FGraham Linehan', 25, '10-06-2001');

SELECT * FROM Producto;

CREATE PROC paSerieListar @parametro VARCHAR(250)
AS
  SELECT id, titulo, sinopsis, director, duracion
  FROM Serie
  WHERE registroActivo=1 AND 
		titulo+director LIKE '%'+REPLACE(@parametro,' ','%')+'%'

EXEC paSerieListar ''