const mysql = require("mysql"); //importing mysql
const express = require("express"); //importing express
var app = express();
const bodyparser = require("body-parser"); //importing body-parser

app.use(bodyparser.json()); // cofigure the express server to access data
app.use(bodyparser.urlencoded({ extended: true }));

let exec_count = 0;
var request = require("request");

//Creating a MySQl Connection
var mysqlConnection = mysql.createConnection({
    host: "localhost",
    user: "root",
    password: "suzad",
    database: "kids_shop",
    multipleStatements: true,
});

//Request Function for sending POST Request
// for more info go to ..  https://stackoverflow.com/questions/32327858/how-to-send-a-post-request-from-node-js-express
/*function updateClient(postData) {
    var clientServerOptions = {
        uri: "http://localhost:61036/product/updateRating",
        body: postData,
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
    };
    request(clientServerOptions, function (error, response) {
        //console.log(response.statusCode);
        return;
    });
}

 */



mysqlConnection.connect((err) => {
    if (!err) console.log("DB connection succeded.");
    else
        console.log(
            "DB connection failed \n Error : " + JSON.stringify(err, undefined, 2)
        );
});

app.listen(3000, () =>
    console.log("Express server is runnig at port no : 3000")
);

app.post("/test", (req, res) => {
    let rating = req.body;
    console.log(rating);
    console.log("Test Called");
});

//Insert a Rate
app.post("/rate", (req, res) => {
    //console.log("hello");
    let rating = req.body;
    var sql =
        "SET @prod_id = ?;SET @rate_value = ?;SET @customer_id =?;\
        CALL store_rating(@prod_id, @rate_value, @customer_id);";
    //console.log("hello world 2");

    exec_count = exec_count + 1;
    if (exec_count >= 5) {
        exec_count = 0;
        var sql2 =
            //"use kids_shop;\
          "select product_id, avg(value) as average, count(product_id) as count from kids_shop.rating group by product_id;";

        /*var options = { sql: sql2, nestTables: true };
        mysqlConnection.query(options, function (err, rows) {
            console.log(rows);
        });

         */


        mysqlConnection.query(
            sql2,
            //[rating.product_id, rating.average, rating.count],
            (err, result) => {
                if (!err) {
                    var allData=[]
                    result.forEach(function(data){
                        //console.log(data.product_id);
                        allData.push({
                            "product_id":String(data.product_id),
                            "average":data.average,
                            "count":data.count
                        })
                    })
                    let total_rating = { result: result };
                    //console.log(total_rating.result)
                    //updateClient(allData);
                    console.log(allData);
                    request({
                        url: "http://localhost:61036/product/updateRating",
                        method: "POST",
                        json: true,
                        body: allData
                    })

                        //function(error,response,body){

                            //console.log(error);
                            //console.log(response.statusCode);
                            //console.log(response.statusMessage);
                        //});

                    //console.log(result);
                    res.status(200).json({
                        statusCode: 201,
                        error: false,
                        msg: "Updated",
                    });
                } else console.log(err);
            }
        );


        // app.post("http://localhost:61036/product/updateCategory")
    } else {
        //console.log("hello world 3");
        mysqlConnection.query(
            sql,
            [rating.productId, rating.rating, rating.raterId],
            (err, result) => {
                if (!err) {

                    //res.send("hello world");
                    //console.log(result);
                } else console.log(err);
            }
        );
        //console.log("hello world 4");

        sql = "SELECT * FROM kids_shop.rating LIMIT 0,300";

        mysqlConnection.query(sql, (err, result) => {
            if (!err) {
                //console.log(result)

                let total_rating = { result: result };
                //console.log(total_rating.result);
                res.send(total_rating);
            } else console.log(err);
        });
    }
});