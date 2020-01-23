# easysched
Schedule creation software

# Git Instructions

Using Git Bash or other command line interface:

Run below command to retrieve the latest version from github to your local machine.
ALWAYS run this command before editing or adding files in the git folder.
```
git pull
```

Below is the process of adding new files to the repository.

First run the "add" command. This can be used to add specific files or all files.
For adding a specific file, run:
```
git add <filename>
```
For adding all files, replace the filename with a period:
```
git add .
```

Next, run the "commit" command. This stages the files to be uploaded with a message:
```
git commit -m "<commit message, example: Added new view file. or Fixed this bug. etc>"
```

Finally, to actually upload the changes to github, run the "push" command. We are probably going to be pushing to the master branch every time.
```
git push origin master
```

# Jan. 23, 2020
Created repository.
Added team charter to docs folder.