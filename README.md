# Client-Console-Application-For-WCF
Insert and select data from Database by a console application which run by WCF.

## How to add WCF Service to a Console Application in Visual Studio 2022
After WCF service started successfully, Go to app.config and copy the url of baseAddress. Something like below,
```
<baseAddresses>
  <add baseAddress="http://localhost:8733/Design_Time_Addresses/WarehouseManagement_WCF/Service1/" />
</baseAddresses>
```
Then Run this into browser,
```
http://localhost:8733/Design_Time_Addresses/WarehouseManagement_WCF/Service1/?wsdl
```
This will show the xml metadata which is needed for a console application to run and utilize the WCF Service.

### Steps to add WCF to Console Application.
1. After creating console app, right click on project and click on ```Add```.
2. After click ```Add``` go to ```Connected Service```.
3. After opening a page click on ```+``` sign in Service References (WCF Service) tab. Select WCF Service and Next.
4. in the url section put ```http://localhost:8733/Design_Time_Addresses/WarehouseManagement_WCF/Service1/?wsdl``` the click go button.
5. It will fetch the Interface from WCF Service along with methods which will display in Operations tab. A by default Namespace will also generate which will be used in console app.
6. Click Next till Finish button is showing (Change access level as per need).
Now You have successfully added the WCF Service (Server) to Console app (Client).

Make sure WCF will be running during this whole process. Now start work on Program.cs file in Console app.
