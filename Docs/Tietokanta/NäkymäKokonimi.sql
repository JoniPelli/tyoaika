USE [Projekti]
GO

/****** Object:  View [dbo].[OmatKirjauksetKokonimi]    Script Date: 3.5.2021 12.27.27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[OmatKirjauksetKokonimi]
AS
SELECT        CONCAT(dbo.Tyontekija.Etunimi,' ',dbo.Tyontekija.Sukunimi) AS Kokonimi, dbo.Tyontekija.Etunimi, dbo.Tyontekija.Sukunimi, dbo.Kirjaus.Pvm, dbo.Tehtavat.Tehtava, dbo.Kohteet.Kohde, dbo.Kirjaus.Tunnit, dbo.Kirjaus.Vapaateksti
FROM            dbo.Kirjaus INNER JOIN
                         dbo.Tyontekija ON dbo.Kirjaus.TyontekijaID = dbo.Tyontekija.TyontekijaID INNER JOIN
                         dbo.Kohteet ON dbo.Kirjaus.KohdeID = dbo.Kohteet.KohdeID INNER JOIN
                         dbo.Tehtavat ON dbo.Kirjaus.TehtavaID = dbo.Tehtavat.TehtavaID
GO
