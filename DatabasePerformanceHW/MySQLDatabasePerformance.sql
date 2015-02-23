CREATE DATABASE `performancetest` /*!40100 DEFAULT CHARACTER SET utf8 */;

create table PerformanceTestTable(
	sampleDate	datetime ,
	sampleText varchar(50))
partition by range (Year(sampleDate)) (
	partition p0 values less than (Year('1990-01-01')),
	partition p1 values less than (Year('2000-01-01')),
	partition p2 values less than (Year('2010-01-01')),
	partition p3 values less than (Year('9999-01-01'))
);

INSERT INTO `PerformanceTestTable` 
VALUES (
(SELECT timestamp('2000-01-01 00:00:01') - INTERVAL FLOOR( RAND( ) * 3665) DAY),
'sample text'
);

DELIMITER $$
CREATE PROCEDURE FillData(a1 INT)
BEGIN
set @ind = 0;
  WHILE @ind < a1 do

INSERT INTO `PerformanceTestTable` 
VALUES (
(SELECT timestamp('2000-01-01 00:00:01') - INTERVAL FLOOR( RAND( ) * 36650) DAY),
'sample text'
);

set @ind = @ind + 1;
END WHILE;
END $$

DELIMITER ;
call FillData(999894);


select * from performancetesttable 
where year(sampleDate) = 1995 limit 1000000;

select * from performancetesttable partition(p1)
where year(sampleDate) = 1995 limit 1000000