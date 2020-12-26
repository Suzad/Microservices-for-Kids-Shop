# Overview of Microservice
For ProductService, we used <strong>Microsoft ASP .NET Core MVC Framework.</strong> for RatingService, we used <strong>Node.js.</strong> We ingrated the microservices by sending a <strong>POST</strong> request from RatingService to ProductService

# Starting the Product Service
1. Open the folder ProductService. click on the solution folder and open the project in Visual Studio.
2. In the folder named <strong>Database</strong>, do to the file named <strong>DBConnections.cs.</strong>
3. There, update the value of <i>username</i>, <i>password</i> and <i>databaseName</i> with the name, password and database name of your MySQL DB.
4. go to Debug on top menu and click <b>"Start without Debugging"</b>
5. A link https://localhost:61360/product/list should open in your browser, with the list of products in your Product table
6. Open Postman. Send a <strong>GET</strong> request to https://localhost:61360/product/list to get product list
7. Send a <strong>POST</strong> request to https://localhost:61360/product/add to add a product, with the input data in form-data format in request body
8. Send a <strong>POST</strong> request to https://localhost:61360/product/updateCategory to update a product, with the input data in form-data format in request body
9. Send a <strong>DELETE</strong> request to https://localhost:61360/product/remove/{id} to remove a product, with ID specifiying productId

# Starting the Rating Service
1. Open the Folder RatingService in Visual Studio Code
2. Run `npm install` in terminal to install necessary packages
3. Go to <strong>Dependencies.txt</strong> and install given dependencies.
4. go to Index.js. in var <strong>mysqlConnection</strong>, update user, password, and database with the name, password and database name of your MySQL DB.
5. Now run `npm start` in terminal. The message <strong>"DB Connection Successful"</strong> should show
6. Now go to postman . sending a <strong>POST</strong> request to https://localhost:3000/rate/ will to add a rating, with the input data in raw format in request body
7. sending the request <strong>5 times</strong>  will update average rating and the Product table is updated.
