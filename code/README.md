# easysched code folder
Schedule creation software code.

# Feb. 13, 2020
Created readme file.

For scaffolding using MySQL:
````
scaffold-DbContext –Connection "Server=localhost;Port=3306;user=root;password=password;Database=easysched" -Provider "Pomelo.EntityFrameworkCore.MySql" -OutputDir "Models" –Context "easyschedContext" –Verbose -Force
````
To update the application when the database changes, run the above command with -f at the end.
