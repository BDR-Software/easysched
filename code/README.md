# easysched code folder
Schedule creation software code.

# Feb. 13, 2020
Created readme file.

Run the below command to create or update the database context in the application.
````
scaffold-DbContext –Connection "Server=localhost;Port=3306;user=root;password=password;Database=easysched" -Provider "Poemlo.EntityFrameworkCore.MySql" -OutputDir "Models" –Context "easyschedContext" -ContextDir "Data" –Verbose -Force
````
