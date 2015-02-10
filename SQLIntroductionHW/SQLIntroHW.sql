--Problem 4.	
--Write a SQL query to find all information about all departments (use "SoftUni" database).
select * from Departments

--Problem 5.	
--Write a SQL query to find all department names.
select Name from Departments

--Problem 6.	
--Write a SQL query to find the salary of each employee. 
select FirstName, LastName, Salary
from Employees

--Problem 7.	
--Write a SQL to find the full name of each employee. 
select FirstName + ' ' + LastName as [Full Name]
from Employees

--Problem 8.	
--Write a SQL query to find the email addresses of each employee. (by his first and last name).
-- Consider that the mail domain is softuni.bg. Emails should look like “John.Doe@softuni.bg". 
--The produced column should be named "Full Email Addresses".
select FirstName+'.'+LastName+'@softuni.bg' as [Full Email Addresses]
from Employees

--Problem 9.	
--Write a SQL query to find all different employee salaries. 
select distinct Salary
from Employees

--Problem 10.	
--Write a SQL query to find all information about the employees whose job title is “Sales Representative“.
select *
from Employees
where JobTitle='Sales Representative'

--Problem 11.	
--Write a SQL query to find the names of all employees whose first name starts with "SA".
select FirstName, LastName
from Employees
where FirstName like 'SA%'

--Problem 12.	
--Write a SQL query to find the names of all employees whose last name contains "ei".
select FirstName, LastName
from Employees
where LastName like '%ei%'

--Problem 13.	
--Write a SQL query to find the salary of all employees whose salary is in the range [20000…30000].
select Salary
from Employees
where Salary between 20000 and 30000

--Problem 14.	
--Write a SQL query to find the names of all employees whose salary is 25000, 14000, 12500 or 23600.
select FirstName, LastName
from Employees
where Salary in(25000, 14000, 12500 , 23600)

--Problem 15.	
--Write a SQL query to find all employees that do not have manager.
select *
from Employees
where ManagerID is null

--Problem 16.	
--Write a SQL query to find all employees that have salary more than 50000. Order them in decreasing order by salary.
select *
from Employees
where Salary > 50000
order by Salary desc

--Problem 17.	
--Write a SQL query to find the top 5 best paid employees.
select TOP 5 *
from Employees
where Salary > 50000
order by Salary desc

--Problem 18.	
--Write a SQL query to find all employees along with their address.
--Use inner join with ON clause.
select *
from Employees e join Addresses a on e.AddressID = a.AddressID

--Problem 19.	
--Write a SQL query to find all employees and their address.
--Use equijoins (conditions in the WHERE clause).
select *
from Employees e, Addresses a
where e.AddressID = a.AddressID

--Problem 20.	
--Write a SQL query to find all employees along with their manager.
select e.FirstName, e.LastName,m.FirstName+' ' +m.LastName as Manager
from employees e  left outer  join employees m
on e.EmployeeID=m.ManagerID

--Problem 21.	
--Write a SQL query to find all employees, along with their manager and their address.
--You should join the 3 tables: Employees e, Employees m and Addresses a.
select e.FirstName, e.LastName, m.FirstName + ' ' + m.LastName as Manager, a.AddressText as Address
from Employees e left outer join Employees m
on e.EmployeeID = m.ManagerID
join Addresses a on e.AddressID = a.AddressID

--Problem 22.	
--Write a SQL query to find all departments and all town names as a single list.
--Use UNION.
select Name from Departments
UNION
select Name from Towns

--Problem 23.	
--Write a SQL query to find all the employees and the manager for each of them along with the employees that do not have manager. 
--Use right outer join. Rewrite the query to use left outer join.
 select e.FirstName, e.LastName,m.FirstName+' ' +m.LastName as Manager
from employees m left outer  join employees e
on e.EmployeeID=m.ManagerID

 select  e.FirstName, e.LastName, m.FirstName+' ' +m.LastName as Manager
from employees e right outer  join employees m
on e.EmployeeID=m.ManagerID

--Problem 24.	
--Write a SQL query to find the names of all employees from the departments "Sales" and "Finance" 
--whose hire year is between 1995 and 2005.
select e.FirstName, e.LastName, d.Name
from Employees e join Departments d 
on d.DepartmentID = e.DepartmentID
where d.Name in('Sales', 'Finance')
and YEAR(e.HireDate) between 1995 and 2005