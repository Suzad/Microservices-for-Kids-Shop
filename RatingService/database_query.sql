drop database if exists kids_shop;
create database kids_shop;
use kids_shop;


-- rating table creation

create table rating(
	product_id varchar(100),
	value int,
	rater_id int
);


-- product table creation

create table product(
	id varchar(100),
	name varchar(100),
	categoryId int,
	averageRating float,
	categoryName varchar(100),
	numberOfRaters int
); 


-- procedure to store rating

drop procedure if exists store_rating;
CREATE PROCEDURE `store_rating`(in prod_id varchar(100),
                            in rate_value int,
                            in customer_id int)
BEGIN
    DECLARE count_product  INT;
    SELECT  COUNT(*)
    INTO count_product
     FROM rating
    WHERE product_id = prod_id AND rater_id = customer_id;
    IF count_product = 0 THEN
        insert into rating (product_id, value,rater_id)
  value (prod_id, rate_value, customer_id);
    ELSE
        UPDATE rating SET value = rate_value  WHERE product_id = prod_id AND rater_id = customer_id;
    END IF;

    SELECT COUNT(*) FROM rating;

END;