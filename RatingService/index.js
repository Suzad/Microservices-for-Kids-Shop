const mysql = require("mysql"); //importing mysql
const express = require("express"); //importing express
var app = express();
const bodyparser = require("body-parser"); //importing body-parser

app.use(bodyparser.json()); // cofigure the express server to access data
app.use(bodyparser.urlencoded({ extended: true }));

//Creating a MySQl Connection
var mysqlConnection = mysql.createConnection({
  host: "localhost",
  user: "root",
  password: "123456",
  database: "kids_shop",
  multipleStatements: true,
});

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

//Insert a Rate
app.post("/rate", (req, res) => {
  let rating = req.body;
  var sql =
    "SET @prod_id = ?;SET @rate_value = ?;SET @customer_id =?;\
    CALL store_rating(@prod_id, @rate_value, @customer_id);";
  console.log(sql);
  mysqlConnection.query(
    sql,
    [rating.productId, rating.rating, rating.raterId],
    (err, result) => {
      if (!err) {
        console.log(result);
      } else console.log(err);
    }
  );

  sql = "SELECT t.* FROM kids_shop.rating t LIMIT 501";

  mysqlConnection.query(sql, (err, result) => {
    if (err) {
      throw err;
    }
    console.log(result.length);
    let total_rating = { result: result };

    res.send(total_rating);
  });
});
