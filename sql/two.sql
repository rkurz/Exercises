/*
Question 2:
- What is the average salary for each department during 2013? 

Assumptions:
- get the average of the active salaries as of the end of 2013
*/
select d.name, AVG(s.amount) as averate_salary
from 
  department d
    inner join employee e on d.id = e.department_id
    left outer join salary s on e.id = s.employee_id
      and s.effective_date <= '12/31/2013'
      and s.expiration_date >= '12/31/2013'
group by d.name