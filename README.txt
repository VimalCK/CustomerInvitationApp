
	CUSTOMER INVITATION APP
---------------------------------------------------------------------------------------
This app invite any customer within (n)km of the Dublin office for some food and drinks.


Prerequsite to run the application
----------------------------------
1. Microsoft .Net core 3.1
2. Windows 10

Prerequsite to debug the application
------------------------------------
1. Microsoft .Net core 3.1
2. Visual Studio Code
3. Windows 10


How to Run application
----------------------
1. Extract source code
2. Double click ..\CustomerInvitationApp\src\CustomerInvitationApp\bin\Debug\netcoreapp3.1\CustomerInvitationApp.exe
3. Application will prompt for input file. User can provide local/remote file name.
	eg: c:\customers.txt
	eg: https://s3.amazonaws.com/intercom-take-home-test/customers.txt

How to run application from source code
---------------------------------------
1. Extract source code
2. Navigate ..\CustomerInvitationApp\
3. Right click "src" to open in Visual Studio Code
4. (Ctrl + Shift + `) and open terminal (Make sure that terminal is in src folder location)
5. Execute below commands in the following order
	1. dotnet restore 
	2. dotnet build  
	3. dotnet run --project CustomerInvitationApp

6. Application will prompt for input file. User can provide local/remote file name.
	eg: c:\customers.txt
	eg: https://s3.amazonaws.com/intercom-take-home-test/customers.txt

How to run Test cases
---------------------
1. Extract source code
2. Navigate ..\CustomerInvitationApp\
3. Right click "test" to open in VS Code
4. (Ctrl + Shift + `) and open terminal (Make sure that terminal is in test folder location)
5. Execute below commands in the following order
	1. dotnet restore
	2. dotnet build
	3. dotnet test /p:CollectCoverage=true
