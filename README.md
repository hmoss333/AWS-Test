This is a simple UI project connected to an AWS DynamocDB database using REST api calls. This project was created as a quick proof of concept on how to implement and use REST calls with AWS Lambda functions

- Get: Works as a search function for the database. The user can search by ID to confirm if an entry exists, and return the object name
- Put: Create a new database entry. The user must provide both a unique ID and an associated name. This can be expanded to support more complex data sets as needed
- Delete: Removes an entry from the database. The user can provide an ID, if a match is found then the corresponding entry is removed from the database

Use case walkthrough:
- Use the Search UI to check if an entry exists in the database
![Search for Entry](https://github.com/hmoss333/AWS-Test/blob/main/Screenshots/Search.png?raw=true "Search")
![Found Entry](https://github.com/hmoss333/AWS-Test/blob/main/Screenshots/Search_Success.png?raw=true "Search Success")
- If we search for an entry that does not exist we get an error message
![Unable to find Entry](https://github.com/hmoss333/AWS-Test/blob/main/Screenshots/Search_Failure.png?raw=true "Search Failed")
- We can confirm that this entry does not exist by checking the database directly
![Checking database](https://github.com/hmoss333/AWS-Test/blob/main/Screenshots/Database_1.png?raw=true "Checking database")
- From here we can add a new entry to the database with the correct data
![Create new Entry](https://github.com/hmoss333/AWS-Test/blob/main/Screenshots/Put.png?raw=true "New Entry")
- Once completed we can confirm that the entry exists in the database
![New entry found](https://github.com/hmoss333/AWS-Test/blob/main/Screenshots/Database_2.png?raw=true "New Entry Found")
- Finally, we can delete the entry once it is no longer needed
![Delete Entry](https://github.com/hmoss333/AWS-Test/blob/main/Screenshots/Delete.png?raw=true "Delete Entry")
