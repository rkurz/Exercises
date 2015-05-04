/*
Question 3:
- What are the minimum and maximum salaries for each location during 2014? 

Assumptions:
- minimum implies the minimum salary recorded
  (if an employee with no salary record should be considered as having a salary of $0 then the inner join on salary would need to become a left outer join and the select clause would need to be updated to replace null salary values with 0's before calculating min values)
*/
select l.name, MIN(s.amount) as minimum_salary, MAX(s.amount) as maximum_salary
from 
  location l
    inner join employee e on l.id = e.location_id
    inner join salary s on e.id = s.employee_id
      and s.effective_date <= '12/31/2014'
      and s.expiration_date >= '1/1/2014'
group by l.name