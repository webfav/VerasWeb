Veras Web Covid19-registrering

Det er et skole-projekt med en asp.net webside, der er published på Azure i web-app og CosmosDB. Den er lavet i Visual Studio 2019 i MVC-arkitektur og authorization.

Man kan registrere sig på siden med dato, navn og CPR. Det er meningen, at man så får svar på testen cirka en uge senere.

Identity-tabellerne for authorization og authentication er lagt på lokal MSsql db med dbcontext, entityframework og overført via migrations. Så det er kun en adminstrator eller en person som kan give svar på test, der skal logge ind og sætte flueben om den registrede er testet positiv med Covid19-smitte.

Man skal også have nugget packets Microsoft.Azure.Cosmos og Newtonsoft.Json.

Pt. virker det ikke med SQL databasen, så man kan ikke logge ind.
Men web-siden ligger her: https://verasweb20200912.azurewebsites.net
