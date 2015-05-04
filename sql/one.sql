/*
Question 1:
- List the employee name, effective date and salary amount for all employees, sorting the results by location name and employee name

Assumptions:
- include all salary records (if instead you only want to see employee's current salary then date filters can be added to exclude salary records that are not active on current date)
- include employees with no salary records
*/
select 
  e.name, s.effective_date, s.amount
from 
  employee e
    inner join location l on e.location_id = l.id
    left outer join salary s on e.id = s.employee_id
order by
  l.name, e.name