USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [Barney]    Script Date: 05/02/2022 19:13:51 ******/
CREATE LOGIN [Barney] WITH PASSWORD=N'L1AlGOgw5g3Qjecmd+7RdWU46jRl3FFZMBIK9Xm+Au4=', DEFAULT_DATABASE=[GNB], DEFAULT_LANGUAGE=[Español], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [Barney] DISABLE
GO
