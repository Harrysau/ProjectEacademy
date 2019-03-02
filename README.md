### Notes

If you run the project and it show error that relate to **database** or **IIS Web Server** please do as one of the following below:

* If the page show **Cannot attach the file DATABASENAME.mdf as database** please go to **Web.config** file and remove **InitialCatalog** or change **Database Name** in ```<connectionString>``` or **delete database file** in ```<<path_to_solution_folder>>\App_Data```
* If error relate to **IIS Web Server** please go to ```<<path_to_solution_folder>>\.vs\config\``` then delete **applicationhost.config** file or navigate to your **Document** folder then delete folder **IISExpress**
