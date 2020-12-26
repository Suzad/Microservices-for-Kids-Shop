create table _changelog
(
    id         int auto_increment
        primary key,
    applied_at datetime     not null,
    created_by varchar(100) not null,
    filename   varchar(200) not null
);

create table category
(
    id   int auto_increment
        primary key,
    name varchar(100) null
);

create table customer
(
    id   int auto_increment
        primary key,
    name varchar(100) null
);

create table employee
(
    id            int          not null
        primary key,
    employee_name varchar(100) null
);

create table invoice
(
    id          int      not null
        primary key,
    customer_id int      not null,
    seller_id   int      not null,
    date_time   datetime null
);

create table product
(
    id             int auto_increment
        primary key,
    name           varchar(100) null,
    categoryId     int          not null,
    categoryName   varchar(100) null,
    averageRating  float        null,
    numberOfRaters int          not null
);

create table rating
(
    id         int auto_increment
        primary key,
    product_id int      not null,
    value      int      null,
    timestamp  datetime null,
    rater_id   int      null
);

create table sale
(
    id         int not null
        primary key,
    product_id int not null,
    invoice_id int not null,
    unit_price int not null,
    count      int not null
);

create
    definer = root@localhost procedure add_rating(IN prod_id int, IN rate_value int, IN customer_id int)
begin
    insert into rating (product_id, value, timestamp, rater_id) values (prod_id, rate_value, now(), customer_id);
end;

create
    definer = root@localhost procedure recalculate_product_average_rating()
begin
    update product p
    set average_rating = (select avg(value) from rating where rating.product_id = p.id)
    where 1 = 1;
end;

create
    definer = root@localhost procedure return_product_average_rating(IN product_id int)
begin
    select average_rating from product where id = product_id ;
end;

create
    definer = root@localhost procedure store_rating(IN prod_id int, IN rate_value int, IN customer_id int)
BEGIN
    DECLARE count_product INT;
    SELECT COUNT(*)
    INTO count_product
    FROM rating
    WHERE product_id = prod_id
      AND rater_id = customer_id;
    IF count_product = 0 THEN
        insert into rating (product_id, value, timestamp, rater_id)
            value (prod_id, rate_value, NOW(), customer_id);
    ELSE
        UPDATE rating SET value = rate_value WHERE product_id = prod_id AND rater_id = customer_id;
    END IF;

    SELECT COUNT(*) FROM rating;

END;

