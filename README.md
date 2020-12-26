# Overview of Microservice
For ProductService, we used <strong>Microsoft ASP .NET Core MVC Framework.</strong> for RatingService, we used <strong>Node.js.</strong> We ingrated the microservices by sending a <strong>POST</strong> request from RatingService to ProductService. for Database, we used <strong>MySQL</strong>

# Step-1: Create Database 
1. At first we have to create the Database Schema "kids_shop". For creating we have to run the sql file from <strong>database_query.sql</strong> which is located at <strong>RatingService</strong> folder in MySQL. It will create the necessary database.

# Step-2: Starting the Product Service
1. Open the folder ProductService. click on the solution folder and open the project in Visual Studio.
2. In the folder named <strong>Database</strong>, go to the file named <strong>DBConnections.cs.</strong>
3. There, update the value of <i>username</i>, <i>password</i> and <i>databaseName</i> with the name, password and database name of your MySQL DB.
4. go to Debug on top menu and click <b>"Start without Debugging"</b>
5. A link https://localhost:61360/product/list should open in your browser, with the list of products in your Product table.
6. Open Postman. Send a <strong>GET</strong> request to https://localhost:61360/product/list to get product list.
7. Send a <strong>POST</strong> request to https://localhost:61360/product/add to add a product, with the input data in form-data format in request body.
8. Send a <strong>POST</strong> request to https://localhost:61360/product/updateCategory to update a product, with the input data in form-data format in request body.
9. Send a <strong>DELETE</strong> request to https://localhost:61360/product/remove/{id} to remove a product, with ID specifiying productId.

# Step-3: Starting the Rating Service
1. Open the Folder RatingService in Visual Studio Code
2. Run the command `npm install` in terminal to install necessary packages
3. Go to <strong>Dependencies.txt</strong> which is located at <strong>RatingService</strongService> folder and install given dependencies.
4. Before running the index.js file we have to change in the variable <strong>mysqlConnection</strong> to update host,user, password, and database with the host,user, password and database name of our MySQL DB.
5. Now run `node index.js` in terminal. The message <strong>"DB Connection Successful"</strong> and <strong>Express server is runnig at port no : 3000</strong> should show. Remember if something is already running in your '3000 port' then you have to change the port number in the index.js file. Suppose app.listen(port, ()=>  ...Here you have to assign port=3001.
6. Now go to postman. Sending a <strong>POST</strong> request to https://localhost:3000/rate/ will to add a rating, with the input data in raw format in request body. The updated "rating" table can be shown in the database.
7. Sending the request <strong>5 times</strong> will update the 'averageRating' column in the "product" table. The updated "product" table can be shown in the database.
