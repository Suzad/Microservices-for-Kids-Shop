USE kids_shop;
CREATE PROCEDURE `store_rating`(in prod_id int,
                            in rate_value int,
                            in customer_id int)
BEGIN
    DECLARE count_product  INT;
    SELECT  COUNT(*)
    INTO count_product
     FROM rating
    WHERE product_id = prod_id AND rater_id = customer_id;
    IF count_product = 0 THEN
        insert into rating (product_id, value, timestamp, rater_id)
  value (prod_id, rate_value, NOW(), customer_id);
    ELSE
        UPDATE rating SET value = rate_value  WHERE product_id = prod_id AND rater_id = customer_id;
    END IF;

    SELECT COUNT(*) FROM rating;

END;

CALL store_rating(6,6,6);
CALL store_rating(9,9,9);
CALL store_rating(6,9,8);
CALL store_rating(2,4,9);
CALL store_rating(2,9,8);